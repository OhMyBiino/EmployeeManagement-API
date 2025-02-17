﻿
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystemModels
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First Name must be 2-50 characters.")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last Name must be 2-50 characters.")]
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }

        [Required]
        public string Email { get; set; }
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department? Dept { get; set; }
        public string PhotoPath { get; set; }
    }
}
