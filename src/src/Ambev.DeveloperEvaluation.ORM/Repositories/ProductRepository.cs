using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IProductRepository using Entity Framework Core
/// </summary>
public class ProductRepository : IProductRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of ProductRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public ProductRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new Product in the database
    /// </summary>
    /// <param name="Product">The Product to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created Product</returns>
    public async Task<Product> CreateAsync(Product Product, CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(Product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return Product;
    }
    /// <summary>
    /// Updates a Product in the database
    /// </summary>
    /// <param name="Product">The Product to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated Product</returns>
    public async Task<Product> UpdateAsync(Product Product, CancellationToken cancellationToken = default)
    {
        var r = await _context.ProductRate.FirstOrDefaultAsync(x => x.ProductId == Product.Id);
        r.Count = Product.Rating.Count;
        r.Rate = Product.Rating.Rate;
        r.UpdatedAt = DateTime.UtcNow;
        Product.Rating = r;
        Product.Rating.Product = Product;
        Product.UpdatedAt = DateTime.UtcNow;
        _context.ProductRate.Update((ProductRate)Product.Rating);
        _context.Products.Update(Product);
        await _context.SaveChangesAsync(cancellationToken);
        return Product;
    }
    

    /// <summary>
    /// Retrieves a Product by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the Product</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Product if found, null otherwise</returns>
    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
         
        var p = await _context.Products.FirstOrDefaultAsync
        (o=> o.Id == id, cancellationToken);
        if (p==null) return null;
        var r = await _context.ProductRate.FirstOrDefaultAsync(y => y.ProductId == id,cancellationToken);
        p.Rating = r;
        return p;
    }


     /// <summary>
    /// Retrieves a product list by filter
    /// </summary>
    /// <param name="id">The unique identifier of the Product</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Product if found, null otherwise</returns>
    public async Task<List<Product>?> GetByIFilterAsync(  String pOrder = "", 
                                                    Dictionary<string,string> pFilter = null, 
                                                    int pPage = 1, 
                                                    int pSize = 10, 
                                                    CancellationToken cancellationToken = default)
    {
        string sValue = string.Empty;
        string [] fieldsFilter = ["id", "title", "description", "category", "image", 
                "price", "_maxprice", "_minprice", 
                "createdat", "_maxcreatedat", "_mincreatedat", 
                "updatedat", "_maxupdatedat", "_minupdatedat"];
        string [] fieldsOrder = ["id", "title", "description", "category", "image", 
                "price", 
                "createdat", 
                "updatedat"];
            var realNames = new Dictionary<string, string>{
                     {"price" , """Price"""}
                    ,{"createdat" , """CreatedAt"""}
                    ,{"updatedat" , """UpdateAt"""}
                    ,{"title" , """Title"""}
                    ,{"description" , """Description"""}
                    ,{"category" , """Category"""}
                    ,{"image" , """Image"""}
                    ,{"_minprice" , """Price"""}
                    ,{"_maxprice" , """Price"""}
                    ,{"_maxcreatedat" , """CreatedAt"""}
                    ,{"_mincreatedat" , """CreatedAt"""}
                    ,{"_maxupdatedat" , """UpdateAt"""}
                    ,{"_minupdatedat" , """UpdateAt"""}
            };
            string sOrder = String.Empty;
            string sFilter = String.Empty;
            var pOrderParams = pOrder.ToString()
            .ToLower().Split(',')
            .Select(x => x.Split(' '))
            .ToDictionary(x => x[0], x => x[1])
            .Select(x => x)
            .Where (y => fieldsOrder.Contains(y.Key))
            .Where (x => x.Value.Equals("asc") || 
                     x.Value.Equals("desc")
                    ).ToList();
        if (pOrderParams.Count > 0) 
            sOrder = pOrderParams
                .Select(w=> String.Format(@" ""{0}"" {1}", realNames[w.Key], w.Value))
                .Aggregate((r,n) => String.Format(" {0} , {1} ",r,n) );
        
        var pFilterParams = pFilter
            .Select(x => x)
            .ToDictionary(k => k.Key, v => v.Value)
                .Where (
                    y => fieldsFilter.Contains(y.Key)
                ).ToList();
        if (pFilterParams.Count>0)
              sFilter=  pFilterParams
                // Trata os casos de string e comparação direta
                .Select(x => new KeyValuePair<string,string>( 
                        x.Value.Contains("*") ?  
                            String.Format(@"""{0}"" Like '",realNames[x.Key]) :
                            x.Key, 
                        x.Value.Contains("*") ?  
                            String.Format("{0}'",x.Value.Trim().Replace("*","%")):
                            x.Value.Trim().Replace("*","%")
                        )
                )
                // Trata os casos de equivalencia min e max
                .Select(x => new KeyValuePair<string,string>( 
                        x.Key.Contains("_min") || x.Key.Contains("_max")?  
                            String.Format(@"""{0}"" {1} '",realNames[x.Key], x.Key.Contains("_min") ? ">" : "<"  ) :
                            x.Key, 
                        x.Key.Contains("_min") || x.Key.Contains("_max") ?  
                            String.Format("{0}'",x.Value.Trim().Replace("*","%")):
                            x.Value.Trim().Replace("*","%")
                        )
                )
                // Transforma em string
                .Select(w=> 
                        w.Value.Contains("%")  || w.Key.Contains(">")   || w.Key.Contains("<")?  
                        String.Format(@" {0}{1} ", w.Key, w.Value)
                        :String.Format(@" ""{0}"" = '{1}'", realNames.FirstOrDefault(f => f.Key == w.Key,w).Value, w.Value)
                
                )
                .Aggregate((r,n) => String.Format(" {0} and {1} ",r,n) );
        string sql = String.Format(@"
                Select * from ""Product""
                {0} {1}
                ",  String.IsNullOrEmpty(sFilter) ? @"                        ": "Where    " + sFilter, 
                    String.IsNullOrEmpty(sOrder)  ? @" order by ""Title"" asc ": "Order by " + sOrder
                );
        var p = _context.Database.SqlQueryRaw<Product>(sql)
                .Skip(((pPage < 1? 1 : pPage)-1)  * pSize )
                .Take<Product>(pSize).ToList();
        p.ForEach(x => x.Rating = 
                    _context.ProductRate.FirstOrDefaultAsync(y => y.ProductId == x.Id,cancellationToken).Result
                );
        return p.ToList<Product>();
    }

  
    /// <summary>
    /// Deletes a Product from the database
    /// </summary>
    /// <param name="id">The unique identifier of the Product to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the Product was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var Product = await GetByIdAsync(id, cancellationToken);
        if (Product == null)
            return false;

        _context.Products.Remove(Product);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
