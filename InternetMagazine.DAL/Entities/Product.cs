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

        [MaxLength(255)]
        public string Desc { get; set; }

        public double Price { get; set; }

        public int? CategoryId { get; set; }
        
        public Category Category { get; set; }
    }
}
