USE [SealOperaciones]
GO
/****** Object:  User [maikol]    Script Date: 13/03/2024 10:16:46 ******/
CREATE USER [maikol] FOR LOGIN [maikol] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [seal]    Script Date: 13/03/2024 10:16:46 ******/
CREATE USER [seal] FOR LOGIN [seal] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [maikol]
GO
ALTER ROLE [db_owner] ADD MEMBER [seal]
GO
/****** Object:  Schema [auth]    Script Date: 13/03/2024 10:16:47 ******/
CREATE SCHEMA [auth]
GO
/****** Object:  Table [auth].[empresa]    Script Date: 13/03/2024 10:16:47 ******/
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
/****** Object:  Table [auth].[empresacontratos]    Script Date: 13/03/2024 10:16:47 ******/
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
/****** Object:  Table [auth].[grupopersona]    Script Date: 13/03/2024 10:16:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [auth].[grupopersona](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[uniqueid] [uniqueidentifier] NOT NULL,
	[personaid] [int] NOT NULL,
	[grupoid] [int] NOT NULL,
	[creado] [datetime] NOT NULL,
	[autor] [int] NOT NULL,
	[modificado] [datetime] NULL,
	[editor] [int] NULL,
 CONSTRAINT [PK_grupopersona] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [auth].[persona]    Script Date: 13/03/2024 10:16:47 ******/
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
 CONSTRAINT [PK_persona] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [auth].[personagrupo]    Script Date: 13/03/2024 10:16:47 ******/
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
/****** Object:  Table [auth].[recurso]    Script Date: 13/03/2024 10:16:47 ******/
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
/****** Object:  Table [auth].[rol]    Script Date: 13/03/2024 10:16:47 ******/
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
/****** Object:  Table [auth].[rolpersona]    Script Date: 13/03/2024 10:16:47 ******/
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
/****** Object:  Table [auth].[rolrecurso]    Script Date: 13/03/2024 10:16:47 ******/
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
/****** Object:  Table [dbo].[FISE]    Script Date: 13/03/2024 10:16:47 ******/
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
/****** Object:  Table [dbo].[libro]    Script Date: 13/03/2024 10:16:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[libro](
	[ruta] [nvarchar](max) NOT NULL,
	[zona] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[libropersona]    Script Date: 13/03/2024 10:16:47 ******/
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
/****** Object:  Table [dbo].[librosuministro]    Script Date: 13/03/2024 10:16:47 ******/
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
/****** Object:  Table [dbo].[operacionarchivos]    Script Date: 13/03/2024 10:16:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[operacionarchivos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[suministro] [nvarchar](50) NULL,
	[operation] [uniqueidentifier] NULL,
	[correlativo] [int] NULL,
	[ruta] [nvarchar](max) NULL,
	[fecha] [datetime] NULL,
 CONSTRAINT [PK_operacionarchivos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[periodo]    Script Date: 13/03/2024 10:16:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[periodo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[uniqueid] [uniqueidentifier] NOT NULL,
	[anyo] [int] NOT NULL,
	[mes] [int] NOT NULL,
	[sincronizado] [bit] NOT NULL,
	[abrierto] [bit] NOT NULL,
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
/****** Object:  Table [dbo].[periodooperacion]    Script Date: 13/03/2024 10:16:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[periodooperacion](
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
 CONSTRAINT [PK_periodooperacion] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PeriodoZonaLibro]    Script Date: 13/03/2024 10:16:47 ******/
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
/****** Object:  Table [dbo].[PersonaObservacion]    Script Date: 13/03/2024 10:16:47 ******/
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
/****** Object:  Table [dbo].[Reclamos]    Script Date: 13/03/2024 10:16:47 ******/
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
/****** Object:  Table [dbo].[tipooperacion]    Script Date: 13/03/2024 10:16:47 ******/
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
/****** Object:  Table [dbo].[zona]    Script Date: 13/03/2024 10:16:47 ******/
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
ALTER TABLE [dbo].[periodooperacion] ADD  CONSTRAINT [DF_periodooperacion_uniqueid]  DEFAULT (newid()) FOR [uniqueid]
GO
ALTER TABLE [dbo].[periodooperacion] ADD  CONSTRAINT [DF_periodooperacion_creado]  DEFAULT (getdate()) FOR [creado]
GO
ALTER TABLE [dbo].[PeriodoZonaLibro] ADD  CONSTRAINT [DF_PeriodoZonaLibro_creado]  DEFAULT (getdate()) FOR [creado]
GO
ALTER TABLE [dbo].[PersonaObservacion] ADD  CONSTRAINT [DF_PersonaObservacion_Fecha]  DEFAULT (getdate()) FOR [Fecha]
GO
/****** Object:  StoredProcedure [dbo].[GenerarPadron]    Script Date: 13/03/2024 10:16:47 ******/
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
    FROM [dbo].[operacion] group by SUBSTRING(cast([libro] as varchar(100)),1,7);

END
GO
