USE GBMChallenge;  
GO

DROP PROCEDURE IF EXISTS dbo.sp_GetInvesmentBy;  
GO  

CREATE PROCEDURE dbo.sp_GetInvesmentBy
(
	@pcOwnerEmail varchar(100),
	@piInvesmentId int
)
AS
SET NOCOUNT ON;
SELECT  fiInvesmentId ,
		fcOwnerEmail,
		fcCountryKey,
		fdcCash ,
		fdCreatedAt
		FROM dbo.Investment WITH(NOLOCK) WHERE fiInvesmentId = @piInvesmentId OR fcOwnerEmail =@pcOwnerEmail
GO  