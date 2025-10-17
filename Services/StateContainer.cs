using MaisonTelecom.Models;
using Microsoft.AspNetCore.Components;

namespace MaisonTelecom.Services
{
    public class StateContainer
    {
        private readonly NavigationManager _navigationManager;

        public StateContainer(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        public List<Product> Wishlist { get; private set; } = new();
        public List<CartItem> Cart { get; private set; } = new(); // Changed to use CartItem

        public event Action OnStateChange;

        public void AddToWishlist(Product product)
        {
            if (!Wishlist.Any(p => p.Id == product.Id))
            {
                Wishlist.Add(product);
                NotifyStateChanged();
                _navigationManager.NavigateTo("/wishlist");
            }
        }

        public void RemoveFromWishlist(Product product)
        {
            Wishlist.RemoveAll(p => p.Id == product.Id);
            NotifyStateChanged();
        }

        public void AddToCart(Product product)
        {
            var cartItem = Cart.FirstOrDefault(ci => ci.Product.Id == product.Id);
            if (cartItem != null)
            {
                cartItem.Quantity++; // If item exists, increase quantity
            }
            else
            {
                Cart.Add(new CartItem { Product = product, Quantity = 1 }); // Otherwise, add it
            }

            NotifyStateChanged();
            _navigationManager.NavigateTo("/cart");
        }

        public void RemoveFromCart(CartItem cartItem)
        {
            Cart.Remove(cartItem);
            NotifyStateChanged();
        }

        public void IncreaseQuantity(int productId)
        {
            var cartItem = Cart.FirstOrDefault(ci => ci.Product.Id == productId);
            if (cartItem != null)
            {
                cartItem.Quantity++;
                NotifyStateChanged();
            }
        }

        public void DecreaseQuantity(int productId)
        {
            var cartItem = Cart.FirstOrDefault(ci => ci.Product.Id == productId);
            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                }
                else
                {
                    Cart.Remove(cartItem); // If quantity is 1, remove the item
                }
                NotifyStateChanged();
            }
        }

        private void NotifyStateChanged() => OnStateChange?.Invoke();
    }
}