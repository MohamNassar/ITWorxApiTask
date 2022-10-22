using System;

namespace Domain.Exceptions
{
    public sealed class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(Guid ProductId)
            : base($"The Product with the identifier {ProductId} was not found.")    
        {
        }
    }
}
