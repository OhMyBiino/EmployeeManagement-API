using EmployeeManagementSystemModels;
namespace EmployeeManagementSystem.API.Models.DepartmentRepository
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetDepartmentsAsync();
        Task<Department> GetDepartmentByIdAsync(int Id);
    }
}
