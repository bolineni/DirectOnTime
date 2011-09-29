-- ======================================================= 
-- Drop stored procedure if it already exists 
-- ======================================================= 

IF EXISTS ( SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
			WHERE SPECIFIC_SCHEMA = N'dbo' 
			AND SPECIFIC_NAME = N'usp_DirectOnTime_Audit_SaveProcessHeaders' ) 
			BEGIN
				DROP PROCEDURE dbo.usp_DirectOnTime_Audit_SaveProcessHeaders
			END
GO 

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ======================================================= 
-- Create Stored Procedure 
-- ======================================================= 
CREATE PROCEDURE [dbo].[usp_DirectOnTime_Audit_SaveProcessHeaders]
	@processType	VARCHAR(15),
	@correlationId 	UNIQUEIDENTIFIER,
	@messageId	 	UNIQUEIDENTIFIER,
	@businessUnit	CHAR(4),
	@userName		CHAR(5),
	@requestTime	VARCHAR(50),
	@receiptId		VARCHAR(15),
	@clientId		VARCHAR(15)
AS
/*******************************************************************
* PROCEDURE: usp_DirectOnTime_Audit_SaveProcessHeaders
* PURPOSE:	 Save the audit process step in to the database.
* CREATED:	 pgskr {Shibu K. Raj}
* MODIFIED 
* DATE			AUTHOR			DESCRIPTION
*-------------------------------------------------------------------
* 09/29/2011	pgskr			Original creation of procedure
*******************************************************************/

SET NOCOUNT ON 

BEGIN TRY
	
	INSERT INTO  PROCESSHEADERS 
		( ProcessType
		, CorrelationId
		, MessageId		
		, BusinessUnit
		, UserName
		, RequestTime
		, ReceiptId
		, ClientId
		)
	VALUES
		( @processType
		, @correlationId
		, @messageId
		, @businessUnit
		, @userName
		, convert(datetime, @requestTime, 121)
		, @receiptId
		, @clientId
		)
		
END TRY 
BEGIN CATCH
	IF XACT_STATE() <> 0
	BEGIN
		ROLLBACK TRANSACTION
	END
	DECLARE @ErrMsg AS VARCHAR(4000)
	SELECT @ErrMsg ='usp_DirectOnTime_Audit_SaveProcessHeaders threw exception: ' + ERROR_MESSAGE()
	RAISERROR(@ErrMsg, 16, 1)
END CATCH
