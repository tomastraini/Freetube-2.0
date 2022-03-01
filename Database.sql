USE [master]
GO

/****** Object:  Database [freetubeC]    Script Date: 28/02/2022 22:01:28 ******/
CREATE DATABASE [freetubeC]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'freetubeC', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\freetubeC.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'freetubeC_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\freetubeC_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [freetubeC].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [freetubeC] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [freetubeC] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [freetubeC] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [freetubeC] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [freetubeC] SET ARITHABORT OFF 
GO

ALTER DATABASE [freetubeC] SET AUTO_CLOSE ON 
GO

ALTER DATABASE [freetubeC] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [freetubeC] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [freetubeC] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [freetubeC] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [freetubeC] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [freetubeC] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [freetubeC] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [freetubeC] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [freetubeC] SET  ENABLE_BROKER 
GO

ALTER DATABASE [freetubeC] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [freetubeC] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [freetubeC] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [freetubeC] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [freetubeC] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [freetubeC] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [freetubeC] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [freetubeC] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [freetubeC] SET  MULTI_USER 
GO

ALTER DATABASE [freetubeC] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [freetubeC] SET DB_CHAINING OFF 
GO

ALTER DATABASE [freetubeC] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [freetubeC] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [freetubeC] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [freetubeC] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [freetubeC] SET QUERY_STORE = OFF
GO

ALTER DATABASE [freetubeC] SET  READ_WRITE 
GO


