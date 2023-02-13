CREATE TABLE [dbo].[TB_Distributor] (
    [distributorId]   INT            IDENTITY (1, 1) NOT NULL,
    [distributorName] NVARCHAR (MAX) NULL,
    [phoneNumber]     NVARCHAR (50)  NOT NULL,
    [email]           NVARCHAR (50)  NOT NULL,
    [address]         NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_TB_Distributor] PRIMARY KEY CLUSTERED ([distributorId] ASC)
);

