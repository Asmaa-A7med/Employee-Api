using Microsoft.AspNetCore.Mvc;
using Repositories.Entities;
using Service;
using webApp.Dtos;

namespace webApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string term)
        {
            if (string.IsNullOrEmpty(term))
                return BadRequest("Search term is required.");

            var employees = await _employeeService.SearchEmployeesAsync(term);

            if (!employees.Any())
                return NotFound("No employees found.");

            return Ok(employees);
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }


        [HttpGet("sort")]
        public async Task<IActionResult> Sort([FromQuery] string sortBy = "name", [FromQuery] bool ascending = true)
        {
            var employees = await _employeeService.GetEmployeesSortedAsync(sortBy, ascending);

            if (!employees.Any())
                return NotFound("No employees found.");

            return Ok(employees);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            if (employee == null)
                return BadRequest("Employee data is null.");

  

            await _employeeService.AddEmployeeAsync(employee);

            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeDTO dto)
        {
            if (dto == null)
                return BadRequest("Invalid data.");

            var result = await _employeeService.UpdateEmployeeAsync(id, dto);
            if (!result)
                return NotFound("Employee not found.");

            return NoContent();
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await _employeeService.DeleteEmployeeAsync(id);
            if (!result)
                return NotFound("Employee not found.");

            return NoContent();
        }

        [HttpDelete("delete-multiple")]
        public async Task<IActionResult> DeleteMultiple([FromBody] List<int> ids)
        {
            if (ids == null || !ids.Any())
                return BadRequest("No IDs provided.");

            var result = await _employeeService.DeleteMultipleEmployeesAsync(ids);
            if (!result)
                return NotFound("Some or all employees not found.");

            return NoContent();
        }
        [HttpGet("paginated")]
        public async Task<IActionResult> GetEmployeesPaginated(
     [FromQuery] int pageNumber = 1,
     [FromQuery] int pageSize = 10,
     [FromQuery] string sortKey = "name",
     [FromQuery] string sortDirection = "asc")
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            var result = await _employeeService.GetPaginatedEmployeesAsync(pageNumber, pageSize, sortKey, sortDirection);

            var response = new
            {
                Data = result.Employees,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = result.TotalCount,
                TotalPages = result.TotalPages
            };

            return Ok(response);
        }




    }


}
