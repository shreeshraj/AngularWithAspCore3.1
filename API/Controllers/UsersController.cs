using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Bcpg.Sig;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    public class EmployeeClass
    {
         public EmployeeClass()
        {
            serverData = "Hello World";

        }

        private readonly String serverData;
        public int Id { get; set; }

        public string Name { get; set; }
        public string Location { get; set; }
    }

    public class PartTimeEmployee:EmployeeClass
    {
        public int HrSalary { get; set; }
    }

    public class FullTimeEmployee:EmployeeClass
    {
        public int MonthlySalary { get; set; }
    }

     
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        [Route("stringReturn")]
        public string ReturnAString()
        {
            return "Hello India";
        }
        [HttpGet]
        [Route("ReturnList")]

        public IEnumerable<EmployeeClass> fullTimeEmployees()
        {
            List<EmployeeClass> employeeClasses = new List<EmployeeClass>();
            employeeClasses.Add(new FullTimeEmployee() { Id=1, Name="Ankur", Location="India"});
            employeeClasses.Add(new FullTimeEmployee() { Id = 1, Name = "Ankur", Location = "India" });
            employeeClasses.Add(new FullTimeEmployee() { Id = 1, Name = "Ankur", Location = "India" });
            employeeClasses.Add(new FullTimeEmployee() { Id = 1, Name = "Ankur", Location = "India" });
            return employeeClasses;
        }
        [HttpGet]
        [Route("ReturnObject")]
        public EmployeeClass employeeClass()
        {

            EmployeeClass employeeClass = new EmployeeClass() { Id=10,Name="Shreesh", Location="Delhi"};
            return employeeClass;
            
        }
        [HttpGet]
        [Route("ActionResult")]
        public ActionResult<EmployeeClass> GetActionResult()
        {
            EmployeeClass employeeClass = new EmployeeClass() { Id = 10, Name = "Shreesh", Location = "Delhi" };
            if (employeeClass == null)
                return NotFound();
            else
               return employeeClass;
        }
        
        [HttpGet]
        [Route("IActionResult")]
        public IActionResult GetActionResult1()
        {
            EmployeeClass employeeClass = new EmployeeClass() { Id = 10, Name = "Shreesh", Location = "Delhi" };
            return Unauthorized();
            //return Ok(employeeClass);

        }

        [HttpGet]
        [Route("IActionResult2")]
        public IActionResult GetActionResult2()
        {
            EmployeeClass employeeClass = new EmployeeClass() { Id = 10, Name = "Shreesh", Location = "Delhi" };
            return Ok(employeeClass);
            //return Ok(employeeClass);

        }
    }
}
