create table customers(
    accNo int primary key,
    accName nvarchar(40) not null,
    accType varchar(8) not null,
    accBalance money not null check (accBalance >= 0),
    activationStatus bit not null,
    accEmail varchar(50) not null,
    password nvarchar(20) not null
);

select * from customers;

insert into customers (accNo,accName,accType,accBalance,activationStatus,accEmail,password) values (1,'Jeff Bezos', 'Checking',922337203685477.5807,1,'Jeff@gmail.com','J377rUlez');

insert into customers values (2,'Julian Metzger','Savings',42,1,'Julian@gmail.com','Juju2cool');
