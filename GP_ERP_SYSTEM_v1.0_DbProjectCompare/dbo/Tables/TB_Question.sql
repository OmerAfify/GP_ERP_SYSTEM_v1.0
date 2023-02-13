CREATE TABLE [dbo].[TB_Question] (
    [QuestionId] INT            IDENTITY (1, 1) NOT NULL,
    [Question]   NVARCHAR (MAX) NULL,
    CONSTRAINT [PK__TB_Quest__0DC06FAC6B4573AC] PRIMARY KEY CLUSTERED ([QuestionId] ASC)
);

