CREATE TABLE [dbo].[TB_HRManagerDetails] (
    [HRId]       INT            IDENTITY (1, 1) NOT NULL,
    [HRFullName] NVARCHAR (MAX) NULL,
    [HRPassword] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_TB_HRManagerDetails] PRIMARY KEY CLUSTERED ([HRId] ASC)
);

