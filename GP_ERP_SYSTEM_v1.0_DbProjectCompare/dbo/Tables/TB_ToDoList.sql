CREATE TABLE [dbo].[TB_ToDoList] (
    [ToDoListId]   INT            IDENTITY (1, 1) NOT NULL,
    [ToDoListName] NVARCHAR (MAX) NULL,
    [ToDoListDesc] NVARCHAR (MAX) NULL,
    [CustomerId]   INT            NOT NULL,
    CONSTRAINT [PK__TB_ToDoL__1BEFD56CDF0D5E6E] PRIMARY KEY CLUSTERED ([ToDoListId] ASC),
    CONSTRAINT [FK__TB_ToDoLi__Custo__45F365D3] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[TB_Customer] ([CustomerId])
);



