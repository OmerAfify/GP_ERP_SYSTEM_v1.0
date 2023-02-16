CREATE TABLE [dbo].[TB_FMS_StatementTemplate] (
    [TempID]   INT            IDENTITY (1, 1) NOT NULL,
    [TempName] NVARCHAR (MAX) NULL,
    [TempDate] DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([TempID] ASC)
);

