using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_Domians.Models
{
    public partial class TbFmsCategory
    {
        public TbFmsCategory()
        {
            TbFmsAccCats = new HashSet<TbFmsAccCat>();
        }

        public int CatId { get; set; }
        public string CatName { get; set; }
        public string CatDescription { get; set; }

        public virtual ICollection<TbFmsAccCat> TbFmsAccCats { get; set; }
    }
}
