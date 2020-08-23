USE [Moula]
GO

/****** Object:  StoredProcedure [dbo].[GetPaymentRequestByReference]    Script Date: 23/08/2020 8:19:11 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetPaymentRequestByReference')
DROP PROCEDURE GetPaymentRequestByReference
GO

CREATE PROCEDURE	[dbo].[GetPaymentRequestByReference]
@reference varchar(50)
AS
BEGIN
	SELECT a.Id, CustomerId, CreatedOn, Amount, c.Description as RequestStatus, b.Description as StatusReason, Reference,RequestStatusId,StatusReasonId 
	FROM PaymentRequests a
	INNER JOIN StatusReasons b ON a.StatusReasonId = b.Id
	INNER JOIN RequestStatuses c ON a.RequestStatusId = c.Id
	WHERE a.Reference = @reference
END  
GO


