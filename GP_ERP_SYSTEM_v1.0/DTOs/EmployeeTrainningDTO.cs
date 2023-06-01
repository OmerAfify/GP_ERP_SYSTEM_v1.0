using System.ComponentModel.DataAnnotations;

namespace GP_ERP_SYSTEM_v1._0.DTOs
{
    public class AddEmployeeTrainningDTO
    {
        [Required]
        public string TrainningType { get; set; }
        public string TrainningDescription { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public int Hrid { get; set; }
    }
    public class EmployeeTrainningDTO : AddEmployeeTrainningDTO
    {
        public int TrainnningId { get; set; }
        public string HRName { get; set; }
        public string EmployeeFullName { get; set; }

    }
}
