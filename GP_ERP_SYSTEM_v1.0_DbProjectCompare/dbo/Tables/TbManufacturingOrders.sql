CREATE TABLE [dbo].[TbManufacturingOrders] (
    [Id]                    INT             IDENTITY (1, 1) NOT NULL,
    [ProductManufacturedId] INT             NOT NULL,
    [QtyToManufacture]      INT             NOT NULL,
    [ManufacturingCost]     DECIMAL (18, 2) NOT NULL,
    [StartingDate]          DATETIME2 (7)   NOT NULL,
    [FinishingDate]         DATETIME2 (7)   NOT NULL,
    [ManufacturingStatusId] INT             NOT NULL,
    CONSTRAINT [PK_TbManufacturingOrders] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TbManufacturingOrders_TB_Product_ProductManufacturedId] FOREIGN KEY ([ProductManufacturedId]) REFERENCES [dbo].[TB_Product] ([productId]) ON DELETE CASCADE,
    CONSTRAINT [FK_TbManufacturingOrders_TbManufacturingStatus_ManufacturingStatusId] FOREIGN KEY ([ManufacturingStatusId]) REFERENCES [dbo].[TbManufacturingStatus] ([statusId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_TbManufacturingOrders_ProductManufacturedId]
    ON [dbo].[TbManufacturingOrders]([ProductManufacturedId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TbManufacturingOrders_ManufacturingStatusId]
    ON [dbo].[TbManufacturingOrders]([ManufacturingStatusId] ASC);

