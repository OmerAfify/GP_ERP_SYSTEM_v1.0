CREATE TABLE [dbo].[TB_FMS_StatementAccounts] (
    [AccName]    NVARCHAR (MAX)  NULL,
    [StaID]      INT             NULL,
    [AccBalance] DECIMAL (18, 2) NULL,
    [ID]         INT             IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_TB_FMS_StatementAccounts] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK__TB_FMS_St__StaID__18EBB532] FOREIGN KEY ([StaID]) REFERENCES [dbo].[TB_FMS_Statement] ([StaID])
);

