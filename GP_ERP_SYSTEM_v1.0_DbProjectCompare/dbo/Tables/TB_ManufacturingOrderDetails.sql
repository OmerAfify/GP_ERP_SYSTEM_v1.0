CREATE TABLE [dbo].[TB_ManufacturingOrderDetails] (
    [manfactoringOrderId] INT NOT NULL,
    [rawMaterialId]       INT NOT NULL,
    [rawMaterialQtyUsed]  INT NOT NULL,
    CONSTRAINT [PK_TB_ManufacturingOrderDetails] PRIMARY KEY CLUSTERED ([manfactoringOrderId] ASC, [rawMaterialId] ASC),
    CONSTRAINT [FK_TB_ManufacturingOrderDetails_TB_ManufacturingOrder] FOREIGN KEY ([manfactoringOrderId]) REFERENCES [dbo].[TB_ManufacturingOrder] ([manufactoringOrderId]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_TB_ManufacturingOrderDetails_TB_RawMaterial] FOREIGN KEY ([rawMaterialId]) REFERENCES [dbo].[TB_RawMaterial] ([materialId]) ON DELETE CASCADE ON UPDATE CASCADE
);

