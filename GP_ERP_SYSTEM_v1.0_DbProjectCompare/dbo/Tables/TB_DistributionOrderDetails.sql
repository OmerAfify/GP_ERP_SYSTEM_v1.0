CREATE TABLE [dbo].[TB_DistributionOrderDetails] (
    [distributionOrderId] INT             NOT NULL,
    [productId]           INT             NOT NULL,
    [qty]                 INT             NOT NULL,
    [price]               DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [COM_PK_distributionOrderId_productId] PRIMARY KEY CLUSTERED ([distributionOrderId] ASC, [productId] ASC),
    CONSTRAINT [FK_DistributionOrderDetails_PK_DistributionOrder] FOREIGN KEY ([distributionOrderId]) REFERENCES [dbo].[TB_DistributionOrder] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_DistributionOrderDetails_PK_Products] FOREIGN KEY ([productId]) REFERENCES [dbo].[TB_Product] ([productId]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_TB_DistributionOrderDetails_productId]
    ON [dbo].[TB_DistributionOrderDetails]([productId] ASC);

