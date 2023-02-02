using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_BusinessLogic.Models
{
    public partial class TbFmsSectionTemplate
    {
        public int? SecTempId { get; set; }
        public string SecTempName { get; set; }
        public string SecTempAccounts { get; set; }

        public virtual TbFmsStatementTemplate SecTemp { get; set; }
    }
}
