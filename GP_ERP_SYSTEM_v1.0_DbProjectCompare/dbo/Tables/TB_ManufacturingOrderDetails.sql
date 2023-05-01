CREATE TABLE [dbo].[TB_ManufacturingOrderDetails] (
    [manfactoringOrderId] INT NOT NULL,
    [rawMaterialId]       INT NOT NULL,
    [rawMaterialQtyUsed]  INT NOT NULL,
    CONSTRAINT [PK_TB_ManufacturingOrderDetails] PRIMARY KEY CLUSTERED ([manfactoringOrderId] ASC, [rawMaterialId] ASC),
    CONSTRAINT [FK_TB_ManufacturingOrderDetails_TB_ManufacturingOrder] FOREIGN KEY ([manfactoringOrderId]) REFERENCES [dbo].[TbManufacturingOrders] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_TB_ManufacturingOrderDetails_TB_RawMaterial] FOREIGN KEY ([rawMaterialId]) REFERENCES [dbo].[TB_RawMaterial] ([materialId]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_TB_ManufacturingOrderDetails_rawMaterialId]
    ON [dbo].[TB_ManufacturingOrderDetails]([rawMaterialId] ASC);

