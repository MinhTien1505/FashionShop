namespace FashionShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public float Price { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(255)]
        public string Categoy { get; set; }
    }
}
