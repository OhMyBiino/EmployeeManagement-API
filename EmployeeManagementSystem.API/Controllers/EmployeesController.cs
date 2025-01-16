using EmployeeManagementSystem.API.Models.EmployeeRepository;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystemModels;

namespace EmployeeManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            try
            {
                var resultEmployees = await _employeeRepository.GetEmployeesAsync();
                return Ok(resultEmployees);
            }
            catch (Exception) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database.");
            }
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int Id)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeByIdAsync(Id);

                if (employee == null) 
                {
                    return NotFound();
                }

                return Ok(employee);
            }
            catch (Exception) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    "Error retrieving data from the database.");
            }
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Employee>>> Search(string name, Gender? gender) 
        {
            try 
            {
                var resultQuery = await _employeeRepository.Search(name,gender);

                if (resultQuery.Any()) 
                {
                    return Ok(resultQuery);
                }

                return NotFound();
            }
            catch (Exception) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error searching from the database.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            try
            {
                if (employee == null) 
                {
                    return BadRequest();
                }

                var existingEmployee = await _employeeRepository.GetEmployeeByEmailAsync(employee.Email);

                if (existingEmployee != null) 
                {
                    ModelState.AddModelError("email", "Email is already been registered.");
                    return BadRequest(ModelState);
                }

                var addedEmployee = await _employeeRepository.AddEmployeeAsync(employee);

                return CreatedAtAction(nameof(GetEmployeeById), new { Id = addedEmployee.EmployeeId },
                   addedEmployee);
            }
            catch (Exception) 
            {
                throw;
            }
        }

        [HttpPut("{Id:int}")]
        public async Task<ActionResult<Employee>> UpdateEmployee(int Id, Employee employee)
        {
            try
            {
                if (Id != employee.EmployeeId) 
                {
                    return BadRequest("Id mismatch.");
                }

                var employeeToUpdate = await _employeeRepository.GetEmployeeByIdAsync(Id);

                if (employeeToUpdate == null) 
                {
                    return NotFound($"Employee with ID: {Id} coud not be found.");
                }

                var updatedEmployee = await _employeeRepository.UpdateEmployeeAsync(employee);

                return Ok(updatedEmployee);
            }
            catch (Exception) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating the data from the database.");
            }
        }

        [HttpDelete("{Id:int}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int Id) 
        {
            try
            {
                //To-Do: make it shortcut, we don't need to also check here if the item to delete
                //is existing, we already did that in the Repository Layer.
                var employeeToDelete = await _employeeRepository.GetEmployeeByIdAsync(Id);

                if (employeeToDelete == null)
                {
                    return NotFound($"Employee with ID: {Id} is not found.");
                }

                var deletedDepartment = await _employeeRepository.DeleteEmployeeAsync(Id);

                return Ok(deletedDepartment);
                //return NoContent();
            }
            catch (Exception) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting the data from the database.");
            }
        }

    }
}
