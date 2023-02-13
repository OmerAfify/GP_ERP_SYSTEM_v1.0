CREATE TABLE [dbo].[TB_Recuirement] (
    [RecuirementId]          INT            IDENTITY (1, 1) NOT NULL,
    [RecuirementCode]        INT            NULL,
    [RecuirementPosition]    NVARCHAR (MAX) NULL,
    [RecuirementDescription] NVARCHAR (MAX) NULL,
    [RecuirementDate]        DATETIME       NULL,
    [EmployeeId]             INT            NULL,
    [HRManagerId]            INT            NULL,
    CONSTRAINT [PK_TB_Recuirement] PRIMARY KEY CLUSTERED ([RecuirementId] ASC),
    CONSTRAINT [FK_TB_Recuirement_TB_EmployeeDetails] FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[TB_EmployeeDetails] ([EmployeeID]),
    CONSTRAINT [FK_TB_Recuirement_TB_HRManagerDetails] FOREIGN KEY ([HRManagerId]) REFERENCES [dbo].[TB_HRManagerDetails] ([HRId])
);

