using EmployeeCleanArch.Application.DTOs;
using EmployeeCleanArch.Application.Features.Employees.Commands.AddNewEmployee;
using EmployeeCleanArch.Application.Features.Employees.Commands.DeleteEmployee;
using EmployeeCleanArch.Application.Features.Employees.Commands.PurgeEmployee;
using EmployeeCleanArch.Application.Features.Employees.Commands.UpdateEmployee;
using EmployeeCleanArch.Application.Features.Employees.Queries.GetAllEmployees;
using EmployeeCleanArch.Application.Features.Employees.Queries.GetEmployeeByDepartment;
using EmployeeCleanArch.Application.Features.Employees.Queries.GetEmployeeById;
using EmployeeCleanArch.Application.Features.Employees.Queries.GetEmployeesbyJoiningDate;
using EmployeeCleanArch.Application.Features.Employees.Queries.GetEmployeesbyNationality;
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

        [HttpGet("ViewByDepartment/{departmentId}")]
        public async Task<IActionResult> GetDepartmentByDepartmentAsync(long departmentId)
        {
            var result = await _sender.Send(new GetEmployeeByDepartmentQuery(departmentId));
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
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

        [HttpGet("ViewByNationality/{nationality}")]
        public async Task<IActionResult> GetEmployeeByNationalityAsync(string nationality)
        {
            var result = await _sender.Send(new GetEmployeesByNationalityQuery(nationality));
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpGet("FilterByDateOfJoining")]
        public async Task<IActionResult> FilterEmployeesByDateOfJoining(DateTime joiningDateFloor, DateTime joiningDateCeiling)
        {
            var result = await _sender.Send(new GetEmployeesByJoiningDateQuery(joiningDateFloor,joiningDateCeiling));
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
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

        [HttpDelete("Purge/{id}")]
        public async Task<IActionResult> PurgeEmployeeByIdAsync(long id)
        {

            var result = await _sender.Send(new PurgeEmployeeCommand(id));
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
