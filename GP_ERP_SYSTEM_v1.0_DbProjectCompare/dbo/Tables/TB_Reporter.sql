CREATE TABLE [dbo].[TB_Reporter] (
    [ReporterID]        INT            IDENTITY (1, 1) NOT NULL,
    [ReporterFirstName] NVARCHAR (MAX) NULL,
    [ReporterLastName]  NVARCHAR (MAX) NULL,
    [ReporterEntryDate] DATETIME       NULL,
    CONSTRAINT [PK__TB_Repor__4406548BB7FD1991] PRIMARY KEY CLUSTERED ([ReporterID] ASC)
);

