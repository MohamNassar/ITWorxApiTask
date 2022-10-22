using System;

namespace Domain.Exceptions
{
    public sealed class CategoryNotFoundException : NotFoundException
    {
        public CategoryNotFoundException(Guid CategoryId)
            : base($"The Category with the identifier {CategoryId} was not found.")
        {
        }
    }
}
