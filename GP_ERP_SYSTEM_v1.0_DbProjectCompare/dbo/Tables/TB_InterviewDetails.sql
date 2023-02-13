CREATE TABLE [dbo].[TB_InterviewDetails] (
    [InterviewId]     INT      IDENTITY (1, 1) NOT NULL,
    [InterviewDate]   DATETIME NULL,
    [InterviewResult] BIT      NULL,
    [RecuriementId]   INT      NULL,
    CONSTRAINT [PK_TB_InterviewDetails] PRIMARY KEY CLUSTERED ([InterviewId] ASC),
    CONSTRAINT [FK_TB_InterviewDetails_TB_Recuirement] FOREIGN KEY ([RecuriementId]) REFERENCES [dbo].[TB_Recuirement] ([RecuirementId])
);

