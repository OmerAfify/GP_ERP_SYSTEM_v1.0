CREATE TABLE [dbo].[TbOrderStatus_Suppliers] (
    [OrderStatusId]   INT            IDENTITY (1, 1) NOT NULL,
    [OrderStatusName] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_TbOrderStatus_Suppliers] PRIMARY KEY CLUSTERED ([OrderStatusId] ASC)
);

