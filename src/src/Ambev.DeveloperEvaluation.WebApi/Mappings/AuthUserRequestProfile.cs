using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser; 
using Ambev.DeveloperEvaluation.WebApi.Features.Auth.AuthenticateUserFeature; 
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings;

public class AuthUserRequestProfile : Profile
{
    public AuthUserRequestProfile()
    {
        CreateMap<AuthenticateUserRequest, AuthenticateUserCommand>();

        CreateMap<AuthenticateUserResult, AuthenticateUserResponse>();
        
    }
}