USE [master]
GO
/****** Object:  Database [SprReimbursementDB]    Script Date: 7/12/2023 5:05:27 PM ******/
CREATE DATABASE [SprReimbursementDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SprReimbursementDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLDEVELOPER\MSSQL\DATA\SprReimbursementDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SprReimbursementDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLDEVELOPER\MSSQL\DATA\SprReimbursementDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [SprReimbursementDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SprReimbursementDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SprReimbursementDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SprReimbursementDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SprReimbursementDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SprReimbursementDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SprReimbursementDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [SprReimbursementDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [SprReimbursementDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SprReimbursementDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SprReimbursementDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SprReimbursementDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SprReimbursementDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SprReimbursementDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SprReimbursementDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SprReimbursementDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SprReimbursementDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [SprReimbursementDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SprReimbursementDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SprReimbursementDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SprReimbursementDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SprReimbursementDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SprReimbursementDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [SprReimbursementDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SprReimbursementDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SprReimbursementDB] SET  MULTI_USER 
GO
ALTER DATABASE [SprReimbursementDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SprReimbursementDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SprReimbursementDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SprReimbursementDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SprReimbursementDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SprReimbursementDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [SprReimbursementDB] SET QUERY_STORE = OFF
GO
USE [SprReimbursementDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 7/12/2023 5:05:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReimbursementModels]    Script Date: 7/12/2023 5:05:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReimbursementModels](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeName] [nvarchar](50) NOT NULL,
	[EmployeeId] [nvarchar](max) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[Amount] [decimal](18, 2) NULL,
	[TransactionDate] [datetime2](7) NOT NULL,
	[SprEmployeeId] [int] NULL,
	[MonthlyTotal] [decimal](18, 2) NULL,
	[FolderPath] [nvarchar](max) NULL,
	[IsApproved] [bit] NOT NULL,
	[ResponseMessage] [nvarchar](255) NULL,
 CONSTRAINT [PK_ReimbursementModels] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SprEmployees]    Script Date: 7/12/2023 5:05:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SprEmployees](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_SprEmployees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230614082541_Initial Create', N'7.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230615031711_UpdateReimbursementTypeColumn', N'7.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230628115449_AddReimbursementFolderPathColumn', N'7.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230628120112_RemoveReceiptImageUrl', N'7.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230628133828_AddReimbursementFolderPathColumn', N'7.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230628135443_RenameReimbursementFolderPathToFolderPath', N'7.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230629092457_AddApprovalAndResponseColumns', N'7.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230630064855_UpdateReimbursementModel', N'7.0.8')
GO
SET IDENTITY_INSERT [dbo].[ReimbursementModels] ON 

INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (22, N'Mike Perez', N'2', N'Food', CAST(3.00 AS Decimal(18, 2)), CAST(N'2023-06-29T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\2\b14d5f7b-071b-46ad-b0ae-013ca091e678', 0, N'')
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (23, N'Laiza Zuniga', N'1', N'Medical', CAST(3.00 AS Decimal(18, 2)), CAST(N'2023-06-29T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\1\3b5a8a42-cfda-4038-b2b6-d862ee4dee1c', 0, N'')
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (24, N'Mike Perez', N'2', N'Transportation', CAST(1609.00 AS Decimal(18, 2)), CAST(N'2023-06-29T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\2\2efd6455-cf20-4b66-a632-91e865080d77', 0, N'')
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (25, N'Mike Perez', N'2', N'Food', CAST(3.00 AS Decimal(18, 2)), CAST(N'2023-06-29T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\2\08fe6ed3-bf6f-4956-b82a-4120d1119d6c', 0, N'')
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (26, N'Laiza Zuniga', N'1', N'Food', CAST(0.00 AS Decimal(18, 2)), CAST(N'2023-06-29T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\1\1897bcae-11da-4883-9d8c-09291595ab05', 0, N'')
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (27, N'Mike Perez', N'2', N'Food', CAST(3.00 AS Decimal(18, 2)), CAST(N'2023-06-29T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\2\e127ee42-e2b8-42ff-8692-f528a214c5c2', 0, N'')
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (28, N'Laiza Zuniga', N'1', N'Food', CAST(3.00 AS Decimal(18, 2)), CAST(N'2023-06-29T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\1\fd7c0573-35f1-4096-a24d-045655965573', 0, N'')
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (29, N'Mike Perez ', N'2', N'Transportation', CAST(0.00 AS Decimal(18, 2)), CAST(N'2023-06-29T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\2\da4e586d-462b-4f57-8a05-5eb880156b6c', 0, N'')
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (30, N'Mike Perez', N'2', N'Medical', CAST(1092.00 AS Decimal(18, 2)), CAST(N'2023-06-29T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\2\d7b48a29-fcbc-45f1-9160-f6842c8dd4f6', 0, N'')
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (31, N'Laiza Zuniga', N'1', N'Transportation', CAST(175.00 AS Decimal(18, 2)), CAST(N'2023-06-29T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\1\4e135142-8f43-4906-ba03-bede7d52f466', 0, N'')
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (32, N'Laiza Zuniga', N'1', N'Food', CAST(175.00 AS Decimal(18, 2)), CAST(N'2023-06-29T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\1\2e131f43-5734-48eb-b478-aba142dcc137', 0, N'')
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (33, N'Laiza Zuniga', N'1', N'Food', CAST(175.00 AS Decimal(18, 2)), CAST(N'2023-06-29T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\1\616296e9-3309-4871-a3b5-e6866824d5d8', 0, N'')
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (34, N'Mike Perez', N'2', N'Food', CAST(0.00 AS Decimal(18, 2)), CAST(N'2023-06-29T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\2\0355672c-b2b3-44dc-8970-0072e26b4409', 0, N'')
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (35, N'Laiza Zuniga', N'1', N'Food', CAST(175.00 AS Decimal(18, 2)), CAST(N'2023-06-29T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\1\5d6337d0-2df7-49aa-9a82-2f6ac7d1d929', 0, N'')
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (36, N'Mike Perez', N'2', N'Food', CAST(175.00 AS Decimal(18, 2)), CAST(N'2023-06-29T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\2\5436c38a-6d3b-4d8b-8903-7afb0ef032a2', 0, N'')
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (37, N'Laiza Zuniga', N'1', N'Food', CAST(1056.00 AS Decimal(18, 2)), CAST(N'2023-06-29T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\1\f8b250fd-d743-4d14-b349-488c5dcaa6aa', 0, N'')
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (38, N'Mike Perez ', N'2', N'Medical', CAST(1092.00 AS Decimal(18, 2)), CAST(N'2023-06-29T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\2\1691c708-5b73-4c66-a76c-e0e73d08d12c', 0, N'')
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (39, N'Mike Perez', N'2', N'Food', CAST(36.00 AS Decimal(18, 2)), CAST(N'2023-06-29T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\2\de7c2a64-7291-48c6-86b7-6d44121dca93', 0, N'Late filing or request')
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (40, N'Laiza Zuniga', N'1', N'Food', CAST(399.00 AS Decimal(18, 2)), CAST(N'2023-06-29T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\1\bc8f488b-fd4c-4d54-a496-c788e05ab52c', 1, N'')
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (41, N'Laiza Zuniga', N'1', N'Medical', CAST(1455.00 AS Decimal(18, 2)), CAST(N'2023-06-29T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\1\d706a789-9a6c-4d50-b84f-029739dd1a65', 0, N'')
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (45, N'Laiza Zuniga', N'1', N'Food', CAST(1455.00 AS Decimal(18, 2)), CAST(N'2023-06-30T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\1\6ff71f9b-b663-4792-8c63-7a32f4c7a2e4', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (46, N'Mike Perez ', N'2', N'Medical', CAST(399.00 AS Decimal(18, 2)), CAST(N'2023-06-30T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\2\da5c37dc-ef0f-4e3b-80df-a0e1c641f676', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (47, N'Laiza Zuniga', N'1', N'Food', CAST(1056.00 AS Decimal(18, 2)), CAST(N'2023-07-09T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\1\b4205334-128b-4346-ba4f-b0e40a9c876d', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (48, N'Mike Perez', N'2', N'Medical', CAST(36.00 AS Decimal(18, 2)), CAST(N'2023-07-10T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\2\46b0c220-4b13-4a63-9ff7-916fb2199188', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (49, N'Laiza Zuniga', N'1', N'Food', CAST(1056.00 AS Decimal(18, 2)), CAST(N'2023-07-10T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\1\72d2857e-6773-41f3-a088-5f108876d737', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (50, N'Mike Perez', N'2', N'Medical', CAST(399.00 AS Decimal(18, 2)), CAST(N'2023-07-10T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\2\144b1833-a910-4bd3-b195-6708bbe70455', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (51, N'Laiza Zuniga', N'1', N'Transportation', CAST(36.00 AS Decimal(18, 2)), CAST(N'2023-07-10T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\1\01ec6522-1202-4cc0-b850-ceccfc386530', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (52, N'Laiza Zuniga', N'1', N'Medical', CAST(399.00 AS Decimal(18, 2)), CAST(N'2023-07-10T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\1\7677b18d-7df0-44ef-b79d-78645b2c7dc2', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (53, N'Laiza Zuniga', N'1', N'Transportation', CAST(1056.00 AS Decimal(18, 2)), CAST(N'2023-07-10T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\1\c6240073-a258-4779-b866-03a07794d136', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (54, N'Mike Perez', N'2', N'Food', CAST(175.00 AS Decimal(18, 2)), CAST(N'2023-07-10T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\2\bace45f9-0b16-4070-8d91-bfbe9a957b7d', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (55, N'Mike Perez', N'2', N'Medical', CAST(647.00 AS Decimal(18, 2)), CAST(N'2023-07-10T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\2\c569efdc-cc2a-419e-b57e-e17d56a16e01', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (56, N'Laiza Zuniga', N'1', N'Food', CAST(399.00 AS Decimal(18, 2)), CAST(N'2023-07-10T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\1\63952915-7f27-4cb9-9df4-eebbf57651d0', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (57, N'Laiza Zuniga', N'1', N'Food', CAST(399.00 AS Decimal(18, 2)), CAST(N'2023-07-10T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\1\c4cd5a2a-54a5-4d96-91a2-b06e22edd52c', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (58, N'Mike Perez', N'2', N'Transportation', CAST(1056.00 AS Decimal(18, 2)), CAST(N'2023-07-10T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\2\0f22ecf8-cc4a-4906-91b0-7533e48ccade', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (59, N'Laiza Zuniga', N'1', N'Food', CAST(1056.00 AS Decimal(18, 2)), CAST(N'2023-07-10T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\1\e636606c-bf9e-4718-8713-e88bafce9a0a', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (60, N'Laiza Zuniga', N'1', N'Food', CAST(1056.00 AS Decimal(18, 2)), CAST(N'2023-07-10T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\1\75969740-9b81-4032-a443-c4df71c0b974', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (61, N'Laiza Zuniga', N'1', N'Food', CAST(1056.00 AS Decimal(18, 2)), CAST(N'2023-07-10T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\1\aa6f1231-b1a6-4d1e-bc63-729446dfa726', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (62, N'Laiza Zuniga', N'1', N'Medical', CAST(1056.00 AS Decimal(18, 2)), CAST(N'2023-07-10T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\1\ba990154-8d41-41a2-9584-a5137d2ef11b', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (63, N'Laiza Zuniga', N'1', N'Medical', CAST(36.00 AS Decimal(18, 2)), CAST(N'2023-07-10T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\1\4e5d0115-a952-454a-8809-0ef0c4f2461d', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (64, N'Mike Perez', N'2', N'Medical', CAST(435.00 AS Decimal(18, 2)), CAST(N'2023-07-10T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\2\11ba1715-b0aa-4299-b382-7afcd4047bb4', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (65, N'Mike Perez', N'2', N'Medical', CAST(1056.00 AS Decimal(18, 2)), CAST(N'2023-07-10T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\2\ae7888fd-ea9a-452c-aa6e-0b316174ce0f', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (66, N'Mike Perez', N'2', N'Transportation', CAST(1000.00 AS Decimal(18, 2)), CAST(N'2023-07-10T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\2\3fe4b277-c44e-4f9d-8e24-c7f001c58784', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (67, N'Mike Perez', N'2', N'Medical', CAST(1056.00 AS Decimal(18, 2)), CAST(N'2023-07-10T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\2\6a8e93d8-b32a-4f16-a8ef-615bc1a1c42d', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (68, N'Mike Perez', N'2', N'Medical', CAST(399.00 AS Decimal(18, 2)), CAST(N'2023-07-10T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\2\09165230-d37c-48d4-be38-03e548657122', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (69, N'Mike Perez', N'2', N'Medical', CAST(399.00 AS Decimal(18, 2)), CAST(N'2023-07-10T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\2\7f57811f-caab-4477-aed7-9e2cc37f4000', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (70, N'Mike Perez', N'2', N'Food', CAST(10.00 AS Decimal(18, 2)), CAST(N'2023-07-10T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\2\51206d36-dadd-4d13-afdc-2bbc18cfab10', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (71, N'Mike Perez', N'2', N'Medical', CAST(36.00 AS Decimal(18, 2)), CAST(N'2023-07-11T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\2\ee1ccef0-f52b-40b3-919b-515d6c5a51ed', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (72, N'Mike Perez', N'2', N'Medical', CAST(36.00 AS Decimal(18, 2)), CAST(N'2023-07-11T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\2\7ff51980-a654-49e0-9cdc-9b205d7ee177', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (73, N'Mike Perez', N'2', N'Food', CAST(1455.00 AS Decimal(18, 2)), CAST(N'2023-07-11T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\2\5f088e06-812a-4ede-af4d-2250464ae987', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (74, N'Mike Perez', N'2', N'Food', CAST(1066.00 AS Decimal(18, 2)), CAST(N'2023-07-11T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\2\4775d9f3-9de8-4b33-a710-6d29b04dac61', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (75, N'Laiza Zuniga', N'1', N'Medical', CAST(399.00 AS Decimal(18, 2)), CAST(N'2023-07-11T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\1\4906f486-880c-4d28-84db-7c22c20fb9bb', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (76, N'Mike Perez', N'2', N'Medical', CAST(1056.00 AS Decimal(18, 2)), CAST(N'2023-07-11T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\2\d8fcc30f-8c73-4ce4-8e9d-0736b09ce0b7', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (77, N'Laiza Zuniga', N'1', N'Medical', CAST(1056.00 AS Decimal(18, 2)), CAST(N'2023-07-11T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\1\f9379c37-0c27-44a6-a75d-3d32a53c7885', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (78, N'Laiza Zuniga', N'1', N'Transportation', CAST(1000.00 AS Decimal(18, 2)), CAST(N'2023-07-11T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\1\3dac3615-f916-4e3a-a8f8-4cb1674b9c01', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (79, N'Laiza Zuniga', N'1', N'Food', CAST(1092.00 AS Decimal(18, 2)), CAST(N'2023-07-11T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\1\f0cc7eff-2205-4654-b82a-0655810d92f6', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (80, N'Mike Perez', N'2', N'Food', CAST(1056.00 AS Decimal(18, 2)), CAST(N'2023-07-11T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\2\0ff2297d-9108-44e5-a274-62945ed4ccb4', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (81, N'Mike Perez', N'2', N'Food', CAST(10.00 AS Decimal(18, 2)), CAST(N'2023-07-11T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\2\75c3177d-6ad6-4f25-8921-d35ff6f5d112', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (82, N'Laiza Zuniga', N'1', N'Food', CAST(1056.00 AS Decimal(18, 2)), CAST(N'2023-07-11T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\1\9e504ff0-7f5d-44ff-a5e6-03bf0a98fbf1', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (83, N'Mike Perez', N'2', N'Transportation', CAST(1000.00 AS Decimal(18, 2)), CAST(N'2023-07-11T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\2\8e05eb29-f618-435f-8e9a-f5e974451299', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (84, N'Mike Perez', N'2', N'Food', CAST(36.00 AS Decimal(18, 2)), CAST(N'2023-07-11T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\2\98c84fc2-ad2c-45f0-ae5c-de01ddb5c637', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (85, N'Mike Perez', N'2', N'Food', CAST(36.00 AS Decimal(18, 2)), CAST(N'2023-07-12T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\2\dbadc476-30a8-4119-a963-9d199b12f848', 0, NULL)
INSERT [dbo].[ReimbursementModels] ([Id], [EmployeeName], [EmployeeId], [Type], [Amount], [TransactionDate], [SprEmployeeId], [MonthlyTotal], [FolderPath], [IsApproved], [ResponseMessage]) VALUES (86, N'Laiza Zuniga', N'1', N'Transportation', CAST(1000.00 AS Decimal(18, 2)), CAST(N'2023-07-12T00:00:00.0000000' AS DateTime2), NULL, NULL, N'ReimbursementRequests\1\1d9e9aa2-3ab6-4a50-bea6-17f37a1642b6', 0, NULL)
SET IDENTITY_INSERT [dbo].[ReimbursementModels] OFF
GO
SET IDENTITY_INSERT [dbo].[SprEmployees] ON 

INSERT [dbo].[SprEmployees] ([Id], [Name]) VALUES (1, N'Laiza Zuniga')
INSERT [dbo].[SprEmployees] ([Id], [Name]) VALUES (2, N'Mike Perez')
SET IDENTITY_INSERT [dbo].[SprEmployees] OFF
GO
/****** Object:  Index [IX_ReimbursementModels_SprEmployeeId]    Script Date: 7/12/2023 5:05:27 PM ******/
CREATE NONCLUSTERED INDEX [IX_ReimbursementModels_SprEmployeeId] ON [dbo].[ReimbursementModels]
(
	[SprEmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ReimbursementModels] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsApproved]
GO
USE [master]
GO
ALTER DATABASE [SprReimbursementDB] SET  READ_WRITE 
GO
