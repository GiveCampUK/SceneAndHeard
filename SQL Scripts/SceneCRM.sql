USE [master]
GO
/****** Object:  Database [SceneCRM]    Script Date: 10/25/2011 00:42:39 ******/
CREATE DATABASE [SceneCRM] ON  PRIMARY 
( NAME = N'SceneCRM', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\SceneCRM.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SceneCRM_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\SceneCRM.ldf' , SIZE = 2304KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [SceneCRM] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SceneCRM].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SceneCRM] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [SceneCRM] SET ANSI_NULLS OFF
GO
ALTER DATABASE [SceneCRM] SET ANSI_PADDING OFF
GO
ALTER DATABASE [SceneCRM] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [SceneCRM] SET ARITHABORT OFF
GO
ALTER DATABASE [SceneCRM] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [SceneCRM] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [SceneCRM] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [SceneCRM] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [SceneCRM] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [SceneCRM] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [SceneCRM] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [SceneCRM] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [SceneCRM] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [SceneCRM] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [SceneCRM] SET  DISABLE_BROKER
GO
ALTER DATABASE [SceneCRM] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [SceneCRM] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [SceneCRM] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [SceneCRM] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [SceneCRM] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [SceneCRM] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [SceneCRM] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [SceneCRM] SET  READ_WRITE
GO
ALTER DATABASE [SceneCRM] SET RECOVERY FULL
GO
ALTER DATABASE [SceneCRM] SET  MULTI_USER
GO
ALTER DATABASE [SceneCRM] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [SceneCRM] SET DB_CHAINING OFF
GO
USE [SceneCRM]
GO
/****** Object:  User [givecamp]    Script Date: 10/25/2011 00:42:39 ******/
CREATE USER [givecamp] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Production]    Script Date: 10/25/2011 00:42:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Production](
	[ProductionId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](64) NULL,
 CONSTRAINT [PK_Production] PRIMARY KEY CLUSTERED 
(
	[ProductionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Student]    Script Date: 10/25/2011 00:42:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Student](
	[StudentId] [int] IDENTITY(1,1) NOT NULL,
	[MembershipNumber] [varchar](16) NOT NULL,
	[Forename] [varchar](64) NULL,
	[Surname] [varchar](64) NOT NULL,
	[QuestionnaireResponse] [varchar](512) NULL,
 CONSTRAINT [PK_Child] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Job]    Script Date: 10/25/2011 00:42:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Job](
	[JobId] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](64) NULL,
 CONSTRAINT [PK_Job] PRIMARY KEY CLUSTERED 
