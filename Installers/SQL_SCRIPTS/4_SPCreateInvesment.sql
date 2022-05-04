USE GBMChallenge;  
GO

DROP PROCEDURE IF EXISTS dbo.sp_CreateInvesment;  
GO  

CREATE PROCEDURE dbo.sp_CreateInvesment
(
	@pcOwnerEmail varchar(100),
	@piCash decimal(6,2),
	@pcCountryKey varchar(5)
)
AS
DECLARE @maxId int;
SET NOCOUNT ON;
SET @maxId= (SELECT ISNULL(MAX(fiInvesmentId),0) FROM dbo.Investment WITH(NOLOCK))+1
IF NOT EXISTS(SELECT fiInvesmentId FROM dbo.Investment WITH(NOLOCK) WHERE fiInvesmentId = @maxId OR fcOwnerEmail =@pcOwnerEmail)
BEGIN
	INSERT INTO dbo.Investment (fiInvesmentId,fcOwnerEmail,fcCountryKey,fdcCash,fdCreatedAt,fbStatus)VALUES(@maxId,@pcOwnerEmail,@pcCountryKey,@piCash,GETDATE(),1)
END
SELECT @maxId as investmentId;
GO  

