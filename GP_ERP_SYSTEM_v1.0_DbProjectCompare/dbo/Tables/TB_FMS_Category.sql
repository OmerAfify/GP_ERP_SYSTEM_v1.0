CREATE TABLE [dbo].[TB_FMS_Category] (
    [CatID]          INT            IDENTITY (1, 1) NOT NULL,
    [CatName]        NVARCHAR (MAX) NULL,
    [CatDescription] NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([CatID] ASC)
);

