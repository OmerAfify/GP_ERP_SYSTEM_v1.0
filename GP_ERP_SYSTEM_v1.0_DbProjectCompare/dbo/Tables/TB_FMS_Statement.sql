CREATE TABLE [dbo].[TB_FMS_Statement] (
    [StaID]      INT             IDENTITY (1, 1) NOT NULL,
    [StaName]    NVARCHAR (MAX)  NULL,
    [StaBalance] DECIMAL (18, 2) NULL,
    [StaDate]    DATETIME        NULL,
    PRIMARY KEY CLUSTERED ([StaID] ASC)
);

