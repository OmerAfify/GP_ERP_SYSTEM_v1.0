CREATE TABLE [dbo].[TB_RawMaterialsInventory] (
    [materialId]      INT             NOT NULL,
    [quantity]        INT             NOT NULL,
    [shippingDate]    DATETIME        NOT NULL,
    [monthlyCosts]    DECIMAL (18, 2) NOT NULL,
    [area]            NVARCHAR (MAX)  NULL,
    [reorderingPoint] INT             NOT NULL,
    [HasReachedROP]   BIT             DEFAULT (CONVERT([bit],(0))) NOT NULL,
    CONSTRAINT [PK__TB_RawMa__99B653FDB26AF845] PRIMARY KEY CLUSTERED ([materialId] ASC),
    CONSTRAINT [FK_RawMaterialsInventory_PK_RawMaterials] FOREIGN KEY ([materialId]) REFERENCES [dbo].[TB_RawMaterial] ([materialId]) ON DELETE CASCADE ON UPDATE CASCADE
);



