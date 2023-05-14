CREATE TABLE [dbo].[TB_DistributionOrder] (
    [Id]                  INT             IDENTITY (1, 1) NOT NULL,
    [DistributorId]       INT             NOT NULL,
    [totalQty]            INT             NOT NULL,
    [totalPrice]          DECIMAL (18, 2) NOT NULL,
    [orderingDate]        DATETIME        NOT NULL,
    [expectedArrivalDate] DATETIME        NOT NULL,
    [orderStatusId]       INT             DEFAULT ((1)) NOT NULL,
    [SubTotal]            DECIMAL (18, 2) DEFAULT ((0.0)) NOT NULL,
    CONSTRAINT [PK_TB_DistributionOrder] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_distributionOrder_PK_Distributor] FOREIGN KEY ([DistributorId]) REFERENCES [dbo].[TB_Distributor] ([distributorId]) ON DELETE CASCADE,
    CONSTRAINT [FK_TB_DistributionOrder_TbDistributionOrderStatus_orderStatusId] FOREIGN KEY ([orderStatusId]) REFERENCES [dbo].[TbDistributionOrderStatus] ([Id]) ON DELETE CASCADE
);






GO
CREATE NONCLUSTERED INDEX [IX_TB_DistributionOrder_orderStatusId]
    ON [dbo].[TB_DistributionOrder]([orderStatusId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TB_DistributionOrder_DistributorId]
    ON [dbo].[TB_DistributionOrder]([DistributorId] ASC);

