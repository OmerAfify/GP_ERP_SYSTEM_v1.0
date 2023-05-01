CREATE TABLE [dbo].[TB_Category] (
    [categoryId]          INT            IDENTITY (1, 1) NOT NULL,
    [categoryName]        NVARCHAR (MAX) NULL,
    [categoryDescription] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_TB_Category] PRIMARY KEY CLUSTERED ([categoryId] ASC)
);

