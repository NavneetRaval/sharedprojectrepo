USE [Product]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 03/06/2020 1:16:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [varchar](100) NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[Quantity] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[AddUpdateProductDetails]    Script Date: 03/06/2020 1:16:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create procedure [dbo].[AddUpdateProductDetails]
(
@ProductId int,
@ProductName varchar (50),
@Price varchar (50),
@Quantity varchar (50)
)
as
begin
if @ProductId>0
begin
Update Product
set ProductName=@ProductName,
Price=@Price,
Quantity=@Quantity
where ProductId=@ProductId
End 
else
begin
Insert into Product values(@ProductName,@Price,@Quantity)
end
end
GO
/****** Object:  StoredProcedure [dbo].[DeleteProductById]    Script Date: 03/06/2020 1:16:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[DeleteProductById]
(
@ProductId int
)
as
begin
Delete from Product where ProductId=@ProductId
End 
GO
/****** Object:  StoredProcedure [dbo].[GetProductData]    Script Date: 03/06/2020 1:16:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[GetProductData]
(
   @FilterText varchar(200) = NULL
)
AS
BEGIN 
	Select 
	ProductId,
	ProductName,
	Price,
	Quantity
	from Product as p 
	Where (p.ProductName LIKE ''+ @FilterText +'%' OR @FilterText IS NULL) 
	or (p.Price LIKE ''+ @FilterText +'%' OR @FilterText IS NULL) 
	or (p.Quantity LIKE ''+ @FilterText +'%' OR @FilterText IS NULL) 
	
END
GO
