CREATE TABLE [dbo].[TbManufacturingStatus] (
    [statusId]   INT            IDENTITY (1, 1) NOT NULL,
    [statusName] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_TbManufacturingStatus] PRIMARY KEY CLUSTERED ([statusId] ASC)
);

