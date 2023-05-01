using System.ComponentModel.DataAnnotations;
using System;

namespace GP_ERP_SYSTEM_v1._0.DTOs
{
        public class AddEmployeeDTO
        {
            [Required(ErrorMessage = "Employee Name is Required")]
            [StringLength(50, MinimumLength = 3, ErrorMessage = "Minimum Characters is 3")]
            public string EmployeeFullName { get; set; }
            public decimal? TaxWithholding { get; set; }
            public int? HoursWorked { get; set; }
            public string PhotoFileName { get; set; }
            public DateTime? DateOfJoining { get; set; }
            public DateTime? AttendenceTime { get; set; }
            public DateTime? Holidays { get; set; }
            [Required]
            public decimal? EmployeeSalary { get; set; }
            [Required]
            public int? HrmanagerId { get; set; }
        }
        public class EmployeeDetailsDTO : AddEmployeeDTO
        {
            public int EmployeeId { get; set; }

            public string HrfullName { get; set; }

        }
    }

