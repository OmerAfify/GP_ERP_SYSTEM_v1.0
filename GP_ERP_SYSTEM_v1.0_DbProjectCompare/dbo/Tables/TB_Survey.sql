CREATE TABLE [dbo].[TB_Survey] (
    [SurveyId]   INT            IDENTITY (1, 1) NOT NULL,
    [SurveyName] NVARCHAR (MAX) NULL,
    [SurveyDesc] NVARCHAR (MAX) NULL,
    [CustomerId] INT            NOT NULL,
    [QuestionId] INT            NOT NULL,
    CONSTRAINT [PK__TB_Surve__A5481F7D0DEB2CDB] PRIMARY KEY CLUSTERED ([SurveyId] ASC),
    CONSTRAINT [FK__TB_Survey__Custo__48CFD27E] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[TB_Customer] ([CustomerId]),
    CONSTRAINT [FK__TB_Survey__Quest__49C3F6B7] FOREIGN KEY ([QuestionId]) REFERENCES [dbo].[TB_Question] ([QuestionId])
);



