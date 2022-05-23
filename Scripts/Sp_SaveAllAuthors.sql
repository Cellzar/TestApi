USE [Test]
GO
/****** Object:  StoredProcedure [dbo].[Sp_SaveAllAuthors]    Script Date: 22/05/2022 9:36:26 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[Sp_SaveAllAuthors] (@tbl Author_type readonly )
AS
BEGIN
	INSERT INTO Author(Id, IdBook, FirstName, LastName, CreationDate)
	SELECT Id, IdBook, FirstName, LastName, getdate() FROM @tbl
END
