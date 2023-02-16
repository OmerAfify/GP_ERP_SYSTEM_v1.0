CREATE TABLE [dbo].[TB_RawMaterial] (
    [materialId]          INT            IDENTITY (1, 1) NOT NULL,
    [materialName]        NVARCHAR (MAX) NULL,
    [materialDescription] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_TB_Supplier] PRIMARY KEY CLUSTERED ([materialId] ASC)
);

