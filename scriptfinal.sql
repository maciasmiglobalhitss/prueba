USE [SINERGIA]
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 22/10/2024 17:54:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[id] [numeric](7, 0) IDENTITY(1,1) NOT NULL,
	[id_modulo] [numeric](7, 0) NOT NULL,
	[orden] [int] NOT NULL,
	[nemonico] [varchar](20) NOT NULL,
	[descripcion] [varchar](100) NOT NULL,
	[ruta] [varchar](150) NOT NULL,
	[icono] [varchar](100) NOT NULL,
	[activo] [bit] NOT NULL,
	[mostrar] [bit] NOT NULL,
	[usuario_creacion] [varchar](100) NOT NULL,
	[fecha_creacion] [datetime] NOT NULL,
	[usuario_modificacion] [varchar](100) NOT NULL,
	[fecha_modificacion] [datetime] NOT NULL,
	[codigo_app_web] [int] NOT NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Menu] ON 

INSERT [dbo].[Menu] ([id], [id_modulo], [orden], [nemonico], [descripcion], [ruta], [icono], [activo], [mostrar], [usuario_creacion], [fecha_creacion], [usuario_modificacion], [fecha_modificacion], [codigo_app_web]) VALUES (CAST(1 AS Numeric(7, 0)), CAST(1 AS Numeric(7, 0)), 1, N'48011000000', N'Catálogo de Especificaciones', N'/specifications-catalog/params', N'bx bxs-box', 1, 1, N'admin', CAST(N'2024-10-21T11:09:39.083' AS DateTime), N'admin', CAST(N'2024-10-21T11:09:39.083' AS DateTime), 2)
INSERT [dbo].[Menu] ([id], [id_modulo], [orden], [nemonico], [descripcion], [ruta], [icono], [activo], [mostrar], [usuario_creacion], [fecha_creacion], [usuario_modificacion], [fecha_modificacion], [codigo_app_web]) VALUES (CAST(2 AS Numeric(7, 0)), CAST(1 AS Numeric(7, 0)), 2, N'48011100000', N'Consulta Parametros', N'/specifications-catalog/params', N' ', 1, 0, N'admin', CAST(N'2024-10-21T11:09:39.083' AS DateTime), N'admin', CAST(N'2024-10-21T11:09:39.083' AS DateTime), 0)
INSERT [dbo].[Menu] ([id], [id_modulo], [orden], [nemonico], [descripcion], [ruta], [icono], [activo], [mostrar], [usuario_creacion], [fecha_creacion], [usuario_modificacion], [fecha_modificacion], [codigo_app_web]) VALUES (CAST(3 AS Numeric(7, 0)), CAST(1 AS Numeric(7, 0)), 3, N'48011200000', N'Peso Embarque', N'/specifications-catalog/params', N' ', 1, 0, N'admin', CAST(N'2024-10-21T11:09:39.087' AS DateTime), N'admin', CAST(N'2024-10-21T11:09:39.087' AS DateTime), 3)
INSERT [dbo].[Menu] ([id], [id_modulo], [orden], [nemonico], [descripcion], [ruta], [icono], [activo], [mostrar], [usuario_creacion], [fecha_creacion], [usuario_modificacion], [fecha_modificacion], [codigo_app_web]) VALUES (CAST(4 AS Numeric(7, 0)), CAST(1 AS Numeric(7, 0)), 4, N'48011300000', N'Cartón', N'/specifications-catalog/params', N' ', 1, 0, N'admin', CAST(N'2024-10-21T11:09:39.087' AS DateTime), N'admin', CAST(N'2024-10-21T11:09:39.087' AS DateTime), 4)
INSERT [dbo].[Menu] ([id], [id_modulo], [orden], [nemonico], [descripcion], [ruta], [icono], [activo], [mostrar], [usuario_creacion], [fecha_creacion], [usuario_modificacion], [fecha_modificacion], [codigo_app_web]) VALUES (CAST(5 AS Numeric(7, 0)), CAST(1 AS Numeric(7, 0)), 5, N'48011400000', N'Paletizado', N'/specifications-catalog/params', N' ', 1, 0, N'admin', CAST(N'2024-10-21T11:09:39.087' AS DateTime), N'admin', CAST(N'2024-10-21T11:09:39.087' AS DateTime), 6)
INSERT [dbo].[Menu] ([id], [id_modulo], [orden], [nemonico], [descripcion], [ruta], [icono], [activo], [mostrar], [usuario_creacion], [fecha_creacion], [usuario_modificacion], [fecha_modificacion], [codigo_app_web]) VALUES (CAST(6 AS Numeric(7, 0)), CAST(1 AS Numeric(7, 0)), 6, N'48011500000', N'Mini Paletizado', N'/specifications-catalog/params', N' ', 1, 0, N'admin', CAST(N'2024-10-21T11:09:39.087' AS DateTime), N'admin', CAST(N'2024-10-21T11:09:39.087' AS DateTime), 20)
INSERT [dbo].[Menu] ([id], [id_modulo], [orden], [nemonico], [descripcion], [ruta], [icono], [activo], [mostrar], [usuario_creacion], [fecha_creacion], [usuario_modificacion], [fecha_modificacion], [codigo_app_web]) VALUES (CAST(7 AS Numeric(7, 0)), CAST(1 AS Numeric(7, 0)), 7, N'48011600000', N'Fruta Edad', N'/specifications-catalog/params', N' ', 1, 0, N'admin', CAST(N'2024-10-21T11:09:39.087' AS DateTime), N'admin', CAST(N'2024-10-21T11:09:39.087' AS DateTime), 8)
INSERT [dbo].[Menu] ([id], [id_modulo], [orden], [nemonico], [descripcion], [ruta], [icono], [activo], [mostrar], [usuario_creacion], [fecha_creacion], [usuario_modificacion], [fecha_modificacion], [codigo_app_web]) VALUES (CAST(8 AS Numeric(7, 0)), CAST(1 AS Numeric(7, 0)), 8, N'48011700000', N'Tolerancia Calidad', N'/specifications-catalog/params', N' ', 1, 0, N'admin', CAST(N'2024-10-21T11:09:39.090' AS DateTime), N'admin', CAST(N'2024-10-21T11:09:39.090' AS DateTime), 90)
INSERT [dbo].[Menu] ([id], [id_modulo], [orden], [nemonico], [descripcion], [ruta], [icono], [activo], [mostrar], [usuario_creacion], [fecha_creacion], [usuario_modificacion], [fecha_modificacion], [codigo_app_web]) VALUES (CAST(9 AS Numeric(7, 0)), CAST(1 AS Numeric(7, 0)), 9, N'48011800000', N'Fumigación', N'/specifications-catalog/params', N' ', 1, 0, N'admin', CAST(N'2024-10-21T11:09:39.090' AS DateTime), N'admin', CAST(N'2024-10-21T11:09:39.090' AS DateTime), 7)
INSERT [dbo].[Menu] ([id], [id_modulo], [orden], [nemonico], [descripcion], [ruta], [icono], [activo], [mostrar], [usuario_creacion], [fecha_creacion], [usuario_modificacion], [fecha_modificacion], [codigo_app_web]) VALUES (CAST(10 AS Numeric(7, 0)), CAST(1 AS Numeric(7, 0)), 10, N'48011900000', N'Fruta Característica', N'/specifications-catalog/params', N' ', 1, 0, N' ', CAST(N'2024-10-21T11:09:39.090' AS DateTime), N'admin', CAST(N'2024-10-21T11:09:39.090' AS DateTime), 10)
INSERT [dbo].[Menu] ([id], [id_modulo], [orden], [nemonico], [descripcion], [ruta], [icono], [activo], [mostrar], [usuario_creacion], [fecha_creacion], [usuario_modificacion], [fecha_modificacion], [codigo_app_web]) VALUES (CAST(11 AS Numeric(7, 0)), CAST(1 AS Numeric(7, 0)), 11, N'48011A00000', N'Plastico', N'/specifications-catalog/params', N' ', 1, 0, N'admin', CAST(N'2024-10-21T11:09:39.090' AS DateTime), N'admin', CAST(N'2024-10-21T11:09:39.090' AS DateTime), 5)
INSERT [dbo].[Menu] ([id], [id_modulo], [orden], [nemonico], [descripcion], [ruta], [icono], [activo], [mostrar], [usuario_creacion], [fecha_creacion], [usuario_modificacion], [fecha_modificacion], [codigo_app_web]) VALUES (CAST(12 AS Numeric(7, 0)), CAST(1 AS Numeric(7, 0)), 12, N'48012000000', N'Especificaciones', N'/specifications-caja/box', N'bx bxs-inbox', 1, 1, N'admin', CAST(N'2024-10-21T11:09:39.090' AS DateTime), N'admin', CAST(N'2024-10-21T11:09:39.090' AS DateTime), 0)
INSERT [dbo].[Menu] ([id], [id_modulo], [orden], [nemonico], [descripcion], [ruta], [icono], [activo], [mostrar], [usuario_creacion], [fecha_creacion], [usuario_modificacion], [fecha_modificacion], [codigo_app_web]) VALUES (CAST(13 AS Numeric(7, 0)), CAST(1 AS Numeric(7, 0)), 12, N'48012100000', N'Especificaciones de Caja', N'/specifications-caja/box', N'bx bxs-inbox', 1, 0, N'admin', CAST(N'2024-10-21T11:09:39.090' AS DateTime), N'admin', CAST(N'2024-10-21T11:09:39.090' AS DateTime), 9)
INSERT [dbo].[Menu] ([id], [id_modulo], [orden], [nemonico], [descripcion], [ruta], [icono], [activo], [mostrar], [usuario_creacion], [fecha_creacion], [usuario_modificacion], [fecha_modificacion], [codigo_app_web]) VALUES (CAST(14 AS Numeric(7, 0)), CAST(1 AS Numeric(7, 0)), 13, N'48013000000', N'Generación Materiales', N'/materials-list-generation/generation', N'bx bxs-basket', 1, 1, N'admin', CAST(N'2024-10-21T11:09:39.090' AS DateTime), N'admin', CAST(N'2024-10-21T11:09:39.090' AS DateTime), 0)
INSERT [dbo].[Menu] ([id], [id_modulo], [orden], [nemonico], [descripcion], [ruta], [icono], [activo], [mostrar], [usuario_creacion], [fecha_creacion], [usuario_modificacion], [fecha_modificacion], [codigo_app_web]) VALUES (CAST(15 AS Numeric(7, 0)), CAST(1 AS Numeric(7, 0)), 13, N'48013100000', N'Lista Materiales', N'/materials-list-generation/generation', N'bx bxs-basket', 1, 0, N'admin', CAST(N'2024-10-21T11:09:39.090' AS DateTime), N'admin', CAST(N'2024-10-21T11:09:39.090' AS DateTime), 91)
INSERT [dbo].[Menu] ([id], [id_modulo], [orden], [nemonico], [descripcion], [ruta], [icono], [activo], [mostrar], [usuario_creacion], [fecha_creacion], [usuario_modificacion], [fecha_modificacion], [codigo_app_web]) VALUES (CAST(16 AS Numeric(7, 0)), CAST(1 AS Numeric(7, 0)), 14, N'48014000000', N'Reporte', N'/report-catalog-product/report', N'bx bxs-file-pdf', 1, 1, N'admin', CAST(N'2024-10-21T11:09:39.093' AS DateTime), N'admin', CAST(N'2024-10-21T11:09:39.093' AS DateTime), 0)
INSERT [dbo].[Menu] ([id], [id_modulo], [orden], [nemonico], [descripcion], [ruta], [icono], [activo], [mostrar], [usuario_creacion], [fecha_creacion], [usuario_modificacion], [fecha_modificacion], [codigo_app_web]) VALUES (CAST(17 AS Numeric(7, 0)), CAST(1 AS Numeric(7, 0)), 14, N'48014100000', N'Reporte de Catálogo', N'/report-catalog-product/report', N'bx bxs-file-pdf', 1, 0, N'admin', CAST(N'2024-10-21T11:09:39.093' AS DateTime), N'admin', CAST(N'2024-10-21T11:09:39.093' AS DateTime), 92)
SET IDENTITY_INSERT [dbo].[Menu] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Menu]    Script Date: 22/10/2024 17:54:31 ******/
ALTER TABLE [dbo].[Menu] ADD  CONSTRAINT [UQ_Menu] UNIQUE NONCLUSTERED 
(
	[nemonico] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Menu]  WITH CHECK ADD  CONSTRAINT [FK_Menu_Modulo] FOREIGN KEY([id_modulo])
REFERENCES [dbo].[Modulo] ([id])
GO
ALTER TABLE [dbo].[Menu] CHECK CONSTRAINT [FK_Menu_Modulo]
GO
