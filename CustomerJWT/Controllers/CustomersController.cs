using CustomerJWT.Dto;
using CustomerJWT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomerJWT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : Controller
    {

        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetResult()
        {
            var Customers = await _context.Customers.AsNoTracking().ToListAsync();
            return Ok(Customers);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            var Customer = await _context.Customers.FindAsync(id);
            return Ok(Customer);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(dto dto)
        {

            var Customer = new Customer
            {
                Name = dto.Name,
                Email = dto.Email,
            };
            _context.Customers.Add(Customer);
            await _context.SaveChangesAsync();
            return Ok(Customer);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, dto dto)
        {

            var Customer = await _context.Customers.FindAsync(id);
            Customer.Name = dto.Name;
            Customer.Email = dto.Email;
            await _context.SaveChangesAsync();
            return Ok(Customer);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await _context.Customers.Where(row => row.Id.Equals(id)).ExecuteDeleteAsync();
            return Ok();
        }
    }
}