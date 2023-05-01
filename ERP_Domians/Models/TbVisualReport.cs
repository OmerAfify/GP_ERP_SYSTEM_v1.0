using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_Domians.Models
{
    public partial class TbVisualReport
    {
        public int ReportId { get; set; }
        public DateTime? ReportDate { get; set; }
        public int RReporterId { get; set; }
        public int RAdminId { get; set; }

        public virtual TbAdminstrator RAdmin { get; set; }
        public virtual TbReporter RReporter { get; set; }
    }
}
