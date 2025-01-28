using EmployeeCleanArch.Application.DTOs;
using EmployeeCleanArch.Application.Features.Employees.Commands.AddNewEmployee;
using EmployeeCleanArch.Application.Features.Employees.Commands.DeleteEmployee;
using EmployeeCleanArch.Application.Features.Employees.Commands.PurgeEmployee;
using EmployeeCleanArch.Application.Features.Employees.Commands.UpdateEmployee;
using EmployeeCleanArch.Application.Features.Employees.Queries.GetAllEmployees;
using EmployeeCleanArch.Application.Features.Employees.Queries.GetEmployeeByDepartment;
using EmployeeCleanArch.Application.Features.Employees.Queries.GetEmployeeById;
using EmployeeCleanArch.Application.Features.Employees.Queries.GetEmployeesbyJoiningDate;
using EmployeeCleanArch.Application.Features.Employees.Queries.GetEmployeesByNameSearch;
using EmployeeCleanArch.Application.Features.Employees.Queries.GetEmployeesbyNationality;
using EmployeeCleanArch.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

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
            Log.Information("Attempting to read all employees");
            var result = await _sender.Send(new GetAllEmployeesQuery());
            if (result == null)
            {
                Log.Error("There are no employees to fetch");
                return NotFound();
            }
            Log.Information("Employees are fetched successfully");
            return Ok(result);
        }

        [HttpGet("ViewByDepartment/{departmentId}")]
        public async Task<IActionResult> GetDepartmentByDepartmentAsync(long departmentId)
        {
            Log.Information("Attempting to read employees with Department Id {id}", departmentId);
            var result = await _sender.Send(new GetEmployeeByDepartmentQuery(departmentId));
            if (result != null)
            {
                Log.Error("There are no employees with Department id {id}", departmentId);
                return Ok(result);
            }
            Log.Information("Employee with Department id {id} is fetched successfully", departmentId);
            return NotFound();
        }

        [HttpGet("ViewById/{id}")]
        public async Task<IActionResult> GetEmployeeByIdAsync(long id)
        {
            Log.Information("Attempting to read employee with id {id}", id);
            var result = await _sender.Send(new GetEmployeeByIdQuery(id));
            if (result != null)
            {
                Log.Error("There is no employee with id {id}", id);
                return Ok(result);
            }
            Log.Information("Employee with id {id} is fetched successfully", id);
            return NotFound();
        }

        [HttpGet("ViewByNationality/{nationality}")]
        public async Task<IActionResult> GetEmployeeByNationalityAsync(string nationality)
        {
            Log.Information("Attempting to read employees with nationality: {nationality}", nationality);
            var result = await _sender.Send(new GetEmployeesByNationalityQuery(nationality));
            if (result != null)
            {
                Log.Error("There are no employees with nationality {nationality}", nationality);
                return Ok(result);
            }
            Log.Information("Employee with nationality {nationality} is fetched successfully", nationality);
            return NotFound();
        }

        [HttpGet("FilterByDateOfJoining")]
        public async Task<IActionResult> FilterEmployeesByDateOfJoining(DateTime joiningDateFloor, DateTime joiningDateCeiling)
        {
            Log.Information("Attempting to read employees with a date of joining filter between {joiningDateFloor} and {joiningDateCeiling}", joiningDateFloor, joiningDateCeiling);
            var result = await _sender.Send(new GetEmployeesByJoiningDateQuery(joiningDateFloor,joiningDateCeiling));
            if (result != null)
            {
                Log.Error("There are no employees for the given date of joining filter");
                return Ok(result);
            }
            Log.Information("Employees fetched for the given date of joining filter added successfully");
            return NotFound();
        }

        [HttpGet("SearchEmployee/{searchKey}")]
        public async Task<IActionResult> SearchEmployeesByName(string searchKey)
        {
            Log.Information("Attempting to read employees for the given search key: '{searchKey}'", searchKey);
            var result = await _sender.Send(new GetEmployeesByNameSearchQuery(searchKey));
            if (result != null)
            {
                Log.Error("There are no employees found for the search key: '{searchKey}'", searchKey);
                return Ok(result);
            }
            Log.Information("Employees fetched successful for the search key: '{searchKey}'", searchKey);
            return NotFound();
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddEmployeeAsync([FromBody] CreateEmployeeDTO employeeDTO)
        {
            Log.Information("Attempting to add a new employee");
            var result = await _sender.Send(new AddNewEmployeeCommand(employeeDTO));
            if (result.IsSuccess)
            {
                Log.Error("Couldn't create a new employee");
                return Ok(result);
            }
            Log.Information("Employee with id {id} added successfully", result.Data.Id);
            return BadRequest(result);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateEmployeeByIdAsync([FromBody] UpdateEmployeeDTO employeeDto, long id)
        {
            Log.Information("Attempting to update the employee with id {id}", id);
            var result = await _sender.Send(new UpdateEmployeeCommand(employeeDto, id));
            if (!result.IsSuccess)
            {
                Log.Error("There is no employee with id {id}", id);
                return BadRequest(result);
            }
            Log.Information("Employee with id {id} updated successfully", id);
            return Ok(result);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteEmployeeByIdAsync(long id)
        {
            Log.Information("Attempting to delete employee with id {id}", id);
            var result = await _sender.Send(new DeleteEmployeeCommand(id));
            if (!result.IsSuccess)
            {
                Log.Error("Couldn't delete the employee");
                return BadRequest(result);
            }
            Log.Information("Employee with id {id} deleted successfully", id);
            return Ok(result);
        }

        [HttpDelete("Purge/{id}")]
        public async Task<IActionResult> PurgeEmployeeByIdAsync(long id)
        {
            Log.Information("Attempting to purge employee with id {id}", id);
            var result = await _sender.Send(new PurgeEmployeeCommand(id));
            if (!result.IsSuccess)
            {
                Log.Error("Couldn't purge the data of the employee");
                return BadRequest(result);
            }
            Log.Information("Employee with id {id} purged successfully", id);
            return Ok(result);
        }
    }
}
