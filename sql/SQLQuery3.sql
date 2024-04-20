USE messengerApp
GO

/****** Object:  Table [dbo].[Messages]    Script Date: 15.03.2023 19:23:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[id_message] [int] IDENTITY(1,1) NOT NULL,
	[subject] [varchar](100) NOT NULL,
	[message] [text] NOT NULL,
	[sent_at] [datetime] NOT NULL,
	[id_user] [int] NOT NULL,
	[receiver] [nvarchar](50) NULL,
	[is_deleted] [int] NULL,
	[is_received] [int] NULL,
	PRIMARY KEY CLUSTERED 
	(
		[id_message] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[User_Messages]    Script Date: 15.03.2023 19:23:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Messages](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[message_id] [int] NOT NULL,
	PRIMARY KEY CLUSTERED 
	(
		[id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 15.03.2023 19:23:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[id_user] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](50) NOT NULL,
	[passw] [nvarchar](255) NOT NULL,
	PRIMARY KEY CLUSTERED 
	(
		[id_user] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Messages] ADD  DEFAULT ((0)) FOR [is_deleted]
GO
ALTER TABLE [dbo].[Messages] ADD  DEFAULT ((0)) FOR [is_received]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD FOREIGN KEY([id_user])
REFERENCES [dbo].[Users] ([id_user])
GO
ALTER TABLE [dbo].[User_Messages]  WITH CHECK ADD FOREIGN KEY([message_id])
REFERENCES [dbo].[Messages] ([id_message])
GO
ALTER TABLE [dbo].[User_Messages]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([id_user])
GO


