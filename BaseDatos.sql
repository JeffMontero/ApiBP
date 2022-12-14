USE [BPDb]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 9/9/2022 10:39:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[IdCliente] [int] IDENTITY(1,1) NOT NULL,
	[Persona] [int] NOT NULL,
	[Clave] [nvarchar](20) NOT NULL,
	[Estado] [nvarchar](1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cuenta]    Script Date: 9/9/2022 10:39:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cuenta](
	[IdCuenta] [int] IDENTITY(1,1) NOT NULL,
	[Cliente] [int] NOT NULL,
	[NumeroCuenta] [nvarchar](10) NOT NULL,
	[TipoCuenta] [nvarchar](1) NOT NULL,
	[SaldoInicial] [decimal](12, 3) NOT NULL,
	[Estado] [nvarchar](1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCuenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movimientos]    Script Date: 9/9/2022 10:39:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movimientos](
	[IdMovimiento] [bigint] IDENTITY(1,1) NOT NULL,
	[Cuenta] [int] NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[TipoMovimiento] [nvarchar](1) NOT NULL,
	[Valor] [decimal](12, 3) NOT NULL,
	[Saldo] [decimal](12, 3) NOT NULL,
	[Estado] [nvarchar](1) NOT NULL,
	[MovDescripcion] [nvarchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdMovimiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Persona]    Script Date: 9/9/2022 10:39:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Persona](
	[IdPersona] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Genero] [nvarchar](1) NOT NULL,
	[Edad] [int] NOT NULL,
	[Identificacion] [nvarchar](10) NOT NULL,
	[Direccion] [nvarchar](200) NOT NULL,
	[Telefono] [nvarchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPersona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Cliente] ON 

INSERT [dbo].[Cliente] ([IdCliente], [Persona], [Clave], [Estado]) VALUES (1, 1, N'1234', N'1')
INSERT [dbo].[Cliente] ([IdCliente], [Persona], [Clave], [Estado]) VALUES (2, 2, N'1234', N'I')
INSERT [dbo].[Cliente] ([IdCliente], [Persona], [Clave], [Estado]) VALUES (3, 6, N'456', N'A')
INSERT [dbo].[Cliente] ([IdCliente], [Persona], [Clave], [Estado]) VALUES (4, 7, N'1234', N'A')
SET IDENTITY_INSERT [dbo].[Cliente] OFF
GO
SET IDENTITY_INSERT [dbo].[Cuenta] ON 

INSERT [dbo].[Cuenta] ([IdCuenta], [Cliente], [NumeroCuenta], [TipoCuenta], [SaldoInicial], [Estado]) VALUES (1, 4, N'478758', N'A', CAST(2000.000 AS Decimal(12, 3)), N'A')
INSERT [dbo].[Cuenta] ([IdCuenta], [Cliente], [NumeroCuenta], [TipoCuenta], [SaldoInicial], [Estado]) VALUES (2, 4, N'585545', N'C', CAST(1000.000 AS Decimal(12, 3)), N'A')
SET IDENTITY_INSERT [dbo].[Cuenta] OFF
GO
SET IDENTITY_INSERT [dbo].[Movimientos] ON 

INSERT [dbo].[Movimientos] ([IdMovimiento], [Cuenta], [Fecha], [TipoMovimiento], [Valor], [Saldo], [Estado], [MovDescripcion]) VALUES (2, 1, CAST(N'2022-09-09T18:23:28.570' AS DateTime), N'C', CAST(1000.000 AS Decimal(12, 3)), CAST(2000.000 AS Decimal(12, 3)), N'A', N'Depósito de:1000')
INSERT [dbo].[Movimientos] ([IdMovimiento], [Cuenta], [Fecha], [TipoMovimiento], [Valor], [Saldo], [Estado], [MovDescripcion]) VALUES (3, 1, CAST(N'2022-09-09T18:28:55.737' AS DateTime), N'D', CAST(10.000 AS Decimal(12, 3)), CAST(1990.000 AS Decimal(12, 3)), N'I', N'Retiro de:10')
SET IDENTITY_INSERT [dbo].[Movimientos] OFF
GO
SET IDENTITY_INSERT [dbo].[Persona] ON 

