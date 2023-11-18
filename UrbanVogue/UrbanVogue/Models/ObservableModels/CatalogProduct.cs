﻿namespace UrbanVogue.Models.ObservableModels
{
    public class CatalogProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal BasePrice { get; set; }
        public decimal Discount { get; set; }
        public float Rating { get; set; }
        public string Image { get; set; }

    }
}
