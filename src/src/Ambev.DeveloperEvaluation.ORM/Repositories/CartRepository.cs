using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of ICartRepository using Entity Framework Core
/// </summary>
public class CartRepository : ICartRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of CartRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public CartRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new Cart in the database
    /// </summary>
    /// <param name="Cart">The Cart to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created Cart</returns>
    public async Task<Cart> CreateAsync(Cart Cart, CancellationToken cancellationToken = default)
    {
        var cartId = Guid.NewGuid();
        Cart.Id = cartId;
        Cart.Products.ToList().ForEach(p => {p.CartId = cartId; p.CreatedAt = DateTime.UtcNow;});
        await _context.Carts.AddAsync(Cart, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return Cart;
    }
    /// <summary>
    /// Updates a Cart in the database
    /// </summary>
    /// <param name="Cart">The Cart to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated Cart</returns>
    public async Task<Cart> UpdateAsync(Cart Cart, CancellationToken cancellationToken = default)
    {
        
        Cart.UpdatedAt = DateTime.UtcNow; 
        _context.Carts.Update(Cart);
        await _context.SaveChangesAsync(cancellationToken);
        return Cart;
    }
    

    /// <summary>
    /// Retrieves a Cart by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the Cart</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Cart if found, null otherwise</returns>
    public List<Cart>? GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    //public async Task<List<Cart>?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
         
        var p = _context.Carts.Where
        (o=> o.Id == id).Select(x => x).ToList();
        if (p==null) return null;
         p.ForEach(x => x.Products = 
                    _context.CartItem.Where(y => y.CartId == x.Id).Select(y => y)
                );
        return p.ToList<Cart>();
    }


     /// <summary>
    /// Retrieves a Cart list by filter
    /// </summary>
    /// <param name="id">The unique identifier of the Cart</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Cart if found, null otherwise</returns>
    public async Task<List<Cart>?> GetByIFilterAsync(  String pOrder = "", 
                                                    Dictionary<string,string> pFilter = null, 
                                                    int pPage = 1, 
                                                    int pSize = 10, 
                                                    CancellationToken cancellationToken = default)
    {
        string sValue = string.Empty;
        string [] fieldsFilter = [
                "id",  
                "createdat", "_maxcreatedat", "_mincreatedat", 
                "updatedat", "_maxupdatedat", "_minupdatedat"];
        string [] fieldsOrder = [
                "id", 
                "createdat", 
                "updatedat"];
            var realNames = new Dictionary<string, string>{
                    {"id" , """Id"""}
                    ,
                    {"createdat" , """CreatedAt"""}
                    ,{"updatedat" , """UpdateAt"""}
                    ,{"_maxcreatedat" , """CreatedAt"""}
                    ,{"_mincreatedat" , """CreatedAt"""}
                    ,{"_maxupdatedat" , """UpdateAt"""}
                    ,{"_minupdatedat" , """UpdateAt"""}
            };
            string sOrder = String.Empty;
            string sFilter = String.Empty;
            var pOrderParams = 
                            String.IsNullOrEmpty( pOrder.Trim())?
                                new List<KeyValuePair<string, string>> ()
                            :
                                pOrder.ToString()
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
                Select * from ""Cart""
                {0} {1}
                ",  String.IsNullOrEmpty(sFilter) ? @"                        ": "Where    " + sFilter, 
                    String.IsNullOrEmpty(sOrder)  ? @" order by ""CreatedAt"" asc ": "Order by " + sOrder
                );
        var p = _context.Database.SqlQueryRaw<Cart>(sql)
                .Skip(((pPage < 1? 1 : pPage)-1)  * pSize )
                .Take<Cart>(pSize).ToList();
         p.ForEach(x => x.Products = 
                    _context.CartItem.Where(y => y.CartId == x.Id).Select(y => y)
                );
        return p.ToList<Cart>();
    }

  
    /// <summary>
    /// Deletes a Cart from the database
    /// </summary>
    /// <param name="id">The unique identifier of the Cart to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the Cart was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var Cart = GetByIdAsync(id, cancellationToken);
        if (Cart == null)
            return false;

        _context.Carts.Remove(Cart.FirstOrDefault());
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
