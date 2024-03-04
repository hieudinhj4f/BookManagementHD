create database BookManagement
go

use BookManagement
go

Create Table Account(
ID NVARCHAR(50),
NAME NVARCHAR(50),
PASSWORD NVARCHAR(50),
PRIMARY KEY (ID),
)

go
Create table AccountInformation(
IDsame NVARCHAR(50),
AGE NVARCHAR(50),
CLASS NVARCHAR(50),
TYPEACCOUNT NVARCHAR(50),
PRIMARY KEY (IDsame),
FOREIGN KEY (IDsame) REFERENCES Account(ID),
)



INSERT INTO Account
VALUES ('001','NGUYEN DINH HIEU','ADMIN'),
	   ('002','NGUYEN DINH THAO','ABCXYZ'),
	   ('003','GIANG HAI YEN','123456');


INSERT INTO AccountInformation
VALUES	( '001','21','KTPM4','Admin'),
		( '002','19','KTMT2','Guess'),
		( '003','23','HTTT1','Guess');


create table BOOKLIST (
IDbook NVARCHAR(50),
IDbookOwner NVARCHAR(50),
BookName NVARCHAR(50),
BookBorrowTime Date,
BookReturnTime Date,
)

insert into BOOKLIST 
Values ('000001',null,'Cong Chua Ngu Trong Rung',null,null),
	   ('000002',null,'Tam Cam',null,null),
	   ('000003',null,'Cay Tre Tram Dot',null,null),
	   ('000004',null,'Thanh Giong',null,null),
	   ('000005',null,'Su tich trau cau ',null,null),
	   ('000006',null,'Sach KTCT phan 1',null,null),
	   ('000007',null,'Sach KTCT phan 2',null,null),
	   ('000008',null,'Sach Triet Hoc Mac LeNin',null,null),
	   ('000009',null,'Sach Lap Trinh Nang Cao 2011',null,null),
	   ('000010',null,'Sach Lap Trinh Nang Cao 2018',null,null);
drop table BOOKLIST


update BOOKLIST
set IDbookOwner = '000' , BookBorrowTime = '2024-01-01', BookReturnTime = '2024-01-01'


update BOOKLIST
set IDbookOwner = null , bookBorrowTime = null 
where IDbook = '000001';

select *  
from BOOKLIST

select NAME,TYPEACCOUNT,IDsame,CLASS,AGE
from Account 
inner join AccountInformation on AccountInformation.IDsame = Account.ID
where NAME = 'NGUYEN DINH HIEU'

