USE [Test]
GO
/****** Object:  StoredProcedure [dbo].[Sp_SaveAllBooks]    Script Date: 22/05/2022 9:36:39 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[Sp_SaveAllBooks] (@tbl Book_type readonly )
AS
BEGIN
	INSERT INTO Book (Id, Title, Description, PageCount, Excerpt, PublicDate)
	SELECT Id, Title, Description, PageCount, Excerpt, cast(PublicDate as date) FROM @tbl
END
