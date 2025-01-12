using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystemModels;
using System.Security.Cryptography.X509Certificates;

namespace EmployeeManagementSystem.API.Database
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed Department Data
            modelBuilder.Entity<Department>().HasData(new Department {
                DepartmentId = 1,
                DepartmentName = "IT"
            });
            modelBuilder.Entity<Department>().HasData(new Department {
                DepartmentId = 2,
                DepartmentName = "HR"
            });

            modelBuilder.Entity<Department>().HasData(new Department {
                DepartmentId = 3,
                DepartmentName = "Payroll"
            });

            modelBuilder.Entity<Department>().HasData(new Department {
                DepartmentId = 4,
                DepartmentName = "Admin"
            });


            //Seed Employee Data
            modelBuilder.Entity<Employee>().HasData(new Employee {
                EmployeeId = 1,
                FirstName = "John",
                LastName = "Doe",
                BirthDate = new DateTime(2000, 12, 5),
                Gender = Gender.Male,
                Email = "JohnDoe@email.com",
                DepartmentId = 1,
                PhotoPath = "images/john.png"
            });

            modelBuilder.Entity<Employee>().HasData(new Employee {
                EmployeeId = 2,
                FirstName = "Sam",
                LastName = "Galloway",
                BirthDate = new DateTime(2001, 10, 2),
                Gender = Gender.Male,
                Email = "SamGalloway@email.com",
                DepartmentId = 2,
                PhotoPath = "images/sam.jpg"
            });

            modelBuilder.Entity<Employee>().HasData(
                new Employee()
                {
                    EmployeeId = 3,
                    FirstName = "Mary",
                    LastName = "Smith",
                    Email = "MarySmith@email.com",
                    BirthDate = new DateTime(2003, 11, 11),
                    Gender = Gender.Female,
                    DepartmentId = 1,
                    PhotoPath = "images/mary.png"
                });

            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 4,
                FirstName = "Sara",
                LastName = "Talubati",
                BirthDate = new DateTime(2002, 10, 24),
                Gender = Gender.Female,
                Email = "SaraTalubati@email.com",
                DepartmentId = 4,
                PhotoPath = "images/sara.png"
            });
        }
    }
}
