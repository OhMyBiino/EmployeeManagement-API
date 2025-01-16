using EmployeeManagementSystem.API.Database;
using EmployeeManagementSystemModels;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.API.Models.DepartmentRepository
{
    public class DepartmentRepository : IDepartmentRepository
    {

        private readonly AppDbContext _context;
        public DepartmentRepository(AppDbContext appDbContext) 
        {
            _context = appDbContext;
        }

        public async Task<IEnumerable<Department>> GetDepartmentsAsync() 
        {
            var departments = await _context.Departments.ToListAsync();

            return departments;
        }

        public async Task<Department> GetDepartmentByIdAsync(int Id) 
        {
            var department = await _context.Departments.FindAsync(Id);

            return department;
        }
    }
}
