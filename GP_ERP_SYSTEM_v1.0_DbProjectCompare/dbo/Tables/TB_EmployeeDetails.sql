CREATE TABLE [dbo].[TB_EmployeeDetails] (
    [EmployeeID]       INT             IDENTITY (1, 1) NOT NULL,
    [EmployeeFullName] NVARCHAR (MAX)  NULL,
    [TaxWithholding]   DECIMAL (18, 2) NULL,
    [HoursWorked]      INT             NULL,
    [PhotoFileName]    NVARCHAR (MAX)  NULL,
    [DateOfJoining]    DATETIME        NULL,
    [HRManagerId]      INT             NOT NULL,
    [AttendenceTime]   DATETIME        NULL,
    [Holidays]         DATE            NULL,
    [EmployeeSalary]   DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_TB_EmployeeDetails] PRIMARY KEY CLUSTERED ([EmployeeID] ASC),
    CONSTRAINT [FK_TB_EmployeeDetails_TB_HRManagerDetails] FOREIGN KEY ([HRManagerId]) REFERENCES [dbo].[TB_HRManagerDetails] ([HRId])
);



