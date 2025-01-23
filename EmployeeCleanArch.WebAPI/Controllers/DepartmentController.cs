using EmployeeCleanArch.Application.DTOs;
using EmployeeCleanArch.Application.Features.Departments.Commands.AddNewDepartment;
using EmployeeCleanArch.Application.Features.Departments.Commands.DeleteDepartment;
using EmployeeCleanArch.Application.Features.Departments.Commands.PurgeDepartment;
using EmployeeCleanArch.Application.Features.Departments.Commands.UpdateDepartment;
using EmployeeCleanArch.Application.Features.Departments.Queries.GetAllDepartments;
using EmployeeCleanArch.Application.Features.Departments.Queries.GetDepartmentById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
            var result = await _sender.Send(new GetAllDepartmentsQuery());
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("ViewById/{id}")]
        public async Task<IActionResult> GetDepartmentByIdAsync(long id)
        {
            var result = await _sender.Send(new GetDepartmentByIdQuery(id));
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddDepartmentAsync([FromBody] CreateDepartmentDTO departmentDto)
        {

            var result = await _sender.Send(new AddNewDepartmentCommand(departmentDto));
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateDepartmentByIdAsync([FromBody] UpdateDepartmentDTO departmentDto,int id)
        {

            var result = await _sender.Send(new UpdateDepartmentCommand(departmentDto,id));
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteDepartmentByIdAsync(int id)
        {

            var result = await _sender.Send(new DeleteDepartmentCommand(id));
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("Purge/{id}")]
        public async Task<IActionResult> PurgeDepartmentByIdAsync(int id)
        {

            var result = await _sender.Send(new PurgeDepartmentCommand(id));
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
