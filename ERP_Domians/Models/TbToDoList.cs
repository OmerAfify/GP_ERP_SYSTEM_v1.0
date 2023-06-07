using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_Domians.Models
{
    public partial class TbToDoList
    {
        public int ToDoListId { get; set; }
        public string ToDoListName { get; set; }
        public string ToDoListDesc { get; set; }
        public int CustomerId { get; set; }

        public virtual TbCustomer Customer { get; set; }
    }
}
