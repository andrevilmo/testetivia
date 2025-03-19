namespace Ambev.DeveloperEvaluation.Common.Model
{
    /// <summary>
    /// Define o contrato para representação de um usuário no sistema.
    /// </summary>
    public interface IProductRate
    {
        /// <summary>
        /// Obtém o identificador único do usuário.
        /// </summary>
        /// <returns>O ID do usuário como uma string.</returns>
        public string Id { get; }

        /// <summary>
        /// Obtém a avaliação do produto
        /// </summary>
        /// <returns>A avaliação do produto.</returns>
        public string Rate { get; }

        /// <summary>
        /// Obtém a avaliação do produto em quantidade
        /// </summary>
        /// <returns>A avaliação do produto em quantidade.</returns>
        public int Count { get; }

        /// <summary>
        /// Obtém o identificador único do produto.
        /// </summary>
        /// <returns>O ID do produto como uma string.</returns>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Obtém o único produto.
        /// </summary>
        /// <returns>O produto como uma estrutura reversa.</returns>
        IProduct Product  { get; set; }
    }
}
