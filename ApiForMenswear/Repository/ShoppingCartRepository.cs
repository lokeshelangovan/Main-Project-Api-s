using ApiForMenswear.Data;
using ApiForMenswear.Interface;
using ApiForMenswear.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiForMenswear.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly MenswearContext _context;

        public ShoppingCartRepository(MenswearContext context)
        {
           _context = context;
        }
        public void AddToCart(ShoppingCart cartItem)
        {
            try
            {
                _context.ShoppingCart.Add(cartItem);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Exception in AddToCart: {ex.ToString()}");
                // Rethrow the exception to propagate it further if needed
                throw;
            }
        }

        public IEnumerable<ShoppingCart> GetShoppingCartItemsByUserId(int userId)
        {
            return _context.ShoppingCart
           .Where(cart => cart.UserId == userId)           
           .ToList();
        }

        public void RemoveFromCart(int cartItemId)
        {
            try
            {
                var cartItem = _context.ShoppingCart.Find(cartItemId);

                if (cartItem != null)
                {
                    _context.ShoppingCart.Remove(cartItem);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Exception in RemoveFromCart: {ex.ToString()}");
                // Rethrow the exception to propagate it further if needed
                throw;
            }
        }

        public void UpdateCartItem(ShoppingCart cartItem)
        {
            try
            {
                var existingCartItem = _context.ShoppingCart.Find(cartItem.Id);

                if (existingCartItem != null)
                {
                    existingCartItem.Quantity = cartItem.Quantity;
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Exception in UpdateCartItem: {ex.ToString()}");
                // Rethrow the exception to propagate it further if needed
                throw;
            }
        }
    }
}
