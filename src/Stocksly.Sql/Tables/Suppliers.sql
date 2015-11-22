CREATE TABLE [dbo].[Suppliers] (
    [Id]                             INT           IDENTITY (1, 1) NOT NULL,
    [Name]                           NVARCHAR (50) NOT NULL,
    [EmailAddress]                   NVARCHAR (50) NULL,
    [Telephone]                      NVARCHAR (50) NULL,
    [PostalAddress_Street]           NVARCHAR (50) NULL,
    [PostalAddress_City]             NVARCHAR (50) NULL,
    [PostalAddress_State]            NVARCHAR (50) NULL,
    [PostalAddress_CountryId]        INT           NULL,
    [PostalAddress_CountryShortName] NVARCHAR (50) NULL,
    [CreditToken]                    NVARCHAR (50) NULL,
    CONSTRAINT [PK_Suppliers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Suppliers_Countries] FOREIGN KEY ([PostalAddress_CountryId]) REFERENCES [dbo].[Countries] ([Id])
);



