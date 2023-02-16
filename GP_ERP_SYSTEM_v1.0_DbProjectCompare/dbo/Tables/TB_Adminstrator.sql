CREATE TABLE [dbo].[TB_Adminstrator] (
    [AdminID]        INT            IDENTITY (1, 1) NOT NULL,
    [ReporterID_FK]  INT            NOT NULL,
    [AdminFirstName] NVARCHAR (MAX) NULL,
    [AdminLastName]  NVARCHAR (MAX) NULL,
    [AdminEntryDate] DATETIME       NULL,
    CONSTRAINT [PK__TB_Admin__719FE4E800A4E6F2] PRIMARY KEY CLUSTERED ([AdminID] ASC),
    CONSTRAINT [FK_TB_Adminstrator_TB_Reporter] FOREIGN KEY ([ReporterID_FK]) REFERENCES [dbo].[TB_Reporter] ([ReporterID])
);

