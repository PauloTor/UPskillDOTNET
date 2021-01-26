SET IDENTITY_INSERT [dbo].[Cliente] ON
INSERT INTO [dbo].[Cliente] ([ClienteID], [NomeCliente], [EmailCliente], [NifCliente], [MetodoPagamento], [Credito]) VALUES (1, 'P', 'p@pt', 123456789, 'cash', 0)
SET IDENTITY_INSERT [dbo].[Cliente] OFF

SET IDENTITY_INSERT [dbo].[Fatura] ON
INSERT INTO [dbo].[Fatura] ([FaturaID], [DataFatura], [MetodoPagamentoFatura], [PrecoFatura], [ReservaID]) VALUES (1, '2020-07-22', 'cash', 20, 1)
SET IDENTITY_INSERT [dbo].[Fatura] OFF

SET IDENTITY_INSERT [dbo].[Lugar] ON
INSERT INTO [dbo].[Lugar] ([LugarID], [Fila], [Sector], [Preço], [ParqueID]) VALUES (1, 1, 1, 3, 1)
SET IDENTITY_INSERT [dbo].[Lugar] OFF

SET IDENTITY_INSERT [dbo].[Morada] ON
INSERT INTO [dbo].[Morada] ([MoradaID], [Rua], [CodigoPostal]) VALUES (1, 'Rua', '4444-333')
SET IDENTITY_INSERT [dbo].[Morada] OFF

SET IDENTITY_INSERT [dbo].[Parque] ON
INSERT INTO [dbo].[Parque] ([ParqueID], [NomeParque], [TipoParque], [MoradaID]) VALUES (1, 'Park', 1, 1)
SET IDENTITY_INSERT [dbo].[Parque] OFF

SET IDENTITY_INSERT [dbo].[Reserva] ON
INSERT INTO [dbo].[Reserva] ([ReservaID], [DataReserva], [DataInicio], [DataFim], [MetodoPagamentoReserva], [PrePagamento], [ClienteID], [LugarID]) VALUES (1, '2020-07-22', '2020-07-22', '2020-07-22', 'cash', 1, 1, 1)
SET IDENTITY_INSERT [dbo].[Reserva] OFF

SET IDENTITY_INSERT [dbo].[SubAluguer] ON
INSERT INTO [dbo].[SubAluguer] ([SubAluguerID], [ReservaID], [ClienteID], [Preço], [Data], [ClienteID1]) VALUES (1, 1, 1, 40, '2020-07-22', 1)
SET IDENTITY_INSERT [dbo].[SubAluguer] OFF