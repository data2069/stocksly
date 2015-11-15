CREATE TABLE [dbo].[Countries] (
    [Id]                 INT           NOT NULL,
    [Name]               NVARCHAR (50) NULL,
    [TimeZone]           NVARCHAR (50) NULL,
    [CurrencyFormat]     NVARCHAR (50) NULL,
    [DateTimeFormat]     NVARCHAR (50) NULL,
    [LanguagePreference] NVARCHAR (50) NULL,
    CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED ([Id] ASC)
);

