using Excel.Pages.Data;
using Microsoft.AspNetCore.Mvc;
namespace Excel.Pages.Controllers
{
    

    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _context;
        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id)
        {
            var emp = _context.Employees.FirstOrDefault(x=>x.Id==id);
            emp.IsActive = true;
            _context.Employees.Update(emp);
            _context.SaveChanges();
            // Your GET logic here
            return Ok("API is working!");
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _context.Employees.ToList();
            return Ok(result);
        }

    }

}
