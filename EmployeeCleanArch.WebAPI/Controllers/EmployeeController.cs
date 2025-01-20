using EmployeeCleanArch.Application.DTOs;
using EmployeeCleanArch.Application.Features.Departments.Commands.DeleteDepartment;
using EmployeeCleanArch.Application.Features.Departments.Commands.UpdateDepartment;
using EmployeeCleanArch.Application.Features.Employees.Commands.AddNewEmployee;
using EmployeeCleanArch.Application.Features.Employees.Commands.DeleteEmployee;
using EmployeeCleanArch.Application.Features.Employees.Commands.UpdateEmployee;
using EmployeeCleanArch.Application.Features.Employees.Queries.GetAllEmployees;
using EmployeeCleanArch.Application.Features.Employees.Queries.GetEmployeeById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeCleanArch.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ISender _sender;

        public EmployeeController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("ViewAll")]
        //We have a format
        public async Task<IActionResult> GetAllEmployeesAsync()
        {
            var result = await _sender.Send(new GetAllEmployeesQuery());
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddEmployeeAsync([FromBody] CreateEmployeeDTO employeeDTO)
        {

            var result = await _sender.Send(new AddNewEmployeeCommand(employeeDTO));
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("ViewById/{id}")]
        public async Task<IActionResult> GetEmployeeByIdAsync(long id)
        {
            var result = await _sender.Send(new GetEmployeeByIdQuery(id));
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateEmployeeByIdAsync([FromBody] UpdateEmployeeDTO employeeDto, long id)
        {

            var result = await _sender.Send(new UpdateEmployeeCommand(employeeDto, id));
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteEmployeeByIdAsync(long id)
        {

            var result = await _sender.Send(new DeleteEmployeeCommand(id));
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
