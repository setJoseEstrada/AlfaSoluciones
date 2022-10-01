create database Alfa

create table Alumnos(

id int primary key identity (1,1),
nombre varchar(50),
genero bit not null,
edad int not null
)

create table Becas(
id int primary key identity (1,1),
nombre varchar(50),
id_tipo int foreign key references TipoBecas
)

create table TipoBecas(
id int primary key identity(1,1),
nombre varchar(50)
)

create table BecasAlumnos(
id int primary key identity(1,1),
idAlumno int foreign key references Alumnos,
idBecas int foreign key references Becas

)

select Becas.nombre,TipoBecas.nombre as [Tipo de Becas]from Becas inner join TipoBecas on Becas.id_tipo = TipoBecas.id

select * from TipoBecas
insert into TipoBecas(nombre) values	('Educativa')

insert into Becas(nombre,id_tipo) values ('Academica',3)

select * from Alumnos
insert into Alumnos(nombre,genero,edad) values ('Ian',1,24)

select	Alumnos.nombre as [Nombre del Alumno],Becas.nombre AS [Nombre de la Beca], TipoBecas.nombre as [Tipo de Beca] 
from  BecasAlumnos
inner join Alumnos on BecasAlumnos.idAlumno= Alumnos.id
inner join Becas on BecasAlumnos.idBecas = Becas.id
inner join TipoBecas on BecasAlumnos.idBecas = TipoBecas.id


insert into BecasAlumnos (idAlumno,idBecas) values(1,3)
select * from BecasAlumnos
select * from Becas
select * from TipoBecas


create table TarIncompatible(
id int primary key identity(1,1),
idTipo1 int foreign key references Becas,
idTipo2 int foreign key references Becas

)

drop table TarIncompatible


create table usuario (

id int primary key identity(1,1),
correo varchar(100),
contrasena varchar(50),
nombre varchar(50)

)


select * from usuario


