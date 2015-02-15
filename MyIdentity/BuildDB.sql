drop table MyUser;
CREATE TABLE dbo.MyUser(
	myUser VARCHAR(256),
	myEmail VARCHAR(256),
	myAddress VARCHAR(256),
	phone VARCHAR(15),
	city VARCHAR(60),
	province VARCHAR(60),
	country VARCHAR(256)
);


SELECT * FROM dbo.AspNetUsers
SELECT * FROM dbo.AspNetRoles
SELECT * FROM dbo.AspNetUserRoles

select * from dbo.myuser