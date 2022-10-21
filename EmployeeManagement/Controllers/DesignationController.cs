using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using RepositoryLayer;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class DesignationController : ControllerBase
    {
        private readonly EmployeeContext _employeeContext;
        public DesignationController(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }
        [HttpPost]
        [Route("designation")]
        public IActionResult Designation([FromBody] Designation designation)
        {
            if (!ModelState.IsValid)
                return BadRequest("not a valid request");
            _employeeContext.designations.Add(designation);
            _employeeContext.SaveChanges();
            return Ok();
        }
    }
}
