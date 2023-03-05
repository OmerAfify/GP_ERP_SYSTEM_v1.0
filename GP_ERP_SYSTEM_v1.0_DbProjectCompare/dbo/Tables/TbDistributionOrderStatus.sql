CREATE TABLE [dbo].[TbDistributionOrderStatus] (
    [Id]     INT            IDENTITY (1, 1) NOT NULL,
    [Status] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_TbDistributionOrderStatus] PRIMARY KEY CLUSTERED ([Id] ASC)
);

