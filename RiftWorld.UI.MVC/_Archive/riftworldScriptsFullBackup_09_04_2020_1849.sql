/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2016 (13.0.5026)
    Source Database Engine Edition : Microsoft SQL Server Express Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2017
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VarietyOfInhabitants]') AND type in (N'U'))
ALTER TABLE [dbo].[VarietyOfInhabitants] DROP CONSTRAINT IF EXISTS [FK_VarietyOfInhabitants_Rifts]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VarietyOfInhabitants]') AND type in (N'U'))
ALTER TABLE [dbo].[VarietyOfInhabitants] DROP CONSTRAINT IF EXISTS [FK_VarietyOfInhabitants_Race]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserDetails]') AND type in (N'U'))
ALTER TABLE [dbo].[UserDetails] DROP CONSTRAINT IF EXISTS [FK_Users_Characters]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StoryTags]') AND type in (N'U'))
ALTER TABLE [dbo].[StoryTags] DROP CONSTRAINT IF EXISTS [FK_StoryTags_Tags]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StoryTags]') AND type in (N'U'))
ALTER TABLE [dbo].[StoryTags] DROP CONSTRAINT IF EXISTS [FK_StoryTags_Stories]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Stories]') AND type in (N'U'))
ALTER TABLE [dbo].[Stories] DROP CONSTRAINT IF EXISTS [FK_Stories_Infos]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SecretSecretTags]') AND type in (N'U'))
ALTER TABLE [dbo].[SecretSecretTags] DROP CONSTRAINT IF EXISTS [FK_SecretSecretTags_SecretTags]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SecretSecretTags]') AND type in (N'U'))
ALTER TABLE [dbo].[SecretSecretTags] DROP CONSTRAINT IF EXISTS [FK_SecretSecretTags_Secrets]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Secrets]') AND type in (N'U'))
ALTER TABLE [dbo].[Secrets] DROP CONSTRAINT IF EXISTS [FK_Secrets_Infos]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Rumors]') AND type in (N'U'))
ALTER TABLE [dbo].[Rumors] DROP CONSTRAINT IF EXISTS [FK_Rumors_Infos]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Rumors]') AND type in (N'U'))
ALTER TABLE [dbo].[Rumors] DROP CONSTRAINT IF EXISTS [FK_Rumors_Characters]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Rifts]') AND type in (N'U'))
ALTER TABLE [dbo].[Rifts] DROP CONSTRAINT IF EXISTS [FK_Rifts_Infos]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Orgs]') AND type in (N'U'))
ALTER TABLE [dbo].[Orgs] DROP CONSTRAINT IF EXISTS [FK_Orgs_Locales]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Orgs]') AND type in (N'U'))
ALTER TABLE [dbo].[Orgs] DROP CONSTRAINT IF EXISTS [FK_Orgs_Infos]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrgEvents]') AND type in (N'U'))
ALTER TABLE [dbo].[OrgEvents] DROP CONSTRAINT IF EXISTS [FK_OrgEvents_Orgs]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrgEvents]') AND type in (N'U'))
ALTER TABLE [dbo].[OrgEvents] DROP CONSTRAINT IF EXISTS [FK_OrgEvents_Events]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NPCs]') AND type in (N'U'))
ALTER TABLE [dbo].[NPCs] DROP CONSTRAINT IF EXISTS [FK_NPCs_Race]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NPCs]') AND type in (N'U'))
ALTER TABLE [dbo].[NPCs] DROP CONSTRAINT IF EXISTS [FK_NPCs_Locales]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NPCs]') AND type in (N'U'))
ALTER TABLE [dbo].[NPCs] DROP CONSTRAINT IF EXISTS [FK_NPCs_Infos]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NPCs]') AND type in (N'U'))
ALTER TABLE [dbo].[NPCs] DROP CONSTRAINT IF EXISTS [FK_NPCs_Gender]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NpcOrgs]') AND type in (N'U'))
ALTER TABLE [dbo].[NpcOrgs] DROP CONSTRAINT IF EXISTS [FK_NpcOrgs_Orgs]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NpcOrgs]') AND type in (N'U'))
ALTER TABLE [dbo].[NpcOrgs] DROP CONSTRAINT IF EXISTS [FK_NpcOrgs_NPCs]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Majorities]') AND type in (N'U'))
ALTER TABLE [dbo].[Majorities] DROP CONSTRAINT IF EXISTS [FK_Majorities_Race]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Majorities]') AND type in (N'U'))
ALTER TABLE [dbo].[Majorities] DROP CONSTRAINT IF EXISTS [FK_Majorities_Locales]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Lores]') AND type in (N'U'))
ALTER TABLE [dbo].[Lores] DROP CONSTRAINT IF EXISTS [FK_Lores_Infos]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Locales]') AND type in (N'U'))
ALTER TABLE [dbo].[Locales] DROP CONSTRAINT IF EXISTS [FK_Locales_NPCs]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Locales]') AND type in (N'U'))
ALTER TABLE [dbo].[Locales] DROP CONSTRAINT IF EXISTS [FK_Locales_Locales1]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Locales]') AND type in (N'U'))
ALTER TABLE [dbo].[Locales] DROP CONSTRAINT IF EXISTS [FK_Locales_Locales]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Locales]') AND type in (N'U'))
ALTER TABLE [dbo].[Locales] DROP CONSTRAINT IF EXISTS [FK_Locales_LocaleLevel]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Locales]') AND type in (N'U'))
ALTER TABLE [dbo].[Locales] DROP CONSTRAINT IF EXISTS [FK_Locales_Infos]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LocaleEvents]') AND type in (N'U'))
ALTER TABLE [dbo].[LocaleEvents] DROP CONSTRAINT IF EXISTS [FK_LocaleEvents_Locales]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LocaleEvents]') AND type in (N'U'))
ALTER TABLE [dbo].[LocaleEvents] DROP CONSTRAINT IF EXISTS [FK_LocaleEvents_Events]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Journals]') AND type in (N'U'))
ALTER TABLE [dbo].[Journals] DROP CONSTRAINT IF EXISTS [FK_Journals_Characters]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Items]') AND type in (N'U'))
ALTER TABLE [dbo].[Items] DROP CONSTRAINT IF EXISTS [FK_Items_Infos]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InfoTags]') AND type in (N'U'))
ALTER TABLE [dbo].[InfoTags] DROP CONSTRAINT IF EXISTS [FK_InfoTags_Tags]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InfoTags]') AND type in (N'U'))
ALTER TABLE [dbo].[InfoTags] DROP CONSTRAINT IF EXISTS [FK_InfoTags_Infos]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Infos]') AND type in (N'U'))
ALTER TABLE [dbo].[Infos] DROP CONSTRAINT IF EXISTS [FK_Infos_InfoType]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Events]') AND type in (N'U'))
ALTER TABLE [dbo].[Events] DROP CONSTRAINT IF EXISTS [FK_Events_Infos]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClassNPCs]') AND type in (N'U'))
ALTER TABLE [dbo].[ClassNPCs] DROP CONSTRAINT IF EXISTS [FK_ClassNPCs_NPCs]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClassNPCs]') AND type in (N'U'))
ALTER TABLE [dbo].[ClassNPCs] DROP CONSTRAINT IF EXISTS [FK_ClassNPCs_Class]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CharSecrets]') AND type in (N'U'))
ALTER TABLE [dbo].[CharSecrets] DROP CONSTRAINT IF EXISTS [FK_CharSecrets_SecretTags]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CharSecrets]') AND type in (N'U'))
ALTER TABLE [dbo].[CharSecrets] DROP CONSTRAINT IF EXISTS [FK_CharSecrets_Characters]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CharOrgs]') AND type in (N'U'))
ALTER TABLE [dbo].[CharOrgs] DROP CONSTRAINT IF EXISTS [FK_CharOrgs_Orgs]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CharOrgs]') AND type in (N'U'))
ALTER TABLE [dbo].[CharOrgs] DROP CONSTRAINT IF EXISTS [FK_CharOrgs_Characters]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Characters]') AND type in (N'U'))
ALTER TABLE [dbo].[Characters] DROP CONSTRAINT IF EXISTS [FK_Characters_Users]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Characters]') AND type in (N'U'))
ALTER TABLE [dbo].[Characters] DROP CONSTRAINT IF EXISTS [FK_Characters_Tier]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Characters]') AND type in (N'U'))
ALTER TABLE [dbo].[Characters] DROP CONSTRAINT IF EXISTS [FK_Characters_Race]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Characters]') AND type in (N'U'))
ALTER TABLE [dbo].[Characters] DROP CONSTRAINT IF EXISTS [FK_Characters_Locales]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Characters]') AND type in (N'U'))
ALTER TABLE [dbo].[Characters] DROP CONSTRAINT IF EXISTS [FK_Characters_Gender]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]') AND type in (N'U'))
ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT IF EXISTS [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]') AND type in (N'U'))
ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT IF EXISTS [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]') AND type in (N'U'))
ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT IF EXISTS [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]') AND type in (N'U'))
ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT IF EXISTS [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
/****** Object:  Table [dbo].[VarietyOfInhabitants]    Script Date: 9/4/2020 6:50:13 PM ******/
DROP TABLE IF EXISTS [dbo].[VarietyOfInhabitants]
GO
/****** Object:  Table [dbo].[UserDetails]    Script Date: 9/4/2020 6:50:13 PM ******/
DROP TABLE IF EXISTS [dbo].[UserDetails]
GO
/****** Object:  Table [dbo].[Tier]    Script Date: 9/4/2020 6:50:13 PM ******/
DROP TABLE IF EXISTS [dbo].[Tier]
GO
/****** Object:  Table [dbo].[Tags]    Script Date: 9/4/2020 6:50:13 PM ******/
DROP TABLE IF EXISTS [dbo].[Tags]
GO
/****** Object:  Table [dbo].[StoryTags]    Script Date: 9/4/2020 6:50:13 PM ******/
DROP TABLE IF EXISTS [dbo].[StoryTags]
GO
/****** Object:  Table [dbo].[Stories]    Script Date: 9/4/2020 6:50:13 PM ******/
DROP TABLE IF EXISTS [dbo].[Stories]
GO
/****** Object:  Table [dbo].[SecretTags]    Script Date: 9/4/2020 6:50:13 PM ******/
DROP TABLE IF EXISTS [dbo].[SecretTags]
GO
/****** Object:  Table [dbo].[SecretSecretTags]    Script Date: 9/4/2020 6:50:13 PM ******/
DROP TABLE IF EXISTS [dbo].[SecretSecretTags]
GO
/****** Object:  Table [dbo].[Secrets]    Script Date: 9/4/2020 6:50:13 PM ******/
DROP TABLE IF EXISTS [dbo].[Secrets]
GO
/****** Object:  Table [dbo].[Rumors]    Script Date: 9/4/2020 6:50:13 PM ******/
DROP TABLE IF EXISTS [dbo].[Rumors]
GO
/****** Object:  Table [dbo].[Rifts]    Script Date: 9/4/2020 6:50:13 PM ******/
DROP TABLE IF EXISTS [dbo].[Rifts]
GO
/****** Object:  Table [dbo].[Race]    Script Date: 9/4/2020 6:50:13 PM ******/
DROP TABLE IF EXISTS [dbo].[Race]
GO
/****** Object:  Table [dbo].[Orgs]    Script Date: 9/4/2020 6:50:13 PM ******/
DROP TABLE IF EXISTS [dbo].[Orgs]
GO
/****** Object:  Table [dbo].[OrgEvents]    Script Date: 9/4/2020 6:50:13 PM ******/
DROP TABLE IF EXISTS [dbo].[OrgEvents]
GO
/****** Object:  Table [dbo].[NPCs]    Script Date: 9/4/2020 6:50:13 PM ******/
DROP TABLE IF EXISTS [dbo].[NPCs]
GO
/****** Object:  Table [dbo].[NpcOrgs]    Script Date: 9/4/2020 6:50:13 PM ******/
DROP TABLE IF EXISTS [dbo].[NpcOrgs]
GO
/****** Object:  Table [dbo].[Majorities]    Script Date: 9/4/2020 6:50:13 PM ******/
DROP TABLE IF EXISTS [dbo].[Majorities]
GO
/****** Object:  Table [dbo].[Lores]    Script Date: 9/4/2020 6:50:13 PM ******/
DROP TABLE IF EXISTS [dbo].[Lores]
GO
/****** Object:  Table [dbo].[Locales]    Script Date: 9/4/2020 6:50:13 PM ******/
DROP TABLE IF EXISTS [dbo].[Locales]
GO
/****** Object:  Table [dbo].[LocaleLevel]    Script Date: 9/4/2020 6:50:13 PM ******/
DROP TABLE IF EXISTS [dbo].[LocaleLevel]
GO
/****** Object:  Table [dbo].[LocaleEvents]    Script Date: 9/4/2020 6:50:13 PM ******/
DROP TABLE IF EXISTS [dbo].[LocaleEvents]
GO
/****** Object:  Table [dbo].[Journals]    Script Date: 9/4/2020 6:50:13 PM ******/
DROP TABLE IF EXISTS [dbo].[Journals]
GO
/****** Object:  Table [dbo].[Items]    Script Date: 9/4/2020 6:50:13 PM ******/
DROP TABLE IF EXISTS [dbo].[Items]
GO
/****** Object:  Table [dbo].[InfoType]    Script Date: 9/4/2020 6:50:13 PM ******/
DROP TABLE IF EXISTS [dbo].[InfoType]
GO
/****** Object:  Table [dbo].[InfoTags]    Script Date: 9/4/2020 6:50:13 PM ******/
DROP TABLE IF EXISTS [dbo].[InfoTags]
GO
/****** Object:  Table [dbo].[Infos]    Script Date: 9/4/2020 6:50:13 PM ******/
DROP TABLE IF EXISTS [dbo].[Infos]
GO
/****** Object:  Table [dbo].[Gender]    Script Date: 9/4/2020 6:50:13 PM ******/
DROP TABLE IF EXISTS [dbo].[Gender]
GO
/****** Object:  Table [dbo].[Events]    Script Date: 9/4/2020 6:50:13 PM ******/
DROP TABLE IF EXISTS [dbo].[Events]
GO
/****** Object:  Table [dbo].[ClassNPCs]    Script Date: 9/4/2020 6:50:13 PM ******/
DROP TABLE IF EXISTS [dbo].[ClassNPCs]
GO
/****** Object:  Table [dbo].[Class]    Script Date: 9/4/2020 6:50:13 PM ******/
DROP TABLE IF EXISTS [dbo].[Class]
GO
/****** Object:  Table [dbo].[CharSecrets]    Script Date: 9/4/2020 6:50:13 PM ******/
DROP TABLE IF EXISTS [dbo].[CharSecrets]
GO
/****** Object:  Table [dbo].[CharOrgs]    Script Date: 9/4/2020 6:50:13 PM ******/
DROP TABLE IF EXISTS [dbo].[CharOrgs]
GO
/****** Object:  Table [dbo].[Characters]    Script Date: 9/4/2020 6:50:13 PM ******/
DROP TABLE IF EXISTS [dbo].[Characters]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 9/4/2020 6:50:13 PM ******/
DROP TABLE IF EXISTS [dbo].[AspNetUsers]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 9/4/2020 6:50:13 PM ******/
DROP TABLE IF EXISTS [dbo].[AspNetUserRoles]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 9/4/2020 6:50:13 PM ******/
DROP TABLE IF EXISTS [dbo].[AspNetUserLogins]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 9/4/2020 6:50:13 PM ******/
DROP TABLE IF EXISTS [dbo].[AspNetUserClaims]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 9/4/2020 6:50:13 PM ******/
DROP TABLE IF EXISTS [dbo].[AspNetRoles]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 9/4/2020 6:50:13 PM ******/
DROP TABLE IF EXISTS [dbo].[__MigrationHistory]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 9/4/2020 6:50:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[__MigrationHistory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 9/4/2020 6:50:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 9/4/2020 6:50:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 9/4/2020 6:50:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 9/4/2020 6:50:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 9/4/2020 6:50:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUsers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Characters]    Script Date: 9/4/2020 6:50:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Characters]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Characters](
	[CharacterId] [smallint] IDENTITY(1,1) NOT NULL,
	[PlayerId] [nvarchar](128) NOT NULL,
	[CharacterName] [varchar](25) NOT NULL,
	[RaceId] [tinyint] NOT NULL,
	[GenderId] [tinyint] NOT NULL,
	[PortraitFileName] [varchar](75) NULL,
	[Description] [varchar](2000) NOT NULL,
	[About] [varchar](8000) NOT NULL,
	[CurrentLocationId] [smallint] NOT NULL,
	[TierId] [tinyint] NOT NULL,
	[IsRetired] [bit] NOT NULL,
	[IsApproved] [bit] NOT NULL,
	[Artist] [nvarchar](40) NULL,
	[ClassString] [varchar](40) NULL,
	[HasUnseenEdit] [bit] NOT NULL,
	[IsDead] [bit] NOT NULL,
	[BackupPortrayerName] [varchar](32) NULL,
	[IsPlayerDemo] [bit] NOT NULL,
	[IsRequestingRetire] [bit] NOT NULL,
 CONSTRAINT [PK_Characters] PRIMARY KEY CLUSTERED 
(
	[CharacterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[CharOrgs]    Script Date: 9/4/2020 6:50:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CharOrgs]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CharOrgs](
	[CharOrgsId] [smallint] IDENTITY(1,1) NOT NULL,
	[CharId] [smallint] NOT NULL,
	[OrgId] [smallint] NOT NULL,
	[BlurbOrgPage] [varchar](50) NULL,
	[IsPublic] [bit] NOT NULL,
	[IsCurrent] [bit] NOT NULL,
	[KatherineApproved] [bit] NOT NULL,
 CONSTRAINT [PK_CharOrgs] PRIMARY KEY CLUSTERED 
(
	[CharOrgsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[CharSecrets]    Script Date: 9/4/2020 6:50:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CharSecrets]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CharSecrets](
	[CharSecretId] [int] IDENTITY(1,1) NOT NULL,
	[CharId] [smallint] NOT NULL,
	[SecretId] [smallint] NOT NULL,
 CONSTRAINT [PK_CharSecrets] PRIMARY KEY CLUSTERED 
(
	[CharSecretId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Class]    Script Date: 9/4/2020 6:50:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Class]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Class](
	[ClassId] [tinyint] IDENTITY(1,1) NOT NULL,
	[ClassName] [varchar](15) NOT NULL,
	[IsPlayerEnabled] [bit] NOT NULL,
 CONSTRAINT [PK_Class] PRIMARY KEY CLUSTERED 
(
	[ClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ClassNPCs]    Script Date: 9/4/2020 6:50:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClassNPCs]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ClassNPCs](
	[ClassNpcId] [int] IDENTITY(1,1) NOT NULL,
	[ClassId] [tinyint] NULL,
	[NpcId] [smallint] NOT NULL,
	[ClassOrder] [tinyint] NULL,
 CONSTRAINT [PK_ClassNPCs] PRIMARY KEY CLUSTERED 
(
	[ClassNpcId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Events]    Script Date: 9/4/2020 6:50:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Events]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Events](
	[EventId] [smallint] IDENTITY(1,1) NOT NULL,
	[InfoId] [smallint] NOT NULL,
	[IsHistory] [bit] NOT NULL,
	[AboutText] [varchar](max) NOT NULL,
	[NormalParticipants] [varchar](max) NOT NULL,
	[DateMonth] [tinyint] NULL,
	[DateSeason] [tinyint] NULL,
 CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Gender]    Script Date: 9/4/2020 6:50:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Gender]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Gender](
	[GenderId] [tinyint] IDENTITY(1,1) NOT NULL,
	[GenderName] [varchar](20) NOT NULL,
	[IconName] [varchar](20) NULL,
 CONSTRAINT [PK_Gender] PRIMARY KEY CLUSTERED 
(
	[GenderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Infos]    Script Date: 9/4/2020 6:50:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Infos]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Infos](
	[InfoId] [smallint] IDENTITY(1,1) NOT NULL,
	[InfoTypeId] [tinyint] NOT NULL,
	[IdWithinType] [smallint] NULL,
	[Blurb] [varchar](350) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[IsPublished] [bit] NOT NULL,
	[IsSecret] [bit] NOT NULL,
 CONSTRAINT [PK_Infos] PRIMARY KEY CLUSTERED 
(
	[InfoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[InfoTags]    Script Date: 9/4/2020 6:50:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InfoTags]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[InfoTags](
	[InfoTagId] [int] IDENTITY(1,1) NOT NULL,
	[InfoId] [smallint] NOT NULL,
	[TagId] [smallint] NOT NULL,
 CONSTRAINT [PK_InfoTags] PRIMARY KEY CLUSTERED 
(
	[InfoTagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[InfoType]    Script Date: 9/4/2020 6:50:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InfoType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[InfoType](
	[InfoTypeId] [tinyint] IDENTITY(1,1) NOT NULL,
	[TypeName] [varchar](15) NOT NULL,
 CONSTRAINT [PK_InfoType] PRIMARY KEY CLUSTERED 
(
	[InfoTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Items]    Script Date: 9/4/2020 6:50:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Items]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Items](
	[ItemId] [smallint] IDENTITY(1,1) NOT NULL,
	[InfoId] [smallint] NOT NULL,
	[PictureFileName] [varchar](75) NULL,
	[DescriptionText] [varchar](max) NOT NULL,
	[PropertyText] [varchar](max) NOT NULL,
	[HistoryText] [varchar](max) NOT NULL,
	[Artist] [nvarchar](40) NULL,
 CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED 
(
	[ItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Journals]    Script Date: 9/4/2020 6:50:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Journals]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Journals](
	[JournalId] [int] IDENTITY(1,1) NOT NULL,
	[CharacterId] [smallint] NOT NULL,
	[OocDateWritten] [date] NOT NULL,
	[TheContent] [varchar](max) NOT NULL,
	[IsApproved] [bit] NOT NULL,
	[HasUnseenEdit] [bit] NOT NULL,
 CONSTRAINT [PK_Journals] PRIMARY KEY CLUSTERED 
(
	[JournalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[LocaleEvents]    Script Date: 9/4/2020 6:50:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LocaleEvents]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[LocaleEvents](
	[LocaleEventId] [smallint] IDENTITY(1,1) NOT NULL,
	[LocaleId] [smallint] NOT NULL,
	[EventId] [smallint] NOT NULL,
 CONSTRAINT [PK_LocaleEvents] PRIMARY KEY CLUSTERED 
(
	[LocaleEventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[LocaleLevel]    Script Date: 9/4/2020 6:50:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LocaleLevel]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[LocaleLevel](
	[LocaleLevelId] [tinyint] IDENTITY(1,1) NOT NULL,
	[LocaleName] [varchar](15) NOT NULL,
 CONSTRAINT [PK_LocaleLevel] PRIMARY KEY CLUSTERED 
(
	[LocaleLevelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Locales]    Script Date: 9/4/2020 6:50:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Locales]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Locales](
	[LocaleId] [smallint] IDENTITY(1,1) NOT NULL,
	[InfoId] [smallint] NOT NULL,
	[LevelOfLocaleId] [tinyint] NOT NULL,
	[RegionId] [smallint] NULL,
	[ClosestCityId] [smallint] NULL,
	[CouncilDelegateId] [smallint] NULL,
	[Appointed] [varchar](8000) NOT NULL,
	[Environment] [varchar](max) NOT NULL,
	[About] [varchar](max) NOT NULL,
	[AvgLifestyle] [varchar](3000) NOT NULL,
 CONSTRAINT [PK_Locales] PRIMARY KEY CLUSTERED 
(
	[LocaleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Lores]    Script Date: 9/4/2020 6:50:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Lores]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Lores](
	[LoreId] [smallint] IDENTITY(1,1) NOT NULL,
	[InfoId] [smallint] NOT NULL,
	[TheContent] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Lores] PRIMARY KEY CLUSTERED 
(
	[LoreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Majorities]    Script Date: 9/4/2020 6:50:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Majorities]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Majorities](
	[MajorityId] [smallint] IDENTITY(1,1) NOT NULL,
	[LocaleId] [smallint] NOT NULL,
	[RaceId] [tinyint] NOT NULL,
	[IsFirst] [bit] NOT NULL,
 CONSTRAINT [PK_Majorities] PRIMARY KEY CLUSTERED 
(
	[MajorityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[NpcOrgs]    Script Date: 9/4/2020 6:50:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NpcOrgs]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[NpcOrgs](
	[NpcOrgId] [smallint] IDENTITY(1,1) NOT NULL,
	[NpcId] [smallint] NOT NULL,
	[OrgId] [smallint] NOT NULL,
	[OrgOrder] [tinyint] NULL,
	[BlurbNpcPage] [varchar](50) NULL,
	[BlurbOrgPage] [varchar](50) NULL,
	[IsCurrent] [bit] NOT NULL,
	[MemberOrder] [tinyint] NULL,
 CONSTRAINT [PK_NpcOrgs] PRIMARY KEY CLUSTERED 
(
	[NpcOrgId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[NPCs]    Script Date: 9/4/2020 6:50:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NPCs]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[NPCs](
	[NpcId] [smallint] IDENTITY(1,1) NOT NULL,
	[InfoId] [smallint] NOT NULL,
	[Alias] [varchar](500) NOT NULL,
	[Quote] [varchar](150) NOT NULL,
	[PortraitFileName] [varchar](75) NULL,
	[RaceId] [tinyint] NULL,
	[CrestFileName] [varchar](75) NULL,
	[ApperanceText] [varchar](2000) NOT NULL,
	[AboutText] [varchar](max) NOT NULL,
	[LastLocationId] [smallint] NULL,
	[PortraitArtist] [nvarchar](40) NULL,
	[CrestArtist] [nvarchar](40) NULL,
	[IsDead] [bit] NOT NULL,
	[GenderId] [tinyint] NOT NULL,
 CONSTRAINT [PK_NPCs] PRIMARY KEY CLUSTERED 
(
	[NpcId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[OrgEvents]    Script Date: 9/4/2020 6:50:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrgEvents]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[OrgEvents](
	[OrgEventsId] [int] IDENTITY(1,1) NOT NULL,
	[OrgId] [smallint] NOT NULL,
	[EventId] [smallint] NOT NULL,
	[Blurb] [varchar](100) NULL,
 CONSTRAINT [PK_OrgEvents] PRIMARY KEY CLUSTERED 
(
	[OrgEventsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Orgs]    Script Date: 9/4/2020 6:50:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Orgs]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Orgs](
	[OrgId] [smallint] IDENTITY(1,1) NOT NULL,
	[InfoId] [smallint] NOT NULL,
	[IsPlayerEnabled] [bit] NOT NULL,
	[SymbolFileName] [varchar](75) NULL,
	[BaseLocationId] [smallint] NULL,
	[AboutText] [varchar](max) NOT NULL,
	[Artist] [nvarchar](40) NULL,
 CONSTRAINT [PK_Orgs] PRIMARY KEY CLUSTERED 
(
	[OrgId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Race]    Script Date: 9/4/2020 6:50:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Race]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Race](
	[RaceId] [tinyint] IDENTITY(1,1) NOT NULL,
	[RaceName] [varchar](15) NOT NULL,
	[IsPlayerEnabled] [bit] NOT NULL,
 CONSTRAINT [PK_Race] PRIMARY KEY CLUSTERED 
(
	[RaceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Rifts]    Script Date: 9/4/2020 6:50:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Rifts]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Rifts](
	[RiftId] [smallint] IDENTITY(1,1) NOT NULL,
	[InfoId] [smallint] NOT NULL,
	[Location] [varchar](300) NOT NULL,
	[Environment] [varchar](max) NOT NULL,
	[Hazards] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Rifts] PRIMARY KEY CLUSTERED 
(
	[RiftId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Rumors]    Script Date: 9/4/2020 6:50:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Rumors]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Rumors](
	[RumorsId] [int] IDENTITY(1,1) NOT NULL,
	[RumorOfId] [smallint] NOT NULL,
	[AuthorId] [smallint] NOT NULL,
	[IsApproved] [bit] NOT NULL,
	[RumorText] [varchar](300) NOT NULL,
 CONSTRAINT [PK_Rumors] PRIMARY KEY CLUSTERED 
(
	[RumorsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Secrets]    Script Date: 9/4/2020 6:50:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Secrets]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Secrets](
	[SecretId] [smallint] IDENTITY(1,1) NOT NULL,
	[IsAboutId] [smallint] NOT NULL,
	[TheContent] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Secrets] PRIMARY KEY CLUSTERED 
(
	[SecretId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[SecretSecretTags]    Script Date: 9/4/2020 6:50:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SecretSecretTags]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SecretSecretTags](
	[SstId] [int] IDENTITY(1,1) NOT NULL,
	[SecretId] [smallint] NOT NULL,
	[SecretTagId] [smallint] NOT NULL,
 CONSTRAINT [PK_SecretSecretTags] PRIMARY KEY CLUSTERED 
(
	[SstId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[SecretTags]    Script Date: 9/4/2020 6:50:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SecretTags]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SecretTags](
	[SecretTagId] [smallint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](30) NOT NULL,
	[Description] [varchar](75) NOT NULL,
 CONSTRAINT [PK_SecretTags] PRIMARY KEY CLUSTERED 
(
	[SecretTagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Stories]    Script Date: 9/4/2020 6:50:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Stories]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Stories](
	[StoryId] [smallint] IDENTITY(1,1) NOT NULL,
	[IsAboutId] [smallint] NOT NULL,
	[DateTold] [date] NOT NULL,
	[CommissionedBy] [varchar](50) NULL,
	[IsCannon] [bit] NOT NULL,
	[TheContent] [varchar](max) NOT NULL,
	[Title] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Stories] PRIMARY KEY CLUSTERED 
(
	[StoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[StoryTags]    Script Date: 9/4/2020 6:50:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StoryTags]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[StoryTags](
	[StoryTagId] [int] IDENTITY(1,1) NOT NULL,
	[StoryId] [smallint] NOT NULL,
	[TagId] [smallint] NOT NULL,
 CONSTRAINT [PK_StoryTags] PRIMARY KEY CLUSTERED 
(
	[StoryTagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tags]    Script Date: 9/4/2020 6:50:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tags]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tags](
	[TagId] [smallint] IDENTITY(1,1) NOT NULL,
	[TagName] [varchar](25) NOT NULL,
	[Description] [varchar](75) NULL,
 CONSTRAINT [PK_Tags] PRIMARY KEY CLUSTERED 
(
	[TagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tier]    Script Date: 9/4/2020 6:50:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tier]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tier](
	[TierId] [tinyint] IDENTITY(1,1) NOT NULL,
	[TierName] [varchar](10) NOT NULL,
	[MinLevel] [tinyint] NOT NULL,
	[MaxLevel] [tinyint] NOT NULL,
 CONSTRAINT [PK_Tier] PRIMARY KEY CLUSTERED 
(
	[TierId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[UserDetails]    Script Date: 9/4/2020 6:50:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserDetails]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserDetails](
	[UserId] [nvarchar](128) NOT NULL,
	[DiscordName] [nvarchar](32) NOT NULL,
	[DiscordDiscriminator] [smallint] NOT NULL,
	[ConsentFileName] [varchar](75) NULL,
	[CurrentCharacterId] [smallint] NULL,
	[IsApproved] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[VarietyOfInhabitants]    Script Date: 9/4/2020 6:50:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VarietyOfInhabitants]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[VarietyOfInhabitants](
	[VarietyOfInhabitantsId] [smallint] IDENTITY(1,1) NOT NULL,
	[RiftId] [smallint] NOT NULL,
	[RaceId] [tinyint] NOT NULL,
	[RaceOrder] [tinyint] NULL,
 CONSTRAINT [PK_VarietyOfInhabitants] PRIMARY KEY CLUSTERED 
(
	[VarietyOfInhabitantsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'202007191735403_InitialCreate', N'RiftWorld.UI.MVC.Models.ApplicationDbContext', 0x1F8B0800000000000400DD5C6D6FE33612FE7EC0FD07419FEE0EA99597EE622FB05BA44ED206B779C13AD9BB6F0B5AA21D62254A95A834C1A1BFAC1FFA93FA173A942859E28B5E6CC5768A051616397C66381C92C3E1307FFCF6FBF8FBE7C0B79E709C90904EECA3D1A16D61EA861EA1CB899DB2C5371FECEFBFFBFBDFC6175EF06C7D2EE84E381DB4A4C9C47E642C3A759CC47DC4014A460171E33009176CE4868183BCD0393E3CFCB77374E46080B001CBB2C69F52CA4880B30FF89C86D4C5114B917F1D7AD84F4439D4CC3254EB06053889908B27F627B260FF0D63DF1B3D5C8DAE3F4F477913DB3AF309027166D85FD816A234648881B0A70F099EB138A4CB590405C8BF7F8930D02D909F60D189D31579D7FE1C1EF3FE38AB8605949B262C0C7A021E9D08053972F3B5D46C970A04155E80AAD90BEF75A6C6897DE5E1ACE853E883026486A7533FE6C413FBBA6471964437988D8A86A31CF23206B85FC2F8EBA88A7860756E77501AD4F1E890FF3BB0A6A9CFD2184F284E598CFC03EB2E9DFBC4FD0F7EB90FBF623A39399A2F4E3EBC7B8FBC93F7DFE29377D59E425F81AE560045777118E11864C38BB2FFB6E5D4DB3972C3B259A54DAE15B025981BB6758D9E3F62BA648F306B8E3FD8D62579C65E51228CEB8112984AD088C5297CDEA4BE8FE63E2EEB9D469EFCFF06AEC7EFDE0FC2F5063D916536F4127F983831CCAB4FD8CF6A934712E5D3AB36DE5F04D9651C06FCBB6E5F79ED975998C62EEF4C6824B947F112B3BA74636765BC9D4C9A430D6FD605EAFE9B369754356F2D29EFD03A33A160B1EDD950C8FBBA7C3B5BDC5914C1E065A6C535D26470861D6B24411C5832E1CA888EBA1A1185CEFD95D7C48B00117F8045B10317704A16240E70D9CB1F423041447BCB7C879204D604EF27943C36880E3F07107D86DD3406539D311444AFCEEDEE31A4F8260DE67C066C8FD7604373FF4B78895C16C61794B7DA18EF63E87E0D537641BD73C4F003730B40FE794F82EE00838873E6BA38492EC198B1370DC1E72E00AF283B39EE0DC757A95D3B25531F9140EF9548EBE9978274E599E82914EFC440A6F3509A44FD182E09ED266A416A1635A768155590F5159583759354509A05CD085AE5CCA906F3F9B2111ADEE9CB60F7DFEBDB6CF336AD051535CE6085C43F628A6358C6BC3BC4188EE96A04BAAC1BBB7016B2E1E34C5F7D6FCA387D467E3A34ABB56643B6080C3F1B32D8FD9F0D999850FC443CEE9574380A15C400DF895E7FCA6A9F739264DB9E0EB56E6E9BF976D600D374394B92D025D92CD004C14408A32E3FF870567B3C23EF8D1C13818E81A113BEE54109F4CD968DEA969E631F336C9DB97990708A121779AA1AA1435E0FC18A1D5523D82A365217EE5F0A4FB0741CF346881F821298A98432755A10EA9208F9AD5A925A76DCC278DF4B1E72CD398E30E50C5B35D185B93E14C20528F94883D2A6A1B153B1B866433478ADA6316F736157E3AE4428B662932DBEB3C12E85FFF62A86D9ACB12D1867B34ABA08600CEBEDC240C559A5AB01C807977D3350E9C4643050E1526DC540EB1ADB8181D655F2E60C343FA2761D7FE9BCBA6FE6593F286F7F5B6F54D70E6CB3A68F3D33CDDCF784360C5AE05835CFF339AFC4CF4C73380339C5F92C11AEAE6C221C7C86593D64B3F277B57EA8D30C221B5113E0CAD05A40C585A002A44CA81EC215B1BC46E98417D103B688BB35C28AB55F82ADD8808A5DBD18AD109AAF4F65E3EC74FA287B565A8362E49D0E0B151C8D41C88B57BDE31D94628ACBAA8AE9E20BF7F1862B1D1383D1A0A016CFD5A0A4A233836BA930CD762DE91CB23E2ED9465A92DC2783968ACE0CAE2561A3ED4AD238053DDC828D5454DFC2079A6C45A4A3DC6DCABAB193274D8982B163C8AE1A5FA328227459C9B61225D62C4FB59A7E33EB9F7E14E4188E9B68B2904A694B4E2C8CD1124BB5C01A24BD2471C2CE114373C4E33C532F50C8B47BAB61F92F5856B74F75108B7DA0A0E6BF450BC3357E6DC3553D12017409DD0CB85B93C5D23546A06F6EF11438E4A35813BE9F867E1A50B397656E9D5FE255DBE7252AC2D891E457BC2845658AAF5BD77FA7D15167C69023557A32EB8F9619C2A4F3C20FAD6ADDE49B9A518A505515C514BEDAD9E8995C9AFE2326BB8CFD07AC15E1756698C853A90288A29E1895540705AC52D71DB59E8D52C5ACD7744794524EAA9052550F29AB89253521AB156BE11934AAA7E8CE414D25A9A2ABB5DD9135492555684DF51AD81A99E5BAEEA89ABC932AB0A6BA3BF62A09455E49F7780F331E6436DBC4F203EF66BB9801E37596C56136C1CABD7E15A852DC134BDCDC2B60A27C2F4DCA78EADBCCA4F260C7662665C030AF41B56BF1FA12D478976FC6ACDD75D796F9A6BB7E335E3FC37D55F3504E7E3249C9BD3C014A27BDB13875B53FB6518E6139896D156A842DFE256138187182D1EC677FEA13CC17F482E01A51B2C009CBF33BECE3C3A363E9A9CEFE3C9B7192C4F335A756D3DB99FA986D21558B3EA1D87D44B19A38B1C1D39215A81293BEA21E7E9ED8FFCF5A9D66E10DFE2B2B3EB0AE92074A7E4EA1E23E4EB1F5AB9A083A4CAA7D87C71DA5A0BFBE895713DD557EF5BF2F79D303EB3686E9746A1D4A8A5E67F8EB6F297A499337DD409AB55F58BCDDD9567BB4A0459566CBFA6F14E6840DF23EA190F21F017AFE675FD1B46F103642D4BC33180A6F10159ADE11AC83657C43E0C127CBDE10F4EBACFE4DC13AA219DF1310DA1F4C7E4DD07D192A5AEE701FD29C9AB6B124657A6ECDC6DE283573D77B9392B4BDD1445713B37BC00D9A7CBD998BF2C6929A07DB3A3539CB8361EFD2EE5F3D51795F7293574EFB6E5392B79985DC70BFF4974A3EDE8374394DFACFEE538CB76D6BA630F09EE769F64B24DE336313DBFCEED385B76D6CA600F19E1B5BAFA4E03DB3B55DED9F3BB6B4CE5BE8CE537CD56C25C3758E2E8ADC96C29B87DCE1F83F0FC108728F327F79A9CF196BCA776D61B82231333527ABC98C9589A3F055289AD9F6EBABD8F01B3B2B689AD91A523C9B788BF5BF91B7A069E66D489CDC45F2B13675519710DEB28E356553BDA564E35A4F5A72DBDB7CD6C6BBF9B7945B3C88526AB3C770BBFC7652890751C99053A747EAB07A510C7B67E56F37C2FE9D90E50A82FF25478ADDDAAE59D25CD145586CDE9244058914A1B9C60C79B0A59EC58C2C90CBA09A07A0B3A7E359508F5F83CCB177456F5316A50CBA8C83B95F0B787127A0897F961F5D97797C1BF1AF64882E80988407EE6FE90F29F1BD52EE4B4D4CC800C1BD0B11EEE563C978D877F95222DD84B42390505FE914DDE320F2012CB9A533F484D7910DCCEF235E22F76515013481B40F445DEDE3738296310A1281B16A0F9F60C35EF0FCDD9F6178FFBFC2540000, N'6.2.0-61023')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'281a4abe-bbea-4fd3-9da0-7f49dc7f29fd', N'Admin')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'd23db799-a495-40e5-a872-c04703c02475', N'Character')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'739f9d6d-9429-45bc-9554-194cfb567c2b', N'Demo')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'74541872-936b-4b0c-af4b-e3a6734c1a4e', N'Mod')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'ff7214da-f972-4af2-b820-8623390552e7', N'Player')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'e3d0890f-5978-49e9-be45-00a5a4b52f88', N'281a4abe-bbea-4fd3-9da0-7f49dc7f29fd')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'fc836dc2-59f4-4a0b-8c42-5dd2a8718b4f', N'281a4abe-bbea-4fd3-9da0-7f49dc7f29fd')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'3a34a24b-d413-4dc6-9e64-feb21d9376e7', N'd23db799-a495-40e5-a872-c04703c02475')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'cba92599-2886-4410-ac11-e5ad3781f76f', N'd23db799-a495-40e5-a872-c04703c02475')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'3a34a24b-d413-4dc6-9e64-feb21d9376e7', N'ff7214da-f972-4af2-b820-8623390552e7')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'6ed5fd28-c488-4ba4-89b0-d1cf7afd292d', N'ff7214da-f972-4af2-b820-8623390552e7')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'99a29eac-aafa-44ce-8349-c8868ba0e3aa', N'ff7214da-f972-4af2-b820-8623390552e7')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'af0db862-e296-4bf6-8e65-18672010853d', N'ff7214da-f972-4af2-b820-8623390552e7')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'cba92599-2886-4410-ac11-e5ad3781f76f', N'ff7214da-f972-4af2-b820-8623390552e7')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd6ab2049-54be-40a2-a251-d62561de57e6', N'ff7214da-f972-4af2-b820-8623390552e7')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'f1615660-5e2a-4e7a-b389-328caa06a457', N'ff7214da-f972-4af2-b820-8623390552e7')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'3a34a24b-d413-4dc6-9e64-feb21d9376e7', N'azura@rift.com', 0, N'AK6eK0A9Q5CvlewV1gXs2dI9w0o1skspNp9fBzT/mtZT+FeVJXIDPx99Wgt2NcrSTg==', N'f6a64f0c-34af-46c0-83d1-17fdaf410dac', NULL, 0, 0, NULL, 1, 0, N'azura@rift.com')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'6ed5fd28-c488-4ba4-89b0-d1cf7afd292d', N'kinna@rift.com', 0, N'ACj1uTivffEA43btvAnCgKW8zelV5UF+f+xavwwIcQmRbqrlx8/Rx1RBjJMhuOrJVg==', N'ccfa37aa-7c63-4768-be53-2ccf1b592697', NULL, 0, 0, NULL, 1, 0, N'kinna@rift.com')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'99a29eac-aafa-44ce-8349-c8868ba0e3aa', N'ineedattention@rift.com', 0, N'AJNL6uAhAYcwhQhixDfdr6rY7xglMSQ3dLQGTH1uCQZEv7Q+P/UwaCDtW80RnIsToQ==', N'cdcdf807-0208-4bb9-9f78-45258503d2a6', NULL, 0, 0, NULL, 1, 0, N'attention#6969')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'af0db862-e296-4bf6-8e65-18672010853d', N'yeet@rift.com', 0, N'AFsIiJvuBWLWMp3CHjeQcf/qlAJyCNpBBfiaOlEEddW35qZc4NKCDcanCW+4Me/wHQ==', N'4848f9d7-0424-4be8-9de7-034371390064', NULL, 0, 0, NULL, 1, 0, N'afkl#0343')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'cba92599-2886-4410-ac11-e5ad3781f76f', N'orchid@rift.com', 0, N'AKhxg6u08DKaOyLzlmkFN++Fs4PUSZbHK4otLyflFwhLuVdlIDgQpx/nw2d9mFJVWQ==', N'd7b3d1b3-d772-4b75-94a0-c427dec836ce', NULL, 0, 0, NULL, 1, 0, N'orchid@rift.com')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'd6ab2049-54be-40a2-a251-d62561de57e6', N'usernametest@rift.com', 0, N'AEqanjkfiEftljNPJZioaQWMMld1m1kGtb8FOycw3WclzKylm/QDPDGqkY2C9WLQYw==', N'dc2fb801-fba6-4855-b7b8-9c9bedec65a8', NULL, 0, 0, NULL, 1, 0, N'usernametest#0059')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'e3d0890f-5978-49e9-be45-00a5a4b52f88', N'skiebyrrd1@gmail.com', 0, N'ALGH7aa39TiLBbvecDgnvwbuv1Naa71FJveSap9JVeF2eXzD9+ATQzsGo0pEXbcjJQ==', N'27f296c3-1e9a-4d85-9032-126c3243caf5', NULL, 0, 0, NULL, 1, 0, N'skiebyrrd1@gmail.com')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'f1615660-5e2a-4e7a-b389-328caa06a457', N'zerotest@rift.com', 0, N'AMdEP0RdFFI2Vt1CaafLa9qVtlO8jHkZTdRNA5Nze+TDWZkTNU6TzJ+98KaTvVwLNQ==', N'd99ca606-3dba-4239-bda1-f5e12b7ff71b', NULL, 0, 0, NULL, 1, 0, N'zerotest@rift.com')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'fc836dc2-59f4-4a0b-8c42-5dd2a8718b4f', N'kendradallen@outlook.com', 0, N'AIcwOlmnowmN/ZAwa/6qbpUs+z7N+B+7tds0GYg0JWWbSxgbFd40cNCG6Hz4wpuf0w==', N'5cc4f6d7-3f2e-47a1-b101-cda8ae63949c', NULL, 0, 0, NULL, 1, 0, N'kendradallen@outlook.com')
SET IDENTITY_INSERT [dbo].[Characters] ON 

INSERT [dbo].[Characters] ([CharacterId], [PlayerId], [CharacterName], [RaceId], [GenderId], [PortraitFileName], [Description], [About], [CurrentLocationId], [TierId], [IsRetired], [IsApproved], [Artist], [ClassString], [HasUnseenEdit], [IsDead], [BackupPortrayerName], [IsPlayerDemo], [IsRequestingRetire]) VALUES (1, N'cba92599-2886-4410-ac11-e5ad3781f76f', N'Maggie', 1, 1, N'character-1.jpg', N'Compared to his fellow party, Percy is rather quiet, usually only speaking when necessary. However, he has shown he can be quite persuasive in social situations and is somewhat of a strategist within the group, as seen in "The Temple Showdown" (1x11).

Percy thinks of himself as being the only one capable of making adult decisions, despite his young age. He is also very pragmatic, with a consistent belief that the ends justify the means. This has led to moments of conflict between him and the party.Background
Percy was raised alongside his six siblings in Whitestone, where his branch of the de Rolo family ruled. Five years before the campaign began, the Briarwoods were invited in as guests, where they proceeded to seize control of Whitestone, murdering the de Rolos and those loyal to them. Percy was taken prisoner and tortured by Anna Ripley. After Percy fled Whitestone with the help of his sister Cassandra, he wandered aimlessly before setting out to avenge his family. Percy then constructed his first gun, the List. On five of its six barrels he inscribed a target of his vengeance: Lord Briarwood, Lady Briarwood, Dr. Ripley, Sir Kerrion Stonefell, and Professor Anders. Percy tracked Dr. Ripley for a year until he made his way to Stilben, where he attempted to kill her. He was captured by Dr. Ripley''s guards and imprisoned until he was freed by the rest of Vox Machina.

Because he is a gunslinger, Percy is the main long-ranged attack member of Vox Machina. He previously fought with his two guns, "The List", a six-barrel pepper-box, and "Bad News", a sniper rifle-esque device. The List has since been destroyed, and Percy has acquired two new weapons, both from Dr. Ripley: a four-shot pistol which he called "Retort", and another pepperbox known as "Animus". He also has experience in using melee weapons, usually a rapier or a longsword.
', N'words
Compared to his fellow party, Percy is rather quiet, usually only speaking when necessary. However, he has shown he can be quite persuasive in social situations and is somewhat of a strategist within the group, as seen in "The Temple Showdown" (1x11).

Percy thinks of himself as being the only one capable of making adult decisions, despite his young age. He is also very pragmatic, with a consistent belief that the ends justify the means. This has led to moments of conflict between him and the party.

Background
Percy was raised alongside his six siblings in Whitestone, where his branch of the de Rolo family ruled. Five years before the campaign began, the Briarwoods were invited in as guests, where they proceeded to seize control of Whitestone, murdering the de Rolos and those loyal to them. Percy was taken prisoner and tortured by Anna Ripley. After Percy fled Whitestone with the help of his sister Cassandra, he wandered aimlessly before setting out to avenge his family. Percy then constructed his first gun, the List. On five of its six barrels he inscribed a target of his vengeance: Lord Briarwood, Lady Briarwood, Dr. Ripley, Sir Kerrion Stonefell, and Professor Anders. Percy tracked Dr. Ripley for a year until he made his way to Stilben, where he attempted to kill her. He was captured by Dr. Ripley''s guards and imprisoned until he was freed by the rest of Vox Machina.

Because he is a gunslinger, Percy is the main long-ranged attack member of Vox Machina. He previously fought with his two guns, "The List", a six-barrel pepper-box, and "Bad News", a sniper rifle-esque device. The List has since been destroyed, and Percy has acquired two new weapons, both from Dr. Ripley: a four-shot pistol which he called "Retort", and another pepperbox known as "Animus". He also has experience in using melee weapons, usually a rapier or a longsword.

Percy is also a tinkerer, creating and modifying items to give both himself and his teammates an edge in battle. He creates explosive and utility arrows for Vex''ahlia, as well as crafting his own ammunition and even weapons, such as his Diplomacy electroshock glove.

words
Compared to his fellow party, Percy is rather quiet, usually only speaking when necessary. However, he has shown he can be quite persuasive in social situations and is somewhat of a strategist within the group, as seen in "The Temple Showdown" (1x11).

Percy thinks of himself as being the only one capable of making adult decisions, despite his young age. He is also very pragmatic, with a consistent belief that the ends justify the means. This has led to moments of conflict between him and the party.

Background
Percy was raised alongside his six siblings in Whitestone, where his branch of the de Rolo family ruled. Five years before the campaign began, the Briarwoods were invited in as guests, where they proceeded to seize control of Whitestone, murdering the de Rolos and those loyal to them. Percy was taken prisoner and tortured by Anna Ripley. After Percy fled Whitestone with the help of his sister Cassandra, he wandered aimlessly before setting out to avenge his family. Percy then constructed his first gun, the List. On five of its six barrels he inscribed a target of his vengeance: Lord Briarwood, Lady Briarwood, Dr. Ripley, Sir Kerrion Stonefell, and Professor Anders. Percy tracked Dr. Ripley for a year until he made his way to Stilben, where he attempted to kill her. He was captured by Dr. Ripley''s guards and imprisoned until he was freed by the rest of Vox Machina.

Because he is a gunslinger, Percy is the main long-ranged attack member of Vox Machina. He previously fought with his two guns, "The List", a six-barrel pepper-box, and "Bad News", a sniper rifle-esque device. The List has since been destroyed, and Percy has acquired two new weapons, both from Dr. Ripley: a four-shot pistol which he called "Retort", and another pepperbox known as "Animus". He also has experience in using melee weapons, usually a rapier or a longsword.

Percy is also a tinkerer, creating and modifying items to give both himself and his teammates an edge in battle. He creates explosive and utility arrows for Vex''ahlia, as well as crafting his own ammunition and even weapons, such as his Diplomacy electroshock glove.
words
Compared to his fellow party, Percy is rather quiet, usually only speaking when necessary. However, he has shown he can be quite persuasive in social situations and is somewhat of a strategist within the group, as seen in "The Temple Showdown" (1x11).

Percy thinks of himself as being the only one capable of making adult decisions, despite his young age. He is also very pragmatic, with a consistent belief that the ends justify the means. This has led to moments of conflict between him and the party.

Background
Percy was raised alongside his six siblings in Whitestone, where his branch of the de Rolo family ruled. Five years before the campaign began, the Briarwoods were invited in as guests, where they proceeded to seize control of Whitestone, murdering the de Rolos and those loyal to them. Percy was taken prisoner and tortured by Anna Ripley. After Percy fled Whitestone with the help of his sister Cassandra, he wandered aimlessly before setting out to avenge his family. Percy then constructed his first gun, the List. On five of its six barrels he inscribed a target of his vengeance: Lord Briarwood, Lady Briarwood, Dr. Ripley, Sir Kerrion Stonefell, and Professor Anders. Percy tracked Dr. Ripley for a year until he made his way to Stilben, where he attempted to kill her. He was captured by Dr. Ripley''s guards and imprisoned until he was freed by the rest of Vox Machina.

Because he is a gunslinger, Percy is the main long-ranged attack member of Vox Machina. He previously fought with his two guns, "The List", a six-barrel pepper-box, and "Bad News", a sniper rifle-esque device. The List has since been destroyed, and Percy has acquired two new weapons, both from Dr. Ripley: a four-shot pistol which he called "Retort", and another pepperbox known as "Animus". He also has experience in using melee weapons, usually a rapier or a longsword.

Percy is also a tinkerer, creating and modifying items to give both himself and his teammates an edge in battle. He creates explosive and utility arrows for Vex''ahlia, as well as crafting his own ammunition and even weapons, such as his Diplomacy electroshock glove.
Percy was raised alongside his six siblings in Whitestone, where his branch of the de Rolo family ruled. Five years before the campaign began, the Briarwoods were invited in as guests, where they proceeded to seize control of Whitestone, murdering the de Rolos and those loyal to them. Percy was taken prisoner and tortured by Anna Ripley. After Percy fled Whitestone with the help of his sister Cassandra, he wandered aimlessly before setting out to avenge his family. Percy then constructed his first gun, the List. On five of its six barrels he inscribed a target of his vengeance: Lord Briarwood, Lady Briarwood, Dr. Ripley, Sir Kerrion Stonefell, and Professor Anders. Percy tracked Dr. Ripley for a year until he made his way to Stilben, where he attempted to kill her. He was captured by Dr. Ripley''s guards and imprisoned until he was freed by the rest of Vox Machin
Percy was raised alongside his six siblings in Whitestone, where his branch of the de Rolo family ruled. Five years before the campaign began, the Briarwoods were invited in as guests, where they proceeded to seize control of Whitestone, murdering the de Rolos and those loyal to them. Percy was taken prisoner and tortured by Anna Ripley. After Percy fled Whitestone with the help of his sister Cassandra, he wandered aimlessly before setting out to avenge his fami
', 1, 3, 0, 1, N'Maggie', N'Rogue', 1, 0, NULL, 0, 0)
INSERT [dbo].[Characters] ([CharacterId], [PlayerId], [CharacterName], [RaceId], [GenderId], [PortraitFileName], [Description], [About], [CurrentLocationId], [TierId], [IsRetired], [IsApproved], [Artist], [ClassString], [HasUnseenEdit], [IsDead], [BackupPortrayerName], [IsPlayerDemo], [IsRequestingRetire]) VALUES (2, N'6ed5fd28-c488-4ba4-89b0-d1cf7afd292d', N'Kendra', 1, 1, N'default.jpg', N'kinna', N'kinna', 1, 1, 1, 1, NULL, N'Sorcerer/Bard', 0, 0, NULL, 0, 0)
INSERT [dbo].[Characters] ([CharacterId], [PlayerId], [CharacterName], [RaceId], [GenderId], [PortraitFileName], [Description], [About], [CurrentLocationId], [TierId], [IsRetired], [IsApproved], [Artist], [ClassString], [HasUnseenEdit], [IsDead], [BackupPortrayerName], [IsPlayerDemo], [IsRequestingRetire]) VALUES (3, N'3a34a24b-d413-4dc6-9e64-feb21d9376e7', N'qweq', 1, 1, N'default.jpg', N'wqe', N'qew', 1, 1, 1, 1, N'qwe', N'qwe', 0, 1, NULL, 0, 0)
INSERT [dbo].[Characters] ([CharacterId], [PlayerId], [CharacterName], [RaceId], [GenderId], [PortraitFileName], [Description], [About], [CurrentLocationId], [TierId], [IsRetired], [IsApproved], [Artist], [ClassString], [HasUnseenEdit], [IsDead], [BackupPortrayerName], [IsPlayerDemo], [IsRequestingRetire]) VALUES (4, N'3a34a24b-d413-4dc6-9e64-feb21d9376e7', N'pre test', 1, 1, N'default.jpg', N'testing 
pre', N'pre 
testing', 1, 1, 1, 1, NULL, N'taf', 0, 0, NULL, 0, 0)
INSERT [dbo].[Characters] ([CharacterId], [PlayerId], [CharacterName], [RaceId], [GenderId], [PortraitFileName], [Description], [About], [CurrentLocationId], [TierId], [IsRetired], [IsApproved], [Artist], [ClassString], [HasUnseenEdit], [IsDead], [BackupPortrayerName], [IsPlayerDemo], [IsRequestingRetire]) VALUES (5, N'3a34a24b-d413-4dc6-9e64-feb21d9376e7', N'adw', 1, 1, N'default.jpg', N'dadwwa
aw
d
aw
daw
da', N'awdawaw
wda
d
awd
ad
', 1, 1, 0, 1, NULL, N'a', 0, 0, NULL, 0, 0)
INSERT [dbo].[Characters] ([CharacterId], [PlayerId], [CharacterName], [RaceId], [GenderId], [PortraitFileName], [Description], [About], [CurrentLocationId], [TierId], [IsRetired], [IsApproved], [Artist], [ClassString], [HasUnseenEdit], [IsDead], [BackupPortrayerName], [IsPlayerDemo], [IsRequestingRetire]) VALUES (7, N'6ed5fd28-c488-4ba4-89b0-d1cf7afd292d', N'Kendra Allen', 2, 2, N'character-7.jpg', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Nam at lectus urna duis convallis convallis tellus. Bibendum est ultricies integer quis. Vitae sapien pellentesque habitant morbi tristique senectus et. Auctor neque vitae tempus quam pellentesque nec nam aliquam sem. Ultrices gravida dictum fusce ut placerat orci. Molestie a iaculis at erat pellentesque. Nec nam aliquam sem et tortor. Cras fermentum odio eu feugiat pretium nibh ipsum. Phasellus vestibulum lorem sed risus ultricies tristique nulla aliquet. Scelerisque in dictum non consectetur a. Quis ipsum suspendisse ultrices gravida dictum fusce ut placerat orci.

Malesuada proin libero nunc consequat. Mollis nunc sed id semper. Orci a scelerisque purus semper eget duis at. Arcu cursus vitae congue mauris rhoncus aenean vel elit scelerisque. Est ante in nibh mauris cursus mattis molestie. Lectus mauris ultrices eros in cursus turpis. Diam sit amet nisl suscipit adipiscing bibendum est. Quisque id diam vel quam elementum. Nisi quis eleifend quam adipiscing vitae proin sagittis. Amet porttitor eget dolor morbi non arcu risus quis.', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Sodales ut eu sem integer. Ultricies mi eget mauris pharetra et ultrices neque ornare aenean. Sit amet mauris commodo quis imperdiet massa tincidunt. Sed lectus vestibulum mattis ullamcorper velit sed. At ultrices mi tempus imperdiet nulla. Diam quam nulla porttitor massa id neque aliquam. Duis at tellus at urna condimentum mattis pellentesque. At tempor commodo ullamcorper a lacus. Accumsan in nisl nisi scelerisque eu ultrices. Velit dignissim sodales ut eu sem integer vitae. Id faucibus nisl tincidunt eget nullam non. Molestie at elementum eu facilisis sed odio morbi quis. Mauris cursus mattis molestie a. Dolor purus non enim praesent elementum facilisis leo vel fringilla. Amet purus gravida quis blandit turpis cursus in hac. Consequat interdum varius sit amet mattis vulputate enim nulla aliquet. Vitae et leo duis ut. Dignissim enim sit amet venenatis. Amet cursus sit amet dictum sit amet.

Senectus et netus et malesuada fames ac turpis egestas. Integer vitae justo eget magna fermentum. Ut sem nulla pharetra diam. Malesuada bibendum arcu vitae elementum curabitur. Aliquet eget sit amet tellus. Amet nisl suscipit adipiscing bibendum est ultricies. Leo vel orci porta non pulvinar neque. Sit amet massa vitae tortor condimentum lacinia. Et malesuada fames ac turpis egestas integer eget. Risus nullam eget felis eget nunc lobortis mattis aliquam. Et magnis dis parturient montes nascetur ridiculus mus mauris. Suspendisse sed nisi lacus sed viverra tellus. Sit amet consectetur adipiscing elit pellentesque habitant. Sed felis eget velit aliquet sagittis id consectetur purus. Nam libero justo laoreet sit amet cursus. Eget dolor morbi non arcu risus quis varius. Mi ipsum faucibus vitae aliquet nec ullamcorper sit amet risus. Consequat ac felis donec et odio pellentesque diam volutpat commodo. Aliquam sem et tortor consequat id porta nibh venenatis cras. Curabitur vitae nunc sed velit.

Posuere lorem ipsum dolor sit. Condimentum lacinia quis vel eros donec. Vel turpis nunc eget lorem dolor sed viverra. Porta nibh venenatis cras sed. Nunc faucibus a pellentesque sit amet. Gravida neque convallis a cras. Augue mauris augue neque gravida in fermentum et sollicitudin. Sed pulvinar proin gravida hendrerit. Mauris augue neque gravida in fermentum et sollicitudin. Bibendum arcu vitae elementum curabitur vitae nunc sed velit. Pharetra pharetra massa massa ultricies mi quis. Massa eget egestas purus viverra accumsan in.', 3, 2, 0, 0, N'Dan', N'weird', 0, 0, NULL, 0, 0)
INSERT [dbo].[Characters] ([CharacterId], [PlayerId], [CharacterName], [RaceId], [GenderId], [PortraitFileName], [Description], [About], [CurrentLocationId], [TierId], [IsRetired], [IsApproved], [Artist], [ClassString], [HasUnseenEdit], [IsDead], [BackupPortrayerName], [IsPlayerDemo], [IsRequestingRetire]) VALUES (10, N'99a29eac-aafa-44ce-8349-c8868ba0e3aa', N'con', 5, 1, NULL, N'con', N'con', 4, 3, 0, 0, NULL, N'con', 0, 0, NULL, 0, 0)
SET IDENTITY_INSERT [dbo].[Characters] OFF
SET IDENTITY_INSERT [dbo].[CharOrgs] ON 

INSERT [dbo].[CharOrgs] ([CharOrgsId], [CharId], [OrgId], [BlurbOrgPage], [IsPublic], [IsCurrent], [KatherineApproved]) VALUES (2, 3, 5, N'words for testing ', 1, 1, 1)
INSERT [dbo].[CharOrgs] ([CharOrgsId], [CharId], [OrgId], [BlurbOrgPage], [IsPublic], [IsCurrent], [KatherineApproved]) VALUES (3, 4, 5, N'dis is not a public membership shhhhh', 0, 1, 1)
INSERT [dbo].[CharOrgs] ([CharOrgsId], [CharId], [OrgId], [BlurbOrgPage], [IsPublic], [IsCurrent], [KatherineApproved]) VALUES (4, 2, 6, N'current mem ND PUBLIC', 1, 1, 1)
INSERT [dbo].[CharOrgs] ([CharOrgsId], [CharId], [OrgId], [BlurbOrgPage], [IsPublic], [IsCurrent], [KatherineApproved]) VALUES (5, 1, 6, N'not public mem', 0, 1, 1)
INSERT [dbo].[CharOrgs] ([CharOrgsId], [CharId], [OrgId], [BlurbOrgPage], [IsPublic], [IsCurrent], [KatherineApproved]) VALUES (6, 4, 6, N'not current', 1, 0, 1)
INSERT [dbo].[CharOrgs] ([CharOrgsId], [CharId], [OrgId], [BlurbOrgPage], [IsPublic], [IsCurrent], [KatherineApproved]) VALUES (7, 5, 10, N'huhuhuhu', 0, 1, 1)
SET IDENTITY_INSERT [dbo].[CharOrgs] OFF
SET IDENTITY_INSERT [dbo].[CharSecrets] ON 

INSERT [dbo].[CharSecrets] ([CharSecretId], [CharId], [SecretId]) VALUES (1, 1, 1)
INSERT [dbo].[CharSecrets] ([CharSecretId], [CharId], [SecretId]) VALUES (2, 1, 4)
INSERT [dbo].[CharSecrets] ([CharSecretId], [CharId], [SecretId]) VALUES (5, 2, 5)
INSERT [dbo].[CharSecrets] ([CharSecretId], [CharId], [SecretId]) VALUES (6, 1, 2)
SET IDENTITY_INSERT [dbo].[CharSecrets] OFF
SET IDENTITY_INSERT [dbo].[Class] ON 

INSERT [dbo].[Class] ([ClassId], [ClassName], [IsPlayerEnabled]) VALUES (1, N'Barbarian', 1)
INSERT [dbo].[Class] ([ClassId], [ClassName], [IsPlayerEnabled]) VALUES (2, N'Bard', 1)
INSERT [dbo].[Class] ([ClassId], [ClassName], [IsPlayerEnabled]) VALUES (3, N'Cleric', 1)
INSERT [dbo].[Class] ([ClassId], [ClassName], [IsPlayerEnabled]) VALUES (4, N'Druid', 1)
INSERT [dbo].[Class] ([ClassId], [ClassName], [IsPlayerEnabled]) VALUES (5, N'Fighter', 1)
INSERT [dbo].[Class] ([ClassId], [ClassName], [IsPlayerEnabled]) VALUES (6, N'Monk', 1)
INSERT [dbo].[Class] ([ClassId], [ClassName], [IsPlayerEnabled]) VALUES (7, N'Paladin', 1)
INSERT [dbo].[Class] ([ClassId], [ClassName], [IsPlayerEnabled]) VALUES (8, N'Ranger', 1)
INSERT [dbo].[Class] ([ClassId], [ClassName], [IsPlayerEnabled]) VALUES (9, N'Rogue', 1)
INSERT [dbo].[Class] ([ClassId], [ClassName], [IsPlayerEnabled]) VALUES (10, N'Sorcerer', 1)
INSERT [dbo].[Class] ([ClassId], [ClassName], [IsPlayerEnabled]) VALUES (11, N'Warlock', 1)
INSERT [dbo].[Class] ([ClassId], [ClassName], [IsPlayerEnabled]) VALUES (12, N'Wizard', 1)
INSERT [dbo].[Class] ([ClassId], [ClassName], [IsPlayerEnabled]) VALUES (13, N'It''s a GOD!', 0)
INSERT [dbo].[Class] ([ClassId], [ClassName], [IsPlayerEnabled]) VALUES (14, N'It''s a GOD!', 0)
SET IDENTITY_INSERT [dbo].[Class] OFF
SET IDENTITY_INSERT [dbo].[ClassNPCs] ON 

INSERT [dbo].[ClassNPCs] ([ClassNpcId], [ClassId], [NpcId], [ClassOrder]) VALUES (2, 5, 2, NULL)
INSERT [dbo].[ClassNPCs] ([ClassNpcId], [ClassId], [NpcId], [ClassOrder]) VALUES (3, 9, 2, NULL)
INSERT [dbo].[ClassNPCs] ([ClassNpcId], [ClassId], [NpcId], [ClassOrder]) VALUES (4, 5, 26, 1)
INSERT [dbo].[ClassNPCs] ([ClassNpcId], [ClassId], [NpcId], [ClassOrder]) VALUES (5, 6, 26, NULL)
INSERT [dbo].[ClassNPCs] ([ClassNpcId], [ClassId], [NpcId], [ClassOrder]) VALUES (6, 4, 27, NULL)
INSERT [dbo].[ClassNPCs] ([ClassNpcId], [ClassId], [NpcId], [ClassOrder]) VALUES (7, 5, 27, NULL)
SET IDENTITY_INSERT [dbo].[ClassNPCs] OFF
SET IDENTITY_INSERT [dbo].[Events] ON 

INSERT [dbo].[Events] ([EventId], [InfoId], [IsHistory], [AboutText], [NormalParticipants], [DateMonth], [DateSeason]) VALUES (3, 78, 0, N'Nada', N'Nada', 1, NULL)
INSERT [dbo].[Events] ([EventId], [InfoId], [IsHistory], [AboutText], [NormalParticipants], [DateMonth], [DateSeason]) VALUES (4, 81, 1, N'<p>adwadw</p>', N'<p>wdd</p>', 1, NULL)
INSERT [dbo].[Events] ([EventId], [InfoId], [IsHistory], [AboutText], [NormalParticipants], [DateMonth], [DateSeason]) VALUES (5, 82, 0, N'Nada', N'Nada', 1, NULL)
INSERT [dbo].[Events] ([EventId], [InfoId], [IsHistory], [AboutText], [NormalParticipants], [DateMonth], [DateSeason]) VALUES (6, 83, 0, N'Nada', N'Nada', 1, NULL)
INSERT [dbo].[Events] ([EventId], [InfoId], [IsHistory], [AboutText], [NormalParticipants], [DateMonth], [DateSeason]) VALUES (7, 92, 0, N'Nada', N'Nada', 2, NULL)
INSERT [dbo].[Events] ([EventId], [InfoId], [IsHistory], [AboutText], [NormalParticipants], [DateMonth], [DateSeason]) VALUES (8, 101, 0, N'Nada', N'Nada', 11, 1)
INSERT [dbo].[Events] ([EventId], [InfoId], [IsHistory], [AboutText], [NormalParticipants], [DateMonth], [DateSeason]) VALUES (9, 105, 0, N'Nada', N'Nada', 3, 4)
SET IDENTITY_INSERT [dbo].[Events] OFF
SET IDENTITY_INSERT [dbo].[Gender] ON 

INSERT [dbo].[Gender] ([GenderId], [GenderName], [IconName]) VALUES (1, N'Female', N'venus')
INSERT [dbo].[Gender] ([GenderId], [GenderName], [IconName]) VALUES (2, N'Male', N'mars')
INSERT [dbo].[Gender] ([GenderId], [GenderName], [IconName]) VALUES (3, N'Bear', NULL)
SET IDENTITY_INSERT [dbo].[Gender] OFF
SET IDENTITY_INSERT [dbo].[Infos] ON 

INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (2, 2, 2, N'empty', N'Cassandra', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (4, 3, 2, N'empty', N'The de Rolo Family', 1, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (5, 4, 1, N'empty', N'Whitestone', 1, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (6, 4, 2, N'empty', N'Greyskull Keep', 1, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (7, 7, 1, N'"The List" was Percival de Rolo''s original pepperbox pistol, and the first gun he ever created', N'The List', 1, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (8, 2, 3, N'things', N'Kendra', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (9, 2, 4, N'things', N'arg', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (10, 2, 5, N'rhehrd', N'efe', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (11, 2, 6, N'asfeasfse', N'Nobel', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (12, 2, 7, N'fasdfafa', N'Customer', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (13, 2, 8, N'efefezfed', N'rageage', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (14, 2, 9, N'filler', N'Kendra', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (15, 2, 10, N'filler', N'Kendra', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (16, 2, 11, N'things', N'stug', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (17, 2, 12, N'things', N'please ', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (18, 2, 13, N'things', N'ascas', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (19, 2, 14, N'things', N'fzdf', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (20, 2, 15, N'things', N'double pic enter', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (21, 2, 16, N'things', N'one null', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (22, 2, 17, N'things', N'both null', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (23, 2, 19, N'things', N'opps', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (24, 7, 2, N'blurb', N'unicorn', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (25, 5, 1, N'riftTest', N'RiftTest', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (26, 7, 3, N'a test a all ind features but only 1 rich editor', N'edit test', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (27, 7, 4, N'everything together all the features of this info type', N'true test', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (28, 1, 1, N'blrub', N'lore full test', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (29, 4, 3, N'a region', N'Tal''Dorei', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (30, 7, 5, N'qwd', N'wq', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (31, 7, 6, N'dssdfe', N'Kendra', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (32, 7, 8, N'efaw', N'efa', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (34, 7, 10, N'save', N'save', 1, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (35, 4, 4, N'wef', N'complex create test', 1, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (36, 4, 5, N'awd', N'wda', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (37, 4, 6, N'awd', N'tag test', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (38, 4, 7, N'wef', N'weff', 1, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (39, 4, 8, N'adw', N'wda', 1, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (40, 4, 9, N'adw', N'awd', 1, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (41, 5, 2, N'ef', N'fe', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (42, 5, 3, N'wef', N'ef', 1, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (43, 5, 4, N'wfeef', N'test of being wrong', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (44, 5, 5, N'ru', N'test of being wrong', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (45, 5, 6, N'as', N'asso', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (46, 5, 7, N'awdawdawd', N'forgery', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (47, 5, 8, N'afewfwea', N'modal test', 1, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (48, 5, 9, N'nada test', N'nadatest ', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (49, 5, 10, N'length look test', N'length look test', 1, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (50, 5, 11, N'1 nada test', N'1 nada test', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (51, 5, 12, N'Test da full make ', N'Test da full make ', 1, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (52, 5, 13, N'awd', N'awd', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (53, 5, 14, N'we', N'we', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (54, 5, 15, N'awd', N'awd', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (55, 5, 16, N'as', N'saaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa', 1, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (57, 4, 11, N'asso create test', N'asso create test', 1, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (58, 5, 17, N'fgdfgdfg', N'gdgdfg', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (59, 5, 18, N'Blurbityburblurb yiss', N'Crystal Grove, sort of', 1, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (61, 4, 12, N'wef', N'wef', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (62, 4, 13, N'asdasd', N'nada test', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (63, 1, 2, N'scd', N'sasdasd', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (64, 7, 11, N'dfasdf', N'sdf', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (65, 3, 4, N'as', N'ssssssssssssssssssssssssssssssnake', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (67, 3, 5, N'many add test', N'many add test', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (69, 1, 3, N'comprehensive test - tagseee', N'comprehensive test - tagooooooo', 1, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (70, 1, 4, N'comprehensive test - no tags unpub', N'comprehensive test - no tags unpub', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (72, 7, 13, N'blurb', N'name', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (73, 7, 14, N'item comprehensive test: bare miniiiiii', N'item comprehensive test: bare miniiiiii', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (74, 5, 20, N'wrath', N'frustration', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (75, 5, 21, N'unpub rift', N'unpub rift', 1, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (76, 5, 22, N'test', N'test', 1, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (78, 6, 3, N'another quick grab', N'another quick grab', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (79, 4, 14, N'The city of lifeu', N'Gambleu', 1, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (80, 4, 15, N'mintes', N'min tst', 1, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (81, 6, 4, N'event tester', N'event test filler', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (82, 6, 5, N'min test', N'min test', 1, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (83, 6, 6, N'blank create test', N'blank create test', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (84, 3, 6, N'org test', N'prg test', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (85, 3, 7, N'org min', N'org min', 1, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (86, 3, 8, N'skip test', N'skip tes', 1, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (87, 2, 26, N'The myth is known to some. Was  this young hero of ancient times truly ever real? A DEMO ', N'Azura Brown', 1, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (89, 2, 27, N'wee', N'weee', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (90, 2, 28, N'min test', N'min test', 1, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (91, 2, 29, N'skup test', N'skip tes', 1, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (92, 6, 7, N'e', N'e', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (93, 5, 23, N'Next to that "inn" that Edwin and Kore almost got kidnapped in, over the drain grate.', N'Aquiren rift, man-made rift', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (94, 4, 16, N'Inn within the Circle of Trade in Tolia', N'One-Eyed Horse', 1, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (95, 3, 10, N'Subtle but above-board group of stealthy types for hire', N'Vel group', 1, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (97, 2, 31, N's', N's', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (98, 5, 24, N's', N's', 1, 1)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (99, 7, 15, N's', N's', 0, 1)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (100, 3, 11, N's', N's', 0, 1)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (101, 6, 8, N's', N's', 0, 1)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (102, 1, 6, N's', N's', 0, 1)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (103, 4, 17, N's', N's', 0, 1)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (104, 3, 3, N'word', N'Test', 1, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (105, 6, 9, N'quick test', N'quick test', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (106, 1, 7, N'oh god, I miniumized data', N'oh god, I miniumized data', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (107, 7, 16, N'minized!', N'minized!', 0, 0)
INSERT [dbo].[Infos] ([InfoId], [InfoTypeId], [IdWithinType], [Blurb], [Name], [IsPublished], [IsSecret]) VALUES (108, 3, 12, N'YEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAH@!', N'Kendra Allen', 0, 0)
SET IDENTITY_INSERT [dbo].[Infos] OFF
SET IDENTITY_INSERT [dbo].[InfoTags] ON 

INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (4, 2, 4)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (9, 5, 1)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (10, 6, 1)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (12, 26, 3)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (13, 27, 1)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (14, 27, 4)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (15, 28, 2)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (16, 28, 4)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (17, 29, 1)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (18, 30, 2)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (19, 30, 4)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (20, 26, 2)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (21, 34, 3)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (22, 35, 2)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (23, 36, 2)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (24, 51, 2)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (25, 55, 2)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (26, 57, 2)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (27, 57, 4)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (28, 58, 1)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (29, 59, 2)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (33, 7, 3)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (34, 7, 4)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (35, 69, 2)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (38, 69, 1)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (39, 69, 4)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (41, 72, 7)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (43, 72, 4)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (44, 72, 6)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (47, 74, 7)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (48, 74, 1)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (49, 74, 2)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (52, 79, 7)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (53, 79, 1)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (54, 79, 6)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (57, 81, 7)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (58, 81, 1)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (59, 81, 2)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (60, 84, 5)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (61, 84, 6)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (62, 84, 7)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (63, 87, 5)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (64, 87, 8)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (67, 89, 5)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (68, 89, 6)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (69, 89, 7)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (70, 93, 6)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (71, 93, 9)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (72, 25, 6)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (73, 25, 7)
INSERT [dbo].[InfoTags] ([InfoTagId], [InfoId], [TagId]) VALUES (74, 25, 9)
SET IDENTITY_INSERT [dbo].[InfoTags] OFF
SET IDENTITY_INSERT [dbo].[InfoType] ON 

INSERT [dbo].[InfoType] ([InfoTypeId], [TypeName]) VALUES (1, N'Lore')
INSERT [dbo].[InfoType] ([InfoTypeId], [TypeName]) VALUES (2, N'NPC')
INSERT [dbo].[InfoType] ([InfoTypeId], [TypeName]) VALUES (3, N'Organization')
INSERT [dbo].[InfoType] ([InfoTypeId], [TypeName]) VALUES (4, N'Locale')
INSERT [dbo].[InfoType] ([InfoTypeId], [TypeName]) VALUES (5, N'Rift')
INSERT [dbo].[InfoType] ([InfoTypeId], [TypeName]) VALUES (6, N'Event')
INSERT [dbo].[InfoType] ([InfoTypeId], [TypeName]) VALUES (7, N'Item')
SET IDENTITY_INSERT [dbo].[InfoType] OFF
SET IDENTITY_INSERT [dbo].[Items] ON 

INSERT [dbo].[Items] ([ItemId], [InfoId], [PictureFileName], [DescriptionText], [PropertyText], [HistoryText], [Artist]) VALUES (1, 7, N'item-7.png', N'<p>The name of the gun came from the inscriptions on five of the six barrels showing the names of people Percy sought vengeance against. The names on it were:<sup id="cite_ref-2" class="reference"><a href="https://criticalrole.fandom.com/wiki/The_List#cite_note-2">[1]</a></sup></p>
<ul>
<li><a title="Sylas Briarwood" href="https://criticalrole.fandom.com/wiki/Sylas_Briarwood">Lord Briarwood</a></li>
<li><a title="Delilah Briarwood" href="https://criticalrole.fandom.com/wiki/Delilah_Briarwood">Lady Briarwood</a></li>
<li><a title="Anna Ripley" href="https://criticalrole.fandom.com/wiki/Anna_Ripley">Doctor Ripley</a></li>
<li><a title="Kerrion Stonefell" href="https://criticalrole.fandom.com/wiki/Kerrion_Stonefell">Sir Kerrion Stonefell</a></li>
<li><a title="Professor Anders" href="https://criticalrole.fandom.com/wiki/Professor_Anders">Professor Anders</a></li>
</ul>
<p>The sixth barrel was originally blank, but after Percy returned to <a title="Whitestone" href="https://criticalrole.fandom.com/wiki/Whitestone">Whitestone</a>, the final name appeared: <a title="Cassandra de Rolo" href="https://criticalrole.fandom.com/wiki/Cassandra_de_Rolo">Cassandra de Rolo</a>, Percy''s younger sister.<sup id="cite_ref-3" class="reference"><a href="https://criticalrole.fandom.com/wiki/The_List#cite_note-3">[2]</a></sup></p>', N'<p>1st and 4th barrel deal additional 1d6 fire and cold damage respectively</p>', N'<p><strong>"The List"</strong> was <a title="Percival de Rolo" href="https://criticalrole.fandom.com/wiki/Percival_de_Rolo">Percival de Rolo</a>''s original pepperbox pistol, and the first gun he ever created under the influence of <a title="Orthax" href="https://criticalrole.fandom.com/wiki/Orthax">Orthax</a>.</p>
<p>It was eventually discovered that Orthax, the shadow demon Percy had unknowingly made a pact with, was bound to him through The List. This tie was severed when <a class="mw-redirect" title="Scanlan" href="https://criticalrole.fandom.com/wiki/Scanlan">Scanlan</a> took the pepperbox and threw it into a pit of acid.<sup id="cite_ref-4" class="reference"><a href="https://criticalrole.fandom.com/wiki/The_List#cite_note-4">[3]</a></sup></p>', N'Constant Scribbles')
INSERT [dbo].[Items] ([ItemId], [InfoId], [PictureFileName], [DescriptionText], [PropertyText], [HistoryText], [Artist]) VALUES (2, 24, N'item-2.jpg', N'des', N'pro', N'his', N'rt')
INSERT [dbo].[Items] ([ItemId], [InfoId], [PictureFileName], [DescriptionText], [PropertyText], [HistoryText], [Artist]) VALUES (3, 26, N'item-26.png', N'<p>aewf</p>', N'<p>efawaefw</p>', N'<p>weffewwef</p>', N'rtr')
INSERT [dbo].[Items] ([ItemId], [InfoId], [PictureFileName], [DescriptionText], [PropertyText], [HistoryText], [Artist]) VALUES (4, 27, N'item-27.png', N'<p>description</p>', N'<p><strong>properties</strong></p>', N'<p><em>history</em></p>', N'trtr')
INSERT [dbo].[Items] ([ItemId], [InfoId], [PictureFileName], [DescriptionText], [PropertyText], [HistoryText], [Artist]) VALUES (5, 30, N'default.jpg', N'<p>qwd</p>', N'<p>wd</p>', N'<p>qwd</p>', NULL)
INSERT [dbo].[Items] ([ItemId], [InfoId], [PictureFileName], [DescriptionText], [PropertyText], [HistoryText], [Artist]) VALUES (6, 31, N'default.jpg', N'feafe', N'efwfaw', N'wefwe', NULL)
INSERT [dbo].[Items] ([ItemId], [InfoId], [PictureFileName], [DescriptionText], [PropertyText], [HistoryText], [Artist]) VALUES (8, 32, N'default.jpg', N'aef', N'aef', N'afee', NULL)
INSERT [dbo].[Items] ([ItemId], [InfoId], [PictureFileName], [DescriptionText], [PropertyText], [HistoryText], [Artist]) VALUES (10, 34, N'default.jpg', N'<p>ave</p>', N'<p>dsv</p>', N'<p>asv</p>', NULL)
INSERT [dbo].[Items] ([ItemId], [InfoId], [PictureFileName], [DescriptionText], [PropertyText], [HistoryText], [Artist]) VALUES (11, 64, N'default.jpg', N'Nada', N'Nada', N'Nada', NULL)
INSERT [dbo].[Items] ([ItemId], [InfoId], [PictureFileName], [DescriptionText], [PropertyText], [HistoryText], [Artist]) VALUES (13, 72, N'item-72.jpg', N'<p>description</p>
<p>is&nbsp;</p>
<p>great</p>', N'<p>properties</p>
<p>is&nbsp;</p>
<p>great</p>', N'<p><strong>history</strong></p>
<p>is&nbsp;</p>
<p>great</p>', N'Maggie')
INSERT [dbo].[Items] ([ItemId], [InfoId], [PictureFileName], [DescriptionText], [PropertyText], [HistoryText], [Artist]) VALUES (14, 73, N'default.jpg', N'Nada', N'Nada', N'Nada', NULL)
INSERT [dbo].[Items] ([ItemId], [InfoId], [PictureFileName], [DescriptionText], [PropertyText], [HistoryText], [Artist]) VALUES (15, 99, N'default.jpg', N'Nada', N'Nada', N'Nada', NULL)
INSERT [dbo].[Items] ([ItemId], [InfoId], [PictureFileName], [DescriptionText], [PropertyText], [HistoryText], [Artist]) VALUES (16, 107, N'default.jpg', N'Nada', N'Nada', N'Nada', NULL)
SET IDENTITY_INSERT [dbo].[Items] OFF
SET IDENTITY_INSERT [dbo].[Journals] ON 

INSERT [dbo].[Journals] ([JournalId], [CharacterId], [OocDateWritten], [TheContent], [IsApproved], [HasUnseenEdit]) VALUES (1, 1, CAST(N'2020-08-19' AS Date), N'tests', 1, 0)
INSERT [dbo].[Journals] ([JournalId], [CharacterId], [OocDateWritten], [TheContent], [IsApproved], [HasUnseenEdit]) VALUES (2, 1, CAST(N'2020-08-19' AS Date), N'I''m blue da bu de da bu dye', 1, 0)
INSERT [dbo].[Journals] ([JournalId], [CharacterId], [OocDateWritten], [TheContent], [IsApproved], [HasUnseenEdit]) VALUES (3, 1, CAST(N'2020-09-20' AS Date), N'Should be first', 1, 0)
INSERT [dbo].[Journals] ([JournalId], [CharacterId], [OocDateWritten], [TheContent], [IsApproved], [HasUnseenEdit]) VALUES (4, 1, CAST(N'2020-08-20' AS Date), N'should be second', 1, 0)
INSERT [dbo].[Journals] ([JournalId], [CharacterId], [OocDateWritten], [TheContent], [IsApproved], [HasUnseenEdit]) VALUES (5, 1, CAST(N'2020-08-10' AS Date), N'should not be seen', 1, 0)
INSERT [dbo].[Journals] ([JournalId], [CharacterId], [OocDateWritten], [TheContent], [IsApproved], [HasUnseenEdit]) VALUES (6, 1, CAST(N'2020-08-19' AS Date), N'probably last ', 1, 0)
INSERT [dbo].[Journals] ([JournalId], [CharacterId], [OocDateWritten], [TheContent], [IsApproved], [HasUnseenEdit]) VALUES (7, 1, CAST(N'2020-08-19' AS Date), N'should be under review', 0, 0)
INSERT [dbo].[Journals] ([JournalId], [CharacterId], [OocDateWritten], [TheContent], [IsApproved], [HasUnseenEdit]) VALUES (8, 1, CAST(N'2020-08-20' AS Date), N'Lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum

Lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum

Lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum', 1, 0)
SET IDENTITY_INSERT [dbo].[Journals] OFF
SET IDENTITY_INSERT [dbo].[LocaleEvents] ON 

INSERT [dbo].[LocaleEvents] ([LocaleEventId], [LocaleId], [EventId]) VALUES (7, 1, 4)
INSERT [dbo].[LocaleEvents] ([LocaleEventId], [LocaleId], [EventId]) VALUES (8, 14, 4)
INSERT [dbo].[LocaleEvents] ([LocaleEventId], [LocaleId], [EventId]) VALUES (9, 15, 4)
SET IDENTITY_INSERT [dbo].[LocaleEvents] OFF
SET IDENTITY_INSERT [dbo].[LocaleLevel] ON 

INSERT [dbo].[LocaleLevel] ([LocaleLevelId], [LocaleName]) VALUES (1, N'Region')
INSERT [dbo].[LocaleLevel] ([LocaleLevelId], [LocaleName]) VALUES (2, N'City')
INSERT [dbo].[LocaleLevel] ([LocaleLevelId], [LocaleName]) VALUES (3, N'POI')
SET IDENTITY_INSERT [dbo].[LocaleLevel] OFF
SET IDENTITY_INSERT [dbo].[Locales] ON 

INSERT [dbo].[Locales] ([LocaleId], [InfoId], [LevelOfLocaleId], [RegionId], [ClosestCityId], [CouncilDelegateId], [Appointed], [Environment], [About], [AvgLifestyle]) VALUES (1, 5, 2, NULL, NULL, NULL, N'<p>empty</p>', N'<p>empty</p>', N'<p>Whitestone is a city-state in the Alabaster Sierras mountain range on the northeastern edge of Tal''Dorei''s continent. It is ruled from Whitestone Castle, the home of the de Rolo family after it was reclaimed from the Briarwoods.</p>
<p>Whitestone now serves as the home and base of operations for several of the members of Vox Machina.</p>', N'<p>empty</p>')
INSERT [dbo].[Locales] ([LocaleId], [InfoId], [LevelOfLocaleId], [RegionId], [ClosestCityId], [CouncilDelegateId], [Appointed], [Environment], [About], [AvgLifestyle]) VALUES (2, 6, 3, NULL, NULL, NULL, N'<p>empty</p>', N'<p>empty</p>', N'<p>Greyskull Keep was the home of Vox Machina. It is located in Tal''Dorei, just outside the slums of Emon on the southern side.</p>
<p>After first saving the city of Emon, Sovereign Uriel Tal''Dorei III and the Council of Tal''Dorei rewarded them with the construction of Greyskull. It took six months to build. Following Vox Machina''s adventures in Whitestone, Sovereign Uriel agreed to make Greyskull Keep the Whitestone Embassy in Emon.</p>', N'<p>empty</p>')
INSERT [dbo].[Locales] ([LocaleId], [InfoId], [LevelOfLocaleId], [RegionId], [ClosestCityId], [CouncilDelegateId], [Appointed], [Environment], [About], [AvgLifestyle]) VALUES (3, 29, 1, NULL, NULL, 2, N'<p>stuff</p>', N'<p>thigns</p>', N'<p>ya</p>', N'<p>nada</p>')
INSERT [dbo].[Locales] ([LocaleId], [InfoId], [LevelOfLocaleId], [RegionId], [ClosestCityId], [CouncilDelegateId], [Appointed], [Environment], [About], [AvgLifestyle]) VALUES (4, 35, 2, NULL, NULL, NULL, N'<p>wef</p>', N'<p>wfe</p>', N'<p>wef</p>', N'<p>sdv</p>')
INSERT [dbo].[Locales] ([LocaleId], [InfoId], [LevelOfLocaleId], [RegionId], [ClosestCityId], [CouncilDelegateId], [Appointed], [Environment], [About], [AvgLifestyle]) VALUES (5, 36, 3, NULL, NULL, NULL, N'<p>awd</p>', N'<p>wd</p>', N'<p>awd</p>', N'<p>awd</p>')
INSERT [dbo].[Locales] ([LocaleId], [InfoId], [LevelOfLocaleId], [RegionId], [ClosestCityId], [CouncilDelegateId], [Appointed], [Environment], [About], [AvgLifestyle]) VALUES (6, 37, 1, NULL, NULL, NULL, N'<p>dsa</p>', N'<p>awd</p>', N'<p>awd</p>', N'<p>awd</p>')
INSERT [dbo].[Locales] ([LocaleId], [InfoId], [LevelOfLocaleId], [RegionId], [ClosestCityId], [CouncilDelegateId], [Appointed], [Environment], [About], [AvgLifestyle]) VALUES (7, 38, 1, NULL, NULL, NULL, N'<p>wef</p>', N'<p>wef</p>', N'<p>efw</p>', N'<p>wef</p>')
INSERT [dbo].[Locales] ([LocaleId], [InfoId], [LevelOfLocaleId], [RegionId], [ClosestCityId], [CouncilDelegateId], [Appointed], [Environment], [About], [AvgLifestyle]) VALUES (8, 39, 1, NULL, NULL, NULL, N'<p>aw</p>', N'<p>daw</p>', N'<p>awd</p>', N'<p>wda</p>')
INSERT [dbo].[Locales] ([LocaleId], [InfoId], [LevelOfLocaleId], [RegionId], [ClosestCityId], [CouncilDelegateId], [Appointed], [Environment], [About], [AvgLifestyle]) VALUES (9, 40, 1, NULL, NULL, NULL, N'<p>adw</p>', N'<p>awd</p>', N'<p>awd</p>', N'<p>adw</p>')
INSERT [dbo].[Locales] ([LocaleId], [InfoId], [LevelOfLocaleId], [RegionId], [ClosestCityId], [CouncilDelegateId], [Appointed], [Environment], [About], [AvgLifestyle]) VALUES (11, 57, 3, 6, 1, 3, N'<p>wadw</p>', N'<p>wdada</p>', N'<p>adwdwawd</p>', N'<p>wdawd</p>')
INSERT [dbo].[Locales] ([LocaleId], [InfoId], [LevelOfLocaleId], [RegionId], [ClosestCityId], [CouncilDelegateId], [Appointed], [Environment], [About], [AvgLifestyle]) VALUES (12, 61, 2, NULL, NULL, NULL, N'<p>awefwe</p>', N'<p>wfe</p>', N'<p>wef</p>', N'<p>fwe</p>')
INSERT [dbo].[Locales] ([LocaleId], [InfoId], [LevelOfLocaleId], [RegionId], [ClosestCityId], [CouncilDelegateId], [Appointed], [Environment], [About], [AvgLifestyle]) VALUES (13, 62, 3, NULL, NULL, NULL, N'Nada', N'Nada', N'Nada', N'Nada')
INSERT [dbo].[Locales] ([LocaleId], [InfoId], [LevelOfLocaleId], [RegionId], [ClosestCityId], [CouncilDelegateId], [Appointed], [Environment], [About], [AvgLifestyle]) VALUES (14, 79, 2, 3, 1, 2, N'Nada', N'<p><span style="font-weight: 400;">Gamble, off on it''s own at the southern tip of it''s region, is an odd balance between a free-for-all and a camraderie-oriented town. </span></p>', N'<p><span style="font-weight: 400;">The current leadership was set up by a small group of people who didn''t like how things were run in the former city of Camdor. Instead of leaving when they had the chance, the would-be heroes staged a long-game coup and a few rapid displacements of awful authority figures.&nbsp;</span></p>
<p>&nbsp;</p>
<p><span style="font-weight: 400;">The name Gamble is multi-layered; the original gamble of choosing to uproot "the Camdor ways", and the daily gamble of trying to bring a wounded city into better times. There are neighborhoods travellers are encouraged to enter only with heavy caution, and others where it seems some of the deepest struggles of the city are in the past.&nbsp;</span></p>
<p>&nbsp;</p>
<p><span style="font-weight: 400;">Many of Gamble''s people are more open-minded to forms of entertainment and engagement that would be seen as degenrative in other places. It''s known as well for it''s various forms of adrenaline-pumping pastimes and, for those in the know, it''s near complete lack of Fey Rifts.</span></p>', N'Nada')
INSERT [dbo].[Locales] ([LocaleId], [InfoId], [LevelOfLocaleId], [RegionId], [ClosestCityId], [CouncilDelegateId], [Appointed], [Environment], [About], [AvgLifestyle]) VALUES (15, 80, 3, NULL, NULL, NULL, N'Nada', N'Nada', N'Nada', N'Nada')
INSERT [dbo].[Locales] ([LocaleId], [InfoId], [LevelOfLocaleId], [RegionId], [ClosestCityId], [CouncilDelegateId], [Appointed], [Environment], [About], [AvgLifestyle]) VALUES (16, 94, 3, 7, 12, 5, N'Nada', N'Nada', N'<p>Copy/paste, copy/paste</p>', N'Nada')
INSERT [dbo].[Locales] ([LocaleId], [InfoId], [LevelOfLocaleId], [RegionId], [ClosestCityId], [CouncilDelegateId], [Appointed], [Environment], [About], [AvgLifestyle]) VALUES (17, 103, 2, NULL, NULL, NULL, N'Nada', N'Nada', N'Nada', N'Nada')
SET IDENTITY_INSERT [dbo].[Locales] OFF
SET IDENTITY_INSERT [dbo].[Lores] ON 

INSERT [dbo].[Lores] ([LoreId], [InfoId], [TheContent]) VALUES (1, 28, N'<p>this is all you&nbsp;</p>
<p>will every really<strong> need</strong></p>')
INSERT [dbo].[Lores] ([LoreId], [InfoId], [TheContent]) VALUES (2, 63, N'<p>asdasd</p>')
INSERT [dbo].[Lores] ([LoreId], [InfoId], [TheContent]) VALUES (3, 69, N'<p>comprehensive test - tags</p>
<p><strong>asdasd&nbsp;</strong></p>
<p><strong>a d<em>aw da d</em></strong></p>
<p><em>d ad a dwd</em></p>')
INSERT [dbo].[Lores] ([LoreId], [InfoId], [TheContent]) VALUES (4, 70, N'<p>comprehensive test - no tags unpub</p>
<p>&nbsp;</p>')
INSERT [dbo].[Lores] ([LoreId], [InfoId], [TheContent]) VALUES (6, 102, N'<p>s</p>')
INSERT [dbo].[Lores] ([LoreId], [InfoId], [TheContent]) VALUES (7, 106, N'<p>oh god, I miniumized data</p>')
SET IDENTITY_INSERT [dbo].[Lores] OFF
SET IDENTITY_INSERT [dbo].[Majorities] ON 

INSERT [dbo].[Majorities] ([MajorityId], [LocaleId], [RaceId], [IsFirst]) VALUES (2, 4, 2, 1)
INSERT [dbo].[Majorities] ([MajorityId], [LocaleId], [RaceId], [IsFirst]) VALUES (5, 1, 1, 1)
INSERT [dbo].[Majorities] ([MajorityId], [LocaleId], [RaceId], [IsFirst]) VALUES (6, 1, 2, 0)
INSERT [dbo].[Majorities] ([MajorityId], [LocaleId], [RaceId], [IsFirst]) VALUES (7, 5, 1, 1)
INSERT [dbo].[Majorities] ([MajorityId], [LocaleId], [RaceId], [IsFirst]) VALUES (8, 7, 1, 1)
INSERT [dbo].[Majorities] ([MajorityId], [LocaleId], [RaceId], [IsFirst]) VALUES (9, 8, 2, 1)
INSERT [dbo].[Majorities] ([MajorityId], [LocaleId], [RaceId], [IsFirst]) VALUES (10, 9, 1, 0)
INSERT [dbo].[Majorities] ([MajorityId], [LocaleId], [RaceId], [IsFirst]) VALUES (11, 11, 1, 1)
SET IDENTITY_INSERT [dbo].[Majorities] OFF
SET IDENTITY_INSERT [dbo].[NpcOrgs] ON 

INSERT [dbo].[NpcOrgs] ([NpcOrgId], [NpcId], [OrgId], [OrgOrder], [BlurbNpcPage], [BlurbOrgPage], [IsCurrent], [MemberOrder]) VALUES (3, 2, 2, NULL, NULL, NULL, 1, NULL)
INSERT [dbo].[NpcOrgs] ([NpcOrgId], [NpcId], [OrgId], [OrgOrder], [BlurbNpcPage], [BlurbOrgPage], [IsCurrent], [MemberOrder]) VALUES (7, 9, 3, 5, NULL, NULL, 1, NULL)
INSERT [dbo].[NpcOrgs] ([NpcOrgId], [NpcId], [OrgId], [OrgOrder], [BlurbNpcPage], [BlurbOrgPage], [IsCurrent], [MemberOrder]) VALUES (8, 10, 2, 3, NULL, NULL, 1, NULL)
INSERT [dbo].[NpcOrgs] ([NpcOrgId], [NpcId], [OrgId], [OrgOrder], [BlurbNpcPage], [BlurbOrgPage], [IsCurrent], [MemberOrder]) VALUES (12, 2, 5, 3, N'many add test', NULL, 1, NULL)
INSERT [dbo].[NpcOrgs] ([NpcOrgId], [NpcId], [OrgId], [OrgOrder], [BlurbNpcPage], [BlurbOrgPage], [IsCurrent], [MemberOrder]) VALUES (13, 6, 5, NULL, N'many add test', N'many add test', 1, 3)
INSERT [dbo].[NpcOrgs] ([NpcOrgId], [NpcId], [OrgId], [OrgOrder], [BlurbNpcPage], [BlurbOrgPage], [IsCurrent], [MemberOrder]) VALUES (14, 15, 5, NULL, NULL, NULL, 0, NULL)
INSERT [dbo].[NpcOrgs] ([NpcOrgId], [NpcId], [OrgId], [OrgOrder], [BlurbNpcPage], [BlurbOrgPage], [IsCurrent], [MemberOrder]) VALUES (15, 2, 6, 6, N'a blurb for cassandra', N'a blurb for org', 1, 12)
INSERT [dbo].[NpcOrgs] ([NpcOrgId], [NpcId], [OrgId], [OrgOrder], [BlurbNpcPage], [BlurbOrgPage], [IsCurrent], [MemberOrder]) VALUES (18, 27, 2, NULL, NULL, NULL, 1, NULL)
INSERT [dbo].[NpcOrgs] ([NpcOrgId], [NpcId], [OrgId], [OrgOrder], [BlurbNpcPage], [BlurbOrgPage], [IsCurrent], [MemberOrder]) VALUES (19, 29, 10, 7, N'!I''m the best one', N'Red shirt, he die soon', 1, 7)
SET IDENTITY_INSERT [dbo].[NpcOrgs] OFF
SET IDENTITY_INSERT [dbo].[NPCs] ON 

INSERT [dbo].[NPCs] ([NpcId], [InfoId], [Alias], [Quote], [PortraitFileName], [RaceId], [CrestFileName], [ApperanceText], [AboutText], [LastLocationId], [PortraitArtist], [CrestArtist], [IsDead], [GenderId]) VALUES (2, 2, N'Lady Cassandra Johanna von Musel Klossowski de Rolo, Cass, Guardian of Woven Stone', N'No. Cassandra is a De Rolo, and you took them away from me. And now we''re taking everything from you.', N'default.jpg', 1, NULL, N'Cassandra is a young woman in her late teens/early twenties. She has dark brown hair with a few streaks of white at her temples, most likely due to her mental and psychological hardship.', N'empty', 1, NULL, NULL, 0, 1)
INSERT [dbo].[NPCs] ([NpcId], [InfoId], [Alias], [Quote], [PortraitFileName], [RaceId], [CrestFileName], [ApperanceText], [AboutText], [LastLocationId], [PortraitArtist], [CrestArtist], [IsDead], [GenderId]) VALUES (3, 8, N'Kinna', N'this is a test', N'default.jpg', 1, NULL, N'such test', N'so wow', 1, NULL, NULL, 0, 1)
INSERT [dbo].[NPCs] ([NpcId], [InfoId], [Alias], [Quote], [PortraitFileName], [RaceId], [CrestFileName], [ApperanceText], [AboutText], [LastLocationId], [PortraitArtist], [CrestArtist], [IsDead], [GenderId]) VALUES (4, 9, N'agr', N'arg', N'default.jpg', 1, NULL, N'arg', N'', 1, NULL, NULL, 0, 1)
INSERT [dbo].[NPCs] ([NpcId], [InfoId], [Alias], [Quote], [PortraitFileName], [RaceId], [CrestFileName], [ApperanceText], [AboutText], [LastLocationId], [PortraitArtist], [CrestArtist], [IsDead], [GenderId]) VALUES (5, 10, N'awfe', N'eafae', N'default.jpg', 1, NULL, N'aefaf', N'&lt;p&gt;here&lt;/p&gt;
&lt;p&gt;&lt;strong&gt;tests&lt;/strong&gt;&lt;/p&gt;', 1, NULL, NULL, 0, 1)
INSERT [dbo].[NPCs] ([NpcId], [InfoId], [Alias], [Quote], [PortraitFileName], [RaceId], [CrestFileName], [ApperanceText], [AboutText], [LastLocationId], [PortraitArtist], [CrestArtist], [IsDead], [GenderId]) VALUES (6, 11, N'jhgsd', N'rhtgrfsd', N'default.jpg', 1, NULL, N'gfsdaafgerd', N'<p>the butt</p>
<p><strong>clench</strong></p>', 1, NULL, NULL, 0, 1)
INSERT [dbo].[NPCs] ([NpcId], [InfoId], [Alias], [Quote], [PortraitFileName], [RaceId], [CrestFileName], [ApperanceText], [AboutText], [LastLocationId], [PortraitArtist], [CrestArtist], [IsDead], [GenderId]) VALUES (7, 12, N'jghjchj', N'gjcgjgj', N'default.jpg', 1, NULL, N'hfgchgfchfgh', N'<p>Here''s&nbsp;</p>
<p>the real test</p>
<p><strong><span style="text-decoration: underline;">dun dun!!!</span></strong></p>', 1, NULL, NULL, 0, 1)
INSERT [dbo].[NPCs] ([NpcId], [InfoId], [Alias], [Quote], [PortraitFileName], [RaceId], [CrestFileName], [ApperanceText], [AboutText], [LastLocationId], [PortraitArtist], [CrestArtist], [IsDead], [GenderId]) VALUES (8, 13, N'wdWDwd', N'FwDw', N'default.jpg', 1, NULL, N'DWDwDWD', N'<p>efzezefez&nbsp;</p>
<p>eF<br />eF efwezsfe</p>
<p>&nbsp;W</p>
<h5>fwekjafheowufhweo</h5>', 1, NULL, NULL, 0, 1)
INSERT [dbo].[NPCs] ([NpcId], [InfoId], [Alias], [Quote], [PortraitFileName], [RaceId], [CrestFileName], [ApperanceText], [AboutText], [LastLocationId], [PortraitArtist], [CrestArtist], [IsDead], [GenderId]) VALUES (9, 14, N'filler', N'filler', N'default.jpg', 1, NULL, N'filler', N'filler', 1, NULL, NULL, 0, 1)
INSERT [dbo].[NPCs] ([NpcId], [InfoId], [Alias], [Quote], [PortraitFileName], [RaceId], [CrestFileName], [ApperanceText], [AboutText], [LastLocationId], [PortraitArtist], [CrestArtist], [IsDead], [GenderId]) VALUES (10, 15, N'filler', N'filler', N'default.jpg', 1, NULL, N'filler', N'filler', 1, NULL, NULL, 0, 1)
INSERT [dbo].[NPCs] ([NpcId], [InfoId], [Alias], [Quote], [PortraitFileName], [RaceId], [CrestFileName], [ApperanceText], [AboutText], [LastLocationId], [PortraitArtist], [CrestArtist], [IsDead], [GenderId]) VALUES (11, 16, N'things', N'yeah', N'default.jpg', 1, NULL, N'words', N'stuff', 1, NULL, NULL, 0, 1)
INSERT [dbo].[NPCs] ([NpcId], [InfoId], [Alias], [Quote], [PortraitFileName], [RaceId], [CrestFileName], [ApperanceText], [AboutText], [LastLocationId], [PortraitArtist], [CrestArtist], [IsDead], [GenderId]) VALUES (12, 17, N'work', N'just ', N'npc-portrait-12', 1, NULL, N'please', N'work for me', 1, NULL, NULL, 0, 1)
INSERT [dbo].[NPCs] ([NpcId], [InfoId], [Alias], [Quote], [PortraitFileName], [RaceId], [CrestFileName], [ApperanceText], [AboutText], [LastLocationId], [PortraitArtist], [CrestArtist], [IsDead], [GenderId]) VALUES (13, 18, N'ascasc', N'ascas', N'npc-portrait-13', 1, NULL, N'ascasc', N'ascascsa', 1, NULL, NULL, 0, 1)
INSERT [dbo].[NPCs] ([NpcId], [InfoId], [Alias], [Quote], [PortraitFileName], [RaceId], [CrestFileName], [ApperanceText], [AboutText], [LastLocationId], [PortraitArtist], [CrestArtist], [IsDead], [GenderId]) VALUES (14, 19, N'faefe', N'awefe', N'npc-portrait-14.jpg', 1, NULL, N'aefae', N'eafe', 1, NULL, NULL, 0, 1)
INSERT [dbo].[NPCs] ([NpcId], [InfoId], [Alias], [Quote], [PortraitFileName], [RaceId], [CrestFileName], [ApperanceText], [AboutText], [LastLocationId], [PortraitArtist], [CrestArtist], [IsDead], [GenderId]) VALUES (15, 20, N'afeaef', N'eafae', N'npc-portrait-15.jpg', 1, N'npc-crest-15.jpg', N'dcsdcsa', N'scscs', 1, NULL, NULL, 0, 1)
INSERT [dbo].[NPCs] ([NpcId], [InfoId], [Alias], [Quote], [PortraitFileName], [RaceId], [CrestFileName], [ApperanceText], [AboutText], [LastLocationId], [PortraitArtist], [CrestArtist], [IsDead], [GenderId]) VALUES (16, 21, N'feafea', N'aefaew', N'default.jpg', 2, N'npc-crest-16.jpg', N'afeafe', N'eafea', 1, NULL, NULL, 0, 1)
INSERT [dbo].[NPCs] ([NpcId], [InfoId], [Alias], [Quote], [PortraitFileName], [RaceId], [CrestFileName], [ApperanceText], [AboutText], [LastLocationId], [PortraitArtist], [CrestArtist], [IsDead], [GenderId]) VALUES (17, 22, N'eafewafe', N'awefawef', N'default.jpg', 1, NULL, N'aefeafaw', N'aefawef', 1, NULL, NULL, 0, 1)
INSERT [dbo].[NPCs] ([NpcId], [InfoId], [Alias], [Quote], [PortraitFileName], [RaceId], [CrestFileName], [ApperanceText], [AboutText], [LastLocationId], [PortraitArtist], [CrestArtist], [IsDead], [GenderId]) VALUES (19, 23, N'woops', N'sorry', N'default.jpg', 2, NULL, N'<p>was</p>', N'<p>mistake</p>', NULL, NULL, NULL, 1, 1)
INSERT [dbo].[NPCs] ([NpcId], [InfoId], [Alias], [Quote], [PortraitFileName], [RaceId], [CrestFileName], [ApperanceText], [AboutText], [LastLocationId], [PortraitArtist], [CrestArtist], [IsDead], [GenderId]) VALUES (26, 87, N'Killshots the Corrupt, The Hero We Deserve, Adversity''s Child, Choking Hazard', N'Well, there is one place it certainly can''t ignore me: it''s throat. Send me there, and I''ll make sure it has no room to pay attention to any of you.', N'npc-portrait-87.jpg', 1, N'npc-crest-87.png', N'<p>A young woman, perhaps even a girl one might say, only 17 years of age by the end of the tales but of goliath proportions. Legends tell that Azura Brown was a human the size of a goliath standing nearly seven feet tall. The myth always told of a dirty, ragged appearance with nary an armor to speak of.&nbsp;</p>
<p>The myth mentions a plain face of just brown eyes and long brown hair brought up out of her eyes.</p>
<p>However, the end of the myth tells of a horrific scarring leaving Azura unrecognizable with burn marks and most of her hair missing. Most depictions have focused on the pre-scarring appearance.&nbsp;</p>', N'<p>The myth of Azura Brown is not one widely known, but none the less, it is still told around funeral pyres in times of struggle.&nbsp;</p>
<p>The exact beginning of Azura Brown vary from telling to telling, with some having her a child of a nobel, other''s a pauper, and still others a simple beggar. Regardless of the exact start they all tell the same tale. A happy life lived before some great tragedy left Azura a beggar on the run, parents dead. From there, Azura was known to have made a life scrapping in the back alleys. Protecting what few she trusted with hard fists. Already even at this young age, she was the size a full grown adult human and still had more growing.&nbsp;</p>
<p>It is this group that she protected that eventually would come to call themselves the Moon''s Shadow. There is debate over the origin of the name. Though even the existence of the Moon''s Shadow is debatable as to this day, none have ever claimed to be a member of such a group.&nbsp;</p>
<p>There are many tales of Azura blinding jumping into battle. Many vast adventures fighting gangs to supernatural creatures. A legend such as Azura stars in many tales. And the true path, if Azura Brown ever was a real person, is unknown.&nbsp;</p>
<p>What is consistent across all stories is her progressive growth. Becoming stronger, larger, protecting more, leading more. Her brashness, boldness, and extreme plans that never failed to put her into grave danger.</p>
<p>The tales in the middle may change but the last tales never do.&nbsp;</p>
<p>In this ancient, mythic times, a great beast of their torturous sun came down upon the land. It burned all it came upon. And when it came to her city, a plan was formed. While the mages performed a ritual to seal the beast away for good, Azura would lead the fighters to distract it. It is here that one of the most famous statements was made by her:</p>
<blockquote>
<p>"Well, there is one place it certainly can''t ignore me: it''s throat. Send me there, and I''ll make sure it has no room to pay attention to any of you."&nbsp;</p>
</blockquote>
<p>So, she was sent. A distraction great enough to buy time. However, the throat of the beast was where it burned the brightest; the greatest clerics in the land were barely able to bring Azura''s body back to host her soul. But deformed she was by her greatest plan. Terrible burn scars across the entire body and even her hair survived not. Azura took to wearing a long hooded cloak after this.&nbsp;</p>
<p>But adventure still she did. Protecting the land. Learning to come home to others. To promise to return.&nbsp;<br />Never once did she fall in battle.&nbsp;<br />They say she still wanders the land today. Bringing a plan of thrills.&nbsp;</p>', NULL, N'Rachel  Fakih', N'Kendra Allen', 0, 1)
INSERT [dbo].[NPCs] ([NpcId], [InfoId], [Alias], [Quote], [PortraitFileName], [RaceId], [CrestFileName], [ApperanceText], [AboutText], [LastLocationId], [PortraitArtist], [CrestArtist], [IsDead], [GenderId]) VALUES (27, 89, N'wee', N'wee', N'npc-portrait-89.png', NULL, N'npc-crest-89.png', N'<p>asd</p>', N'<p>adasdas</p>', 4, N'eaaa a', N'ep', 1, 3)
INSERT [dbo].[NPCs] ([NpcId], [InfoId], [Alias], [Quote], [PortraitFileName], [RaceId], [CrestFileName], [ApperanceText], [AboutText], [LastLocationId], [PortraitArtist], [CrestArtist], [IsDead], [GenderId]) VALUES (28, 90, N'Nada', N'min test', N'default.jpg', NULL, N'org_default.jpg', N'Nada', N'Nada', NULL, NULL, NULL, 0, 3)
INSERT [dbo].[NPCs] ([NpcId], [InfoId], [Alias], [Quote], [PortraitFileName], [RaceId], [CrestFileName], [ApperanceText], [AboutText], [LastLocationId], [PortraitArtist], [CrestArtist], [IsDead], [GenderId]) VALUES (29, 91, N'Nada', N'skip test', N'default.jpg', NULL, N'org_default.jpg', N'Nada', N'Nada', NULL, NULL, NULL, 0, 3)
INSERT [dbo].[NPCs] ([NpcId], [InfoId], [Alias], [Quote], [PortraitFileName], [RaceId], [CrestFileName], [ApperanceText], [AboutText], [LastLocationId], [PortraitArtist], [CrestArtist], [IsDead], [GenderId]) VALUES (31, 97, N'Nada', N's', N'default.jpg', NULL, N'org_default.jpg', N'Nada', N'Nada', NULL, NULL, NULL, 0, 3)
SET IDENTITY_INSERT [dbo].[NPCs] OFF
SET IDENTITY_INSERT [dbo].[OrgEvents] ON 

INSERT [dbo].[OrgEvents] ([OrgEventsId], [OrgId], [EventId], [Blurb]) VALUES (6, 2, 4, N'words and blurbs')
INSERT [dbo].[OrgEvents] ([OrgEventsId], [OrgId], [EventId], [Blurb]) VALUES (7, 3, 4, NULL)
INSERT [dbo].[OrgEvents] ([OrgEventsId], [OrgId], [EventId], [Blurb]) VALUES (10, 6, 5, NULL)
SET IDENTITY_INSERT [dbo].[OrgEvents] OFF
SET IDENTITY_INSERT [dbo].[Orgs] ON 

INSERT [dbo].[Orgs] ([OrgId], [InfoId], [IsPlayerEnabled], [SymbolFileName], [BaseLocationId], [AboutText], [Artist]) VALUES (2, 4, 1, N'default.jpg', NULL, N'<p>The de Rolo family is a noble family residing in the castle Whitestone.</p>
<h2>History</h2>
<p>Roughly two hundred years ago, the de Rolo family wrecked their ship in the Shearing Channel during their expedition from Wildemount. Survivors braved the wilderness of Alabaster Sierras for weeks before finding a glowing tree and building their home around it. Today this settlement has grown into a robust city, largely due to unique mineral called whitestone.</p>
<p>&nbsp;</p>', NULL)
INSERT [dbo].[Orgs] ([OrgId], [InfoId], [IsPlayerEnabled], [SymbolFileName], [BaseLocationId], [AboutText], [Artist]) VALUES (3, 104, 1, N'default.jpg', NULL, N'word', NULL)
INSERT [dbo].[Orgs] ([OrgId], [InfoId], [IsPlayerEnabled], [SymbolFileName], [BaseLocationId], [AboutText], [Artist]) VALUES (4, 65, 0, N'default.jpg', NULL, N'<p>aasaaas</p>', NULL)
INSERT [dbo].[Orgs] ([OrgId], [InfoId], [IsPlayerEnabled], [SymbolFileName], [BaseLocationId], [AboutText], [Artist]) VALUES (5, 67, 1, N'default.jpg', NULL, N'<p>stuff</p>', NULL)
INSERT [dbo].[Orgs] ([OrgId], [InfoId], [IsPlayerEnabled], [SymbolFileName], [BaseLocationId], [AboutText], [Artist]) VALUES (6, 84, 1, N'symbol-84.jpg', 1, N'<p>wrods woa f</p>
<p>&nbsp;foaif oie</p>
<p>&nbsp;io a eauilhf iwe</p>', N'Talesin Jaffee')
INSERT [dbo].[Orgs] ([OrgId], [InfoId], [IsPlayerEnabled], [SymbolFileName], [BaseLocationId], [AboutText], [Artist]) VALUES (7, 85, 0, N'default.jpg', NULL, N'Nada', NULL)
INSERT [dbo].[Orgs] ([OrgId], [InfoId], [IsPlayerEnabled], [SymbolFileName], [BaseLocationId], [AboutText], [Artist]) VALUES (8, 86, 0, N'default.jpg', NULL, N'Nada', NULL)
INSERT [dbo].[Orgs] ([OrgId], [InfoId], [IsPlayerEnabled], [SymbolFileName], [BaseLocationId], [AboutText], [Artist]) VALUES (10, 95, 1, N'default.jpg', NULL, N'<p>more blerdebler</p>', N'eeeeeeeeeeeeeeeeeeeeeeee')
INSERT [dbo].[Orgs] ([OrgId], [InfoId], [IsPlayerEnabled], [SymbolFileName], [BaseLocationId], [AboutText], [Artist]) VALUES (11, 100, 0, N'default.jpg', NULL, N'Nada', NULL)
INSERT [dbo].[Orgs] ([OrgId], [InfoId], [IsPlayerEnabled], [SymbolFileName], [BaseLocationId], [AboutText], [Artist]) VALUES (12, 108, 0, N'default.jpg', 2, N'Nada', NULL)
SET IDENTITY_INSERT [dbo].[Orgs] OFF
SET IDENTITY_INSERT [dbo].[Race] ON 

INSERT [dbo].[Race] ([RaceId], [RaceName], [IsPlayerEnabled]) VALUES (1, N'Human', 1)
INSERT [dbo].[Race] ([RaceId], [RaceName], [IsPlayerEnabled]) VALUES (2, N'Half-Elf', 1)
INSERT [dbo].[Race] ([RaceId], [RaceName], [IsPlayerEnabled]) VALUES (3, N'Dwarf', 1)
INSERT [dbo].[Race] ([RaceId], [RaceName], [IsPlayerEnabled]) VALUES (4, N'Bear', 0)
INSERT [dbo].[Race] ([RaceId], [RaceName], [IsPlayerEnabled]) VALUES (5, N'Tiefling', 1)
SET IDENTITY_INSERT [dbo].[Race] OFF
SET IDENTITY_INSERT [dbo].[Rifts] ON 

INSERT [dbo].[Rifts] ([RiftId], [InfoId], [Location], [Environment], [Hazards]) VALUES (1, 25, N'place', N'<p>thing</p>', N'<p>stuff</p>')
INSERT [dbo].[Rifts] ([RiftId], [InfoId], [Location], [Environment], [Hazards]) VALUES (2, 41, N'ef', N'<p>fe</p>', N'<p>fe</p>')
INSERT [dbo].[Rifts] ([RiftId], [InfoId], [Location], [Environment], [Hazards]) VALUES (3, 42, N'few', N'<p>wef</p>', N'<p>fwe</p>')
INSERT [dbo].[Rifts] ([RiftId], [InfoId], [Location], [Environment], [Hazards]) VALUES (4, 43, N'af;iwej', N'<p>reag</p>', N'<p>efwwef</p>')
INSERT [dbo].[Rifts] ([RiftId], [InfoId], [Location], [Environment], [Hazards]) VALUES (5, 44, N'ru', N'<p>ru</p>', N'<p>ru</p>')
INSERT [dbo].[Rifts] ([RiftId], [InfoId], [Location], [Environment], [Hazards]) VALUES (6, 45, N'as', N'<p>as</p>', N'<p>as</p>')
INSERT [dbo].[Rifts] ([RiftId], [InfoId], [Location], [Environment], [Hazards]) VALUES (7, 46, N'awd', N'<p>awda</p>', N'<p>wdaw</p>')
INSERT [dbo].[Rifts] ([RiftId], [InfoId], [Location], [Environment], [Hazards]) VALUES (8, 47, N'modal test', N'<p>wefawef</p>', N'<p>wefef</p>')
INSERT [dbo].[Rifts] ([RiftId], [InfoId], [Location], [Environment], [Hazards]) VALUES (9, 48, N'nada test', N'Nada', N'Nada')
INSERT [dbo].[Rifts] ([RiftId], [InfoId], [Location], [Environment], [Hazards]) VALUES (10, 49, N'length look test', N'<p>Compared to his fellow party, Percy is rather quiet, usually only speaking when necessary. However, he has shown he can be quite persuasive in social situations and is somewhat of a strategist within the group, as seen in "The Temple Showdown" (1x11).</p>
<p>Percy thinks of himself as being the only one capable of making adult decisions, despite his young age. He is also very pragmatic, with a consistent belief that the ends justify the means. This has led to moments of conflict between him and the party.</p>', N'<p>Percy was raised alongside his six siblings in Whitestone, where his branch of the de Rolo family ruled. Five years before the campaign began, the Briarwoods were invited in as guests, where they proceeded to seize control of Whitestone, murdering the de Rolos and those loyal to them. Percy was taken prisoner and tortured by Anna Ripley. After Percy fled Whitestone with the help of his sister Cassandra, he wandered aimlessly before setting out to avenge his family. Percy then constructed his first gun, the List. On five of its six barrels he inscribed a target of his vengeance: Lord Briarwood, Lady Briarwood, Dr. Ripley, Sir Kerrion Stonefell, and Professor Anders. Percy tracked Dr. Ripley for a year until he made his way to Stilben, where he attempted to kill her. He was captured by Dr. Ripley''s guards and imprisoned until he was freed by the rest of Vox Machina.</p>
<p>Because he is a gunslinger, Percy is the main long-ranged attack member of Vox Machina. He previously fought with his two guns, "The List", a six-barrel pepper-box, and "Bad News", a sniper rifle-esque device. The List has since been destroyed, and Percy has acquired two new weapons, both from Dr. Ripley: a four-shot pistol which he called "Retort", and another pepperbox known as "Animus". He also has experience in using melee weapons, usually a rapier or a longsword.</p>')
INSERT [dbo].[Rifts] ([RiftId], [InfoId], [Location], [Environment], [Hazards]) VALUES (11, 50, N'1 nada test', N'Nada', N'<p>Percy is a young human in his early twenties,.He originally had brown hair, but it turned white as a result of the trauma he suffered after the Briarwoods killed his family and he ran away with the aid of his sister Cassandra.</p>
<p>After joining the Slayer''s Take, Percy has the brand of the guild on his right arm.</p>')
INSERT [dbo].[Rifts] ([RiftId], [InfoId], [Location], [Environment], [Hazards]) VALUES (12, 51, N'Test da full make ', N'<p>We</p>', N'<p>fafaww</p>
<p><a title="link test" href="/Rifts/Create">link test</a></p>')
INSERT [dbo].[Rifts] ([RiftId], [InfoId], [Location], [Environment], [Hazards]) VALUES (13, 52, N'awd', N'<p><a href="Create">http://localhost:53604/Rifts/Create</a></p>', N'<p>awd</p>')
INSERT [dbo].[Rifts] ([RiftId], [InfoId], [Location], [Environment], [Hazards]) VALUES (14, 53, N'we', N'<p><a href="~Rifts/Create">/Rifts/Create</a></p>', N'Nada')
INSERT [dbo].[Rifts] ([RiftId], [InfoId], [Location], [Environment], [Hazards]) VALUES (15, 54, N'awd', N'<p><a href="/Rifts/Create">http://localhost:53604/Rifts/Create</a></p>', N'Nada')
INSERT [dbo].[Rifts] ([RiftId], [InfoId], [Location], [Environment], [Hazards]) VALUES (16, 55, N'as', N'<p>asa</p>', N'Nada')
INSERT [dbo].[Rifts] ([RiftId], [InfoId], [Location], [Environment], [Hazards]) VALUES (17, 58, N'gdfgs', N'<p>gdfdfgdfg</p>', N'Nada')
INSERT [dbo].[Rifts] ([RiftId], [InfoId], [Location], [Environment], [Hazards]) VALUES (18, 59, N'Tolia', N'<p>Forest, but sideways!</p>', N'<p>falling into the void</p>
<p>le cult</p>')
INSERT [dbo].[Rifts] ([RiftId], [InfoId], [Location], [Environment], [Hazards]) VALUES (20, 74, N'anger', N'<p>ttt</p>', N'<p>changed again and again and again</p>')
INSERT [dbo].[Rifts] ([RiftId], [InfoId], [Location], [Environment], [Hazards]) VALUES (21, 75, N'unpub rift', N'Nada', N'Nada')
INSERT [dbo].[Rifts] ([RiftId], [InfoId], [Location], [Environment], [Hazards]) VALUES (22, 76, N'test', N'<p>etst</p>', N'<p>etsset</p>')
INSERT [dbo].[Rifts] ([RiftId], [InfoId], [Location], [Environment], [Hazards]) VALUES (23, 93, N'Tolia: Circle of Protection', N'<p>Sideways forest (copy/paste from the chat lol)</p>', N'<p>~Literally a vertical forest<br />~Yawning colorful void<br />~Aquiren people and/or traps set by them<br />~Hungry perytons</p>')
INSERT [dbo].[Rifts] ([RiftId], [InfoId], [Location], [Environment], [Hazards]) VALUES (24, 98, N's', N'Nada', N'Nada')
SET IDENTITY_INSERT [dbo].[Rifts] OFF
SET IDENTITY_INSERT [dbo].[Rumors] ON 

INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (5, 2, 1, 1, N'me too!')
INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (8, 9, 1, 0, N'testing testing 1, 2, 3')
INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (10, 8, 1, 1, N'sweeeeet! I have soooo many things to say')
INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (11, 10, 1, 0, N'Really trying here')
INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (12, 9, 1, 0, N'testing testing 1, 2, 3')
INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (13, 2, 1, 0, N'2e')
INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (14, 11, 1, 0, N'testing testing 1, 2, 3')
INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (15, 8, 1, 0, N'testing testing 1, 2, 3')
INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (16, 2, 1, 0, N'sweeeeet! I have soooo many things to say')
INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (17, 11, 1, 0, N'fwa')
INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (18, 11, 1, 0, N'testing testing 1, 2, 3')
INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (19, 11, 1, 0, N'testing testing 1, 2, 3')
INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (21, 12, 1, 0, N'sweeeeet! I have soooo many things to say')
INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (22, 12, 1, 0, N'please please please work')
INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (23, 12, 1, 0, N'sweeeeet! I have soooo many things to say')
INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (24, 12, 1, 0, N'I just want it to work')
INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (25, 13, 1, 0, N'testing testing 1, 2, 3')
INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (27, 9, 1, 0, N'this should work')
INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (28, 9, 1, 0, N'why are you like this')
INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (29, 12, 1, 0, N'gggghghhhbh')
INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (30, 10, 1, 0, N'efawfawefaw')
INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (32, 13, 1, 0, N'me too!')
INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (33, 16, 1, 0, N'teyinf ao hES')
INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (35, 13, 1, 0, N'wtf')
INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (37, 9, 1, 0, N'why two')
INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (39, 12, 1, 0, N'wtf')
INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (42, 18, 1, 0, N'why 2')
INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (43, 14, 1, 0, N'why aren''t you working')
INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (45, 12, 1, 0, N'tried')
INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (49, 2, 1, 0, N'words')
INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (51, 12, 1, 0, N'faefewafwe')
INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (56, 13, 1, 0, N'cvvbvbnbnbn')
INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (57, 19, 1, 0, N'testing testing 1, 2, 3')
INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (63, 2, 1, 0, N'why')
INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (65, 23, 1, 0, N'really why')
INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (73, 8, 1, 0, N'great... it''s submitting 2 now')
INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (75, 9, 1, 0, N'I forgot to change the partial view so sue me')
INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (79, 10, 1, 0, N'what makes weird load? Was it missing bracket?')
INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (80, 10, 1, 0, N'all good?')
INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (81, 12, 1, 0, N'where''s da numbers?')
INSERT [dbo].[Rumors] ([RumorsId], [RumorOfId], [AuthorId], [IsApproved], [RumorText]) VALUES (86, 28, 1, 1, N'Here we are a rumor on a lore!')
SET IDENTITY_INSERT [dbo].[Rumors] OFF
SET IDENTITY_INSERT [dbo].[Secrets] ON 

INSERT [dbo].[Secrets] ([SecretId], [IsAboutId], [TheContent]) VALUES (3, 2, N'cassdandra''s a vampire. Duh-duh!')
INSERT [dbo].[Secrets] ([SecretId], [IsAboutId], [TheContent]) VALUES (4, 2, N'cassandra''s test for match tag')
INSERT [dbo].[Secrets] ([SecretId], [IsAboutId], [TheContent]) VALUES (6, 7, N'be very quiet about this')
INSERT [dbo].[Secrets] ([SecretId], [IsAboutId], [TheContent]) VALUES (8, 87, N'Azura Brown was a werewolf. This is why she managed to survive all those years.')
INSERT [dbo].[Secrets] ([SecretId], [IsAboutId], [TheContent]) VALUES (10, 8, N'Kendra is Aneth''s patron')
SET IDENTITY_INSERT [dbo].[Secrets] OFF
SET IDENTITY_INSERT [dbo].[SecretSecretTags] ON 

INSERT [dbo].[SecretSecretTags] ([SstId], [SecretId], [SecretTagId]) VALUES (5, 3, 6)
INSERT [dbo].[SecretSecretTags] ([SstId], [SecretId], [SecretTagId]) VALUES (6, 4, 1)
INSERT [dbo].[SecretSecretTags] ([SstId], [SecretId], [SecretTagId]) VALUES (8, 6, 5)
INSERT [dbo].[SecretSecretTags] ([SstId], [SecretId], [SecretTagId]) VALUES (10, 8, 8)
INSERT [dbo].[SecretSecretTags] ([SstId], [SecretId], [SecretTagId]) VALUES (12, 10, 2)
SET IDENTITY_INSERT [dbo].[SecretSecretTags] OFF
SET IDENTITY_INSERT [dbo].[SecretTags] ON 

INSERT [dbo].[SecretTags] ([SecretTagId], [Name], [Description]) VALUES (1, N'Test', N'it''s a test')
INSERT [dbo].[SecretTags] ([SecretTagId], [Name], [Description]) VALUES (2, N'Pokemon', N'more tests')
INSERT [dbo].[SecretTags] ([SecretTagId], [Name], [Description]) VALUES (3, N'Voice', N'best wifi password')
INSERT [dbo].[SecretTags] ([SecretTagId], [Name], [Description]) VALUES (4, N'Do not use', N'don''t use it ')
INSERT [dbo].[SecretTags] ([SecretTagId], [Name], [Description]) VALUES (5, N'Invisible', N'nada')
INSERT [dbo].[SecretTags] ([SecretTagId], [Name], [Description]) VALUES (6, N'vampire', N'vampire''s only')
INSERT [dbo].[SecretTags] ([SecretTagId], [Name], [Description]) VALUES (7, N'Where''s the moon', N'anything pertaining to the real location of the moon')
INSERT [dbo].[SecretTags] ([SecretTagId], [Name], [Description]) VALUES (8, N'Demo', N'Specifically for the demo entries'' secrets')
SET IDENTITY_INSERT [dbo].[SecretTags] OFF
SET IDENTITY_INSERT [dbo].[Stories] ON 

INSERT [dbo].[Stories] ([StoryId], [IsAboutId], [DateTold], [CommissionedBy], [IsCannon], [TheContent], [Title]) VALUES (6, 25, CAST(N'2020-09-03' AS Date), NULL, 1, N'<p>dwadwaaDW</p>', N'stuf')
SET IDENTITY_INSERT [dbo].[Stories] OFF
SET IDENTITY_INSERT [dbo].[Tags] ON 

INSERT [dbo].[Tags] ([TagId], [TagName], [Description]) VALUES (1, N'Tal''Dorei', NULL)
INSERT [dbo].[Tags] ([TagId], [TagName], [Description]) VALUES (2, N'Wildemount', NULL)
INSERT [dbo].[Tags] ([TagId], [TagName], [Description]) VALUES (3, N'Vox Machina', NULL)
INSERT [dbo].[Tags] ([TagId], [TagName], [Description]) VALUES (4, N'de Rolo', NULL)
INSERT [dbo].[Tags] ([TagId], [TagName], [Description]) VALUES (5, N'Non-Canon', N'A tag for stories that are not canon')
INSERT [dbo].[Tags] ([TagId], [TagName], [Description]) VALUES (6, N'Fey', NULL)
INSERT [dbo].[Tags] ([TagId], [TagName], [Description]) VALUES (7, N'Villians', N'For all your bad guy needs')
INSERT [dbo].[Tags] ([TagId], [TagName], [Description]) VALUES (8, N'Demo', N'For demo entries')
INSERT [dbo].[Tags] ([TagId], [TagName], [Description]) VALUES (9, N'Aneth', N'Tiefling warlock, ritualist')
SET IDENTITY_INSERT [dbo].[Tags] OFF
SET IDENTITY_INSERT [dbo].[Tier] ON 

INSERT [dbo].[Tier] ([TierId], [TierName], [MinLevel], [MaxLevel]) VALUES (1, N'Low', 1, 4)
INSERT [dbo].[Tier] ([TierId], [TierName], [MinLevel], [MaxLevel]) VALUES (2, N'Mid', 5, 10)
INSERT [dbo].[Tier] ([TierId], [TierName], [MinLevel], [MaxLevel]) VALUES (3, N'High', 11, 16)
INSERT [dbo].[Tier] ([TierId], [TierName], [MinLevel], [MaxLevel]) VALUES (4, N'God', 17, 20)
SET IDENTITY_INSERT [dbo].[Tier] OFF
INSERT [dbo].[UserDetails] ([UserId], [DiscordName], [DiscordDiscriminator], [ConsentFileName], [CurrentCharacterId], [IsApproved]) VALUES (N'3a34a24b-d413-4dc6-9e64-feb21d9376e7', N'azura', 4933, N'KellenResume.pdf', 5, 1)
INSERT [dbo].[UserDetails] ([UserId], [DiscordName], [DiscordDiscriminator], [ConsentFileName], [CurrentCharacterId], [IsApproved]) VALUES (N'6ed5fd28-c488-4ba4-89b0-d1cf7afd292d', N'Kinna', 777, N'KellenResume.pdf', NULL, 1)
INSERT [dbo].[UserDetails] ([UserId], [DiscordName], [DiscordDiscriminator], [ConsentFileName], [CurrentCharacterId], [IsApproved]) VALUES (N'99a29eac-aafa-44ce-8349-c8868ba0e3aa', N'attention', 6969, N'attention_6969.pdf', NULL, 1)
INSERT [dbo].[UserDetails] ([UserId], [DiscordName], [DiscordDiscriminator], [ConsentFileName], [CurrentCharacterId], [IsApproved]) VALUES (N'af0db862-e296-4bf6-8e65-18672010853d', N'afkl', 343, N'afkl_343.pdf', NULL, 1)
INSERT [dbo].[UserDetails] ([UserId], [DiscordName], [DiscordDiscriminator], [ConsentFileName], [CurrentCharacterId], [IsApproved]) VALUES (N'cba92599-2886-4410-ac11-e5ad3781f76f', N'Orchid', 9999, N'KellenResume.pdf', 1, 1)
INSERT [dbo].[UserDetails] ([UserId], [DiscordName], [DiscordDiscriminator], [ConsentFileName], [CurrentCharacterId], [IsApproved]) VALUES (N'd6ab2049-54be-40a2-a251-d62561de57e6', N'usernametest', 59, NULL, NULL, 0)
INSERT [dbo].[UserDetails] ([UserId], [DiscordName], [DiscordDiscriminator], [ConsentFileName], [CurrentCharacterId], [IsApproved]) VALUES (N'e3d0890f-5978-49e9-be45-00a5a4b52f88', N'SparklyDM', 8174, NULL, NULL, 1)
INSERT [dbo].[UserDetails] ([UserId], [DiscordName], [DiscordDiscriminator], [ConsentFileName], [CurrentCharacterId], [IsApproved]) VALUES (N'f1615660-5e2a-4e7a-b389-328caa06a457', N'zerotest', 46, NULL, NULL, 0)
INSERT [dbo].[UserDetails] ([UserId], [DiscordName], [DiscordDiscriminator], [ConsentFileName], [CurrentCharacterId], [IsApproved]) VALUES (N'fc836dc2-59f4-4a0b-8c42-5dd2a8718b4f', N'Kendra', 42, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[VarietyOfInhabitants] ON 

INSERT [dbo].[VarietyOfInhabitants] ([VarietyOfInhabitantsId], [RiftId], [RaceId], [RaceOrder]) VALUES (1, 3, 1, 3)
INSERT [dbo].[VarietyOfInhabitants] ([VarietyOfInhabitantsId], [RiftId], [RaceId], [RaceOrder]) VALUES (2, 3, 2, 4)
INSERT [dbo].[VarietyOfInhabitants] ([VarietyOfInhabitantsId], [RiftId], [RaceId], [RaceOrder]) VALUES (4, 6, 2, 1)
INSERT [dbo].[VarietyOfInhabitants] ([VarietyOfInhabitantsId], [RiftId], [RaceId], [RaceOrder]) VALUES (5, 6, 1, 8)
INSERT [dbo].[VarietyOfInhabitants] ([VarietyOfInhabitantsId], [RiftId], [RaceId], [RaceOrder]) VALUES (6, 7, 1, 3)
INSERT [dbo].[VarietyOfInhabitants] ([VarietyOfInhabitantsId], [RiftId], [RaceId], [RaceOrder]) VALUES (7, 7, 2, 2)
INSERT [dbo].[VarietyOfInhabitants] ([VarietyOfInhabitantsId], [RiftId], [RaceId], [RaceOrder]) VALUES (8, 2, 1, 2)
INSERT [dbo].[VarietyOfInhabitants] ([VarietyOfInhabitantsId], [RiftId], [RaceId], [RaceOrder]) VALUES (9, 5, 1, NULL)
INSERT [dbo].[VarietyOfInhabitants] ([VarietyOfInhabitantsId], [RiftId], [RaceId], [RaceOrder]) VALUES (10, 8, 1, NULL)
INSERT [dbo].[VarietyOfInhabitants] ([VarietyOfInhabitantsId], [RiftId], [RaceId], [RaceOrder]) VALUES (11, 12, 1, 7)
INSERT [dbo].[VarietyOfInhabitants] ([VarietyOfInhabitantsId], [RiftId], [RaceId], [RaceOrder]) VALUES (12, 16, 2, 1)
INSERT [dbo].[VarietyOfInhabitants] ([VarietyOfInhabitantsId], [RiftId], [RaceId], [RaceOrder]) VALUES (13, 1, 1, 1)
INSERT [dbo].[VarietyOfInhabitants] ([VarietyOfInhabitantsId], [RiftId], [RaceId], [RaceOrder]) VALUES (14, 1, 2, 2)
INSERT [dbo].[VarietyOfInhabitants] ([VarietyOfInhabitantsId], [RiftId], [RaceId], [RaceOrder]) VALUES (15, 18, 1, NULL)
INSERT [dbo].[VarietyOfInhabitants] ([VarietyOfInhabitantsId], [RiftId], [RaceId], [RaceOrder]) VALUES (16, 18, 2, NULL)
INSERT [dbo].[VarietyOfInhabitants] ([VarietyOfInhabitantsId], [RiftId], [RaceId], [RaceOrder]) VALUES (17, 20, 1, 2)
INSERT [dbo].[VarietyOfInhabitants] ([VarietyOfInhabitantsId], [RiftId], [RaceId], [RaceOrder]) VALUES (21, 20, 4, 1)
INSERT [dbo].[VarietyOfInhabitants] ([VarietyOfInhabitantsId], [RiftId], [RaceId], [RaceOrder]) VALUES (22, 20, 5, 4)
INSERT [dbo].[VarietyOfInhabitants] ([VarietyOfInhabitantsId], [RiftId], [RaceId], [RaceOrder]) VALUES (23, 23, 2, 2)
INSERT [dbo].[VarietyOfInhabitants] ([VarietyOfInhabitantsId], [RiftId], [RaceId], [RaceOrder]) VALUES (24, 23, 1, 1)
INSERT [dbo].[VarietyOfInhabitants] ([VarietyOfInhabitantsId], [RiftId], [RaceId], [RaceOrder]) VALUES (25, 23, 5, 3)
SET IDENTITY_INSERT [dbo].[VarietyOfInhabitants] OFF
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]'))
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]'))
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]'))
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]'))
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Characters_Gender]') AND parent_object_id = OBJECT_ID(N'[dbo].[Characters]'))
ALTER TABLE [dbo].[Characters]  WITH CHECK ADD  CONSTRAINT [FK_Characters_Gender] FOREIGN KEY([GenderId])
REFERENCES [dbo].[Gender] ([GenderId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Characters_Gender]') AND parent_object_id = OBJECT_ID(N'[dbo].[Characters]'))
ALTER TABLE [dbo].[Characters] CHECK CONSTRAINT [FK_Characters_Gender]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Characters_Locales]') AND parent_object_id = OBJECT_ID(N'[dbo].[Characters]'))
ALTER TABLE [dbo].[Characters]  WITH CHECK ADD  CONSTRAINT [FK_Characters_Locales] FOREIGN KEY([CurrentLocationId])
REFERENCES [dbo].[Locales] ([LocaleId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Characters_Locales]') AND parent_object_id = OBJECT_ID(N'[dbo].[Characters]'))
ALTER TABLE [dbo].[Characters] CHECK CONSTRAINT [FK_Characters_Locales]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Characters_Race]') AND parent_object_id = OBJECT_ID(N'[dbo].[Characters]'))
ALTER TABLE [dbo].[Characters]  WITH CHECK ADD  CONSTRAINT [FK_Characters_Race] FOREIGN KEY([RaceId])
REFERENCES [dbo].[Race] ([RaceId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Characters_Race]') AND parent_object_id = OBJECT_ID(N'[dbo].[Characters]'))
ALTER TABLE [dbo].[Characters] CHECK CONSTRAINT [FK_Characters_Race]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Characters_Tier]') AND parent_object_id = OBJECT_ID(N'[dbo].[Characters]'))
ALTER TABLE [dbo].[Characters]  WITH CHECK ADD  CONSTRAINT [FK_Characters_Tier] FOREIGN KEY([TierId])
REFERENCES [dbo].[Tier] ([TierId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Characters_Tier]') AND parent_object_id = OBJECT_ID(N'[dbo].[Characters]'))
ALTER TABLE [dbo].[Characters] CHECK CONSTRAINT [FK_Characters_Tier]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Characters_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Characters]'))
ALTER TABLE [dbo].[Characters]  WITH CHECK ADD  CONSTRAINT [FK_Characters_Users] FOREIGN KEY([PlayerId])
REFERENCES [dbo].[UserDetails] ([UserId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Characters_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Characters]'))
ALTER TABLE [dbo].[Characters] CHECK CONSTRAINT [FK_Characters_Users]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CharOrgs_Characters]') AND parent_object_id = OBJECT_ID(N'[dbo].[CharOrgs]'))
ALTER TABLE [dbo].[CharOrgs]  WITH CHECK ADD  CONSTRAINT [FK_CharOrgs_Characters] FOREIGN KEY([CharId])
REFERENCES [dbo].[Characters] ([CharacterId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CharOrgs_Characters]') AND parent_object_id = OBJECT_ID(N'[dbo].[CharOrgs]'))
ALTER TABLE [dbo].[CharOrgs] CHECK CONSTRAINT [FK_CharOrgs_Characters]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CharOrgs_Orgs]') AND parent_object_id = OBJECT_ID(N'[dbo].[CharOrgs]'))
ALTER TABLE [dbo].[CharOrgs]  WITH CHECK ADD  CONSTRAINT [FK_CharOrgs_Orgs] FOREIGN KEY([OrgId])
REFERENCES [dbo].[Orgs] ([OrgId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CharOrgs_Orgs]') AND parent_object_id = OBJECT_ID(N'[dbo].[CharOrgs]'))
ALTER TABLE [dbo].[CharOrgs] CHECK CONSTRAINT [FK_CharOrgs_Orgs]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CharSecrets_Characters]') AND parent_object_id = OBJECT_ID(N'[dbo].[CharSecrets]'))
ALTER TABLE [dbo].[CharSecrets]  WITH CHECK ADD  CONSTRAINT [FK_CharSecrets_Characters] FOREIGN KEY([CharId])
REFERENCES [dbo].[Characters] ([CharacterId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CharSecrets_Characters]') AND parent_object_id = OBJECT_ID(N'[dbo].[CharSecrets]'))
ALTER TABLE [dbo].[CharSecrets] CHECK CONSTRAINT [FK_CharSecrets_Characters]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CharSecrets_SecretTags]') AND parent_object_id = OBJECT_ID(N'[dbo].[CharSecrets]'))
ALTER TABLE [dbo].[CharSecrets]  WITH CHECK ADD  CONSTRAINT [FK_CharSecrets_SecretTags] FOREIGN KEY([SecretId])
REFERENCES [dbo].[SecretTags] ([SecretTagId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CharSecrets_SecretTags]') AND parent_object_id = OBJECT_ID(N'[dbo].[CharSecrets]'))
ALTER TABLE [dbo].[CharSecrets] CHECK CONSTRAINT [FK_CharSecrets_SecretTags]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ClassNPCs_Class]') AND parent_object_id = OBJECT_ID(N'[dbo].[ClassNPCs]'))
ALTER TABLE [dbo].[ClassNPCs]  WITH CHECK ADD  CONSTRAINT [FK_ClassNPCs_Class] FOREIGN KEY([ClassId])
REFERENCES [dbo].[Class] ([ClassId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ClassNPCs_Class]') AND parent_object_id = OBJECT_ID(N'[dbo].[ClassNPCs]'))
ALTER TABLE [dbo].[ClassNPCs] CHECK CONSTRAINT [FK_ClassNPCs_Class]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ClassNPCs_NPCs]') AND parent_object_id = OBJECT_ID(N'[dbo].[ClassNPCs]'))
ALTER TABLE [dbo].[ClassNPCs]  WITH CHECK ADD  CONSTRAINT [FK_ClassNPCs_NPCs] FOREIGN KEY([NpcId])
REFERENCES [dbo].[NPCs] ([NpcId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ClassNPCs_NPCs]') AND parent_object_id = OBJECT_ID(N'[dbo].[ClassNPCs]'))
ALTER TABLE [dbo].[ClassNPCs] CHECK CONSTRAINT [FK_ClassNPCs_NPCs]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Events_Infos]') AND parent_object_id = OBJECT_ID(N'[dbo].[Events]'))
ALTER TABLE [dbo].[Events]  WITH CHECK ADD  CONSTRAINT [FK_Events_Infos] FOREIGN KEY([InfoId])
REFERENCES [dbo].[Infos] ([InfoId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Events_Infos]') AND parent_object_id = OBJECT_ID(N'[dbo].[Events]'))
ALTER TABLE [dbo].[Events] CHECK CONSTRAINT [FK_Events_Infos]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Infos_InfoType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Infos]'))
ALTER TABLE [dbo].[Infos]  WITH CHECK ADD  CONSTRAINT [FK_Infos_InfoType] FOREIGN KEY([InfoTypeId])
REFERENCES [dbo].[InfoType] ([InfoTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Infos_InfoType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Infos]'))
ALTER TABLE [dbo].[Infos] CHECK CONSTRAINT [FK_Infos_InfoType]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_InfoTags_Infos]') AND parent_object_id = OBJECT_ID(N'[dbo].[InfoTags]'))
ALTER TABLE [dbo].[InfoTags]  WITH CHECK ADD  CONSTRAINT [FK_InfoTags_Infos] FOREIGN KEY([InfoId])
REFERENCES [dbo].[Infos] ([InfoId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_InfoTags_Infos]') AND parent_object_id = OBJECT_ID(N'[dbo].[InfoTags]'))
ALTER TABLE [dbo].[InfoTags] CHECK CONSTRAINT [FK_InfoTags_Infos]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_InfoTags_Tags]') AND parent_object_id = OBJECT_ID(N'[dbo].[InfoTags]'))
ALTER TABLE [dbo].[InfoTags]  WITH CHECK ADD  CONSTRAINT [FK_InfoTags_Tags] FOREIGN KEY([TagId])
REFERENCES [dbo].[Tags] ([TagId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_InfoTags_Tags]') AND parent_object_id = OBJECT_ID(N'[dbo].[InfoTags]'))
ALTER TABLE [dbo].[InfoTags] CHECK CONSTRAINT [FK_InfoTags_Tags]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Items_Infos]') AND parent_object_id = OBJECT_ID(N'[dbo].[Items]'))
ALTER TABLE [dbo].[Items]  WITH CHECK ADD  CONSTRAINT [FK_Items_Infos] FOREIGN KEY([InfoId])
REFERENCES [dbo].[Infos] ([InfoId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Items_Infos]') AND parent_object_id = OBJECT_ID(N'[dbo].[Items]'))
ALTER TABLE [dbo].[Items] CHECK CONSTRAINT [FK_Items_Infos]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Journals_Characters]') AND parent_object_id = OBJECT_ID(N'[dbo].[Journals]'))
ALTER TABLE [dbo].[Journals]  WITH CHECK ADD  CONSTRAINT [FK_Journals_Characters] FOREIGN KEY([CharacterId])
REFERENCES [dbo].[Characters] ([CharacterId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Journals_Characters]') AND parent_object_id = OBJECT_ID(N'[dbo].[Journals]'))
ALTER TABLE [dbo].[Journals] CHECK CONSTRAINT [FK_Journals_Characters]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_LocaleEvents_Events]') AND parent_object_id = OBJECT_ID(N'[dbo].[LocaleEvents]'))
ALTER TABLE [dbo].[LocaleEvents]  WITH CHECK ADD  CONSTRAINT [FK_LocaleEvents_Events] FOREIGN KEY([EventId])
REFERENCES [dbo].[Events] ([EventId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_LocaleEvents_Events]') AND parent_object_id = OBJECT_ID(N'[dbo].[LocaleEvents]'))
ALTER TABLE [dbo].[LocaleEvents] CHECK CONSTRAINT [FK_LocaleEvents_Events]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_LocaleEvents_Locales]') AND parent_object_id = OBJECT_ID(N'[dbo].[LocaleEvents]'))
ALTER TABLE [dbo].[LocaleEvents]  WITH CHECK ADD  CONSTRAINT [FK_LocaleEvents_Locales] FOREIGN KEY([LocaleId])
REFERENCES [dbo].[Locales] ([LocaleId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_LocaleEvents_Locales]') AND parent_object_id = OBJECT_ID(N'[dbo].[LocaleEvents]'))
ALTER TABLE [dbo].[LocaleEvents] CHECK CONSTRAINT [FK_LocaleEvents_Locales]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Locales_Infos]') AND parent_object_id = OBJECT_ID(N'[dbo].[Locales]'))
ALTER TABLE [dbo].[Locales]  WITH CHECK ADD  CONSTRAINT [FK_Locales_Infos] FOREIGN KEY([InfoId])
REFERENCES [dbo].[Infos] ([InfoId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Locales_Infos]') AND parent_object_id = OBJECT_ID(N'[dbo].[Locales]'))
ALTER TABLE [dbo].[Locales] CHECK CONSTRAINT [FK_Locales_Infos]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Locales_LocaleLevel]') AND parent_object_id = OBJECT_ID(N'[dbo].[Locales]'))
ALTER TABLE [dbo].[Locales]  WITH CHECK ADD  CONSTRAINT [FK_Locales_LocaleLevel] FOREIGN KEY([LevelOfLocaleId])
REFERENCES [dbo].[LocaleLevel] ([LocaleLevelId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Locales_LocaleLevel]') AND parent_object_id = OBJECT_ID(N'[dbo].[Locales]'))
ALTER TABLE [dbo].[Locales] CHECK CONSTRAINT [FK_Locales_LocaleLevel]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Locales_Locales]') AND parent_object_id = OBJECT_ID(N'[dbo].[Locales]'))
ALTER TABLE [dbo].[Locales]  WITH CHECK ADD  CONSTRAINT [FK_Locales_Locales] FOREIGN KEY([RegionId])
REFERENCES [dbo].[Locales] ([LocaleId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Locales_Locales]') AND parent_object_id = OBJECT_ID(N'[dbo].[Locales]'))
ALTER TABLE [dbo].[Locales] CHECK CONSTRAINT [FK_Locales_Locales]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Locales_Locales1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Locales]'))
ALTER TABLE [dbo].[Locales]  WITH CHECK ADD  CONSTRAINT [FK_Locales_Locales1] FOREIGN KEY([ClosestCityId])
REFERENCES [dbo].[Locales] ([LocaleId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Locales_Locales1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Locales]'))
ALTER TABLE [dbo].[Locales] CHECK CONSTRAINT [FK_Locales_Locales1]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Locales_NPCs]') AND parent_object_id = OBJECT_ID(N'[dbo].[Locales]'))
ALTER TABLE [dbo].[Locales]  WITH CHECK ADD  CONSTRAINT [FK_Locales_NPCs] FOREIGN KEY([CouncilDelegateId])
REFERENCES [dbo].[NPCs] ([NpcId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Locales_NPCs]') AND parent_object_id = OBJECT_ID(N'[dbo].[Locales]'))
ALTER TABLE [dbo].[Locales] CHECK CONSTRAINT [FK_Locales_NPCs]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Lores_Infos]') AND parent_object_id = OBJECT_ID(N'[dbo].[Lores]'))
ALTER TABLE [dbo].[Lores]  WITH CHECK ADD  CONSTRAINT [FK_Lores_Infos] FOREIGN KEY([InfoId])
REFERENCES [dbo].[Infos] ([InfoId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Lores_Infos]') AND parent_object_id = OBJECT_ID(N'[dbo].[Lores]'))
ALTER TABLE [dbo].[Lores] CHECK CONSTRAINT [FK_Lores_Infos]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Majorities_Locales]') AND parent_object_id = OBJECT_ID(N'[dbo].[Majorities]'))
ALTER TABLE [dbo].[Majorities]  WITH CHECK ADD  CONSTRAINT [FK_Majorities_Locales] FOREIGN KEY([LocaleId])
REFERENCES [dbo].[Locales] ([LocaleId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Majorities_Locales]') AND parent_object_id = OBJECT_ID(N'[dbo].[Majorities]'))
ALTER TABLE [dbo].[Majorities] CHECK CONSTRAINT [FK_Majorities_Locales]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Majorities_Race]') AND parent_object_id = OBJECT_ID(N'[dbo].[Majorities]'))
ALTER TABLE [dbo].[Majorities]  WITH CHECK ADD  CONSTRAINT [FK_Majorities_Race] FOREIGN KEY([RaceId])
REFERENCES [dbo].[Race] ([RaceId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Majorities_Race]') AND parent_object_id = OBJECT_ID(N'[dbo].[Majorities]'))
ALTER TABLE [dbo].[Majorities] CHECK CONSTRAINT [FK_Majorities_Race]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_NpcOrgs_NPCs]') AND parent_object_id = OBJECT_ID(N'[dbo].[NpcOrgs]'))
ALTER TABLE [dbo].[NpcOrgs]  WITH CHECK ADD  CONSTRAINT [FK_NpcOrgs_NPCs] FOREIGN KEY([NpcId])
REFERENCES [dbo].[NPCs] ([NpcId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_NpcOrgs_NPCs]') AND parent_object_id = OBJECT_ID(N'[dbo].[NpcOrgs]'))
ALTER TABLE [dbo].[NpcOrgs] CHECK CONSTRAINT [FK_NpcOrgs_NPCs]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_NpcOrgs_Orgs]') AND parent_object_id = OBJECT_ID(N'[dbo].[NpcOrgs]'))
ALTER TABLE [dbo].[NpcOrgs]  WITH CHECK ADD  CONSTRAINT [FK_NpcOrgs_Orgs] FOREIGN KEY([OrgId])
REFERENCES [dbo].[Orgs] ([OrgId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_NpcOrgs_Orgs]') AND parent_object_id = OBJECT_ID(N'[dbo].[NpcOrgs]'))
ALTER TABLE [dbo].[NpcOrgs] CHECK CONSTRAINT [FK_NpcOrgs_Orgs]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_NPCs_Gender]') AND parent_object_id = OBJECT_ID(N'[dbo].[NPCs]'))
ALTER TABLE [dbo].[NPCs]  WITH CHECK ADD  CONSTRAINT [FK_NPCs_Gender] FOREIGN KEY([GenderId])
REFERENCES [dbo].[Gender] ([GenderId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_NPCs_Gender]') AND parent_object_id = OBJECT_ID(N'[dbo].[NPCs]'))
ALTER TABLE [dbo].[NPCs] CHECK CONSTRAINT [FK_NPCs_Gender]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_NPCs_Infos]') AND parent_object_id = OBJECT_ID(N'[dbo].[NPCs]'))
ALTER TABLE [dbo].[NPCs]  WITH CHECK ADD  CONSTRAINT [FK_NPCs_Infos] FOREIGN KEY([InfoId])
REFERENCES [dbo].[Infos] ([InfoId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_NPCs_Infos]') AND parent_object_id = OBJECT_ID(N'[dbo].[NPCs]'))
ALTER TABLE [dbo].[NPCs] CHECK CONSTRAINT [FK_NPCs_Infos]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_NPCs_Locales]') AND parent_object_id = OBJECT_ID(N'[dbo].[NPCs]'))
ALTER TABLE [dbo].[NPCs]  WITH CHECK ADD  CONSTRAINT [FK_NPCs_Locales] FOREIGN KEY([LastLocationId])
REFERENCES [dbo].[Locales] ([LocaleId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_NPCs_Locales]') AND parent_object_id = OBJECT_ID(N'[dbo].[NPCs]'))
ALTER TABLE [dbo].[NPCs] CHECK CONSTRAINT [FK_NPCs_Locales]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_NPCs_Race]') AND parent_object_id = OBJECT_ID(N'[dbo].[NPCs]'))
ALTER TABLE [dbo].[NPCs]  WITH CHECK ADD  CONSTRAINT [FK_NPCs_Race] FOREIGN KEY([RaceId])
REFERENCES [dbo].[Race] ([RaceId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_NPCs_Race]') AND parent_object_id = OBJECT_ID(N'[dbo].[NPCs]'))
ALTER TABLE [dbo].[NPCs] CHECK CONSTRAINT [FK_NPCs_Race]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OrgEvents_Events]') AND parent_object_id = OBJECT_ID(N'[dbo].[OrgEvents]'))
ALTER TABLE [dbo].[OrgEvents]  WITH CHECK ADD  CONSTRAINT [FK_OrgEvents_Events] FOREIGN KEY([EventId])
REFERENCES [dbo].[Events] ([EventId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OrgEvents_Events]') AND parent_object_id = OBJECT_ID(N'[dbo].[OrgEvents]'))
ALTER TABLE [dbo].[OrgEvents] CHECK CONSTRAINT [FK_OrgEvents_Events]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OrgEvents_Orgs]') AND parent_object_id = OBJECT_ID(N'[dbo].[OrgEvents]'))
ALTER TABLE [dbo].[OrgEvents]  WITH CHECK ADD  CONSTRAINT [FK_OrgEvents_Orgs] FOREIGN KEY([OrgId])
REFERENCES [dbo].[Orgs] ([OrgId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OrgEvents_Orgs]') AND parent_object_id = OBJECT_ID(N'[dbo].[OrgEvents]'))
ALTER TABLE [dbo].[OrgEvents] CHECK CONSTRAINT [FK_OrgEvents_Orgs]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Orgs_Infos]') AND parent_object_id = OBJECT_ID(N'[dbo].[Orgs]'))
ALTER TABLE [dbo].[Orgs]  WITH CHECK ADD  CONSTRAINT [FK_Orgs_Infos] FOREIGN KEY([InfoId])
REFERENCES [dbo].[Infos] ([InfoId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Orgs_Infos]') AND parent_object_id = OBJECT_ID(N'[dbo].[Orgs]'))
ALTER TABLE [dbo].[Orgs] CHECK CONSTRAINT [FK_Orgs_Infos]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Orgs_Locales]') AND parent_object_id = OBJECT_ID(N'[dbo].[Orgs]'))
ALTER TABLE [dbo].[Orgs]  WITH CHECK ADD  CONSTRAINT [FK_Orgs_Locales] FOREIGN KEY([BaseLocationId])
REFERENCES [dbo].[Locales] ([LocaleId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Orgs_Locales]') AND parent_object_id = OBJECT_ID(N'[dbo].[Orgs]'))
ALTER TABLE [dbo].[Orgs] CHECK CONSTRAINT [FK_Orgs_Locales]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rifts_Infos]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rifts]'))
ALTER TABLE [dbo].[Rifts]  WITH CHECK ADD  CONSTRAINT [FK_Rifts_Infos] FOREIGN KEY([InfoId])
REFERENCES [dbo].[Infos] ([InfoId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rifts_Infos]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rifts]'))
ALTER TABLE [dbo].[Rifts] CHECK CONSTRAINT [FK_Rifts_Infos]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rumors_Characters]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rumors]'))
ALTER TABLE [dbo].[Rumors]  WITH CHECK ADD  CONSTRAINT [FK_Rumors_Characters] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[Characters] ([CharacterId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rumors_Characters]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rumors]'))
ALTER TABLE [dbo].[Rumors] CHECK CONSTRAINT [FK_Rumors_Characters]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rumors_Infos]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rumors]'))
ALTER TABLE [dbo].[Rumors]  WITH CHECK ADD  CONSTRAINT [FK_Rumors_Infos] FOREIGN KEY([RumorOfId])
REFERENCES [dbo].[Infos] ([InfoId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rumors_Infos]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rumors]'))
ALTER TABLE [dbo].[Rumors] CHECK CONSTRAINT [FK_Rumors_Infos]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Secrets_Infos]') AND parent_object_id = OBJECT_ID(N'[dbo].[Secrets]'))
ALTER TABLE [dbo].[Secrets]  WITH CHECK ADD  CONSTRAINT [FK_Secrets_Infos] FOREIGN KEY([IsAboutId])
REFERENCES [dbo].[Infos] ([InfoId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Secrets_Infos]') AND parent_object_id = OBJECT_ID(N'[dbo].[Secrets]'))
ALTER TABLE [dbo].[Secrets] CHECK CONSTRAINT [FK_Secrets_Infos]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SecretSecretTags_Secrets]') AND parent_object_id = OBJECT_ID(N'[dbo].[SecretSecretTags]'))
ALTER TABLE [dbo].[SecretSecretTags]  WITH CHECK ADD  CONSTRAINT [FK_SecretSecretTags_Secrets] FOREIGN KEY([SecretId])
REFERENCES [dbo].[Secrets] ([SecretId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SecretSecretTags_Secrets]') AND parent_object_id = OBJECT_ID(N'[dbo].[SecretSecretTags]'))
ALTER TABLE [dbo].[SecretSecretTags] CHECK CONSTRAINT [FK_SecretSecretTags_Secrets]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SecretSecretTags_SecretTags]') AND parent_object_id = OBJECT_ID(N'[dbo].[SecretSecretTags]'))
ALTER TABLE [dbo].[SecretSecretTags]  WITH CHECK ADD  CONSTRAINT [FK_SecretSecretTags_SecretTags] FOREIGN KEY([SecretTagId])
REFERENCES [dbo].[SecretTags] ([SecretTagId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SecretSecretTags_SecretTags]') AND parent_object_id = OBJECT_ID(N'[dbo].[SecretSecretTags]'))
ALTER TABLE [dbo].[SecretSecretTags] CHECK CONSTRAINT [FK_SecretSecretTags_SecretTags]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Stories_Infos]') AND parent_object_id = OBJECT_ID(N'[dbo].[Stories]'))
ALTER TABLE [dbo].[Stories]  WITH CHECK ADD  CONSTRAINT [FK_Stories_Infos] FOREIGN KEY([IsAboutId])
REFERENCES [dbo].[Infos] ([InfoId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Stories_Infos]') AND parent_object_id = OBJECT_ID(N'[dbo].[Stories]'))
ALTER TABLE [dbo].[Stories] CHECK CONSTRAINT [FK_Stories_Infos]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryTags_Stories]') AND parent_object_id = OBJECT_ID(N'[dbo].[StoryTags]'))
ALTER TABLE [dbo].[StoryTags]  WITH CHECK ADD  CONSTRAINT [FK_StoryTags_Stories] FOREIGN KEY([StoryId])
REFERENCES [dbo].[Stories] ([StoryId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryTags_Stories]') AND parent_object_id = OBJECT_ID(N'[dbo].[StoryTags]'))
ALTER TABLE [dbo].[StoryTags] CHECK CONSTRAINT [FK_StoryTags_Stories]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryTags_Tags]') AND parent_object_id = OBJECT_ID(N'[dbo].[StoryTags]'))
ALTER TABLE [dbo].[StoryTags]  WITH CHECK ADD  CONSTRAINT [FK_StoryTags_Tags] FOREIGN KEY([TagId])
REFERENCES [dbo].[Tags] ([TagId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryTags_Tags]') AND parent_object_id = OBJECT_ID(N'[dbo].[StoryTags]'))
ALTER TABLE [dbo].[StoryTags] CHECK CONSTRAINT [FK_StoryTags_Tags]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Users_Characters]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserDetails]'))
ALTER TABLE [dbo].[UserDetails]  WITH CHECK ADD  CONSTRAINT [FK_Users_Characters] FOREIGN KEY([CurrentCharacterId])
REFERENCES [dbo].[Characters] ([CharacterId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Users_Characters]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserDetails]'))
ALTER TABLE [dbo].[UserDetails] CHECK CONSTRAINT [FK_Users_Characters]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_VarietyOfInhabitants_Race]') AND parent_object_id = OBJECT_ID(N'[dbo].[VarietyOfInhabitants]'))
ALTER TABLE [dbo].[VarietyOfInhabitants]  WITH CHECK ADD  CONSTRAINT [FK_VarietyOfInhabitants_Race] FOREIGN KEY([RaceId])
REFERENCES [dbo].[Race] ([RaceId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_VarietyOfInhabitants_Race]') AND parent_object_id = OBJECT_ID(N'[dbo].[VarietyOfInhabitants]'))
ALTER TABLE [dbo].[VarietyOfInhabitants] CHECK CONSTRAINT [FK_VarietyOfInhabitants_Race]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_VarietyOfInhabitants_Rifts]') AND parent_object_id = OBJECT_ID(N'[dbo].[VarietyOfInhabitants]'))
ALTER TABLE [dbo].[VarietyOfInhabitants]  WITH CHECK ADD  CONSTRAINT [FK_VarietyOfInhabitants_Rifts] FOREIGN KEY([RiftId])
REFERENCES [dbo].[Rifts] ([RiftId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_VarietyOfInhabitants_Rifts]') AND parent_object_id = OBJECT_ID(N'[dbo].[VarietyOfInhabitants]'))
ALTER TABLE [dbo].[VarietyOfInhabitants] CHECK CONSTRAINT [FK_VarietyOfInhabitants_Rifts]
GO
