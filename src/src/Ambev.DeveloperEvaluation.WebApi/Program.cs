using Ambev.DeveloperEvaluation.Application;
using Ambev.DeveloperEvaluation.Common.HealthChecks;
using Ambev.DeveloperEvaluation.Common.Logging;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.IoC;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Middleware;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;
using StackExchange.Redis;
using Serilog.Sinks.Debug;

namespace Ambev.DeveloperEvaluation.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args); 
        try
        {
            Log.Information("Starting web application");

            builder.AddDefaultLogging();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.AddBasicHealthChecks();
            builder.Services.AddSwaggerGen();
            
            builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(

                        String.IsNullOrEmpty (Environment.GetEnvironmentVariable("REDIS_HOST"))?
                            builder.Configuration.GetConnectionString("REDIS_HOST"):
                            Environment.GetEnvironmentVariable("REDIS_HOST")

            ));

            
            builder.Services.AddSingleton<MongoService>();

            builder.Services.AddDbContext<DefaultContext>(options =>
                options.UseNpgsql(
                        String.IsNullOrEmpty (Environment.GetEnvironmentVariable("CONNECTION_STRING"))?
                            builder.Configuration.GetConnectionString("DefaultConnection"):
                            Environment.GetEnvironmentVariable("CONNECTION_STRING"),
                    b => {
                        b.MigrationsAssembly("Ambev.DeveloperEvaluation.ORM");
                        b.EnableRetryOnFailure();
                    }
                )
            );

            builder.Services.AddJwtAuthentication(builder.Configuration);

            builder.RegisterDependencies();

            builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(ApplicationLayer).Assembly);

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    typeof(ApplicationLayer).Assembly,
                    typeof(Program).Assembly
                );
            });

            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            

            var app = builder.Build();
            
            app.UseMiddleware<ValidationExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseBasicHealthChecks();

            app.MapControllers();

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();
            Log.Debug(String.Format( "Error on Startup {0}", ex.Message + " => " + ex.StackTrace.ToString()));
            Log.Information(String.Format( "Error on Startup {0}", ex.Message + " => " + ex.StackTrace.ToString()));
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}

