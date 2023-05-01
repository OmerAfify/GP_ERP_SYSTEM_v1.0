using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_Domians.Models
{
    public partial class TbFmsStatement
    {
        public TbFmsStatement()
        {
            TbFmsStatementAccounts = new HashSet<TbFmsStatementAccount>();
        }

        public int StaId { get; set; }
        public string StaName { get; set; }
        public decimal? StaBalance { get; set; }
        public DateTime? StaDate { get; set; }

        public virtual ICollection<TbFmsStatementAccount> TbFmsStatementAccounts { get; set; }
    }
}
