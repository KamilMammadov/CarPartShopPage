﻿namespace CarPartShop.Areas.Client.ViewModels
{
    public class ProductCookieViewModel
    {
        public ProductCookieViewModel(int id, string? title, string? imageUrl, 
            int quantity, decimal price, decimal total,int? colorId,int? sizeId)
        {
            Id = id;
            Title = title;
            ImageUrl = imageUrl;
            Quantity = quantity;
            Price = price;
            Total = total;
            ColorId = colorId;
            SizeId = sizeId;    
        }

        public int Id { get; set; }
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? Total { get; set; }
        public int? ColorId { get; set; }
        public int? SizeId { get; set; }

        public ProductCookieViewModel()
        {

        }
    }
}