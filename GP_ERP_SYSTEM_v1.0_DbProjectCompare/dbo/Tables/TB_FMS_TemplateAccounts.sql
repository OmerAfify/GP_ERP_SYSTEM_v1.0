CREATE TABLE [dbo].[TB_FMS_TemplateAccounts] (
    [AccID]  INT NULL,
    [TempID] INT NULL,
    [ID]     INT IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_TB_FMS_TemplateAccounts] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK__TB_FMS_Te__AccID__160F4887] FOREIGN KEY ([AccID]) REFERENCES [dbo].[TB_FMS_Account] ([AccID]),
    CONSTRAINT [FK__TB_FMS_Te__TempI__17036CC0] FOREIGN KEY ([TempID]) REFERENCES [dbo].[TB_FMS_StatementTemplate] ([TempID])
);

