CREATE TABLE [dbo].[TB_Supplier] (
    [supplierId]                 INT            IDENTITY (1, 1) NOT NULL,
    [supplierName]               NVARCHAR (MAX) NOT NULL,
    [supplierDescription]        NVARCHAR (MAX) NULL,
    [phoneNumber]                NVARCHAR (50)  NOT NULL,
    [email]                      NVARCHAR (50)  NOT NULL,
    [address]                    NVARCHAR (MAX) NULL,
    [AdverageDeliveryTimeInDays] INT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_TB_Supplier_1] PRIMARY KEY CLUSTERED ([supplierId] ASC)
);



