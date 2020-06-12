# C-sharp.Net-Dapper-DevExpress-WinForm
Masaüstü uygulamasında DevExpress15 ve Dapper (micro orm) kullanarak 
C# .net dilinde geliştirildi.Veriler listelenerek temel crud işlemleri uygulandı. 
DevExpress gridview'ine doubleclick olayına form girdilerine veri yükleme fonksiyonu yazıldı. 

SQL script;

USE [CARI]
GO
 
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CARI](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CARIKOD] [nvarchar](50) NULL,
	[CARIISIM] [nvarchar](150) NULL,
	[ADRES] [nvarchar](150) NULL,
	[IL] [nvarchar](50) NULL,
	[ILCE] [nvarchar](50) NULL,
	[ULKEKODU] [nvarchar](50) NULL,
	[TELEFON] [nvarchar](15) NULL,
	[FAX] [nvarchar](15) NULL,
	[VERGIDAIRESI] [nvarchar](50) NULL,
	[VERGINO] [nvarchar](50) NULL,
	[TCNO] [nvarchar](11) NULL,
	[POSTAKODU] [nvarchar](7) NULL,
	[TIP] [int] NULL,
	[EMAIL] [nvarchar](100) NULL,
	[WEBADRESI] [nvarchar](150) NULL,
 CONSTRAINT [PK_CARI] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
