﻿using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_BusinessLogic.Models
{
    public partial class TbRawMaterialsInventory
    {
        public int MaterialId { get; set; }
        public int Quantity { get; set; }
        public DateTime ShippingDate { get; set; }
        public decimal MonthlyCosts { get; set; }
        public string Area { get; set; }
        public int ReorderingPoint { get; set; }

        public virtual TbRawMaterial Material { get; set; }
    }
}
