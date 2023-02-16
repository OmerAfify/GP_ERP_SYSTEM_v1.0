CREATE TABLE [dbo].[TB_DistributionOrder] (
    [distributionOrderId] INT             IDENTITY (1, 1) NOT NULL,
    [distributorId]       INT             NOT NULL,
    [totalQty]            INT             NOT NULL,
    [totalPrice]          DECIMAL (18, 2) NOT NULL,
    [orderingDate]        DATETIME        NOT NULL,
    [expectedArrivalDate] DATETIME        NOT NULL,
    [orderStatus]         INT             DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([distributionOrderId] ASC),
    CONSTRAINT [FK_distributionOrder_PK_Distributor] FOREIGN KEY ([distributorId]) REFERENCES [dbo].[TB_Distributor] ([distributorId]) ON DELETE CASCADE ON UPDATE CASCADE
);

