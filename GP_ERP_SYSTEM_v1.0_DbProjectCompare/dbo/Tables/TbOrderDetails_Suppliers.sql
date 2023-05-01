CREATE TABLE [dbo].[TbOrderDetails_Suppliers] (
    [Id]                               INT             IDENTITY (1, 1) NOT NULL,
    [OrderedRawMaterials_MaterialId]   INT             NULL,
    [OrderedRawMaterials_MaterialName] NVARCHAR (MAX)  NULL,
    [OrderedRawMaterials_SalesPrice]   DECIMAL (18, 2) NULL,
    [Quantity]                         INT             NOT NULL,
    [Price]                            DECIMAL (18, 2) NOT NULL,
    [TbOrder_SupplierId]               INT             NULL,
    CONSTRAINT [PK_TbOrderDetails_Suppliers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TbOrderDetails_Suppliers_TbOrder_Suppliers_TbOrder_SupplierId] FOREIGN KEY ([TbOrder_SupplierId]) REFERENCES [dbo].[TbOrder_Suppliers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_TbOrderDetails_Suppliers_TbOrder_SupplierId]
    ON [dbo].[TbOrderDetails_Suppliers]([TbOrder_SupplierId] ASC);

