IF DB_ID('Warehouse') IS NULL
    create database Warehouse;
GO	
use Warehouse
--Deleting tables and views, if they exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblProduct')
	drop table tblProduct;
IF EXISTS(select * FROM sys.views where name = 'vwProduct')
	drop view vwProduct;
GO
--Creating a table of products
create table tblProduct(
ProductID int IDENTITY(1,1) PRIMARY KEY,
ProductName varchar(50) NOT NULL,
ProductKey varchar(50) NOT NULL UNIQUE,
Quantity int NOT NULL CHECK(Quantity >= 0 AND Quantity <= 100),
Price numeric(8,2) NOT NULL CHECK(Price > 0),
Stored varchar(3) NOT NULL CHECK(Stored IN('yes', 'no'))
);
--Creating a view of products
GO
create view vwProduct as
select *
from tblProduct;