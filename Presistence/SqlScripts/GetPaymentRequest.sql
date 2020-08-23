USE [Moula]
GO
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetPaymentRequest')
DROP PROCEDURE GetPaymentRequest
GO


/****** Object:  StoredProcedure [dbo].[GetPaymentRequest]    Script Date: 22/08/2020 6:11:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE	[dbo].[GetPaymentRequest]
@Id UniqueIdentifier
AS
BEGIN
	SELECT a.Id, CustomerId, CreatedOn, Amount, c.Description as RequestStatus, b.Description as StatusReason, Reference,RequestStatusId,StatusReasonId 
	FROM PaymentRequests a
	INNER JOIN StatusReasons b ON a.StatusReasonId = b.Id
	INNER JOIN RequestStatuses c ON a.RequestStatusId = c.Id
	WHERE a.Id = @Id
END  
GO


