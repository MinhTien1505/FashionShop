namespace FashionShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string Username { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [Required]
        [StringLength(255)]
        public string Fullname { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Avatar { get; set; }

        [StringLength(50)]
        public string Position { get; set; }
    }
}
