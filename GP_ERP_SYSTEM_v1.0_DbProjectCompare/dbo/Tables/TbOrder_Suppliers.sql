CREATE TABLE [dbo].[TbOrder_Suppliers] (
    [Id]                  INT             IDENTITY (1, 1) NOT NULL,
    [SupplierId]          INT             NOT NULL,
    [TotalQty]            INT             NOT NULL,
    [SubTotalPrice]       DECIMAL (18, 2) NOT NULL,
    [ShippingCost]        DECIMAL (18, 2) NOT NULL,
    [TotalPrice]          DECIMAL (18, 2) NOT NULL,
    [OrderStatusId]       INT             NOT NULL,
    [OrderingDate]        DATETIME2 (7)   NOT NULL,
    [ExpectedArrivalDate] DATETIME2 (7)   NOT NULL,
    CONSTRAINT [PK_TbOrder_Suppliers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TbOrder_Suppliers_TB_Supplier_SupplierId] FOREIGN KEY ([SupplierId]) REFERENCES [dbo].[TB_Supplier] ([supplierId]) ON DELETE CASCADE,
    CONSTRAINT [FK_TbOrder_Suppliers_TbOrderStatus_Suppliers_OrderStatusId] FOREIGN KEY ([OrderStatusId]) REFERENCES [dbo].[TbOrderStatus_Suppliers] ([OrderStatusId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_TbOrder_Suppliers_SupplierId]
    ON [dbo].[TbOrder_Suppliers]([SupplierId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TbOrder_Suppliers_OrderStatusId]
    ON [dbo].[TbOrder_Suppliers]([OrderStatusId] ASC);

