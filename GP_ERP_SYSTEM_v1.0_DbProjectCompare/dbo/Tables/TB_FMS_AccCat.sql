CREATE TABLE [dbo].[TB_FMS_AccCat] (
    [AccID] INT NOT NULL,
    [CatID] INT NOT NULL,
    CONSTRAINT [composite_pk category_account] PRIMARY KEY CLUSTERED ([AccID] ASC, [CatID] ASC),
    CONSTRAINT [FK__TB_FMS_Ac__AccID__114A936A] FOREIGN KEY ([AccID]) REFERENCES [dbo].[TB_FMS_Account] ([AccID]),
    CONSTRAINT [FK__TB_FMS_Ac__CatID__123EB7A3] FOREIGN KEY ([CatID]) REFERENCES [dbo].[TB_FMS_Category] ([CatID])
);



