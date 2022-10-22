using System;

namespace Domain.Exceptions
{
    public sealed class ProductDoesNotBelongToCategoryException : BadRequestException
    {
        public ProductDoesNotBelongToCategoryException(Guid CategoryId, Guid ProductId)
            : base($"The Product with the identifier {ProductId} does not belong to the Category with the identifier {CategoryId}")
        {
        }
    }
}
