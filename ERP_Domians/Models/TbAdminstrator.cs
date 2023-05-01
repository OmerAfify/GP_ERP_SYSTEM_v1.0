using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_Domians.Models
{
    public partial class TbAdminstrator
    {
        public TbAdminstrator()
        {
            TbVisualReports = new HashSet<TbVisualReport>();
        }

        public int AdminId { get; set; }
        public int ReporterIdFk { get; set; }
        public string AdminFirstName { get; set; }
        public string AdminLastName { get; set; }
        public DateTime? AdminEntryDate { get; set; }

        public virtual TbReporter ReporterIdFkNavigation { get; set; }
        public virtual ICollection<TbVisualReport> TbVisualReports { get; set; }
    }
}
