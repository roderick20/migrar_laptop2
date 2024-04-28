USE [master]
GO
/****** Object:  Database [SealOperaciones2]    Script Date: 16/04/2024 11:15:09 ******/
CREATE DATABASE [SealOperaciones2]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SealOperaciones', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\SealOperaciones2.mdf' , SIZE = 401408KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SealOperaciones_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\SealOperaciones2_log.ldf' , SIZE = 1712128KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [SealOperaciones2] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SealOperaciones2].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SealOperaciones2] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SealOperaciones2] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SealOperaciones2] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SealOperaciones2] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SealOperaciones2] SET ARITHABORT OFF 
GO
ALTER DATABASE [SealOperaciones2] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SealOperaciones2] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SealOperaciones2] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SealOperaciones2] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SealOperaciones2] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SealOperaciones2] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SealOperaciones2] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SealOperaciones2] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SealOperaciones2] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SealOperaciones2] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SealOperaciones2] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SealOperaciones2] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SealOperaciones2] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SealOperaciones2] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SealOperaciones2] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SealOperaciones2] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SealOperaciones2] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SealOperaciones2] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SealOperaciones2] SET  MULTI_USER 
GO
ALTER DATABASE [SealOperaciones2] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SealOperaciones2] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SealOperaciones2] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SealOperaciones2] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SealOperaciones2] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SealOperaciones2] SET QUERY_STORE = OFF
GO
USE [SealOperaciones2]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [SealOperaciones2]
GO
/****** Object:  User [seal]    Script Date: 16/04/2024 11:15:11 ******/
CREATE USER [seal] FOR LOGIN [seal] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [maikol]    Script Date: 16/04/2024 11:15:11 ******/
CREATE USER [maikol] FOR LOGIN [maikol] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [seal]
GO
ALTER ROLE [db_owner] ADD MEMBER [maikol]
GO
/****** Object:  Schema [auth]    Script Date: 16/04/2024 11:15:12 ******/
CREATE SCHEMA [auth]
GO
/****** Object:  Table [auth].[empresa]    Script Date: 16/04/2024 11:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [auth].[empresa](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[uniqueid] [uniqueidentifier] NOT NULL,
	[nombre] [nvarchar](max) NOT NULL,
	[creado] [datetime] NOT NULL,
	[autor] [int] NOT NULL,
	[modificado] [datetime] NULL,
	[editor] [int] NULL,
 CONSTRAINT [PK_empresa] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [auth].[empresacontratos]    Script Date: 16/04/2024 11:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [auth].[empresacontratos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[uniqueid] [uniqueidentifier] NOT NULL,
	[empresaid] [int] NOT NULL,
	[fechaInicio] [datetime] NOT NULL,
	[fechaFin] [datetime] NOT NULL,
	[creado] [datetime] NOT NULL,
	[autor] [int] NOT NULL,
	[modificado] [datetime] NULL,
	[editor] [int] NULL,
 CONSTRAINT [PK_empresacontratos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [auth].[persona]    Script Date: 16/04/2024 11:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [auth].[persona](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[uniqueid] [uniqueidentifier] NOT NULL,
	[nombres] [nvarchar](max) NOT NULL,
	[codigo] [nvarchar](max) NULL,
	[color] [nvarchar](max) NULL,
	[login] [nvarchar](max) NULL,
	[tipodoc] [int] NULL,
	[telefono] [nvarchar](max) NULL,
	[password] [nvarchar](max) NULL,
	[correo] [nvarchar](max) NULL,
	[numerodoc] [nvarchar](max) NULL,
	[esgrupo] [bit] NULL,
	[esadmin] [bit] NULL,
	[creado] [datetime] NOT NULL,
	[autor] [int] NOT NULL,
	[modificado] [datetime] NULL,
	[editor] [int] NULL,
	[empresaid] [int] NULL,
	[imei] [nvarchar](max) NULL,
	[habilitado] [int] NULL,
 CONSTRAINT [PK_persona] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [auth].[personagrupo]    Script Date: 16/04/2024 11:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [auth].[personagrupo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[uniqueid] [uniqueidentifier] NOT NULL,
	[personaid] [int] NOT NULL,
	[grupoid] [int] NULL,
	[creado] [datetime] NOT NULL,
	[autor] [int] NOT NULL,
	[modificado] [datetime] NULL,
	[editor] [int] NULL,
 CONSTRAINT [PK_personagrupo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [auth].[recurso]    Script Date: 16/04/2024 11:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [auth].[recurso](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[uniqueid] [uniqueidentifier] NOT NULL,
	[aplicacionid] [int] NOT NULL,
	[descripcion] [nvarchar](max) NOT NULL,
	[link] [nvarchar](max) NOT NULL,
	[creado] [datetime] NOT NULL,
	[autor] [int] NOT NULL,
	[modificado] [datetime] NULL,
	[editor] [int] NULL,
 CONSTRAINT [PK_recursos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [auth].[rol]    Script Date: 16/04/2024 11:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [auth].[rol](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[uniqueid] [uniqueidentifier] NOT NULL,
	[nombre] [nvarchar](max) NOT NULL,
	[creado] [datetime] NOT NULL,
	[autor] [int] NOT NULL,
	[modificado] [datetime] NULL,
	[editor] [int] NULL,
 CONSTRAINT [PK_rol] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [auth].[rolpersona]    Script Date: 16/04/2024 11:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [auth].[rolpersona](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[uniqueid] [uniqueidentifier] NULL,
	[personaid] [int] NOT NULL,
	[rolid] [int] NOT NULL,
	[creado] [datetime] NULL,
	[autor] [int] NOT NULL,
	[modificado] [datetime] NULL,
	[editor] [int] NULL,
 CONSTRAINT [PK_rolpersona] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [auth].[rolrecurso]    Script Date: 16/04/2024 11:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [auth].[rolrecurso](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[uniqueid] [uniqueidentifier] NOT NULL,
	[recursoid] [int] NOT NULL,
	[rolid] [int] NOT NULL,
	[creado] [datetime] NOT NULL,
	[autor] [int] NOT NULL,
	[modificado] [datetime] NULL,
	[editor] [int] NULL,
 CONSTRAINT [PK_rolrecurso] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ControlDescarga]    Script Date: 16/04/2024 11:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ControlDescarga](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Libro] [int] NULL,
	[Periodo] [int] NULL,
	[Zona] [int] NULL,
	[TecnicoId] [int] NULL,
	[Fecha] [datetime] NULL,
 CONSTRAINT [PK_ControlDescarga] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FISE]    Script Date: 16/04/2024 11:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FISE](
	[CodigoCuponFISE] [bigint] NOT NULL,
	[ValeFISE] [nvarchar](19) NULL,
	[CodigoSuministro] [bigint] NOT NULL,
	[NombreSuministro] [varchar](120) NOT NULL,
	[CodigoRutaSuministro] [char](20) NOT NULL,
	[CodigoPeriodoComercial] [int] NULL,
	[Activo] [int] NOT NULL,
	[Girado] [char](20) NOT NULL,
	[NombreBeneficiario] [nvarchar](250) NOT NULL,
	[DireccionBeneficiario] [varchar](30) NOT NULL,
	[NumeroDocumentoIdentidad] [varchar](11) NULL,
	[NombreDistrito] [nvarchar](30) NOT NULL,
	[MontoSubvencion] [numeric](5, 0) NOT NULL,
	[FechaFacturacion] [datetime] NOT NULL,
	[CodigoZonaAdministrativa] [tinyint] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[libro]    Script Date: 16/04/2024 11:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[libro](
	[ruta] [nvarchar](max) NOT NULL,
	[zona] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[libropersona]    Script Date: 16/04/2024 11:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[libropersona](
	[Libro] [bigint] NOT NULL,
	[Personaid] [int] NOT NULL,
	[Tipo] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[librosuministro]    Script Date: 16/04/2024 11:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[librosuministro](
	[uniqueid] [uniqueidentifier] NOT NULL,
	[CodigoLibro] [bigint] NOT NULL,
	[suministroid] [int] NOT NULL,
	[creado] [datetime] NOT NULL,
	[autor] [int] NOT NULL,
	[modificado] [datetime] NULL,
	[editor] [int] NULL,
 CONSTRAINT [PK_librosuministro] PRIMARY KEY CLUSTERED 
(
	[CodigoLibro] ASC,
	[suministroid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[operacion]    Script Date: 16/04/2024 11:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[operacion](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[uniqueid] [uniqueidentifier] NOT NULL,
	[periodo] [int] NOT NULL,
	[zona] [int] NOT NULL,
	[libro] [bigint] NOT NULL,
	[tecnicoid] [int] NULL,
	[suministro] [int] NOT NULL,
	[tipooperacion] [int] NULL,
	[creado] [datetime] NOT NULL,
	[autor] [int] NOT NULL,
	[modificado] [datetime] NULL,
	[editor] [int] NULL,
	[lmin] [int] NULL,
	[lmax] [int] NULL,
	[lminfoto] [int] NULL,
	[lmaxfoto] [int] NULL,
	[lectura] [int] NULL,
	[variacion] [int] NULL,
	[consumo] [int] NULL,
	[atipico] [int] NULL,
	[fechalectura] [datetime] NULL,
	[fecharepecion] [datetime] NULL,
	[imei] [nvarchar](max) NULL,
	[reclamo] [int] NULL,
	[fise] [int] NULL,
	[muestra] [int] NULL,
	[ejecutada] [int] NULL,
	[ejecutadafecha] [datetime] NULL,
	[ejecutadafechaservidor] [datetime] NULL,
	[Observacion] [int] NULL,
	[ObservacionTxt] [nvarchar](max) NULL,
	[secuenciaEjecutada] [int] NULL,
	[ejecutadalatitud] [float] NULL,
	[ejecutadalogitud] [float] NULL,
	[MuestraNombre] [nvarchar](max) NULL,
	[MuestraDni] [nvarchar](max) NULL,
	[MuestraCelular] [nvarchar](max) NULL,
	[Observado] [int] NULL,
	[ObservadoFecha] [datetime] NULL,
	[ObservadoTecnicoId] [int] NULL,
	[ObservadoEjecutadoFecha] [datetime] NULL,
	[ObservadoEjecutadoCodigo] [int] NULL,
	[ObservadoEjecutadoObs] [nvarchar](max) NULL,
	[ObservadoEjecutadoLat] [float] NULL,
	[ObservadoEjecutadoLon] [float] NULL,
	[ObservadoMuestraNombre] [nvarchar](max) NULL,
	[ObservadoMuestraDni] [nvarchar](max) NULL,
	[ObservadoMuestraCelular] [nvarchar](max) NULL,
 CONSTRAINT [PK_operacion] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[operacionarchivos]    Script Date: 16/04/2024 11:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[operacionarchivos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[operacion] [uniqueidentifier] NULL,
	[correlativo] [int] NULL,
	[ruta] [nvarchar](max) NULL,
	[fecha] [datetime] NOT NULL,
 CONSTRAINT [PK_operacionarchivos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[periodo]    Script Date: 16/04/2024 11:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[periodo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[uniqueid] [uniqueidentifier] NOT NULL,
	[anyo] [int] NOT NULL,
	[mes] [int] NOT NULL,
	[sincronizado] [bit] NULL,
	[abrierto] [bit] NULL,
	[creado] [datetime] NOT NULL,
	[autor] [int] NOT NULL,
	[modificado] [datetime] NULL,
	[editor] [int] NULL,
 CONSTRAINT [PK_Periodo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PeriodoOperacion]    Script Date: 16/04/2024 11:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PeriodoOperacion](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[uniqueid] [uniqueidentifier] NOT NULL,
	[periodoid] [int] NOT NULL,
	[tipooperacionid] [int] NOT NULL,
	[estado] [int] NOT NULL,
	[creado] [datetime] NOT NULL,
	[autor] [int] NOT NULL,
	[modificado] [datetime] NULL,
	[editor] [int] NULL,
	[zonaid] [int] NULL,
	[fechainicio] [datetime] NULL,
	[fechafin] [datetime] NULL,
	[informe] [nvarchar](max) NULL,
	[fechacierre] [datetime] NULL,
	[fechacierreAutor] [int] NULL,
 CONSTRAINT [PK_periodooperacion] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PeriodoZonaLibro]    Script Date: 16/04/2024 11:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PeriodoZonaLibro](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[periodo] [int] NOT NULL,
	[zona] [int] NOT NULL,
	[libro] [bigint] NULL,
	[creado] [datetime] NOT NULL,
	[tecnicoid] [int] NULL,
	[modificado] [datetime] NULL,
 CONSTRAINT [PK_PeriodoZonaLibro] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonaObservacion]    Script Date: 16/04/2024 11:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonaObservacion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Persona] [int] NULL,
	[Suministro] [nvarchar](50) NULL,
	[Fecha] [datetime] NULL,
	[Observacion] [nvarchar](50) NULL,
 CONSTRAINT [PK_PersonaObservacion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reclamos]    Script Date: 16/04/2024 11:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reclamos](
	[CodigoReclamo] [bigint] NULL,
	[FechaRegistroReclamo] [datetime] NOT NULL,
	[NombreClaseReclamo] [varchar](80) NOT NULL,
	[CodigoSuministro] [bigint] NULL,
	[NombreSolicitante] [varchar](40) NOT NULL,
	[CodigoRutaSuministro] [char](20) NOT NULL,
	[DireccionSolicitante] [varchar](120) NOT NULL,
	[DatoMCo] [varchar](40) NULL,
	[CorreoNotificacionDigital] [varchar](50) NULL,
	[CodigoPeriodoComercial] [int] NULL,
	[CodigoEstadoReclamo] [tinyint] NOT NULL,
	[NombreEstadoReclamoComercial] [varchar](60) NOT NULL,
	[NumeroDocumentoIdentidad] [varchar](20) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[suministro]    Script Date: 16/04/2024 11:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[suministro](
	[uniqueid] [uniqueidentifier] NOT NULL,
	[numero] [bigint] NOT NULL,
	[direccion] [nvarchar](max) NULL,
	[medidor] [nvarchar](max) NULL,
	[creado] [datetime] NOT NULL,
	[autor] [int] NOT NULL,
	[modificado] [datetime] NULL,
	[editor] [int] NULL,
	[latitud] [float] NULL,
	[longitud] [float] NULL,
	[CodigoZona] [nchar](2) NULL,
	[CodigoRuta] [nchar](20) NULL,
	[PeriodoFacturacion] [int] NULL,
	[Tarifa] [varchar](50) NULL,
	[Muestra] [int] NULL,
	[MuestraNumero] [int] NULL,
	[GrupoImpresion] [int] NULL,
	[Nombre] [nvarchar](max) NULL,
	[NIM] [nchar](10) NULL,
	[NroRecibo] [nchar](15) NULL,
	[direccionreparto] [nvarchar](max) NULL,
 CONSTRAINT [PK_suministro] PRIMARY KEY CLUSTERED 
(
	[numero] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tipooperacion]    Script Date: 16/04/2024 11:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tipooperacion](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[uniqueid] [uniqueidentifier] NOT NULL,
	[nombre] [nvarchar](max) NOT NULL,
	[creado] [datetime] NOT NULL,
	[autor] [int] NOT NULL,
	[modificado] [datetime] NULL,
	[editor] [int] NULL,
 CONSTRAINT [PK_tipooperacion] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[zona]    Script Date: 16/04/2024 11:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[zona](
	[nombre] [nvarchar](max) NOT NULL,
	[codigo] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [auth].[persona] ADD  CONSTRAINT [DF_persona_uniqueid]  DEFAULT (newid()) FOR [uniqueid]
GO
ALTER TABLE [auth].[persona] ADD  CONSTRAINT [DF_persona_creado]  DEFAULT (getdate()) FOR [creado]
GO
ALTER TABLE [dbo].[operacion] ADD  CONSTRAINT [DF_operacion_uniqueid]  DEFAULT (newid()) FOR [uniqueid]
GO
ALTER TABLE [dbo].[operacion] ADD  CONSTRAINT [DF_operacion_creado]  DEFAULT (getdate()) FOR [creado]
GO
ALTER TABLE [dbo].[operacionarchivos] ADD  CONSTRAINT [DF_operacionarchivos_fecha]  DEFAULT (getdate()) FOR [fecha]
GO
ALTER TABLE [dbo].[periodo] ADD  CONSTRAINT [DF_periodo_uniqueid]  DEFAULT (newid()) FOR [uniqueid]
GO
ALTER TABLE [dbo].[periodo] ADD  CONSTRAINT [DF_periodo_creado]  DEFAULT (getdate()) FOR [creado]
GO
ALTER TABLE [dbo].[PeriodoOperacion] ADD  CONSTRAINT [DF_periodooperacion_uniqueid]  DEFAULT (newid()) FOR [uniqueid]
GO
ALTER TABLE [dbo].[PeriodoOperacion] ADD  CONSTRAINT [DF_periodooperacion_creado]  DEFAULT (getdate()) FOR [creado]
GO
ALTER TABLE [dbo].[PeriodoZonaLibro] ADD  CONSTRAINT [DF_PeriodoZonaLibro_creado]  DEFAULT (getdate()) FOR [creado]
GO
ALTER TABLE [dbo].[PersonaObservacion] ADD  CONSTRAINT [DF_PersonaObservacion_Fecha]  DEFAULT (getdate()) FOR [Fecha]
GO
/****** Object:  StoredProcedure [dbo].[GenerarPadron]    Script Date: 16/04/2024 11:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GenerarPadron]
    @Periodo INT,
    @Zona INT
AS
BEGIN
	INSERT INTO [dbo].[operacion]([periodo]
			,[zona]
			,[libro]
			,[suministro]
			,[tipooperacion]
			,autor,reclamo,fise) 
	SELECT 
			@Periodo
			,RTRIM(LTRIM(su.CodigoZona))
			,RTRIM(LTRIM(su.CodigoRuta))
			,su.numero
			,2
			,1
			,0
			,0
	FROM [dbo].[suministro] as su                 
	WHERE su.CodigoZona = @Zona AND [GrupoImpresion] is null;

	INSERT INTO [dbo].[PeriodoZonaLibro]([periodo], [zona], [libro]) 
    SELECT 
		@Periodo
		,@Zona
       ,SUBSTRING(cast([libro] as varchar(100)),1,7)
    FROM [dbo].[operacion] 
	WHERE Zona = @Zona 
	group by SUBSTRING(cast([libro] as varchar(100)),1,7);

END
GO
/****** Object:  StoredProcedure [dbo].[VerifivarPeriodo]    Script Date: 16/04/2024 11:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[VerifivarPeriodo]
(
  @GUID UNIQUEIDENTIFIER
)
AS
BEGIN

  DECLARE @Periodo INT
  DECLARE @Zona INT

  DECLARE @Resultado INT

  SELECT  @Periodo = [periodo] ,@Zona = [zona] FROM [dbo].[operacion] WHERE [uniqueid] = @GUID

  SELECT TOP (1) @Resultado = [estado] FROM [dbo].[PeriodoOperacion] WHERE [periodoid] = @Periodo AND [zonaid] = @Zona

  RETURN @Resultado
END
GO
USE [master]
GO
ALTER DATABASE [SealOperaciones2] SET  READ_WRITE 
GO
