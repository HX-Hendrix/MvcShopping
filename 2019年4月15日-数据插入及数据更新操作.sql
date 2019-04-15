--建表
CREATE TABLE [dbo].[Members] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Email]      NVARCHAR (250) NOT NULL,
    [Password]   NVARCHAR (40)  NOT NULL,
    [Name]       NVARCHAR (5)   NOT NULL,
    [Nickname]   NVARCHAR (10)  NOT NULL,
    [RegisterOn] DATETIME       NOT NULL,
    [AuthCode]   NVARCHAR (36)  NULL,
    CONSTRAINT [PK_dbo.Members] PRIMARY KEY CLUSTERED ([Id] ASC)
);
CREATE TABLE [dbo].[OrderDetails] (
    [Id]             INT IDENTITY (1, 1) NOT NULL,
    [Price]          INT NOT NULL,
    [Amount]         INT NOT NULL,
    [OrderHeader_Id] INT NOT NULL,
    [Product_Id]     INT NOT NULL,
    CONSTRAINT [PK_dbo.OrderDetails] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.OrderDetails_dbo.OrderHeaders_OrderHeader_Id] FOREIGN KEY ([OrderHeader_Id]) REFERENCES [dbo].[OrderHeaders] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.OrderDetails_dbo.Products_Product_Id] FOREIGN KEY ([Product_Id]) REFERENCES [dbo].[Products] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_OrderHeader_Id]
    ON [dbo].[OrderDetails]([OrderHeader_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Product_Id]
    ON [dbo].[OrderDetails]([Product_Id] ASC);

CREATE TABLE [dbo].[OrderHeaders] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [ContactName]    NVARCHAR (40)  NOT NULL,
    [ContactPhoneNo] NVARCHAR (25)  NOT NULL,
    [ContactAddress] NVARCHAR (MAX) NOT NULL,
    [TotalPrice]     INT            NOT NULL,
    [Memo]           NVARCHAR (MAX) NULL,
    [BuyOn]          DATETIME       NOT NULL,
    [Member_Id]      INT            NOT NULL,
    CONSTRAINT [PK_dbo.OrderHeaders] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.OrderHeaders_dbo.Members_Member_Id] FOREIGN KEY ([Member_Id]) REFERENCES [dbo].[Members] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Member_Id]
    ON [dbo].[OrderHeaders]([Member_Id] ASC);


CREATE TABLE [dbo].[ProductCategories] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (20) NOT NULL,
    CONSTRAINT [PK_dbo.ProductCategories] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Products] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [Name]               NVARCHAR (60)  NOT NULL,
    [Description]        NVARCHAR (250) NOT NULL,
    [Color]              INT            NOT NULL,
    [Price]              INT            NOT NULL,
    [PublishOn]          DATETIME       NULL,
    [ProductCategory_Id] INT            NOT NULL,
    CONSTRAINT [PK_dbo.Products] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Products_dbo.ProductCategories_ProductCategory_Id] FOREIGN KEY ([ProductCategory_Id]) REFERENCES [dbo].[ProductCategories] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ProductCategory_Id]
    ON [dbo].[Products]([ProductCategory_Id] ASC);



select * from Members
select * from OrderDetails
select * from OrderHeaders
select * from ProductCategories
select * from Products

insert into Products (Name, 
Description, 
Color, 
Price, 
PublishOn, 
ProductCategory_Id) (
select Name, 
Description, 
Color, 
Price, 
PublishOn, 
ProductCategory_Id from Products
)

update Products 
set Products.Name = left(b.Name,8) + CAST(b.Id as varchar)
from Products as b
where Id = b.Id;


update Members
set AuthCode = null
where Id = 1