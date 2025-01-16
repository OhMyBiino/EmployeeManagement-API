using EmployeeManagementSystem.API.Database;
using System.Runtime.CompilerServices;
using EmployeeManagementSystemModels;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.API.Models.EmployeeRepository
{
    public class EmployeeRepository : IEmployeeRepository
    {

        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            var Employees = await _context.Employees.ToListAsync();

            return Employees;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int Id)
        {
            var employee = await _context.Employees
                .Include(e => e.Dept)
                .FirstOrDefaultAsync(e => e.EmployeeId == Id);

            return employee;
        }

        public async Task<Employee> GetEmployeeByEmailAsync(string email)
        {
            var employee = await _context.Employees
                .Include(e => e.Dept)
                .FirstOrDefaultAsync(e => e.Email == email);

            return employee;
        }

        public async Task<IEnumerable<Employee>> Search(string name, Gender? gender) 
        {
            IQueryable<Employee> query = _context.Employees;

            if (!String.IsNullOrEmpty(name)) 
            {
                query = query
                    .Include(e => e.Dept)
                    .Where(e => e.FirstName.Contains(name) || e.LastName.Contains(name));
            }

            if (gender != null) 
            {
                query = query.Where(e => e.Gender == gender);
            }

            var resultQuery = query.ToList();
            return resultQuery;
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee) 
        {
            var employeeToAdd = await _context.Employees.AddAsync(employee);

            if (employeeToAdd != null) 
            {
                await _context.SaveChangesAsync();
            }

            return employeeToAdd.Entity;
        }

        public async Task<Employee> UpdateEmployeeAsync(Employee employee) 
        {
            var employeeToUpdate = await _context.Employees
                .FirstOrDefaultAsync(e => e.EmployeeId == employee.EmployeeId);

            if (employeeToUpdate != null) 
            {
                employeeToUpdate.FirstName = employee.FirstName;
                employeeToUpdate.LastName = employee.LastName;
                employeeToUpdate.Email = employee.Email;
                employeeToUpdate.BirthDate = employee.BirthDate;
                employeeToUpdate.Gender = employee.Gender;
                employeeToUpdate.DepartmentId = employee.DepartmentId;
                employeeToUpdate.PhotoPath = employee.PhotoPath;
            }

            await _context.SaveChangesAsync();
            return employeeToUpdate;
        }

        public async Task<Employee> DeleteEmployeeAsync(int Id) 
        {
            var employeeToDelete = await _context.Employees.FindAsync(Id);

            if (employeeToDelete != null) 
            {
                _context.Employees.Remove(employeeToDelete);
                await _context.SaveChangesAsync();
            }

            return employeeToDelete;
        }
    }
}
