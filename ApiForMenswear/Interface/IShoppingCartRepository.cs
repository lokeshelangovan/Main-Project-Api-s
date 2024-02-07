using ApiForMenswear.Models;

namespace ApiForMenswear.Interface
{
    public interface IShoppingCartRepository
    {
        IEnumerable<ShoppingCart> GetShoppingCartItemsByUserId(int userId);
        void AddToCart(ShoppingCart cartItem);
        void UpdateCartItem(ShoppingCart cartItem);
        void RemoveFromCart(int cartItemId);
    }
}
