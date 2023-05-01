using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_Domians.Models
{
    public partial class TbFmsTemplateAccount
    {
        public int AccId { get; set; }
        public int TempId { get; set; }

        public virtual TbFmsAccount Acc { get; set; }
        public virtual TbFmsStatementTemplate Temp { get; set; }
    }
}
