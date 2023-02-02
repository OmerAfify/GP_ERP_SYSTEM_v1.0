using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_BusinessLogic.Models
{
    public partial class TbFmsStatement
    {
        public int StaId { get; set; }
        public string StaName { get; set; }
        public decimal? StaBalance { get; set; }
        public DateTime? StaDate { get; set; }
    }
}
