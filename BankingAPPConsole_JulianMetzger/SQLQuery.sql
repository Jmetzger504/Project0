create database bankingApp;
use bankingApp;

drop table accountDetails;

create table accountDetails
(
	Id int primary key identity,
	Name varchar(30),
	Type varchar(1),
	Balance money,
	Status bit,
	Email varchar(40) unique,
	Password varchar(20)
)


insert into accountDetails values ('Jeff Bezos','c',999999999999,1,'Jeff@gmail.com','JeffTheBest');
insert into accountDetails values ('Julian Metzger','c', 42,1,'Jmetzger@gmail.com','Password');

select * from accountDetails where Email = 'Jeff@gmail.com' and Password = 'JeffTheBest';

select top 1 Id from accountDetails order by Id desc;

select * from accountDetails;

insert into accountDetails values('Jeff','c',1,1,'Jmetzger5@gmail.com','pass');i