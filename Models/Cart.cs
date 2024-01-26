﻿using System.Collections.Generic;

public class Cart
{
    public List<CartItem> Items { get; set; } = new List<CartItem>();
}

public class CartItem
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
