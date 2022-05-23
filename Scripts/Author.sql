USE [Test]
GO

/****** Object:  Table [dbo].[Author]    Script Date: 22/05/2022 9:35:25 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Author](
	[Id] [int] NOT NULL,
	[IdBook] [int] NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[CreationDate] [datetime] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Author] ADD  CONSTRAINT [DF_Autor_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO


