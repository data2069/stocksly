CREATE TABLE [dbo].[PurchaseOrderItems] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [PurchaseOrderId]    INT           NOT NULL,
    [ProductId]          INT           NOT NULL,
    [ProductCode]        NVARCHAR (50) NOT NULL,
    [ProductDisplayName] NVARCHAR (50) NOT NULL,
    [CategoryId]         INT           NOT NULL,
    [Quantity]           INT           CONSTRAINT [DF_PurchaseOrderItems_Quantity] DEFAULT ((1)) NOT NULL,
    [UnitPrice]          MONEY         CONSTRAINT [DF_PurchaseOrderItems_UnitPrice] DEFAULT ((0)) NOT NULL,
    [Total]              MONEY         CONSTRAINT [DF_PurchaseOrderItems_Total] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PurchaseOrderItems] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PurchaseOrderItems_Categories] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([Id]),
    CONSTRAINT [FK_PurchaseOrderItems_Products] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([Id]),
    CONSTRAINT [FK_PurchaseOrderItems_PurchaseOrders] FOREIGN KEY ([Id]) REFERENCES [dbo].[PurchaseOrders] ([Id])
);

