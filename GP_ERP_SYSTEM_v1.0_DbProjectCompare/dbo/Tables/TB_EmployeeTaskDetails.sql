CREATE TABLE [dbo].[TB_EmployeeTaskDetails] (
    [TaskId]           INT            IDENTITY (1, 1) NOT NULL,
    [TaskDescription]  NVARCHAR (MAX) NULL,
    [TaskAssignedTime] DATETIME       NULL,
    [TaskDeadlineTime] DATETIME       NULL,
    [BounsHours]       INT            NULL,
    [EmplyeeId]        INT            NOT NULL,
    CONSTRAINT [PK_TB_EmployeeTaskDetails] PRIMARY KEY CLUSTERED ([TaskId] ASC),
    CONSTRAINT [FK_TB_EmployeeTaskDetails_TB_EmployeeDetails] FOREIGN KEY ([EmplyeeId]) REFERENCES [dbo].[TB_EmployeeDetails] ([EmployeeID])
);

