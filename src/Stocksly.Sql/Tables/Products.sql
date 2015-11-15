CREATE TABLE [dbo].[Products] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [Code]            NVARCHAR (50) NOT NULL,
    [DisplayName]     NVARCHAR (50) NOT NULL,
    [Barcode]         NVARCHAR (50) NULL,
    [ReorderQuantity] INT           CONSTRAINT [DF_Products_ReorderQuantity] DEFAULT ((100)) NOT NULL,
    [Stocks]          INT           CONSTRAINT [DF_Products_Stocks] DEFAULT ((100)) NOT NULL,
    [UnitPrice]       MONEY         NOT NULL,
    [SupplierId]      INT           NOT NULL,
    [CategoryId]      INT           NOT NULL,
    [Archived]        BIT           CONSTRAINT [DF_Products_Archived] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Products_Categories] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([Id]),
    CONSTRAINT [FK_Products_Suppliers] FOREIGN KEY ([SupplierId]) REFERENCES [dbo].[Suppliers] ([Id])
);

