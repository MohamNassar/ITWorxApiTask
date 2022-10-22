using System;
using System.Collections.Generic;

namespace Contracts
{
    public class CategoryDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<ProductDto> Products { get; set; }


    }
}
