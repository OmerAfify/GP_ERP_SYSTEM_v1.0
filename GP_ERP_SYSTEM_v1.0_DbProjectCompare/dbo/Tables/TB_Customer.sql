CREATE TABLE [dbo].[TB_Customer] (
    [CustomerId] INT            IDENTITY (1, 1) NOT NULL,
    [FullName]   NVARCHAR (MAX) NOT NULL,
    [Email]      NVARCHAR (MAX) NULL,
    [Phone]      DECIMAL (18)   NULL,
    [Address]    NVARCHAR (MAX) NULL,
    [Sex]        BIT            NULL,
    [Age]        DECIMAL (18)   NULL,
    [Image]      IMAGE          NULL,
    CONSTRAINT [PK__TB_Custo__A4AE64D85267C97A] PRIMARY KEY CLUSTERED ([CustomerId] ASC)
);

