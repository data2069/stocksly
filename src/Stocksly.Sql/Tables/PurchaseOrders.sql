CREATE TABLE [dbo].[PurchaseOrders] (
    [Id]                   INT           IDENTITY (1, 1) NOT NULL,
    [OrderTime]            DATETIME      CONSTRAINT [DF_PurchaseOrders_OrderTime] DEFAULT (getdate()) NOT NULL,
    [OrderItems]           INT           CONSTRAINT [DF_PurchaseOrders_OrderItems] DEFAULT ((1)) NOT NULL,
    [Total]                MONEY         CONSTRAINT [DF_PurchaseOrders_Total] DEFAULT ((0)) NOT NULL,
    [SupplierId]           INT           NOT NULL,
    [SupplierName]         NVARCHAR (50) NOT NULL,
    [SupplierCreditToken]  NVARCHAR (50) NULL,
    [SupplierEmailAddress] NVARCHAR (50) NULL,
    CONSTRAINT [PK_PurchaseOrders] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PurchaseOrders_Suppliers] FOREIGN KEY ([SupplierId]) REFERENCES [dbo].[Suppliers] ([Id])
);

