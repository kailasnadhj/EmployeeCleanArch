using EmployeeCleanArch.Application.DTOs;
using EmployeeCleanArch.Application.Features.Departments.Commands;
using EmployeeCleanArch.Application.Features.Departments.Queries;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> GetAllDepartmentsAsync()
        {
            var result = await _sender.Send(new GetAllDepartmentsQuery());
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
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Department not created");
        }
    }
}
