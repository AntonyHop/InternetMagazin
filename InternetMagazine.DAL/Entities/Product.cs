﻿using System.ComponentModel.DataAnnotations;

namespace InternetMagazine.DAL.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(20)]
        [Required]
        public string Name { get; set; }

        public string Author { get; set; }

        [MaxLength(255)]
        public string Desc { get; set; }

        public decimal Price { get; set; }

        public int? CategoryId { get; set; }
        
        public Category Category { get; set; }

        [MaxLength(50)]
        public string ImgUrl { get; set; }

        public int CountOfpay { get; set; }
    }
}
