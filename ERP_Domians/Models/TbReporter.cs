using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_Domians.Models
{
    public partial class TbReporter
    {
        public TbReporter()
        {
            TbAdminstrators = new HashSet<TbAdminstrator>();
            TbVisualReports = new HashSet<TbVisualReport>();
        }

        public int ReporterId { get; set; }
        public string ReporterFirstName { get; set; }
        public string ReporterLastName { get; set; }
        public DateTime? ReporterEntryDate { get; set; }

        public virtual ICollection<TbAdminstrator> TbAdminstrators { get; set; }
        public virtual ICollection<TbVisualReport> TbVisualReports { get; set; }
    }
}
