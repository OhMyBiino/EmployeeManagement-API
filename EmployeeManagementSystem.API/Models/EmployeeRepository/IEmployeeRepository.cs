﻿using EmployeeManagementSystemModels;

namespace EmployeeManagementSystem.API.Models.EmployeeRepository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync();

        Task<Employee> GetEmployeeByIdAsync(int Id);

        Task<Employee> GetEmployeeByEmailAsync(string email);

        Task<Employee> AddEmployeeAsync(Employee employee);
        Task<Employee> UpdateEmployeeAsync(Employee employee);

        Task<Employee> DeleteEmployeeAsync(int Id);

    }
}
