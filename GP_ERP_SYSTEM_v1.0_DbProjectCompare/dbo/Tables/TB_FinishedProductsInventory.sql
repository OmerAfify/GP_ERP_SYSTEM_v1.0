CREATE TABLE [dbo].[TB_FinishedProductsInventory] (
    [productId]       INT             NOT NULL,
    [quantity]        INT             NOT NULL,
    [shippingDate]    DATETIME        NULL,
    [monthlyCosts]    DECIMAL (18, 2) NULL,
    [area]            NVARCHAR (50)   NULL,
    [reorderingPoint] INT             NOT NULL,
    [HasReachedROP]   BIT             DEFAULT (CONVERT([bit],(0))) NOT NULL,
    CONSTRAINT [PK_TB_FinishedProductsInventory] PRIMARY KEY CLUSTERED ([productId] ASC),
    CONSTRAINT [FK__TB_Finish__produ__3C69FB99] FOREIGN KEY ([productId]) REFERENCES [dbo].[TB_Product] ([productId]) ON DELETE CASCADE ON UPDATE CASCADE
);



