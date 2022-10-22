using System;

namespace Contracts
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImgURL { get; set; }
        public int Quantity { get; set; }
    }
}
