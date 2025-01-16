using EmployeeManagementSystem.API.Models.DepartmentRepository;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystemModels;

namespace EmployeeManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {

        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentsController(IDepartmentRepository departmetnRepository)
        {
            _departmentRepository = departmetnRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            try 
            {
                var departments = (await _departmentRepository.GetDepartmentsAsync()).ToList();

                return Ok(departments);
            }
            catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database.");
            }
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Department>> GetDepartmentById(int Id) 
        {
            try
            {
                var department = await _departmentRepository.GetDepartmentByIdAsync(Id);

                if (department == null) 
                {
                    return NotFound($"Department with ID: {Id} not found.");
                }

                return Ok(department);
                
            }
            catch (Exception) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error retrieving Department with ID: {Id} from the database.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Department>> CreateDepartment(Department department) 
        {
            try
            {
                if (department == null) 
                {
                    return BadRequest();
                }

                //if Id already exist
                var existingDepartmentId = await _departmentRepository
                        .GetDepartmentByIdAsync(department.DepartmentId);

                if (existingDepartmentId != null) 
                {
                    return BadRequest($"Department with ID: {department.DepartmentId} is already existing.");
                }

                //if Name already registered
                var existingDepartmentName = await _departmentRepository
                        .GetDepartmentByName(department.DepartmentName);

                if (existingDepartmentName != null)
                {
                    return BadRequest("Department name is already been registered.");
                }

                var addedDepartment = await _departmentRepository.AddDepartmentAsync(department);

                return CreatedAtAction(nameof(GetDepartmentById), 
                        new { Id = addedDepartment.DepartmentId}, addedDepartment);

            }
            catch (Exception) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error adding data to the database.");
            }
        }


        [HttpPut("{Id:int}")]
        public async Task<ActionResult<Department>> UpdateDepartment(int Id, Department department) 
        {
            try
            {
                if (Id != department.DepartmentId) 
                {
                    return BadRequest("Error updating the Department because of the ID mismatch.");
                }

                //if ID is already taken
                var existingDepartmentId = await _departmentRepository.GetDepartmentByIdAsync(Id);

                if (existingDepartmentId == null) 
                {
                    return BadRequest($"Department with ID:{Id} cannot be found.");
                }

                //if Name already registered
                var existingDepartmentName = await _departmentRepository
                        .GetDepartmentByName(department.DepartmentName);

                if (existingDepartmentName != null) 
                {
                    return BadRequest("Department name is already been registered.");
                }

                var updatedDepartment = 
                        await _departmentRepository.UpdateDepartmentAsync(department);

                return Ok(updatedDepartment);
            }
            catch (Exception) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data from the database.");
            }
        }

        [HttpDelete]
        public async Task<ActionResult<Department>> DeleteDepartment(int Id) 
        {
            try
            {
                var departmentToDelete = await _departmentRepository.GetDepartmentByIdAsync(Id);

                if (departmentToDelete == null) 
                {
                    return BadRequest($"Department with ID:{Id} not found.");
                }

                var deletedDepartment = await _departmentRepository.DeleteDepartmentAsync(Id);

                return Ok(departmentToDelete);
            }
            catch (Exception) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data from database.");
            }
        }

    }
}
