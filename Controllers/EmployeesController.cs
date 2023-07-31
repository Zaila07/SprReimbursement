using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SprEmployeeReimbursement.Business.DataTransferObject;
using SprEmployeeReimbursement.Business.ServiceCollection;
using SprEmployeeReimbursement.DataAccess.Models;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace SprEmployeeReimbursement.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IConfiguration configuration, IEmployeeService employeeService)
        {
            _configuration = configuration;
            _employeeService = employeeService;
        }
        [ProducesResponseType(typeof(CreateEmployeeDto), statusCode: StatusCodes.Status201Created)]
        [MapToApiVersion("1")]
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDto createEmployeeDto)
        {
            var createdEmployee = await _employeeService.CreateEmployee(createEmployeeDto);
            return Created($"api/v1/Employees/{createdEmployee}", createdEmployee);
        }

        [ProducesResponseType(typeof(SprEmployee), statusCode: StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<IActionResult>GetEmployeeByID(int id)
        {
            return Ok(await _employeeService.GetEmployeeById(id));

        }
        [ProducesResponseType(typeof(employeeDto), statusCode: StatusCodes.Status200OK)]
        [HttpGet] 
        public async Task<IActionResult> GetAllEmployees()
        {

            return Ok( await _employeeService.GetAllEmployees());
        }

        [ProducesResponseType(typeof(List<string>), statusCode: StatusCodes.Status200OK)]
        [HttpGet("names")]
        public async Task<IActionResult> GetEmployeeNames()
        { 
           return Ok (await _employeeService.GetEmployeeNames());
        }


        [ProducesResponseType(typeof(UpdateEmployeeDto),statusCode:StatusCodes.Status200OK)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee( [FromBody] UpdateEmployeeDto updateEmployeeDto)
        { 
            return Ok(await _employeeService.UpdateEmployee(updateEmployeeDto));
        }
        [ProducesResponseType(typeof(bool),statusCode:StatusCodes.Status200OK)]
        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(int id)
        { 
            return Ok(await _employeeService.DeleteEmployee(id));
        
        }
    }
 
    }


