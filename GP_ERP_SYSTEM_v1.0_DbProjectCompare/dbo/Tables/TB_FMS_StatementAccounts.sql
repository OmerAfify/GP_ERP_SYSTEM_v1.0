CREATE TABLE [dbo].[TB_FMS_StatementAccounts] (
    [AccName]    NVARCHAR (450)  NOT NULL,
    [StaID]      INT             NOT NULL,
    [AccBalance] DECIMAL (18, 2) NULL,
    CONSTRAINT [composite primary key] PRIMARY KEY CLUSTERED ([AccName] ASC, [StaID] ASC),
    CONSTRAINT [FK__TB_FMS_St__StaID__18EBB532] FOREIGN KEY ([StaID]) REFERENCES [dbo].[TB_FMS_Statement] ([StaID])
);



