using EmployeeManagementSystemModels;
namespace EmployeeManagementSystem.API.Models.DepartmentRepository
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetDepartmentsAsync();
        Task<Department> GetDepartmentByIdAsync(int Id);
        Task<Department> GetDepartmentByName(string name);
        Task<Department> AddDepartmentAsync(Department department);
        Task<Department> UpdateDepartmentAsync(Department department);
        Task<Department> DeleteDepartmentAsync(int Id);
    }
}
