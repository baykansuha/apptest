using apptest.Data;
using apptest.Dtos;
using apptest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apptest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly DataContext _context;
        public CustomerController(DataContext context)
        {
            _context = context;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> InsertCustomer([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest(customer);
            }
            if (customer.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpPut("{id}")] 
        public ActionResult UpdateCustomer(int id, [FromBody] CustomerDTO customer) 
        {
            if (customer == null || id!= customer.Id)
            {
                return BadRequest();
            }

            Customer model = new()
            {
                Id = customer.Id,
                Name = customer.Name,
                Address = customer.Address,
            };

            _context.Customers.Update(model);
            _context.SaveChanges();

            return NoContent();
        }
        [HttpGet("{id}")]
        public ActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.FirstOrDefault(x => x.Id == id);

            return Ok(customer);
        }
        [HttpGet]
        public ActionResult GetCustomers()
        {
            return Ok(_context.Customers.ToList());
        }
    }
}
