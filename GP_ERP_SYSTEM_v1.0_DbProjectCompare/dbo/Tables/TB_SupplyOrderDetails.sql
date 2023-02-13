CREATE TABLE [dbo].[TB_SupplyOrderDetails] (
    [supplyOrderId] INT             NOT NULL,
    [materialId]    INT             NOT NULL,
    [qty]           INT             NOT NULL,
    [price]         DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [COM_PK_supplyOrderId_materialId] PRIMARY KEY CLUSTERED ([supplyOrderId] ASC, [materialId] ASC),
    CONSTRAINT [FK_SupplyOrderDetails_PK_RawMaterial] FOREIGN KEY ([materialId]) REFERENCES [dbo].[TB_RawMaterial] ([materialId]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_SupplyOrderDetails_PK_SupplyOrder] FOREIGN KEY ([supplyOrderId]) REFERENCES [dbo].[TB_SupplyOrder] ([supplyOrderId]) ON DELETE CASCADE ON UPDATE CASCADE
);

