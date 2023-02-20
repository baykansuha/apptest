using apptest.Data;
using apptest.Dtos;
using apptest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apptest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DataContext _context;
        public ProductController(DataContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult> InsertProduct([FromBody]Product product)
        {
            if (product == null)
            {
                return BadRequest(product);
            }
            if (product.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpPut("{id}", Name = "UpdateProduct")]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody] ProductDto product)
        {
            if (product == null || id != product.Id)
            {
                return BadRequest();
            }

            Product model = new()
            {
                Id = product.Id,
                Description = product.Description,
                Barcode = product.Barcode,
                Amount = product.Amount,
                Count = product.Count,
                Price = product.Price
            };

            _context.Products.Update(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetProduct(int id)
        {
            var product =  await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            return Ok(product);
        }
        [HttpGet]
        public async Task<ActionResult> GetProduct()
        {
            return Ok(await _context.Products.ToListAsync());
        }
    }
}
