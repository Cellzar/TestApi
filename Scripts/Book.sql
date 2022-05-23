USE [Test]
GO

/****** Object:  Table [dbo].[Book]    Script Date: 22/05/2022 9:36:07 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Book](
	[Id] [int] NOT NULL,
	[Title] [varchar](100) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[PageCount] [int] NOT NULL,
	[Excerpt] [varchar](max) NOT NULL,
	[PublicDate] [datetime2](7) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


