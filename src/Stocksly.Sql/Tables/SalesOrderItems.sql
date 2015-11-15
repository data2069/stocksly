CREATE TABLE [dbo].[SalesOrderItems] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [SalesOrderId]       INT           NOT NULL,
    [ProductId]          INT           NOT NULL,
    [ProductCode]        NVARCHAR (50) NOT NULL,
    [ProductDisplayName] NVARCHAR (50) NOT NULL,
    [CategoryId]         INT           NOT NULL,
    [Quantity]           INT           CONSTRAINT [DF_SalesOrderItems_Quantity] DEFAULT ((1)) NOT NULL,
    [UnitPrice]          MONEY         CONSTRAINT [DF_SalesOrderItems_UnitPrice] DEFAULT ((0)) NOT NULL,
    [Discount]           MONEY         CONSTRAINT [DF_SalesOrderItems_Discount] DEFAULT ((0)) NOT NULL,
    [Total]              MONEY         CONSTRAINT [DF_SalesOrderItems_Total] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_SalesOrderItems] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SalesOrderItems_Categories] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([Id]),
    CONSTRAINT [FK_SalesOrderItems_Products] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([Id]),
    CONSTRAINT [FK_SalesOrderItems_SalesOrders] FOREIGN KEY ([SalesOrderId]) REFERENCES [dbo].[SalesOrders] ([Id])
);

