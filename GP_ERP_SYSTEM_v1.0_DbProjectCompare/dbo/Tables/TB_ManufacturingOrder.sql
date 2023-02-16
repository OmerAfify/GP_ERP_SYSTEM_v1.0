CREATE TABLE [dbo].[TB_ManufacturingOrder] (
    [manufactoringOrderId]  INT            IDENTITY (1, 1) NOT NULL,
    [productManufacturedId] INT            NOT NULL,
    [leadTime_per_Days]     NVARCHAR (MAX) NULL,
    [qtyToProduce]          INT            NOT NULL,
    CONSTRAINT [PK_TB_ManufacturingOrder] PRIMARY KEY CLUSTERED ([manufactoringOrderId] ASC),
    CONSTRAINT [FK_TB_ManufacturingOrder_TB_Product] FOREIGN KEY ([productManufacturedId]) REFERENCES [dbo].[TB_Product] ([productId]) ON DELETE CASCADE ON UPDATE CASCADE
);

