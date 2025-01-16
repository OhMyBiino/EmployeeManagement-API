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

        public async Task<Department> GetDepartmentByName(string name) {

            var department = await _context.Departments
                    .FirstOrDefaultAsync(d => d.DepartmentName == name);

            return department;
        }

        public async Task<Department> AddDepartmentAsync(Department department) 
        {
            var addedDepartment = await _context.Departments.AddAsync(department);

            await _context.SaveChangesAsync();
            return addedDepartment.Entity;
        }

        public async Task<Department> UpdateDepartmentAsync(Department department) 
        {
            var departmentToUpdate = await _context.Departments
                .FirstOrDefaultAsync(d => d.DepartmentId == department.DepartmentId);

            if (departmentToUpdate != null) 
            {
                departmentToUpdate.DepartmentName = department.DepartmentName;
            }
            
            await _context.SaveChangesAsync();
            return departmentToUpdate;
        }

        public async Task<Department> DeleteDepartmentAsync(int Id) 
        {
            var departmentToDelete = await _context.Departments.FindAsync(Id);

            if (departmentToDelete != null) 
            {
                _context.Departments.Remove(departmentToDelete);
                await _context.SaveChangesAsync();
            }

            return departmentToDelete;
        }
    }
}
