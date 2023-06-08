using System;
using System.ComponentModel.DataAnnotations;

namespace GP_ERP_SYSTEM_v1._0.DTOs
{

    public class AddEmployeeDTO
    {
        [Required(ErrorMessage = "Please Enter Employee Name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Minimum Characters is 3")]
        public string EmployeeFullName { get; set; }
        [Required(ErrorMessage = "Please Enter TaxWithholding")]
        public decimal? TaxWithholding { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "You should input number for hours")]
        public int? HoursWorked { get; set; }
        [Required]
        public DateTime? DateOfJoining { get; set; }
        [Required]
        public DateTime? AttendenceTime { get; set; }
        [Required]
        public DateTime? Holidays { get; set; }
        [Required]
        public decimal EmployeeSalary { get; set; }
        [Required(ErrorMessage = "Please Enter HR Manager Id")]
        public int Hrid { get; set; }
    }
    public class EmployeeDetailsDTO : AddEmployeeDTO
    {
        public int EmployeeId { get; set; }
        public string HRName { get; set; }
    }

}
