using ApiForMenswear.Interface;
using ApiForMenswear.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiForMenswear.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository)
        {
           _shoppingCartRepository = shoppingCartRepository;
        }

        [HttpGet("ShoppingCart/{userId}")]
        public IEnumerable<ShoppingCart> GetShoppingCartItems(int userId)
        {
            return _shoppingCartRepository.GetShoppingCartItemsByUserId(userId);
        }

        [HttpPost("ShoppingCart")]
        public IActionResult AddToCart([FromBody] ShoppingCart cartItem)
        {
            try
            {
                _shoppingCartRepository.AddToCart(cartItem);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log the entire exception, including inner exception
                Console.WriteLine($"Exception: {ex.ToString()}");

                // Return a more detailed error response
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }



        [HttpPut("ShoppingCart/{cartItem}")]
        public IActionResult UpdateCartItem([FromBody] ShoppingCart cartItem)
        {
            _shoppingCartRepository.UpdateCartItem(cartItem);
            return Ok();
        }

        [HttpDelete("{cartItemId}")]
        public IActionResult RemoveFromCart(int cartItemId)
        {
            _shoppingCartRepository.RemoveFromCart(cartItemId);
            return Ok();
        }
    }
}
