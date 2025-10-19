using MaisonTelecom.Data;
using MaisonTelecom.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;

namespace MaisonTelecom.Services
{
    public class StateContainer
    {
        private readonly NavigationManager _navigationManager;
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        private readonly IJSRuntime _jsRuntime;

        private string _sessionId;
        private bool _isInitialized;

        public StateContainer(NavigationManager navigationManager, IDbContextFactory<ApplicationDbContext> dbContextFactory, IJSRuntime jsRuntime)
        {
            _navigationManager = navigationManager;
            _dbContextFactory = dbContextFactory;
            _jsRuntime = jsRuntime;
        }

        public List<Product> Wishlist { get; private set; } = new();
        public List<CartItem> Cart { get; private set; } = new();

        public event Action OnStateChange;

        public async Task InitializeAsync()
        {
            if (_isInitialized) return;

            _sessionId = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "sessionId");

            if (string.IsNullOrEmpty(_sessionId))
            {
                _sessionId = Guid.NewGuid().ToString();
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "sessionId", _sessionId);
            }

            await LoadCartAsync();
            await LoadWishlistAsync();
            _isInitialized = true;
        }

        private async Task LoadCartAsync()
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            var cart = await context.CartItems
                .Include(ci => ci.Product)
                .Where(ci => ci.SessionId == _sessionId)
                .ToListAsync();

            Cart = cart ?? new List<CartItem>();
            NotifyStateChanged();
        }

        private async Task LoadWishlistAsync()
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            var wishlist = await context.WishlistItems
                .Include(wi => wi.Product)
                .Where(wi => wi.SessionId == _sessionId)
                .Select(wi => wi.Product)
                .ToListAsync();

            Wishlist = wishlist ?? new List<Product>();
            NotifyStateChanged();
        }

        public async Task AddToWishlist(Product product)
        {
            if (!_isInitialized) await InitializeAsync();
            if (!Wishlist.Any(p => p.Id == product.Id))
            {
                using var context = await _dbContextFactory.CreateDbContextAsync();
                var wishlistItem = new WishlistItem
                {
                    ProductId = product.Id,
                    SessionId = _sessionId
                };

                context.WishlistItems.Add(wishlistItem);
                await context.SaveChangesAsync();

                Wishlist.Add(product);
                NotifyStateChanged();
                _navigationManager.NavigateTo("/wishlist");
            }
        }

        public async Task RemoveFromWishlist(Product product)
        {
            if (!_isInitialized) await InitializeAsync();
            using var context = await _dbContextFactory.CreateDbContextAsync();
            var wishlistItem = await context.WishlistItems
                .FirstOrDefaultAsync(wi => wi.ProductId == product.Id && wi.SessionId == _sessionId);

            if (wishlistItem != null)
            {
                context.WishlistItems.Remove(wishlistItem);
                await context.SaveChangesAsync();
            }

            Wishlist.RemoveAll(p => p.Id == product.Id);
            NotifyStateChanged();
        }

        public async Task AddToCart(Product product)
        {
            if (!_isInitialized) await InitializeAsync();
            using var context = await _dbContextFactory.CreateDbContextAsync();
            var cartItem = await context.CartItems
                .FirstOrDefaultAsync(ci => ci.ProductId == product.Id && ci.SessionId == _sessionId);

            if (cartItem != null)
            {
                cartItem.Quantity++;
            }
            else
            {
                cartItem = new CartItem { ProductId = product.Id, Quantity = 1, SessionId = _sessionId };
                context.CartItems.Add(cartItem);
            }

            await context.SaveChangesAsync();
            await LoadCartAsync();
            _navigationManager.NavigateTo("/cart");
        }

        public async Task RemoveFromCart(CartItem cartItem)
        {
            if (!_isInitialized) await InitializeAsync();
            using var context = await _dbContextFactory.CreateDbContextAsync();
            var itemToRemove = await context.CartItems.FindAsync(cartItem.Id);
            if (itemToRemove != null)
            {
                context.CartItems.Remove(itemToRemove);
                await context.SaveChangesAsync();
            }
            await LoadCartAsync();
        }

        public async Task IncreaseQuantity(int productId)
        {
            if (!_isInitialized) await InitializeAsync();
            using var context = await _dbContextFactory.CreateDbContextAsync();
            var cartItem = await context.CartItems
                .FirstOrDefaultAsync(ci => ci.ProductId == productId && ci.SessionId == _sessionId);

            if (cartItem != null)
            {
                cartItem.Quantity++;
                await context.SaveChangesAsync();
                await LoadCartAsync();
            }
        }

        public async Task DecreaseQuantity(int productId)
        {
            if (!_isInitialized) await InitializeAsync();
            using var context = await _dbContextFactory.CreateDbContextAsync();
            var cartItem = await context.CartItems
                .FirstOrDefaultAsync(ci => ci.ProductId == productId && ci.SessionId == _sessionId);

            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                }
                else
                {
                    context.CartItems.Remove(cartItem);
                }
                await context.SaveChangesAsync();
                await LoadCartAsync();
            }
        }

        private void NotifyStateChanged() => OnStateChange?.Invoke();
    }
}