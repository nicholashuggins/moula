USE [Moula]
GO
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetPaymentRequests')
DROP PROCEDURE GetPaymentRequests
GO
/****** Object:  StoredProcedure [dbo].[GetPaymentRequests]    Script Date: 22/08/2020 6:12:53 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetPaymentRequests]
	@custId varchar(20)
AS
BEGIN
	SELECT a.Id, CustomerId, CreatedOn, Amount, c.Description as RequestStatus, b.Description as StatusReason, Reference,RequestStatusId, StatusReasonId
	FROM PaymentRequests a
	INNER JOIN StatusReasons b ON a.StatusReasonId = b.Id
	INNER JOIN RequestStatuses c ON a.RequestStatusId = c.Id
	WHERE a.CustomerId = @custId
END  
GO


