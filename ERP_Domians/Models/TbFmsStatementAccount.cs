using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_Domians.Models
{
    public partial class TbFmsStatementAccount
    {
        public string AccName { get; set; }
        public int StaId { get; set; }
        public decimal? AccBalance { get; set; }

        public virtual TbFmsStatement Sta { get; set; }
    }
}
