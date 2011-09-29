-- ======================================================= 
-- Drop stored procedure if it already exists 
-- ======================================================= 

IF EXISTS ( SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
			WHERE SPECIFIC_SCHEMA = N'dbo' 
			AND SPECIFIC_NAME = N'usp_DirectOnTime_Audit_SaveProcessStep' ) 
			BEGIN
				DROP PROCEDURE dbo.usp_DirectOnTime_Audit_SaveProcessStep
			END
GO 

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ======================================================= 
-- Create Stored Procedure 
-- ======================================================= 
CREATE PROCEDURE [dbo].[usp_DirectOnTime_Audit_SaveProcessStep]
	@correlationId 	UNIQUEIDENTIFIER,
	@messageId		UNIQUEIDENTIFIER,
	@messageType	VARCHAR(15),
	@stepName		VARCHAR(25),
	@stepStatus		VARCHAR(10),
	@stepStartTime	VARCHAR(50)
AS
/*******************************************************************
* PROCEDURE: usp_DirectOnTime_Audit_SaveProcessStep
* PURPOSE:	 Save the audit process step in to the database.
* CREATED:	 pgskr {Shibu K. Raj}
* MODIFIED 
* DATE			AUTHOR			DESCRIPTION
*-------------------------------------------------------------------
* 09/29/2011	pgskr			Original creation of procedure
*******************************************************************/

SET NOCOUNT ON 

BEGIN TRY
	
	INSERT INTO  PROCESSSTEPS 
		( CorrelationId
		, MessageId
		, MessageType
		, StepName
		, StepStatus
		, StepStartTime
		)
	VALUES
		( @correlationId
		, @messageId
		, @messageType
		, @stepName
		, @stepStatus
		, convert(datetime, @stepStartTime, 121)
		)
		
END TRY 
BEGIN CATCH
	IF XACT_STATE() <> 0
	BEGIN
		ROLLBACK TRANSACTION
	END
	DECLARE @ErrMsg AS VARCHAR(4000)
	SELECT @ErrMsg ='usp_DirectOnTime_Audit_SaveProcessStep threw exception: ' + ERROR_MESSAGE()
	RAISERROR(@ErrMsg, 16, 1)
END CATCH
