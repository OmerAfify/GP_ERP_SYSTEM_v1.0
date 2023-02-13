CREATE TABLE [dbo].[TB_FMS_Account] (
    [AccID]        INT             IDENTITY (1, 1) NOT NULL,
    [AccName]      NVARCHAR (MAX)  NULL,
    [AccBalance]   DECIMAL (18, 2) NULL,
    [AccDebit]     DECIMAL (18, 2) NULL,
    [AccCredit]    DECIMAL (18, 2) NULL,
    [IncreaseMode] INT             NULL,
    PRIMARY KEY CLUSTERED ([AccID] ASC)
);

