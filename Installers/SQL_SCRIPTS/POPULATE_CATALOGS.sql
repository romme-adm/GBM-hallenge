
/*Operations types*/
DECLARE  @buyOp varchar(3)='BUY'
DECLARE  @sellOp varchar(4)='SELL'
DECLARE  @buyId int=1
DECLARE  @sellId int=2
/*stocks*/
DECLARE  @aapl varchar(4)='AAPL'
DECLARE  @nftx varchar(4)='NFTX'
DECLARE  @aaplId int=1
DECLARE  @nftxId int=2

/*Operations types*/
IF NOT EXISTS(SELECT fiOperationId FROM dbo.OperationTypes WITH(NOLOCK) WHERE fcName=@buyOp or fiOperationId=@buyId)
BEGIN
	INSERT INTO  dbo.OperationTypes(fiOperationId,fcName)VALUES(@buyId,@buyOp)
END
IF NOT EXISTS(SELECT fiOperationId FROM dbo.OperationTypes WITH(NOLOCK) WHERE fcName=@sellOp or fiOperationId=@sellId)
BEGIN
	INSERT INTO  dbo.OperationTypes(fiOperationId,fcName)VALUES(@sellId,@sellOp)
END
/*Initial stock*/
IF NOT EXISTS(SELECT fiStockId FROM dbo.Stock WITH(NOLOCK) WHERE fcName=@aapl or fiStockId=@aaplId)
BEGIN
	INSERT INTO  dbo.Stock(fiStockId,fcName,fdCreatedAt,fbStatus)VALUES(@aaplId,@aapl,GETDATE(),1)
END
IF NOT EXISTS(SELECT fiStockId FROM dbo.Stock WITH(NOLOCK) WHERE fcName=@nftx or fiStockId=@nftxId)
BEGIN
	INSERT INTO  dbo.Stock(fiStockId,fcName,fdCreatedAt,fbStatus)VALUES(@nftxId,@nftx,GETDATE(),1)
END

/*Set initial warehouse*/
DECLARE @stockMax int = 100
DECLARE @counter int=0
DECLARE @maxId int =0 
/*AAPL STOCK*/
WHILE @counter <= @stockMax
BEGIN
    SET @maxId= (SELECT ISNULL(MAX(fiWareHouseId),0) FROM dbo.WareHouseStock WITH(NOLOCK))+1
	INSERT INTO dbo.WareHouseStock(fiWareHouseId,fiStockId,fdCreatedAt,fiStatus)VALUES(@maxId,@aaplId,GETDATE(),0)
	SET @counter=@counter+1;
END
SET @counter=0
/*NFTX STOCK*/
WHILE @counter <= @stockMax
BEGIN
    SET @maxId= (SELECT ISNULL(MAX(fiWareHouseId),0) FROM dbo.WareHouseStock WITH(NOLOCK))+1
	INSERT INTO dbo.WareHouseStock(fiWareHouseId,fiStockId,fdCreatedAt,fiStatus)VALUES(@maxId,@nftxId,GETDATE(),0)
	SET @counter=@counter+1;
END
