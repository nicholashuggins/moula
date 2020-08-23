USE [Moula]
GO

SET ANSI_PADDING ON
GO
DROP INDEX IF EXISTS idxCustormerId ON PaymentRequests
GO

/****** Object:  Index [idxCustormerId]    Script Date: 23/08/2020 6:17:32 PM ******/
CREATE NONCLUSTERED INDEX [idxCustormerId] ON [dbo].[PaymentRequests]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