INSERT [dbo].[Persona] ([IdPersona], [Nombre], [Genero], [Edad], [Identificacion], [Direccion], [Telefono]) VALUES (1, N'Juan', N'M', 29, N'1745236415', N'Quito', N'0987456123')
INSERT [dbo].[Persona] ([IdPersona], [Nombre], [Genero], [Edad], [Identificacion], [Direccion], [Telefono]) VALUES (2, N'prueba', N'p', 50, N'454545454', N'lks', N'9999999')
INSERT [dbo].[Persona] ([IdPersona], [Nombre], [Genero], [Edad], [Identificacion], [Direccion], [Telefono]) VALUES (6, N'Marielena Montalvo', N'F', 20, N'1456987536', N'Amazonas y NNUU', N'097548965')
INSERT [dbo].[Persona] ([IdPersona], [Nombre], [Genero], [Edad], [Identificacion], [Direccion], [Telefono]) VALUES (7, N'Jose Lema', N'M', 40, N'1455574897', N'Otavalo sn y principal', N'098254785')
SET IDENTITY_INSERT [dbo].[Persona] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Cuenta__E039507B0DAF0CB0]    Script Date: 9/9/2022 10:39:51 PM ******/
ALTER TABLE [dbo].[Cuenta] ADD UNIQUE NONCLUSTERED 
(
	[NumeroCuenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Persona__D6F931E5023D5A04]    Script Date: 9/9/2022 10:39:51 PM ******/
ALTER TABLE [dbo].[Persona] ADD UNIQUE NONCLUSTERED 
(
	[Identificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Cliente]  WITH CHECK ADD FOREIGN KEY([Persona])
REFERENCES [dbo].[Persona] ([IdPersona])
GO
ALTER TABLE [dbo].[Cuenta]  WITH CHECK ADD FOREIGN KEY([Cliente])
REFERENCES [dbo].[Cliente] ([IdCliente])
GO
ALTER TABLE [dbo].[Movimientos]  WITH CHECK ADD FOREIGN KEY([Cuenta])
REFERENCES [dbo].[Cuenta] ([IdCuenta])
GO
/****** Object:  StoredProcedure [dbo].[DeleteCliente]    Script Date: 9/9/2022 10:39:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteCliente]
@IdCliente int

AS

DECLARE @Estado nvarchar(1)
SET @Estado='I'
BEGIN
UPDATE Cliente SET 
Estado = @Estado
where IdCliente= @IdCliente

SELECT 'codigo'= @IdCliente
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteCuenta]    Script Date: 9/9/2022 10:39:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteCuenta]
@IdCuenta int

AS

DECLARE @Estado nvarchar(1)
SET @Estado='I'
BEGIN
UPDATE Cuenta SET 
Estado = @Estado
where IdCuenta= @IdCuenta

SELECT 'codigo'= @IdCuenta
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteMovimiento]    Script Date: 9/9/2022 10:39:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteMovimiento]
@IdMovimiento int

AS

DECLARE @Estado nvarchar(1)
SET @Estado='I'
BEGIN
UPDATE Movimientos SET 
Estado = @Estado
where IdMovimiento= @IdMovimiento

SELECT 'codigo'= @IdMovimiento
END
GO
/****** Object:  StoredProcedure [dbo].[InserMovimiento]    Script Date: 9/9/2022 10:39:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InserMovimiento]
	@Cuenta int,
	@TipoMovimiento nvarchar(1), 
	@Valor decimal(12,3),
	@Saldo decimal(12,3),
	@MovDescripcion nvarchar(200)
		
AS 
DECLARE @Estado nvarchar(1)
SET @Estado='A'
BEGIN
	
		INSERT INTO Movimientos
		(		
	Cuenta,
	Fecha,
	TipoMovimiento,
	Valor,
	Saldo,
	Estado,
	MovDescripcion
		)

		VALUES
		(
	@Cuenta,
	GETDATE(), 
	@TipoMovimiento,
	@Valor,
	@Saldo,
	@Estado,
	@MovDescripcion
	 	
		)
	SELECT 'codigo'= Cast (SCOPE_IDENTITY()as int) 
	END
GO
/****** Object:  StoredProcedure [dbo].[InsertCliente]    Script Date: 9/9/2022 10:39:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertCliente]
	@Persona int ,
	@Clave nvarchar(20) 	
AS 

DECLARE @Estado nvarchar(1)
SET @Estado='A'
BEGIN
	
		INSERT INTO Cliente
		(		
			Persona,
			Clave,
			Estado
		)

		VALUES
		(
			@Persona ,
			@Clave,
			@Estado
		)
	SELECT 'codigo'= cast (SCOPE_IDENTITY() as int)
	END
GO
/****** Object:  StoredProcedure [dbo].[InsertCuenta]    Script Date: 9/9/2022 10:39:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertCuenta]
	@Cliente int,
	@Numerocuenta nvarchar(10), 
	@TipoCuenta nvarchar(1),
	@SaldoInicial decimal(12,3)
		
AS 
DECLARE @Estado nvarchar(1)
SET @Estado='A'
BEGIN
	
		INSERT INTO Cuenta
		(		
	Cliente,
	NumeroCuenta,
	TipoCuenta,
	SaldoInicial,
	Estado
		)

		VALUES
		(
	@Cliente,
	@Numerocuenta, 
	@TipoCuenta,
	@SaldoInicial,
	@Estado
	 	
		)
	SELECT 'codigo'= Cast (SCOPE_IDENTITY()as int) 
	END
GO
/****** Object:  StoredProcedure [dbo].[InsertPersona]    Script Date: 9/9/2022 10:39:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertPersona]
	@Nombre nvarchar(100),
	@Genero nvarchar(1), 
	@Edad int,
	@Identificacion nvarchar(10),
	@Direccion nvarchar(200),
	@Telefono nvarchar(10) 	
AS 
BEGIN
	
		INSERT INTO Persona
		(		
	Nombre,
	Genero,
	Edad,
	Identificacion,
	Direccion,
	Telefono
		)

		VALUES
		(
	@Nombre,
	@Genero, 
	@Edad,
	@Identificacion,
	@Direccion,
	@Telefono 	
		)
	SELECT 'codigo'= Cast (SCOPE_IDENTITY()as int) 
	END
GO
/****** Object:  StoredProcedure [dbo].[UpdateCliente]    Script Date: 9/9/2022 10:39:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateCliente]
    @IdCliente int,
	@Clave nvarchar(20),
	@Estado nvarchar(1)
AS


BEGIN
UPDATE Cliente SET 

	
	Clave = @Clave,
	Estado = @Estado


where IdCliente = @IdCliente

SELECT 'codigo'= @IdCliente

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateCuenta]    Script Date: 9/9/2022 10:39:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateCuenta]
	@IdCuenta int,
    @Cliente int,
	@Numerocuenta nvarchar(10), 
	@TipoCuenta nvarchar(1),
	@SaldoInicial decimal(12,3),
	@Estado nvarchar(1)
AS 
BEGIN
	
		UPDATE Cuenta SET
		
	Cliente =@Cliente,
	NumeroCuenta =@Numerocuenta,
	TipoCuenta =@TipoCuenta,
	SaldoInicial= @SaldoInicial,
	Estado =@Estado
	
	
	WHERE IdCuenta =@IdCuenta
	 	
	
	SELECT 'codigo'= @IdCuenta
	END
GO
/****** Object:  StoredProcedure [dbo].[UpdatePersona]    Script Date: 9/9/2022 10:39:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdatePersona]
    @IdPersona int,
	@Nombre nvarchar(100),
	@Genero nvarchar(1), 
	@Edad int,
	@Identificacion nvarchar(10),
	@Direccion nvarchar(200),
	@Telefono nvarchar(10) 	
AS 
BEGIN
	
		UPDATE Persona SET
		
	Nombre =@Nombre ,
	Genero =@Genero,
	Edad =@Edad,
	Identificacion= @Identificacion,
	Direccion =@Direccion,
	Telefono =@Telefono
	
	WHERE IdPersona =@IdPersona
	 	
	
	SELECT 'codigo'= @IdPersona
	END
GO
