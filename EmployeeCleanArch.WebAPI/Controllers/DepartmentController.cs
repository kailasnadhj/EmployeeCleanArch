using EmployeeCleanArch.Application.DTOs;
using EmployeeCleanArch.Application.Features.Departments.Commands.AddNewDepartment;
using EmployeeCleanArch.Application.Features.Departments.Commands.DeleteDepartment;
using EmployeeCleanArch.Application.Features.Departments.Commands.PurgeDepartment;
using EmployeeCleanArch.Application.Features.Departments.Commands.UpdateDepartment;
using EmployeeCleanArch.Application.Features.Departments.Queries.GetAllDepartments;
using EmployeeCleanArch.Application.Features.Departments.Queries.GetDepartmentById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace EmployeeCleanArch.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ISender _sender;

        public DepartmentController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("ViewAll")]
        //We have a format
        public async Task<IActionResult> GetAllDepartmentsAsync()
        {
            Log.Information("Attempting to read all departments");
            var result = await _sender.Send(new GetAllDepartmentsQuery());
            if (result == null)
            {
                Log.Error("There are no departments to fetch");
                return NotFound();
            }
            Log.Information("Departments are fetched successfully");
            return Ok(result);
        }

        [HttpGet("ViewById/{id}")]
        public async Task<IActionResult> GetDepartmentByIdAsync(long id)
        {
            Log.Information("Attempting to read department with id {id}",id);
            var result = await _sender.Send(new GetDepartmentByIdQuery(id));
            if (result == null)
            {
                Log.Error("There is no department with id {id}",id);
                return NotFound();
            }
            Log.Information("Department with id {id} is fetched successfully",id);
            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddDepartmentAsync([FromBody] CreateDepartmentDTO departmentDto)
        {
            Log.Information("Attempting to add a new department");
            var result = await _sender.Send(new AddNewDepartmentCommand(departmentDto));
            if (!result.IsSuccess)
            {
                Log.Error("Couldn't create a new department");
                return BadRequest(result);
            }
            Log.Information("Department with id {id} added successfully",result.Data.Id);
            return Ok(result);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateDepartmentByIdAsync([FromBody] UpdateDepartmentDTO departmentDto,int id)
        {
            Log.Information("Attempting to update the department with id {id}",id);
            var result = await _sender.Send(new UpdateDepartmentCommand(departmentDto,id));
            if (!result.IsSuccess)
            {
                Log.Error("There is no department with id {id}", id);
                return BadRequest(result);
            }
            Log.Information("Department with id {id} updated successfully", id);
            return Ok(result);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteDepartmentByIdAsync(int id)
        {
            Log.Information("Attempting to delete department with id {id}",id);
            var result = await _sender.Send(new DeleteDepartmentCommand(id));
            if (!result.IsSuccess)
            {
                Log.Error("Couldn't delete the departmnet");
                return BadRequest(result);
            }
            Log.Information("Department with id {id} deleted successfully", id);
            return Ok(result);
        }

        [HttpDelete("Purge/{id}")]
        public async Task<IActionResult> PurgeDepartmentByIdAsync(int id)
        {
            Log.Information("Attempting to purge department with id {id}",id);
            var result = await _sender.Send(new PurgeDepartmentCommand(id));
            if (!result.IsSuccess)
            {
                Log.Error("Couldn't purge the data of the department");
                return BadRequest(result);
            }
            Log.Information("Department with id {id} purged successfully", id);
            return Ok(result);
        }
    }
}
