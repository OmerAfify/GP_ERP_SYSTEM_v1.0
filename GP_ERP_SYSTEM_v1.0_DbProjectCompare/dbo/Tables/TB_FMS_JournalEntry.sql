CREATE TABLE [dbo].[TB_FMS_JournalEntry] (
    [JEID]          INT             IDENTITY (1, 1) NOT NULL,
    [JEName]        NVARCHAR (MAX)  NULL,
    [JEDescription] NVARCHAR (MAX)  NULL,
    [JECredit]      DECIMAL (18, 2) NULL,
    [JEDebit]       DECIMAL (18, 2) NULL,
    [JEDate]        DATETIME        NULL,
    [JEAccount1]    INT             NULL,
    [JEAccount2]    INT             NULL,
    PRIMARY KEY CLUSTERED ([JEID] ASC),
    FOREIGN KEY ([JEAccount1]) REFERENCES [dbo].[TB_FMS_Account] ([AccID]),
    FOREIGN KEY ([JEAccount2]) REFERENCES [dbo].[TB_FMS_Account] ([AccID])
);



