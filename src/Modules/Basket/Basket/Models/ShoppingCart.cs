﻿
namespace Basket.Models
{
    public class ShoppingCart : Aggregate<Guid>
    {
        public string UserName { get; private set; } = default!;

        private readonly List<ShoppingCartItem> _items = new();

        public IReadOnlyList<ShoppingCartItem> Items => _items.AsReadOnly();

        public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);

        private ShoppingCart() { }

        public static ShoppingCart Create(string userName)
        {
            ArgumentException.ThrowIfNullOrEmpty(userName);

            var shoppingCart = new ShoppingCart
            {
                Id = Guid.NewGuid(),
                UserName = userName
            };

            return shoppingCart;
        }

        public void AddItem(Guid productId, int quantity, string color, decimal price, string productName)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

            var existingItem = Items.FirstOrDefault(x => x.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                var newItem = new ShoppingCartItem(Id, productId, quantity, color, price, productName);

                _items.Add(newItem);
            }
        }

        public void RemoveItem(Guid productId)
        {
            var existingItem = Items.FirstOrDefault(x => x.ProductId == productId);

            if (existingItem != null)
            {
                _items.Remove(existingItem);
            }
        }
    }
}
