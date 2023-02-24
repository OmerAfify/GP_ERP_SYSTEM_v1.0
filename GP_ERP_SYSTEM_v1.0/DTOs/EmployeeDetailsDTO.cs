using System;

namespace GP_ERP_SYSTEM_v1._0.DTOs
{

    public class AddEmployeeDTO
    {
        public string EmployeeFullName { get; set; }
        public decimal? TaxWithholding { get; set; }
        public int? HoursWorked { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public DateTime? AttendenceTime { get; set; }
        public DateTime? Holidays { get; set; }
        public decimal? EmployeeSalary { get; set; }
    }
    public class EmployeeDetailsDTO : AddEmployeeDTO
    {
        public int EmployeeId { get; set; }
    }

}
