using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_Domians.Models
{
    public partial class TbCustomer
    {
        public TbCustomer()
        {
            TbSurveys = new HashSet<TbSurvey>();
            TbTasks = new HashSet<TbTask>();
            TbToDoLists = new HashSet<TbToDoList>();
        }

        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Sex { get; set; } 
        public decimal Age { get; set; }
        public byte[] Image { get; set; }

        public virtual ICollection<TbSurvey> TbSurveys { get; set; }
        public virtual ICollection<TbTask> TbTasks { get; set; }
        public virtual ICollection<TbToDoList> TbToDoLists { get; set; }
    }

}
