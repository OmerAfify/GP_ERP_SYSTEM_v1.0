using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GP_ERP_SYSTEM_v1._0.DTOs
{
    public class AddToDoListDTO
    {
        [Required]
        public string ToDoListName { get; set; }
        [Required]
        public string ToDoListDesc { get; set; }
        [Required]
        public int CustomerId { get; set; }

    }
    public class ToDoListDTO : AddToDoListDTO
    {
        public int ToDoListId { get; set; }
        public string CustomerFullName { get; set; }
        public string CustomerPhone { get; set; }
    }
}
