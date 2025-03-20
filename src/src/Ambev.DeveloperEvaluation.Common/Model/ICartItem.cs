namespace Ambev.DeveloperEvaluation.Common.Model
{
    /// <summary>
    /// Define o contrato para representação de um usuário no sistema.
    /// </summary>
    public interface ICartItem
    {

        /// <summary>
        /// Gets the Cart's item Id.
        /// Unique product identifier
        /// </summary>
        public string Id { get;  } 

        /// <summary>
        /// Gets the Cart's item product
        /// </summary>
        public IProduct Product { get; set; } 

        /// <summary>
        /// Gets the Cart's item product id
        /// </summary>
        public Guid ProductId { get; set; } 

        /// <summary>
        /// Gets the Cart's item quantity.
        ///
        /// </summary>
        public int Quantity { get; set; } 

        /// <summary>
        /// Gets the date and time when the Cart was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets the date and time of the last update to the Cart's information.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }


        
        /// <summary>
        /// Gets the Cart
        /// </summary>
        public ICart Cart { get; set; } 


         
        /// <summary>
        /// Gets the Cart's id
        /// </summary>
        public Guid CartId { get; set; } 

    }
}
