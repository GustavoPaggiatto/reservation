USE [master]
GO
/****** Object:  Database [Reservation]    Script Date: 27/01/2021 17:33:45 ******/
CREATE DATABASE [Reservation]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Reservation', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.PROJECTS\MSSQL\DATA\Reservation.mdf' , SIZE = 4288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Reservation_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.PROJECTS\MSSQL\DATA\Reservation_log.ldf' , SIZE = 1072KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Reservation] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Reservation].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Reservation] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Reservation] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Reservation] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Reservation] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Reservation] SET ARITHABORT OFF 
GO
ALTER DATABASE [Reservation] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Reservation] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Reservation] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Reservation] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Reservation] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Reservation] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Reservation] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Reservation] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Reservation] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Reservation] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Reservation] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Reservation] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Reservation] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Reservation] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Reservation] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Reservation] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Reservation] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Reservation] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Reservation] SET  MULTI_USER 
GO
ALTER DATABASE [Reservation] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Reservation] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Reservation] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Reservation] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Reservation] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Reservation]
GO
/****** Object:  UserDefinedFunction [dbo].[split]    Script Date: 27/01/2021 17:33:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[split] ( @stringToSplit VARCHAR(MAX) )
RETURNS
 @returnList TABLE ([Name] [nvarchar] (500))
AS
BEGIN
	 DECLARE @name NVARCHAR(255)
	 DECLARE @pos INT

	 WHILE CHARINDEX(',', @stringToSplit) > 0
	 BEGIN
		  SELECT @pos  = CHARINDEX(',', @stringToSplit)  
		  SELECT @name = SUBSTRING(@stringToSplit, 1, @pos-1)

		  INSERT INTO @returnList 
		  SELECT @name

		  SELECT @stringToSplit = SUBSTRING(@stringToSplit, @pos+1, LEN(@stringToSplit)-@pos)
	 END

	 INSERT INTO @returnList
	 SELECT @stringToSplit

	 RETURN
END

GO
/****** Object:  Table [dbo].[Contact]    Script Date: 27/01/2021 17:33:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Contact](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](200) NOT NULL,
	[birthDate] [date] NOT NULL,
	[contactTypeId] [int] NOT NULL,
	[phone] [varchar](30) NULL,
	[logo] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ContactType]    Script Date: 27/01/2021 17:33:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ContactType](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[description] [varchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Reserve]    Script Date: 27/01/2021 17:33:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Reserve](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[schedule] [datetime] NOT NULL,
	[description] [varchar](max) NOT NULL,
	[contactId] [int] NOT NULL,
	[ranking] [int] NOT NULL DEFAULT ((0)),
	[favorite] [bit] NOT NULL DEFAULT ((0)),
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Contact]  WITH CHECK ADD  CONSTRAINT [fk_contact_contactType_id] FOREIGN KEY([contactTypeId])
REFERENCES [dbo].[ContactType] ([id])
GO
ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [fk_contact_contactType_id]
GO
ALTER TABLE [dbo].[Reserve]  WITH CHECK ADD  CONSTRAINT [fk_reserve_contact_id] FOREIGN KEY([contactId])
REFERENCES [dbo].[Contact] ([id])
GO
ALTER TABLE [dbo].[Reserve] CHECK CONSTRAINT [fk_reserve_contact_id]
GO
/****** Object:  StoredProcedure [dbo].[sp_get_contacts_by_reserveIds]    Script Date: 27/01/2021 17:33:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_get_contacts_by_reserveIds]
(
	@contactIds varchar(max)
) as
begin
	select
		convert(int,s.Name) [contactId]
	into
		#temp
	from
		dbo.split(@contactIds) s	

	select
		c.*
	from
		Contact c
			join #temp t on t.contactId=c.id
end
GO
USE [master]
GO
ALTER DATABASE [Reservation] SET  READ_WRITE 
GO

use Reservation
go

insert into ContactType values('people')
insert into ContactType values('company')