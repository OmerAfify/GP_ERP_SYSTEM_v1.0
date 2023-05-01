CREATE TABLE [dbo].[TB_FMS_TemplateAccounts] (
    [AccID]  INT NOT NULL,
    [TempID] INT NOT NULL,
    CONSTRAINT [composite_pk template_account] PRIMARY KEY CLUSTERED ([AccID] ASC, [TempID] ASC),
    CONSTRAINT [FK__TB_FMS_Te__AccID__160F4887] FOREIGN KEY ([AccID]) REFERENCES [dbo].[TB_FMS_Account] ([AccID]),
    CONSTRAINT [FK__TB_FMS_Te__TempI__17036CC0] FOREIGN KEY ([TempID]) REFERENCES [dbo].[TB_FMS_StatementTemplate] ([TempID])
);



