using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_Domians.Models
{
    public partial class TbFmsJournalEntry
    {
        public int Jeid { get; set; }
        public string Jename { get; set; }
        public string Jedescription { get; set; }
        public decimal? Jecredit { get; set; }
        public decimal? Jedebit { get; set; }
        public DateTime? Jedate { get; set; }
        public int? Jeaccount1 { get; set; }
        public int? Jeaccount2 { get; set; }

        public virtual TbFmsAccount Jeaccount1Navigation { get; set; }
        public virtual TbFmsAccount Jeaccount2Navigation { get; set; }
    }
}
