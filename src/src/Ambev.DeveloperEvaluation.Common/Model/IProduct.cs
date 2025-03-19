namespace Ambev.DeveloperEvaluation.Common.Model
{
    /// <summary>
    /// Define o contrato para representação de um usuário no sistema.
    /// </summary>
    public interface IProduct
    {
        /// <summary>
        /// Gets the Product's Id.
        /// Unique product identifier
        /// </summary>
        public string Id { get;  } 

        /// <summary>
        /// Gets the Product's title  .
        ///
        /// </summary>
        public string Title { get;  } 

        /// <summary>
        /// Gets the Product's description  .
        ///
        /// </summary>
        public string Description { get;  }  


        /// <summary>
        /// Gets the Product's category  .
        ///
        /// </summary>
        public string Category { get;  }  
        
        /// <summary>
        /// Gets the Product's image  .
        ///
        /// </summary>
        public string Image { get;  }  

        /// <summary>
        /// Gets the products price .
        ///
        /// </summary>
        public decimal Price { get;  } 

        /// <summary>
        /// Gets the products rate .
        ///
        /// </summary>
        public IProductRate Rating { get;  }  

        /// <summary>
        /// Gets the products rate id.
        ///
        /// </summary>
        public string RatingId { get;  } 

    }
}
