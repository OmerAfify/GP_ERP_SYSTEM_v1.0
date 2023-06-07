CREATE TABLE [dbo].[TB_Task] (
    [TaskId]     INT            IDENTITY (1, 1) NOT NULL,
    [CustomerId] INT            NOT NULL,
    [TaskName]   NVARCHAR (MAX) NULL,
    [TaskDate]   DATETIME       NULL,
    [TaskDesc]   NVARCHAR (MAX) NULL,
    CONSTRAINT [PK__TB_Task__7C6949B1BE263896] PRIMARY KEY CLUSTERED ([TaskId] ASC),
    CONSTRAINT [FK_TB_Task_TB_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[TB_Customer] ([CustomerId])
);



