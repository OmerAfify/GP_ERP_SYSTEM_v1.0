CREATE TABLE [dbo].[TB_Product] (
    [productId]          INT             IDENTITY (1, 1) NOT NULL,
    [productName]        NVARCHAR (MAX)  NULL,
    [productDescription] NVARCHAR (MAX)  NULL,
    [purchasePrice]      DECIMAL (18, 2) NOT NULL,
    [salesPrice]         DECIMAL (18, 2) NOT NULL,
    [categoryId]         INT             NOT NULL,
    CONSTRAINT [PK_Tb_Product] PRIMARY KEY CLUSTERED ([productId] ASC),
    CONSTRAINT [FK_Product_PK_Category] FOREIGN KEY ([categoryId]) REFERENCES [dbo].[TB_Category] ([categoryId]) ON DELETE CASCADE ON UPDATE CASCADE
);

