using apptest.Data;
using apptest.Dtos;
using apptest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apptest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerOrderController : ControllerBase
    {
        //public static List<CustomerOrder> CustomerOrders;
        private DataContext _context { get; }
        public CustomerOrderController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult InsertOrder([FromBody] OrderDto orderDto)
        {
            var customerBasket = _context.Baskets.Where(x => x.CustomerId == orderDto.CustomerId).ToList();
            
            if (customerBasket == null)
            {
                _context.Baskets.Add(new Basket()
                {
                    CustomerId = orderDto.CustomerId,
                    ProductId = orderDto.ProductId,
                    Count = orderDto.Count
                });
            }
            else
            {
                var productOfCustomer = customerBasket.FirstOrDefault(x => x.ProductId == orderDto.ProductId);
                if (productOfCustomer == null)
                {
                    _context.Baskets.Add(new Basket()
                    {
                        ProductId = orderDto.ProductId,
                        CustomerId = orderDto.CustomerId,
                        Count = orderDto.Count,
                    });
                }
                else
                {
                    productOfCustomer.Count += orderDto.Count;
                }
            }

            var result = _context.SaveChanges();

            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id,[FromBody]OrderDto orderDto)
        {
            if (orderDto == null)
            {
                return BadRequest();
            }

            Basket model = new()
            {
                ProductId = orderDto.ProductId,
                CustomerId = orderDto.CustomerId,
                Count = orderDto.Count,
            };

            _context.Baskets.Update(model);
            _context.SaveChanges();

            return NoContent();
        }
        [HttpGet("{customerId}")]
        public IActionResult GetOrder(int customerId)
        {
            var basket = _context.Baskets.FirstOrDefault(x => x.CustomerId == customerId);

            return Ok(basket);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var products = _context.Baskets.FirstOrDefault(u => u.Id == id);

            if (products == null)
            {
                return NotFound();
            }
            _context.Baskets.Remove(products);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
