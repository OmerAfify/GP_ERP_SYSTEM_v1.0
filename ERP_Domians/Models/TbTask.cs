using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_Domians.Models
{
    public partial class TbTask
    {
        public int TaskId { get; set; }
        public int CustomerId { get; set; }
        public string TaskName { get; set; }
        public DateTime? TaskDate { get; set; }
        public string TaskDesc { get; set; }

        public virtual TbCustomer Customer { get; set; }
    }
}
