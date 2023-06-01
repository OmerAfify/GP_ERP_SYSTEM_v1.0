CREATE TABLE [dbo].[TB_EmployeeTrainning] (
    [TrainnningId]         INT            IDENTITY (1, 1) NOT NULL,
    [TrainningType]        NVARCHAR (MAX) NULL,
    [TrainningDescription] NVARCHAR (MAX) NULL,
    [EmployeeId]           INT            NOT NULL,
    [HRMangerId]           INT            NOT NULL,
    CONSTRAINT [PK_TB_EmployeeTrainning] PRIMARY KEY CLUSTERED ([TrainnningId] ASC),
    CONSTRAINT [FK_TB_EmployeeTrainning_TB_EmployeeDetails] FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[TB_EmployeeDetails] ([EmployeeID]),
    CONSTRAINT [FK_TB_EmployeeTrainning_TB_HRManagerDetails] FOREIGN KEY ([HRMangerId]) REFERENCES [dbo].[TB_HRManagerDetails] ([HRId])
);

