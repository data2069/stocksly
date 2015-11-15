﻿CREATE TABLE [dbo].[Customers] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (50) NOT NULL,
    [EmailAddress] NVARCHAR (50) NULL,
    [Mobile]       NVARCHAR (50) NULL,
    [Street]       NVARCHAR (50) NULL,
    [City]         NVARCHAR (50) NULL,
    [State]        NVARCHAR (50) NULL,
    [CountryId]    INT           NULL,
    [Country]      NVARCHAR (50) NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Customers_Countries] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Countries] ([Id])
);
