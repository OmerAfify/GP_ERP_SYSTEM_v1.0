CREATE TABLE [dbo].[TB_SupplyOrder] (
    [supplyOrderId]       INT             IDENTITY (1, 1) NOT NULL,
    [supplierId]          INT             NOT NULL,
    [totalQty]            INT             NOT NULL,
    [totalPrice]          DECIMAL (18, 2) NOT NULL,
    [orderingDate]        DATETIME        NOT NULL,
    [expectedArrivalDate] DATETIME        NOT NULL,
    [orderStatus]         INT             DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([supplyOrderId] ASC),
    CONSTRAINT [FK_SupplyOrder_PK_Supplier] FOREIGN KEY ([supplierId]) REFERENCES [dbo].[TB_Supplier] ([supplierId]) ON DELETE CASCADE ON UPDATE CASCADE
);

