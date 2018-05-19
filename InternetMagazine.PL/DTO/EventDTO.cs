﻿using System.ComponentModel.DataAnnotations;

namespace InternetMagazine.PL.DTO
{
    public class EventDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public string Desc { get; set; }

        public decimal Price { get; set; }

        public int? CategoryId { get; set; }
        
        public RoomDTO Category { get; set; }

        public string ImgUrl { get; set; }
    }
}