using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_Domians.Models
{
    public partial class TbFmsAccCat
    {
        public int AccId { get; set; }
        public int CatId { get; set; }

        public virtual TbFmsAccount Acc { get; set; }
        public virtual TbFmsCategory Cat { get; set; }
    }
}
