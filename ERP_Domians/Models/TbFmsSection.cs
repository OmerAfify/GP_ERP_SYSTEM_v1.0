using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_BusinessLogic.Models
{
    public partial class TbFmsSection
    {
        public int? SecId { get; set; }
        public string SecName { get; set; }
        public string SecAccounts { get; set; }

        public virtual TbFmsStatement Sec { get; set; }
    }
}
