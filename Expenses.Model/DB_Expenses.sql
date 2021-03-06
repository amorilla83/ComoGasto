USE [master]
GO
/****** Object:  Database [DB_Expenses]    Script Date: 01/12/2018 21:25:29 ******/
CREATE DATABASE [DB_Expenses]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DB_Expenses', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\DB_Expenses.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DB_Expenses_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\DB_Expenses_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DB_Expenses] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DB_Expenses].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DB_Expenses] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DB_Expenses] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DB_Expenses] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DB_Expenses] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DB_Expenses] SET ARITHABORT OFF 
GO
ALTER DATABASE [DB_Expenses] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DB_Expenses] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DB_Expenses] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DB_Expenses] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DB_Expenses] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DB_Expenses] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DB_Expenses] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DB_Expenses] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DB_Expenses] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DB_Expenses] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DB_Expenses] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DB_Expenses] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DB_Expenses] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DB_Expenses] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DB_Expenses] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DB_Expenses] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DB_Expenses] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DB_Expenses] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DB_Expenses] SET  MULTI_USER 
GO
ALTER DATABASE [DB_Expenses] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DB_Expenses] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DB_Expenses] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DB_Expenses] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [DB_Expenses] SET DELAYED_DURABILITY = DISABLED 
GO
USE [DB_Expenses]
GO
/****** Object:  Table [dbo].[Brand]    Script Date: 01/12/2018 21:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brand](
	[IdBrand] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Brand] PRIMARY KEY CLUSTERED 
(
	[IdBrand] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Expense]    Script Date: 01/12/2018 21:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Expense](
	[IdExpense] [int] IDENTITY(1,1) NOT NULL,
	[Concept] [nvarchar](50) NULL,
	[Details] [nvarchar](200) NULL,
	[Price] [money] NULL,
	[IdTypePayment] [int] NULL,
	[Date] [datetime] NULL,
	[IdStore] [int] NULL,
	[IdSection] [int] NULL,
 CONSTRAINT [PK_Expense] PRIMARY KEY CLUSTERED 
(
	[IdExpense] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ExpenseItem]    Script Date: 01/12/2018 21:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExpenseItem](
	[IdExpenseItem] [int] NOT NULL,
	[IdExpense] [int] NOT NULL,
	[IdProductBrand] [int] NULL,
	[IdTypeMeasure] [int] NULL,
	[Quantity] [nvarchar](50) NULL,
	[Units] [int] NULL,
	[Price] [money] NOT NULL,
	[IdProduct] [int] NOT NULL,
 CONSTRAINT [PK_ExpenseItem] PRIMARY KEY CLUSTERED 
(
	[IdExpenseItem] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Favourite]    Script Date: 01/12/2018 21:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Favourite](
	[IdFavourite] [int] IDENTITY(1,1) NOT NULL,
	[IdProductBrand] [int] NULL,
	[IdStore] [int] NULL,
 CONSTRAINT [PK_Favourite] PRIMARY KEY CLUSTERED 
(
	[IdFavourite] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HistoPrice]    Script Date: 01/12/2018 21:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistoPrice](
	[IdHistoPrice] [int] IDENTITY(1,1) NOT NULL,
	[IdProductBrand] [int] NOT NULL,
	[Price] [money] NOT NULL,
	[IdStore] [int] NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_HistoPrice] PRIMARY KEY CLUSTERED 
(
	[IdHistoPrice] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Label]    Script Date: 01/12/2018 21:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Label](
	[IdLabel] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[IdSection] [int] NULL,
 CONSTRAINT [PK_Label] PRIMARY KEY CLUSTERED 
(
	[IdLabel] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Product]    Script Date: 01/12/2018 21:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[IdProduct] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NULL,
	[Detail] [nvarchar](200) NULL,
	[IdLabel] [int] NULL,
	[Image] [nvarchar](200) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[IdProduct] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductBrand]    Script Date: 01/12/2018 21:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductBrand](
	[IdProductBrand] [int] IDENTITY(1,1) NOT NULL,
	[IdProduct] [int] NOT NULL,
	[Quantity] [nvarchar](50) NULL,
	[IdBrand] [int] NOT NULL,
 CONSTRAINT [PK_ProductBrand] PRIMARY KEY CLUSTERED 
(
	[IdProductBrand] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Section]    Script Date: 01/12/2018 21:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Section](
	[IdSection] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NULL,
 CONSTRAINT [PK_Section] PRIMARY KEY CLUSTERED 
(
	[IdSection] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Store]    Script Date: 01/12/2018 21:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Store](
	[IdStore] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Logo] [nvarchar](200) NULL,
	[IdTypeStore] [int] NULL,
 CONSTRAINT [PK_Store] PRIMARY KEY CLUSTERED 
(
	[IdStore] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TypeMeasure]    Script Date: 01/12/2018 21:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeMeasure](
	[IdTypeMeasure] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_TypeMeasure] PRIMARY KEY CLUSTERED 
(
	[IdTypeMeasure] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TypePayment]    Script Date: 01/12/2018 21:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypePayment](
	[IdTypePayment] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_TypePayment] PRIMARY KEY CLUSTERED 
(
	[IdTypePayment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TypeStore]    Script Date: 01/12/2018 21:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeStore](
	[IdTypeStore] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_TypeStore] PRIMARY KEY CLUSTERED 
(
	[IdTypeStore] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Expense]  WITH CHECK ADD  CONSTRAINT [FK_Expense_Section] FOREIGN KEY([IdSection])
REFERENCES [dbo].[Section] ([IdSection])
GO
ALTER TABLE [dbo].[Expense] CHECK CONSTRAINT [FK_Expense_Section]
GO
ALTER TABLE [dbo].[Expense]  WITH CHECK ADD  CONSTRAINT [FK_Expense_Store] FOREIGN KEY([IdStore])
REFERENCES [dbo].[Store] ([IdStore])
GO
ALTER TABLE [dbo].[Expense] CHECK CONSTRAINT [FK_Expense_Store]
GO
ALTER TABLE [dbo].[Expense]  WITH CHECK ADD  CONSTRAINT [FK_Expense_TypePayment] FOREIGN KEY([IdTypePayment])
REFERENCES [dbo].[TypePayment] ([IdTypePayment])
GO
ALTER TABLE [dbo].[Expense] CHECK CONSTRAINT [FK_Expense_TypePayment]
GO
ALTER TABLE [dbo].[ExpenseItem]  WITH CHECK ADD  CONSTRAINT [FK_ExpenseItem_Expense] FOREIGN KEY([IdExpense])
REFERENCES [dbo].[Expense] ([IdExpense])
GO
ALTER TABLE [dbo].[ExpenseItem] CHECK CONSTRAINT [FK_ExpenseItem_Expense]
GO
ALTER TABLE [dbo].[ExpenseItem]  WITH CHECK ADD  CONSTRAINT [FK_ExpenseItem_Product] FOREIGN KEY([IdProduct])
REFERENCES [dbo].[Product] ([IdProduct])
GO
ALTER TABLE [dbo].[ExpenseItem] CHECK CONSTRAINT [FK_ExpenseItem_Product]
GO
ALTER TABLE [dbo].[ExpenseItem]  WITH CHECK ADD  CONSTRAINT [FK_ExpenseItem_TypeMeasure] FOREIGN KEY([IdTypeMeasure])
REFERENCES [dbo].[TypeMeasure] ([IdTypeMeasure])
GO
ALTER TABLE [dbo].[ExpenseItem] CHECK CONSTRAINT [FK_ExpenseItem_TypeMeasure]
GO
ALTER TABLE [dbo].[Favourite]  WITH CHECK ADD  CONSTRAINT [FK_Favourite_ProductBrand] FOREIGN KEY([IdProductBrand])
REFERENCES [dbo].[ProductBrand] ([IdProductBrand])
GO
ALTER TABLE [dbo].[Favourite] CHECK CONSTRAINT [FK_Favourite_ProductBrand]
GO
ALTER TABLE [dbo].[Favourite]  WITH CHECK ADD  CONSTRAINT [FK_Favourite_Store] FOREIGN KEY([IdStore])
REFERENCES [dbo].[Store] ([IdStore])
GO
ALTER TABLE [dbo].[Favourite] CHECK CONSTRAINT [FK_Favourite_Store]
GO
ALTER TABLE [dbo].[HistoPrice]  WITH CHECK ADD  CONSTRAINT [FK_HistoPrice_ProductBrand] FOREIGN KEY([IdProductBrand])
REFERENCES [dbo].[ProductBrand] ([IdProductBrand])
GO
ALTER TABLE [dbo].[HistoPrice] CHECK CONSTRAINT [FK_HistoPrice_ProductBrand]
GO
ALTER TABLE [dbo].[HistoPrice]  WITH CHECK ADD  CONSTRAINT [FK_HistoPrice_Store] FOREIGN KEY([IdStore])
REFERENCES [dbo].[Store] ([IdStore])
GO
ALTER TABLE [dbo].[HistoPrice] CHECK CONSTRAINT [FK_HistoPrice_Store]
GO
ALTER TABLE [dbo].[Label]  WITH CHECK ADD  CONSTRAINT [FK_Label_Section] FOREIGN KEY([IdSection])
REFERENCES [dbo].[Section] ([IdSection])
GO
ALTER TABLE [dbo].[Label] CHECK CONSTRAINT [FK_Label_Section]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Label] FOREIGN KEY([IdLabel])
REFERENCES [dbo].[Label] ([IdLabel])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Label]
GO
ALTER TABLE [dbo].[ProductBrand]  WITH CHECK ADD  CONSTRAINT [FK_ProductBrand_Brand] FOREIGN KEY([IdBrand])
REFERENCES [dbo].[Brand] ([IdBrand])
GO
ALTER TABLE [dbo].[ProductBrand] CHECK CONSTRAINT [FK_ProductBrand_Brand]
GO
ALTER TABLE [dbo].[ProductBrand]  WITH CHECK ADD  CONSTRAINT [FK_ProductBrand_Product] FOREIGN KEY([IdProduct])
REFERENCES [dbo].[Product] ([IdProduct])
GO
ALTER TABLE [dbo].[ProductBrand] CHECK CONSTRAINT [FK_ProductBrand_Product]
GO
ALTER TABLE [dbo].[Store]  WITH CHECK ADD  CONSTRAINT [FK_Store_TypeStore] FOREIGN KEY([IdTypeStore])
REFERENCES [dbo].[TypeStore] ([IdTypeStore])
GO
ALTER TABLE [dbo].[Store] CHECK CONSTRAINT [FK_Store_TypeStore]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sección del gasto' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Section'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tienda en la que se realiza el gasto' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Store'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo al que pertenece la tienda' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TypeStore'
GO
USE [master]
GO
ALTER DATABASE [DB_Expenses] SET  READ_WRITE 
GO
