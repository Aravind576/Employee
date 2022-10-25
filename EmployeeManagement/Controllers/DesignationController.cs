using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ModelLayer;
using RepositoryLayer;
using System.Linq.Expressions;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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


        [HttpGet]
        public List<Designation> Get()
        {
            return _employeeContext.designations.ToList();
            //var data = _employeeContext.employee.Include(c => c.designations).ToList();
            //return Ok(data);
        }
    }
}
