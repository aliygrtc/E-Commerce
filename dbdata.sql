USE [WoolandDB]
GO
SET IDENTITY_INSERT [dbo].[Brand] ON 

INSERT [dbo].[Brand] ([ID], [Name], [DisplayIndex]) VALUES (1, N'Defacto', 1)
INSERT [dbo].[Brand] ([ID], [Name], [DisplayIndex]) VALUES (2, N'Koton', 2)
INSERT [dbo].[Brand] ([ID], [Name], [DisplayIndex]) VALUES (3, N'Zara', 3)
INSERT [dbo].[Brand] ([ID], [Name], [DisplayIndex]) VALUES (4, N'Bershka', 4)
SET IDENTITY_INSERT [dbo].[Brand] OFF
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([ID], [ParentID], [Name], [DisplayIndex]) VALUES (1, NULL, N'Giyim', 1)
INSERT [dbo].[Category] ([ID], [ParentID], [Name], [DisplayIndex]) VALUES (3, NULL, N'Ayakkabı', 3)
INSERT [dbo].[Category] ([ID], [ParentID], [Name], [DisplayIndex]) VALUES (4, 1, N'Elbise', 4)
INSERT [dbo].[Category] ([ID], [ParentID], [Name], [DisplayIndex]) VALUES (5, 3, N'Spor Ayakkabı', 1)
INSERT [dbo].[Category] ([ID], [ParentID], [Name], [DisplayIndex]) VALUES (6, 3, N'Bot', 2)
INSERT [dbo].[Category] ([ID], [ParentID], [Name], [DisplayIndex]) VALUES (12, 3, N'Klasik', 3)
INSERT [dbo].[Category] ([ID], [ParentID], [Name], [DisplayIndex]) VALUES (13, 3, N'Çizme', 5)
INSERT [dbo].[Category] ([ID], [ParentID], [Name], [DisplayIndex]) VALUES (14, 1, N'Tişört', 2)
INSERT [dbo].[Category] ([ID], [ParentID], [Name], [DisplayIndex]) VALUES (15, 1, N'Gömlek', 3)
INSERT [dbo].[Category] ([ID], [ParentID], [Name], [DisplayIndex]) VALUES (16, NULL, N'Çanta', 4)
INSERT [dbo].[Category] ([ID], [ParentID], [Name], [DisplayIndex]) VALUES (17, 16, N'Omuz', 1)
INSERT [dbo].[Category] ([ID], [ParentID], [Name], [DisplayIndex]) VALUES (18, 16, N'Sırt', 2)
INSERT [dbo].[Category] ([ID], [ParentID], [Name], [DisplayIndex]) VALUES (19, 16, N'Bel', 3)
INSERT [dbo].[Category] ([ID], [ParentID], [Name], [DisplayIndex]) VALUES (28, NULL, N'Bebek', 4)
INSERT [dbo].[Category] ([ID], [ParentID], [Name], [DisplayIndex]) VALUES (35, 28, N'Tulum', 1)
INSERT [dbo].[Category] ([ID], [ParentID], [Name], [DisplayIndex]) VALUES (36, 28, N'Ayakkabı', 2)
INSERT [dbo].[Category] ([ID], [ParentID], [Name], [DisplayIndex]) VALUES (39, NULL, N'Saat', 6)
INSERT [dbo].[Category] ([ID], [ParentID], [Name], [DisplayIndex]) VALUES (40, 39, N'Kol Saati', 1)
INSERT [dbo].[Category] ([ID], [ParentID], [Name], [DisplayIndex]) VALUES (41, 39, N'Duvar Saati', 2)
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([ID], [UserName], [Password], [NameSurname], [Status], [LastLoginDate], [Email]) VALUES (1, N'ali', N'202CB962AC59075B964B07152D234B70', N'Ali Yoğurtçu', 1, NULL, N'aliygrtc@gmail.com')
INSERT [dbo].[User] ([ID], [UserName], [Password], [NameSurname], [Status], [LastLoginDate], [Email]) VALUES (6, N'mehmet', N'202cb962ac59075b964b07152d234b70', N'Mehmet Uçar', 1, CAST(N'2024-01-12T00:00:00.0000000' AS DateTime2), N'mhmtucr@gmail.com')
INSERT [dbo].[User] ([ID], [UserName], [Password], [NameSurname], [Status], [LastLoginDate], [Email]) VALUES (18, N'onur1', N'202CB962AC59075B964B07152D234B70', N'Onur Özer', 1, NULL, N'onur@gmail.com')
INSERT [dbo].[User] ([ID], [UserName], [Password], [NameSurname], [Status], [LastLoginDate], [Email]) VALUES (26, N'emre', N'202CB962AC59075B964B07152D234B70', N'Emre Kaçar', 1, CAST(N'2024-03-11T20:08:09.7320419' AS DateTime2), N'emre@gmail.com')
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ID], [Name], [Price], [Stock], [Description], [Detail], [CargoDetail], [Status], [CategoryID], [BrandID], [UserID], [Star]) VALUES (1, N'Şişme Mont ', CAST(500.00 AS Decimal(18, 2)), 50, N'Rüzgar Geçirmez - Yanmaz', N'<p>Kış G&uuml;nleri İ&ccedil;in Birebir</p>
', N'<p>&Uuml;cretsiz Kargo</p>
', 1, 1, 4, 1, 4)
INSERT [dbo].[Product] ([ID], [Name], [Price], [Stock], [Description], [Detail], [CargoDetail], [Status], [CategoryID], [BrandID], [UserID], [Star]) VALUES (2, N'Altın Yıldız Classic', CAST(4200.00 AS Decimal(18, 2)), 30, N'Erkek Takım Elbise ', N'<p>Yırtılmaz Kalite</p>
', N'<p>&Uuml;cretsiz Kargo</p>
', 1, 1, 3, 1, 5)
INSERT [dbo].[Product] ([ID], [Name], [Price], [Stock], [Description], [Detail], [CargoDetail], [Status], [CategoryID], [BrandID], [UserID], [Star]) VALUES (9, N'Oduncu gömleği', CAST(350.00 AS Decimal(18, 2)), 36, N'Kaliteli Ürün', N'<p>L beden oduncu g&ouml;mleği.</p>
', N'<p>Aras kargo</p>
', 1, 15, 3, 1, 5)
INSERT [dbo].[Product] ([ID], [Name], [Price], [Stock], [Description], [Detail], [CargoDetail], [Status], [CategoryID], [BrandID], [UserID], [Star]) VALUES (11, N'Deri Ceket', CAST(543.00 AS Decimal(18, 2)), 65, N'Kaliteli Ürün', N'<p>Erkek Siyah Safari Deri Ceket</p>
', N'<p>&Uuml;cretsiz Kargo</p>
', 1, 4, 4, 1, 5)
INSERT [dbo].[Product] ([ID], [Name], [Price], [Stock], [Description], [Detail], [CargoDetail], [Status], [CategoryID], [BrandID], [UserID], [Star]) VALUES (12, N'Aırboos Sneakers', CAST(700.00 AS Decimal(18, 2)), 70, N'Aırboss Erkek Sneakers Menşei: Türkiye Teknik Özellikler: Normal Kalıp', N'<p>Taban y&uuml;ksekliği 4cm&#39;dir</p>
', N'<p>&Uuml;cretsiz Kargo</p>
', 1, 5, 1, 1, 4)
INSERT [dbo].[Product] ([ID], [Name], [Price], [Stock], [Description], [Detail], [CargoDetail], [Status], [CategoryID], [BrandID], [UserID], [Star]) VALUES (13, N'Siyah Tişört', CAST(300.00 AS Decimal(18, 2)), 46, N'Serin Yaz Günlerinde Terletmez', N'<h1>Los Angeles Baskılı Oversize Siyah T-shirt</h1>
', N'<p>&Uuml;cretsiz Kargo</p>
', 1, 14, 4, 1, 4)
INSERT [dbo].[Product] ([ID], [Name], [Price], [Stock], [Description], [Detail], [CargoDetail], [Status], [CategoryID], [BrandID], [UserID], [Star]) VALUES (14, N'Katusa Çanta ', CAST(899.00 AS Decimal(18, 2)), 30, N'Deri Yırtılmaz, Yanmaz', N'<h1>Siyah Yavrulu Omuz &Ccedil;antası</h1>
', N'<p>200 TL &Uuml;zeri &Uuml;cretsiz Kargo</p>
', 1, 17, 3, 1, 5)
INSERT [dbo].[Product] ([ID], [Name], [Price], [Stock], [Description], [Detail], [CargoDetail], [Status], [CategoryID], [BrandID], [UserID], [Star]) VALUES (15, N'Rainbow Yenidoğan', CAST(250.00 AS Decimal(18, 2)), 58, N'Kaliteli Ürün', N'<h1>Rainbow Yenidoğan Kimono&amp;Alt&amp;&Ouml;nl&uuml;k&amp;Şapka Set</h1>
', N'<p>&Uuml;cretsiz Kargo</p>
', 1, 35, 2, 1, 4)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[Order] ON 

INSERT [dbo].[Order] ([ID], [OrderNumber], [PaymentOption], [OrderStatus], [RecDate], [Address], [Country], [City], [ZipCode], [PhoneNumber], [NameSurname]) VALUES (23, N'99152531399', 1, 0, CAST(N'2024-03-10T13:52:53.9399352' AS DateTime2), N'Mahallesi sokak', N'Türkiye', N'İstanbul', N'12231', N'05459845564', N'Ali Görmüş')
INSERT [dbo].[Order] ([ID], [OrderNumber], [PaymentOption], [OrderStatus], [RecDate], [Address], [Country], [City], [ZipCode], [PhoneNumber], [NameSurname]) VALUES (28, N'850521216878878', 1, 0, CAST(N'2024-03-10T16:52:12.2337850' AS DateTime2), N'Küçükçekmece', N'Türkiye', N'İstanbul', N'54894', N'05956564216', N'Ahmet Yılmaz ')
INSERT [dbo].[Order] ([ID], [OrderNumber], [PaymentOption], [OrderStatus], [RecDate], [Address], [Country], [City], [ZipCode], [PhoneNumber], [NameSurname]) VALUES (29, N'522533319545545', 1, 0, CAST(N'2024-03-11T19:53:33.8594486' AS DateTime2), N'Küçükçekmece', N'Türkiye', N'İstanbul', N'55648', N'05945846448', N'Emre Kaçar')
INSERT [dbo].[Order] ([ID], [OrderNumber], [PaymentOption], [OrderStatus], [RecDate], [Address], [Country], [City], [ZipCode], [PhoneNumber], [NameSurname]) VALUES (30, N'570593319574574', 1, 0, CAST(N'2024-03-11T19:59:33.8325697' AS DateTime2), N'Küçükçekmece', N'Türkiye', N'İstanbul', N'55456', N'05816545498', N'Emre Kaçar')
INSERT [dbo].[Order] ([ID], [OrderNumber], [PaymentOption], [OrderStatus], [RecDate], [Address], [Country], [City], [ZipCode], [PhoneNumber], [NameSurname]) VALUES (31, N'145101220149150', 1, 0, CAST(N'2024-03-11T20:10:12.1601450' AS DateTime2), N'Küçükçekmece', N'Türkiye', N'İstanbul', N'45295', N'05849984654', N'Ali Yoğurtçu')
SET IDENTITY_INSERT [dbo].[Order] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderDetails] ON 

INSERT [dbo].[OrderDetails] ([ID], [OrderID], [ProductID], [ProductName], [ProductPicture], [ProductPrice], [Quantity]) VALUES (26, 23, 2, N'Altın Yıldız Classic', N'/images/ProductPicture/takım1.jpg', CAST(4200.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[OrderDetails] ([ID], [OrderID], [ProductID], [ProductName], [ProductPicture], [ProductPrice], [Quantity]) VALUES (31, 28, 2, N'Altın Yıldız Classic', N'/images/ProductPicture/takım1.jpg', CAST(4200.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[OrderDetails] ([ID], [OrderID], [ProductID], [ProductName], [ProductPicture], [ProductPrice], [Quantity]) VALUES (32, 29, 2, N'Altın Yıldız Classic', N'/images/ProductPicture/takım1.jpg', CAST(4200.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[OrderDetails] ([ID], [OrderID], [ProductID], [ProductName], [ProductPicture], [ProductPrice], [Quantity]) VALUES (33, 30, 2, N'Altın Yıldız Classic', N'/images/ProductPicture/takım1.jpg', CAST(4200.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[OrderDetails] ([ID], [OrderID], [ProductID], [ProductName], [ProductPicture], [ProductPrice], [Quantity]) VALUES (34, 31, 22, N'Pantolon', N'/images/ProductPicture/pant.webp', CAST(300.00 AS Decimal(18, 2)), 1)
SET IDENTITY_INSERT [dbo].[OrderDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductPicture] ON 

INSERT [dbo].[ProductPicture] ([ID], [Name], [Picture], [DisplayIndex], [ProductID]) VALUES (1, N'ceket1', N'/images/ProductPicture/ceket1.webp', 1, 1)
INSERT [dbo].[ProductPicture] ([ID], [Name], [Picture], [DisplayIndex], [ProductID]) VALUES (2, N'ceket2', N'/images/ProductPicture/ceket2.webp', 2, 1)
INSERT [dbo].[ProductPicture] ([ID], [Name], [Picture], [DisplayIndex], [ProductID]) VALUES (3, N'ceket3', N'/images/ProductPicture/ceket3.webp', 3, 1)
INSERT [dbo].[ProductPicture] ([ID], [Name], [Picture], [DisplayIndex], [ProductID]) VALUES (4, N'ceket4', N'/images/ProductPicture/ceket4.webp', 4, 1)
INSERT [dbo].[ProductPicture] ([ID], [Name], [Picture], [DisplayIndex], [ProductID]) VALUES (5, N'takım1', N'/images/ProductPicture/takım1.jpg', 1, 2)
INSERT [dbo].[ProductPicture] ([ID], [Name], [Picture], [DisplayIndex], [ProductID]) VALUES (6, N'takım2', N'/images/ProductPicture/takım2.jpg', 2, 2)
INSERT [dbo].[ProductPicture] ([ID], [Name], [Picture], [DisplayIndex], [ProductID]) VALUES (7, N'takım3', N'/images/ProductPicture/takım3.jpg', 3, 2)
INSERT [dbo].[ProductPicture] ([ID], [Name], [Picture], [DisplayIndex], [ProductID]) VALUES (8, N'takım4', N'/images/ProductPicture/takım4.jpg', 4, 2)
INSERT [dbo].[ProductPicture] ([ID], [Name], [Picture], [DisplayIndex], [ProductID]) VALUES (11, N'1', N'/images/ProductPicture/1.webp', 1, 12)
INSERT [dbo].[ProductPicture] ([ID], [Name], [Picture], [DisplayIndex], [ProductID]) VALUES (14, N'ceket', N'/images/ProductPicture/23.webp', 1, 11)
INSERT [dbo].[ProductPicture] ([ID], [Name], [Picture], [DisplayIndex], [ProductID]) VALUES (16, N'oduncu', N'/images/ProductPicture/oduncu.webp', 1, 9)
INSERT [dbo].[ProductPicture] ([ID], [Name], [Picture], [DisplayIndex], [ProductID]) VALUES (17, N'tişört', N'/images/ProductPicture/tişört.webp', 1, 13)
INSERT [dbo].[ProductPicture] ([ID], [Name], [Picture], [DisplayIndex], [ProductID]) VALUES (18, N'1', N'/images/ProductPicture/1ç.webp', 1, 14)
INSERT [dbo].[ProductPicture] ([ID], [Name], [Picture], [DisplayIndex], [ProductID]) VALUES (19, N'2', N'/images/ProductPicture/2ç.webp', 2, 14)
INSERT [dbo].[ProductPicture] ([ID], [Name], [Picture], [DisplayIndex], [ProductID]) VALUES (20, N'1', N'/images/ProductPicture/1b.webp', 1, 15)
INSERT [dbo].[ProductPicture] ([ID], [Name], [Picture], [DisplayIndex], [ProductID]) VALUES (21, N'2', N'/images/ProductPicture/2b.webp', 2, 15)
SET IDENTITY_INSERT [dbo].[ProductPicture] OFF
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240307134339_Initial', N'8.0.1')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240308121139_ProductStar', N'8.0.1')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240308125148_ProductStarUpdate', N'8.0.1')
GO
SET IDENTITY_INSERT [dbo].[Admin] ON 

INSERT [dbo].[Admin] ([ID], [UserName], [Password], [NameSurname], [LastLoginDate], [LastLoginIP]) VALUES (1, N'ali', N'202cb962ac59075b964b07152d234b70', N'Ali Yoğurtçu', CAST(N'2024-03-08T15:51:48.2605021' AS DateTime2), N'')
SET IDENTITY_INSERT [dbo].[Admin] OFF
GO
SET IDENTITY_INSERT [dbo].[Slide] ON 

INSERT [dbo].[Slide] ([ID], [Slogan], [Title], [Description], [Picture], [Link], [DisplayIndex]) VALUES (1, N'Yaza Mükemmel Gir!', N'Birinci Kalite', N'Ürünlerimizi değerlendirmenin en hızlı ve kolay yolu', N'/images/slide/a1.jpeg', N'#', 1)
INSERT [dbo].[Slide] ([ID], [Slogan], [Title], [Description], [Picture], [Link], [DisplayIndex]) VALUES (8, N'Alın Satın ', N'Değiştir Değerlendir', N'Mükemmel Kalite Ürünler', N'/images/slide/a2.webp', N'#', 2)
INSERT [dbo].[Slide] ([ID], [Slogan], [Title], [Description], [Picture], [Link], [DisplayIndex]) VALUES (9, N'Değiştir, Değerlendir', N'Bize Güvenin!', N'Alın, Satın, Değerlendirin', N'/images/slide/a3.jpeg', N'#', 3)
SET IDENTITY_INSERT [dbo].[Slide] OFF
GO
