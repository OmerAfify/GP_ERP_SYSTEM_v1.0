using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_Domians.Models
{
    public partial class TbCategory
    {
        public TbCategory()
        {
            TbProducts = new HashSet<TbProduct>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }

        public virtual ICollection<TbProduct> TbProducts { get; set; }
    }
}
