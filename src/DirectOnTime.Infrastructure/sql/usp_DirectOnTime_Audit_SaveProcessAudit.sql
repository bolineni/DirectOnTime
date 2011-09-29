-- ======================================================= 
-- Drop stored procedure if it already exists 
-- ======================================================= 

IF EXISTS ( SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
			WHERE SPECIFIC_SCHEMA = N'dbo' 
			AND SPECIFIC_NAME = N'usp_DirectOnTime_Audit_SaveProcessAudit' ) 
			BEGIN
				DROP PROCEDURE dbo.usp_DirectOnTime_Audit_SaveProcessAudit
			END
GO 

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ======================================================= 
-- Create Stored Procedure 
-- ======================================================= 
CREATE PROCEDURE [dbo].[usp_DirectOnTime_Audit_SaveProcessAudit]
	@correlationId 	UNIQUEIDENTIFIER,
	@businessUnit	CHAR(4),
	@userName		CHAR(5),
	@requestTime	VARCHAR(50),
	@auditStatus	VARCHAR(15),
	@auditMessage	VARCHAR(250)
AS
/*******************************************************************
* PROCEDURE: usp_DirectOnTime_Audit_SaveProcessAudit
* PURPOSE:	 Save the audit process step in to the database.
* CREATED:	 pgskr {Shibu K. Raj}
* MODIFIED 
* DATE			AUTHOR			DESCRIPTION
*-------------------------------------------------------------------
* 09/29/2011	pgskr			Original creation of procedure
*******************************************************************/

SET NOCOUNT ON 

BEGIN TRY
	
	INSERT INTO  PROCESSAUDITS 
		( CorrelationId
		, BusinessUnit
		, UserName
		, RequestTime
		, AuditStatus
		, AuditMessage
		)
	VALUES
		( @correlationId
		, @businessUnit
		, @userName
		, convert(datetime, @requestTime, 121)
		, @auditStatus
		, @auditMessage
		)
		
END TRY 
BEGIN CATCH
	IF XACT_STATE() <> 0
	BEGIN
		ROLLBACK TRANSACTION
	END
	DECLARE @ErrMsg AS VARCHAR(4000)
	SELECT @ErrMsg ='usp_DirectOnTime_Audit_SaveProcessAudit threw exception: ' + ERROR_MESSAGE()
	RAISERROR(@ErrMsg, 16, 1)
END CATCH
