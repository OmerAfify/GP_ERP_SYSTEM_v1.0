using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_Domians.Models
{
    public partial class TbManufacturingOrder
    {
        public TbManufacturingOrder()
        {

        }

        

        public TbManufacturingOrder(int productManufacturedId, int qtyToManufacture, DateTime startingDate,
                                     DateTime finishingDate, decimal manufacturingCost, int manufacturingStatusId
            , IReadOnlyList<TbManufacturingOrderDetail> manufacturingOrderDetails)
        {
            this.ProductManufacturedId = productManufacturedId;
            this.QtyToManufacture = qtyToManufacture;
            this.StartingDate = startingDate;
            this.FinishingDate = finishingDate;
            this.ManufacturingCost = manufacturingCost;
            this.ManufacturingStatusId = manufacturingStatusId;
            this.ManufacturingOrderDetails = manufacturingOrderDetails;

        }


        public int Id { get; set; }
        public int ProductManufacturedId { get; set; }
        public virtual TbProduct ProductManufactured { get; set; }


        public int QtyToManufacture { get; set; }

        public decimal ManufacturingCost { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime FinishingDate { get; set; }

        public int ManufacturingStatusId { get; set; }
        public TbManufacturingStatus ManufacturingStatus { get; set; }

        public virtual IReadOnlyList<TbManufacturingOrderDetail> ManufacturingOrderDetails { get; set; }
    }
}
