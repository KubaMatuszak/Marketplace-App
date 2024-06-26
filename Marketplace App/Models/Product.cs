﻿using System.ComponentModel.DataAnnotations;

namespace Marketplace_App.Models
{
    public class Product
    {
        [Key]
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price {  get; set; }
        public string Image { get; set; }
    }
}
