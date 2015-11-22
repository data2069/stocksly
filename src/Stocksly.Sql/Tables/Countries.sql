CREATE TABLE [dbo].[Countries] (
    [Id]                 INT           NOT NULL,
    [Code]               NVARCHAR (50) NULL,
    [ShortName]          NCHAR (10)    NULL,
    [DisplayName]        NVARCHAR (50) NULL,
    [TimeZone]           NVARCHAR (50) NULL,
    [CurrencyFormat]     NVARCHAR (50) NULL,
    [DateTimeFormat]     NVARCHAR (50) NULL,
    [LanguagePreference] NVARCHAR (50) NULL,
    CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED ([Id] ASC)
);



