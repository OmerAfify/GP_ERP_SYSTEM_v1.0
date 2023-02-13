CREATE TABLE [dbo].[TB_VisualReport] (
    [ReportID]     INT      IDENTITY (1, 1) NOT NULL,
    [ReportDate]   DATETIME NULL,
    [R_ReporterID] INT      NOT NULL,
    [R_AdminID]    INT      NOT NULL,
    CONSTRAINT [PK__TB_Visua__D5BD48E54B3BA761] PRIMARY KEY CLUSTERED ([ReportID] ASC),
    CONSTRAINT [FK_TB_VisualReport_TB_Adminstrator] FOREIGN KEY ([R_AdminID]) REFERENCES [dbo].[TB_Adminstrator] ([AdminID]),
    CONSTRAINT [FK_TB_VisualReport_TB_Reporter] FOREIGN KEY ([R_ReporterID]) REFERENCES [dbo].[TB_Reporter] ([ReporterID])
);

