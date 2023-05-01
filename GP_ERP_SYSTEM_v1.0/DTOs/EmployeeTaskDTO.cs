
﻿using System;

namespace GP_ERP_SYSTEM_v1._0.DTOs
{
    public class AddEmployeeTaskDTO
    {
        public string TaskDescription { get; set; }
        public DateTime? TaskAssignedTime { get; set; }
        public DateTime? TaskDeadlineTime { get; set; }
        public int? BounsHours { get; set; }
    }

    public class EmployeeTaskDTO : AddEmployeeTrainningDTO
    {
        public int TaskId { get; set; }
        public int? EmplyeeId { get; set; }

    }
}
