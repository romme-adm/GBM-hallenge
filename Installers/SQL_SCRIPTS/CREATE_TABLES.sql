USE GBMChallenge;
GO
IF OBJECT_ID ('dbo.Stock', 'U') IS NULL 
BEGIN
	CREATE TABLE dbo.Stock
	(
		fiStockId int,
		fcName varchar(20),
		fdCreatedAt datetime,
		fbStatus bit,
		PRIMARY KEY (fiStockId)
	)
END

IF OBJECT_ID('dbo.WareHouseStock','U') IS NULL
BEGIN
	CREATE TABLE dbo.WareHouseStock
	(
		fiWareHouseId int,
	    fiStockId int,
		fdCreatedAt datetime,
		fiStatus bit,
		PRIMARY KEY (fiWareHouseId)
	)
END

IF OBJECT_ID ('dbo.Investment', 'U') IS NULL 
BEGIN
	CREATE TABLE dbo.Investment
	(
		fiInvesmentId int,
		fcOwnerEmail varchar(100),
		fcCountryKey varchar(5),
		fdcCash decimal(6,2),
		fdCreatedAt datetime,
		fbStatus bit,
		PRIMARY KEY (fiInvesmentId)
	)
END

IF OBJECT_ID ('dbo.StockOperations', 'U') IS NULL 
BEGIN
	CREATE TABLE dbo.StockOperations
	(
		fiOperationId int,
		fdCreatedAt datetime,
		fiStockId int,
		fdcAmount decimal(6,2),
		fiOperationTypeId int,
		PRIMARY KEY (fiOperationId)
	)
END

IF OBJECT_ID ('dbo.OperationTypes', 'U') IS NULL 
BEGIN
	CREATE TABLE dbo.OperationTypes
	(
		fiOperationId int,
		fcName varchar(50),
		PRIMARY KEY (fiOperationId)
	)
END

GO