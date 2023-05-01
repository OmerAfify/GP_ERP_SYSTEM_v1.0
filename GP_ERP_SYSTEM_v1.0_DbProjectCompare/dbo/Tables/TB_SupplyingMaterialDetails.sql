CREATE TABLE [dbo].[TB_SupplyingMaterialDetails] (
    [supplierId]   INT             NOT NULL,
    [materialId]   INT             NOT NULL,
    [pricePerUnit] DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [COM_PK_supplierId_materialId] PRIMARY KEY CLUSTERED ([supplierId] ASC, [materialId] ASC),
    CONSTRAINT [FK_SupplyingMaterialDetails_PK_RawMaterial] FOREIGN KEY ([materialId]) REFERENCES [dbo].[TB_RawMaterial] ([materialId]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_SupplyingMaterialDetails_PK_Supplier] FOREIGN KEY ([supplierId]) REFERENCES [dbo].[TB_Supplier] ([supplierId]) ON DELETE CASCADE ON UPDATE CASCADE
);



