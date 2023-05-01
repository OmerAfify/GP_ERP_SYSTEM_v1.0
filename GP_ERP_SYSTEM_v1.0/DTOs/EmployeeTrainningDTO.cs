namespace GP_ERP_SYSTEM_v1._0.DTOs
{


    public class AddEmployeeTrainningDTO
    {
        public string TrainningType { get; set; }
        public string TrainningDescription { get; set; }
    }
    public class EmployeeTrainningDTO : AddEmployeeTrainningDTO
    {
        public int TrainnningId { get; set; }
        public int EmployeeId { get; set; }
        public int HrmangerId { get; set; }

    }
}
