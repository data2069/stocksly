CREATE TABLE [dbo].[SalesOrders] (
    [Id]                   INT           IDENTITY (1, 1) NOT NULL,
    [OrderTime]            DATETIME      CONSTRAINT [DF_SalesOrders_OrderTime] DEFAULT (getdate()) NOT NULL,
    [OrderItems]           INT           CONSTRAINT [DF_SalesOrders_OrderItems] DEFAULT ((1)) NOT NULL,
    [Discount]             MONEY         CONSTRAINT [DF_SalesOrders_Discount] DEFAULT ((0)) NOT NULL,
    [Total]                MONEY         CONSTRAINT [DF_SalesOrders_Total] DEFAULT ((0)) NOT NULL,
    [CustomerId]           INT           NOT NULL,
    [CustomerName]         NVARCHAR (50) NOT NULL,
    [CustomerEmailAddress] NVARCHAR (50) NULL,
    [CustomerMobile]       NVARCHAR (50) NULL,
    CONSTRAINT [PK_SalesOrders] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SalesOrders_Customers] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers] ([Id])
);

