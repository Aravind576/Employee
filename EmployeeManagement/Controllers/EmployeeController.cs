using BussinessLayer;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IDataBaseHandler _dataBaseHandler;
        public EmployeeController(IDataBaseHandler dataBaseHandler)
        {
            _dataBaseHandler = dataBaseHandler;
        }
        [HttpGet]
        public ActionResult<List<EmployeeDetail>> Get()
        {
            return _dataBaseHandler.Get();
        }
        [HttpGet]
        [Route("get/{username}")]
        public ActionResult<EmployeeDetail> Get(string username)
        {
            return _dataBaseHandler.Get(username);
        }

    }
}
