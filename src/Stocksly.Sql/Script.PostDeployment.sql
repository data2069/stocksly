/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

/* 
Pushing default data.
*/
print 'Pushing default seed data.';

set identity_insert dbo.Categories on;
insert into dbo.Categories (Id, Name) values
(1, 'Coffee'),
(2, 'Tea'),
(3, 'Supplimentary');
set identity_insert dbo.Categories off;

set identity_insert dbo.Countries on;
insert into dbo.Countries (Id, Code, ShortName, DisplayName, LanguagePreference) values
(1,'+94', 'LKA', 'Sri Lanka', 'en-us');
set identity_insert dbo.Countries off;

set identity_insert dbo.Suppliers off;
insert into dbo.Suppliers (Id, Name, EmailAddress, Telephone) values
(1, 'Ceylone Tobaco', 'sigars@tobaco.lk', '77944171'), 
(2, 'Neliya Cottah', 'express@cottah.lk', '11509813'),
(3, 'Thissa Veda Gedara', 'thissahami@gmail.com', '342339592');
set identity_insert dbo.Suppliers on;

set identity_insert dbo.Customers off;
insert into dbo.Customers(Id, Name, Mobile, EmailAddress, PostalAddress_Street, PostalAddress_CountryId, PostalAddress_CountryShortName) values
(1, 'Slash KP', '778112942', 'kp@gmail.com', 'Kuthulvatta', 1, 'LKA');
set identity_insert dbo.Products on;

set identity_insert dbo.Products off;
insert into dbo.Products (Id, Code, DisplayName, ReorderLevel, UnitPrice, CategoryId, SupplierId) values
(1, 'COF87992', 'Arabic Blended Coffee Beans', 40, 1500, 1, 2), 
(2, 'TEA19284', 'Neliya Black Tea', 50, 1350, 2, 1), 
(3, 'HNY08237', 'Kitul Peni', 15, 2730, 3, 3);
set identity_insert dbo.Products on;
