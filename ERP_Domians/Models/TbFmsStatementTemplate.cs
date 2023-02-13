using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_Domians.Models
{
    public partial class TbFmsStatementTemplate
    {
        public TbFmsStatementTemplate()
        {
            TbFmsTemplateAccounts = new HashSet<TbFmsTemplateAccount>();
        }

        public int TempId { get; set; }
        public string TempName { get; set; }
        public DateTime? TempDate { get; set; }

        public virtual ICollection<TbFmsTemplateAccount> TbFmsTemplateAccounts { get; set; }
    }
}