(
	[JobId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Defines the various roles - director, dramaturge, box office, etc - filled by volunteers' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Job'
GO
/****** Object:  Table [dbo].[Feedback]    Script Date: 10/25/2011 00:42:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Feedback](
	[FeedbackId] [int] IDENTITY(1,1) NOT NULL,
	[FeedbackText] [nvarchar](500) NOT NULL,
	[FeedbackLeft] [date] NULL,
	[eventBriteId] [varchar](64) NULL,
	[ContactName] [nvarchar](100) NULL,
	[ContactEmailAddress] [nvarchar](256) NULL,
	[OrganisationName] [nvarchar](100) NULL,
	[Approved] [bit] NULL,
 CONSTRAINT [PK_Feedback] PRIMARY KEY CLUSTERED 
(
	[FeedbackId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CourseType]    Script Date: 10/25/2011 00:42:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CourseType](
	[CourseTypeCode] [varchar](8) NOT NULL,
	[CourseTypeName] [varchar](64) NULL,
 CONSTRAINT [PK_CourseType] PRIMARY KEY CLUSTERED 
(
	[CourseTypeCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Volunteer]    Script Date: 10/25/2011 00:42:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Volunteer](
	[VolunteerId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](64) NULL,
	[Surname] [varchar](64) NULL,
	[PerformanceAttended] [varchar](1024) NULL,
	[AvailableFrom] [datetime] NULL,
	[ReferredByVolunteerId] [int] NULL,
	[PartnerFirstName] [varchar](64) NULL,
	[PartnerSurname] [varchar](64) NULL,
	[Notes] [varchar](2048) NULL,
	[AccessPersonId] [int] NULL,
	[Telephone] [varchar](32) NULL,
	[MobilePhone] [varchar](32) NULL,
	[EmailAddress] [varchar](128) NULL,
	[EmailAddress2] [varchar](128) NULL,
	[Address] [varchar](512) NULL,
	[Postcode] [varchar](16) NULL,
	[CvWebUrl] [varchar](256) NULL,
	[SpotlightNumber] [bit] NULL,
	[AgentName] [varchar](128) NULL,
	[Organisation] [varchar](256) NULL,
	[EyesEars] [bit] NULL,
	[Schools] [bit] NULL,
	[Trustee] [bit] NULL,
	[Deadwood] [bit] NULL,
	[NoMailout] [bit] NULL,
	[EEDirectDebit] [bit] NULL,
	[OtherProfession] [varchar](64) NULL,
 CONSTRAINT [PK_Volunteer] PRIMARY KEY CLUSTERED 
(
	[VolunteerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Term]    Script Date: 10/25/2011 00:42:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Term](
	[TermId] [int] IDENTITY(1,1) NOT NULL,
	[TermName] [varchar](16) NULL,
 CONSTRAINT [PK_Term] PRIMARY KEY CLUSTERED 
(
	[TermId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[VolunteerCapability]    Script Date: 10/25/2011 00:42:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VolunteerCapability](
	[VolunteerId] [int] NOT NULL,
	[JobId] [int] NOT NULL,
 CONSTRAINT [PK_VolunteerCapability] PRIMARY KEY CLUSTERED 
(
	[VolunteerId] ASC,
	[JobId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Course]    Script Date: 10/25/2011 00:42:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Course](
	[CourseId] [int] IDENTITY(1,1) NOT NULL,
	[CourseTypeCode] [varchar](8) NULL,
	[TermId] [int] NULL,
	[Year] [int] NULL,
 CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
(
	[CourseId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CrbCheck]    Script Date: 10/25/2011 00:42:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CrbCheck](
	[CrbCheckId] [int] IDENTITY(1,1) NOT NULL,
	[VolunteerId] [int] NULL,
	[ApplicationSent] [bit] NULL,
	[ApplicationDate] [datetime] NULL,
	[Approved] [bit] NULL,
	[ApprovalDate] [datetime] NULL,
	[CrbNumber] [varchar](64) NULL,
 CONSTRAINT [PK_CrbCheck] PRIMARY KEY CLUSTERED 
(
	[CrbCheckId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductionVolunteer]    Script Date: 10/25/2011 00:42:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductionVolunteer](
	[VolunteerId] [int] NOT NULL,
	[JobId] [int] NOT NULL,
	[ProductionId] [int] NOT NULL,
	[Notes] [varchar](2048) NULL,
 CONSTRAINT [PK_ProductionVolunteer] PRIMARY KEY CLUSTERED 
(
	[VolunteerId] ASC,
	[JobId] ASC,
	[ProductionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Play]    Script Date: 10/25/2011 00:42:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Play](
	[PlayId] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[Title] [varchar](128) NULL,
	[ProductionId] [int] NULL,
 CONSTRAINT [PK_Play] PRIMARY KEY CLUSTERED 
(
	[PlayId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Performance]    Script Date: 10/25/2011 00:42:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Performance](
	[PerformanceId] [int] IDENTITY(1,1) NOT NULL,
	[ProductionId] [int] NULL,
	[PerformanceDateTime] [datetime] NULL,
	[EventBriteId] [varchar](64) NULL,
 CONSTRAINT [PK_Performance] PRIMARY KEY CLUSTERED 
(
	[PerformanceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PlayVolunteer]    Script Date: 10/25/2011 00:42:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PlayVolunteer](
	[PlayId] [int] NOT NULL,
	[VolunteerId] [int] NOT NULL,
	[JobId] [int] NOT NULL,
	[Notes] [varchar](2048) NULL,
 CONSTRAINT [PK_PlayVolunteer] PRIMARY KEY CLUSTERED 
(
	[PlayId] ASC,
	[VolunteerId] ASC,
	[JobId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CourseVolunteer]    Script Date: 10/25/2011 00:42:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CourseVolunteer](
	[CourseId] [int] NOT NULL,
	[VolunteerId] [int] NOT NULL,
	[JobId] [int] NOT NULL,
	[Notes] [varchar](2048) NULL,
 CONSTRAINT [PK_CourseVolunteer] PRIMARY KEY CLUSTERED 
(
	[CourseId] ASC,
	[VolunteerId] ASC,
	[JobId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CourseAttendance]    Script Date: 10/25/2011 00:42:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CourseAttendance](
	[StudentId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[Completed] [bit] NULL,
	[PlayId] [int] NULL,
 CONSTRAINT [PK_CourseAttendance] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC,
	[CourseId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VolunteerCrbChecks]    Script Date: 10/25/2011 00:42:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VolunteerCrbChecks]
AS
SELECT     v.VolunteerId, v.FirstName, v.Surname, v.PerformanceAttended, crb.CrbNumber, MAX(crb.ApplicationDate) AS ApplicationDate, DATEADD(Year, 3, 
                      MAX(crb.ApprovalDate)) AS ApplicationExpiryDate, crb.Approved, crb.ApprovalDate
FROM         dbo.Volunteer AS v LEFT OUTER JOIN
                      dbo.CrbCheck AS crb ON v.VolunteerId = crb.VolunteerId
GROUP BY v.VolunteerId, v.FirstName, v.Surname, v.PerformanceAttended, crb.CrbNumber, crb.ApplicationSent, crb.Approved, crb.ApprovalDate
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "v"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 185
               Right = 254
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "crb"
            Begin Extent = 
               Top = 6
               Left = 292
               Bottom = 181
               Right = 484
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 10
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1995
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VolunteerCrbChecks'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VolunteerCrbChecks'
GO
/****** Object:  View [dbo].[VolunteerHistory]    Script Date: 10/25/2011 00:42:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VolunteerHistory]
AS
SELECT     v.volunteerid, v.firstname, v.surname, 'play' AS 'type', p.title, j.[description], pv.notes, p.playid, null as 'courseid', null as 'productionid'
FROM         volunteer v left JOIN
                      playvolunteer pv ON v.volunteerid = pv.volunteerid left JOIN
                      job j ON j.jobid = pv.jobid left JOIN
                      play p ON p.playid = pv.playid
UNION
SELECT     v.volunteerid, v.firstname, v.surname, 'course' AS 'type', ct.coursetypename, j.[description], cv.notes, null as 'playid', c.courseid, null as 'productionid'
FROM         volunteer v left JOIN
                      coursevolunteer cv ON v.volunteerid = cv.volunteerid left JOIN
                      job j ON j.jobid = cv.jobid left JOIN
                      course c ON c.courseid = cv.courseid left JOIN
                      coursetype ct ON c.coursetypecode = ct.coursetypecode
UNION
SELECT     v.volunteerid, v.firstname, v.surname, 'production' AS 'type', prod.title, j.[description], prodv.notes, null as 'playid', null as 'courseid', prod.productionid
FROM         volunteer v left JOIN
                      productionvolunteer prodv ON v.volunteerid = prodv.volunteerid left JOIN
                      job j ON j.jobid = prodv.jobid left JOIN
                      production prod ON prod.productionid = prodv.productionid
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1[50] 2[25] 3) )"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2[66] 3) )"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 5
   End
   Begin DiagramPane = 
      PaneHidden = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      PaneHidden = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VolunteerHistory'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VolunteerHistory'
GO
/****** Object:  View [dbo].[StudentHistory]    Script Date: 10/25/2011 00:42:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE VIEW [dbo].[StudentHistory]
AS
SELECT     s.StudentId, s.MembershipNumber, s.Forename AS StudentForename, s.Surname AS StudentSurname, ct.CourseTypeName, c.Year, ca.Completed, 
                      p.Title AS 'PlayTitle', prod.Title AS 'ProductionTitle'
FROM         dbo.Student AS s LEFT OUTER JOIN
                      dbo.CourseAttendance AS ca ON s.StudentId = ca.StudentId LEFT OUTER JOIN
                      dbo.Course AS c ON c.CourseId = ca.CourseId LEFT OUTER JOIN
                      dbo.CourseType AS ct ON ct.CourseTypeCode = c.CourseTypeCode LEFT OUTER JOIN
                      dbo.Play AS p ON p.PlayId = ca.PlayId LEFT OUTER JOIN
                      dbo.Production AS prod ON prod.ProductionId = p.ProductionId
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "s"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 198
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ca"
            Begin Extent = 
               Top = 6
               Left = 236
               Bottom = 110
               Right = 396
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 6
               Left = 434
               Bottom = 125
               Right = 606
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ct"
            Begin Extent = 
               Top = 6
               Left = 644
               Bottom = 95
               Right = 818
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "p"
            Begin Extent = 
               Top = 6
               Left = 856
               Bottom = 125
               Right = 1016
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "prod"
            Begin Extent = 
               Top = 6
               Left = 1054
               Bottom = 95
               Right = 1214
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 15' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'StudentHistory'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'00
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'StudentHistory'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'StudentHistory'
GO
/****** Object:  ForeignKey [FK_Volunteer_Volunteer]    Script Date: 10/25/2011 00:42:43 ******/
ALTER TABLE [dbo].[Volunteer]  WITH CHECK ADD  CONSTRAINT [FK_Volunteer_Volunteer] FOREIGN KEY([ReferredByVolunteerId])
REFERENCES [dbo].[Volunteer] ([VolunteerId])
GO
ALTER TABLE [dbo].[Volunteer] CHECK CONSTRAINT [FK_Volunteer_Volunteer]
GO
/****** Object:  ForeignKey [FK_VolunteerCapability_Job]    Script Date: 10/25/2011 00:42:43 ******/
ALTER TABLE [dbo].[VolunteerCapability]  WITH NOCHECK ADD  CONSTRAINT [FK_VolunteerCapability_Job] FOREIGN KEY([JobId])
REFERENCES [dbo].[Job] ([JobId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[VolunteerCapability] CHECK CONSTRAINT [FK_VolunteerCapability_Job]
GO
/****** Object:  ForeignKey [FK_VolunteerCapability_Volunteer]    Script Date: 10/25/2011 00:42:43 ******/
ALTER TABLE [dbo].[VolunteerCapability]  WITH NOCHECK ADD  CONSTRAINT [FK_VolunteerCapability_Volunteer] FOREIGN KEY([VolunteerId])
REFERENCES [dbo].[Volunteer] ([VolunteerId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[VolunteerCapability] CHECK CONSTRAINT [FK_VolunteerCapability_Volunteer]
GO
/****** Object:  ForeignKey [FK_Course_CourseType]    Script Date: 10/25/2011 00:42:43 ******/
ALTER TABLE [dbo].[Course]  WITH CHECK ADD  CONSTRAINT [FK_Course_CourseType] FOREIGN KEY([CourseTypeCode])
REFERENCES [dbo].[CourseType] ([CourseTypeCode])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Course] CHECK CONSTRAINT [FK_Course_CourseType]
GO
/****** Object:  ForeignKey [FK_Course_Term]    Script Date: 10/25/2011 00:42:43 ******/
ALTER TABLE [dbo].[Course]  WITH CHECK ADD  CONSTRAINT [FK_Course_Term] FOREIGN KEY([TermId])
REFERENCES [dbo].[Term] ([TermId])
GO
ALTER TABLE [dbo].[Course] CHECK CONSTRAINT [FK_Course_Term]
GO
/****** Object:  ForeignKey [FK_CrbCheck_Volunteer]    Script Date: 10/25/2011 00:42:43 ******/
ALTER TABLE [dbo].[CrbCheck]  WITH NOCHECK ADD  CONSTRAINT [FK_CrbCheck_Volunteer] FOREIGN KEY([VolunteerId])
REFERENCES [dbo].[Volunteer] ([VolunteerId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CrbCheck] CHECK CONSTRAINT [FK_CrbCheck_Volunteer]
GO
/****** Object:  ForeignKey [FK_ProductionVolunteer_Job]    Script Date: 10/25/2011 00:42:43 ******/
ALTER TABLE [dbo].[ProductionVolunteer]  WITH NOCHECK ADD  CONSTRAINT [FK_ProductionVolunteer_Job] FOREIGN KEY([JobId])
REFERENCES [dbo].[Job] ([JobId])
GO
ALTER TABLE [dbo].[ProductionVolunteer] CHECK CONSTRAINT [FK_ProductionVolunteer_Job]
GO
/****** Object:  ForeignKey [FK_ProductionVolunteer_Production]    Script Date: 10/25/2011 00:42:43 ******/
ALTER TABLE [dbo].[ProductionVolunteer]  WITH NOCHECK ADD  CONSTRAINT [FK_ProductionVolunteer_Production] FOREIGN KEY([ProductionId])
REFERENCES [dbo].[Production] ([ProductionId])
GO
ALTER TABLE [dbo].[ProductionVolunteer] CHECK CONSTRAINT [FK_ProductionVolunteer_Production]
GO
/****** Object:  ForeignKey [FK_ProductionVolunteer_Volunteer]    Script Date: 10/25/2011 00:42:43 ******/
ALTER TABLE [dbo].[ProductionVolunteer]  WITH NOCHECK ADD  CONSTRAINT [FK_ProductionVolunteer_Volunteer] FOREIGN KEY([VolunteerId])
REFERENCES [dbo].[Volunteer] ([VolunteerId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductionVolunteer] CHECK CONSTRAINT [FK_ProductionVolunteer_Volunteer]
GO
/****** Object:  ForeignKey [FK_Play_Production]    Script Date: 10/25/2011 00:42:43 ******/
ALTER TABLE [dbo].[Play]  WITH CHECK ADD  CONSTRAINT [FK_Play_Production] FOREIGN KEY([ProductionId])
REFERENCES [dbo].[Production] ([ProductionId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Play] CHECK CONSTRAINT [FK_Play_Production]
GO
/****** Object:  ForeignKey [FK_Play_Student]    Script Date: 10/25/2011 00:42:43 ******/
ALTER TABLE [dbo].[Play]  WITH NOCHECK ADD  CONSTRAINT [FK_Play_Student] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([StudentId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Play] CHECK CONSTRAINT [FK_Play_Student]
GO
/****** Object:  ForeignKey [FK_Performance_Production]    Script Date: 10/25/2011 00:42:43 ******/
ALTER TABLE [dbo].[Performance]  WITH NOCHECK ADD  CONSTRAINT [FK_Performance_Production] FOREIGN KEY([ProductionId])
REFERENCES [dbo].[Production] ([ProductionId])
GO
ALTER TABLE [dbo].[Performance] CHECK CONSTRAINT [FK_Performance_Production]
GO
/****** Object:  ForeignKey [FK_PlayVolunteer_Job]    Script Date: 10/25/2011 00:42:43 ******/
ALTER TABLE [dbo].[PlayVolunteer]  WITH CHECK ADD  CONSTRAINT [FK_PlayVolunteer_Job] FOREIGN KEY([JobId])
REFERENCES [dbo].[Job] ([JobId])
GO
ALTER TABLE [dbo].[PlayVolunteer] CHECK CONSTRAINT [FK_PlayVolunteer_Job]
GO
/****** Object:  ForeignKey [FK_PlayVolunteer_Play]    Script Date: 10/25/2011 00:42:43 ******/
ALTER TABLE [dbo].[PlayVolunteer]  WITH CHECK ADD  CONSTRAINT [FK_PlayVolunteer_Play] FOREIGN KEY([PlayId])
REFERENCES [dbo].[Play] ([PlayId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PlayVolunteer] CHECK CONSTRAINT [FK_PlayVolunteer_Play]
GO
/****** Object:  ForeignKey [FK_PlayVolunteer_Volunteer]    Script Date: 10/25/2011 00:42:43 ******/
ALTER TABLE [dbo].[PlayVolunteer]  WITH CHECK ADD  CONSTRAINT [FK_PlayVolunteer_Volunteer] FOREIGN KEY([VolunteerId])
REFERENCES [dbo].[Volunteer] ([VolunteerId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PlayVolunteer] CHECK CONSTRAINT [FK_PlayVolunteer_Volunteer]
GO
/****** Object:  ForeignKey [FK_CourseVolunteer_Course]    Script Date: 10/25/2011 00:42:43 ******/
ALTER TABLE [dbo].[CourseVolunteer]  WITH NOCHECK ADD  CONSTRAINT [FK_CourseVolunteer_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([CourseId])
GO
ALTER TABLE [dbo].[CourseVolunteer] CHECK CONSTRAINT [FK_CourseVolunteer_Course]
GO
/****** Object:  ForeignKey [FK_CourseVolunteer_Job]    Script Date: 10/25/2011 00:42:43 ******/
ALTER TABLE [dbo].[CourseVolunteer]  WITH NOCHECK ADD  CONSTRAINT [FK_CourseVolunteer_Job] FOREIGN KEY([JobId])
REFERENCES [dbo].[Job] ([JobId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CourseVolunteer] CHECK CONSTRAINT [FK_CourseVolunteer_Job]
GO
/****** Object:  ForeignKey [FK_CourseVolunteer_Volunteer]    Script Date: 10/25/2011 00:42:43 ******/
ALTER TABLE [dbo].[CourseVolunteer]  WITH NOCHECK ADD  CONSTRAINT [FK_CourseVolunteer_Volunteer] FOREIGN KEY([VolunteerId])
REFERENCES [dbo].[Volunteer] ([VolunteerId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CourseVolunteer] CHECK CONSTRAINT [FK_CourseVolunteer_Volunteer]
GO
/****** Object:  ForeignKey [FK_CourseAttendance_Child]    Script Date: 10/25/2011 00:42:43 ******/
ALTER TABLE [dbo].[CourseAttendance]  WITH NOCHECK ADD  CONSTRAINT [FK_CourseAttendance_Child] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([StudentId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CourseAttendance] CHECK CONSTRAINT [FK_CourseAttendance_Child]
GO
/****** Object:  ForeignKey [FK_CourseAttendance_Course]    Script Date: 10/25/2011 00:42:43 ******/
ALTER TABLE [dbo].[CourseAttendance]  WITH CHECK ADD  CONSTRAINT [FK_CourseAttendance_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([CourseId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CourseAttendance] CHECK CONSTRAINT [FK_CourseAttendance_Course]
GO
/****** Object:  ForeignKey [FK_CourseAttendance_Play]    Script Date: 10/25/2011 00:42:43 ******/
ALTER TABLE [dbo].[CourseAttendance]  WITH CHECK ADD  CONSTRAINT [FK_CourseAttendance_Play] FOREIGN KEY([PlayId])
REFERENCES [dbo].[Play] ([PlayId])
GO
ALTER TABLE [dbo].[CourseAttendance] CHECK CONSTRAINT [FK_CourseAttendance_Play]
GO
