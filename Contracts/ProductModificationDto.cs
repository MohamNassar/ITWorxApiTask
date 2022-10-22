using System;
using System.ComponentModel.DataAnnotations;


namespace Contracts
{
    public abstract class ProductModificationDto
    {
        [Required(ErrorMessage = "CategoryId is required")]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }
        [StringLength(2000, ErrorMessage = "Name can't be longer than 2000 characters")]
        public string ImgURL { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }
    }
}