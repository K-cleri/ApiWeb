------Tabla de Clientes-----
create table clientes(
ID integer primary key identity(1,1),
Nombre Varchar(50),
Direccion varchar(50),
Telefono varchar(10),
Email varchar(40),
Rfc varchar(25),
Estado varchar(10)
);
insert into clientes(Nombre,Direccion,Telefono,Email,Rfc,Estado)values('Carlos Rodriguez','Vinolitos #2546','6679584621','ejemplo@ejem.co','esdljhf456e45','Activo')
insert into clientes(Nombre,Direccion,Telefono,Email,Rfc,Estado)values('Jose Palomares','Hidalgo #46','6669548712','emplo@ejem.co','sdf654gsd456','Activo')
insert into clientes(Nombre,Direccion,Telefono,Email,Rfc,Estado)values('Raul Beltran','San Rafael #6','6678545692','ejlo@ejem.co','wer56456sdf65','Activo')
insert into clientes(Nombre,Direccion,Telefono,Email,Rfc,Estado)values('Gonzalo Valenzuela','Vinos #9','6678654984','eplo@ejem.co','sdf5645sdf564','Activo')
insert into clientes(Nombre,Direccion,Telefono,Email,Rfc,Estado)values('Rodrigo Cazares','litos #2','6669854892','mplo@ejem.co','sdf564sdg564','Activo')
select * from clientes
-------Tabla de Roles-------
create table roles(
ID integer primary key identity(1,1),
Nombre varchar(20),
Descripcion varchar(50),
Estado varchar(10)
);
insert into roles(Nombre,Descripcion,Estado)values('Cajero','Atiende la caja','Activo')
insert into roles(Nombre,Descripcion,Estado)values('Gerente','Encargado de area','Activo')
select * from roles
------Tabla de Usuarios------
create table usuarios(
ID integer primary key identity(1,1),
IDRol integer foreign key references roles(ID)
on update cascade
on delete cascade,
Nombre varchar(50),
Direccion varchar(50),
Telefono varchar(10),
Email varchar(40),
Estado varchar(10),
);
insert into usuarios(Nombre,Direccion,Telefono,Email,Estado,IDRol)values('Jose Jimenez','Vinolitos #254','6675894562','ejem@ejemplo.com','Activo',1)
insert into usuarios(Nombre,Direccion,Telefono,Email,Estado,IDRol)values('Carlos Ramos','Vinolitos #254','6674594562','ejem@ejplo.com','Activo',1)
insert into usuarios(Nombre,Direccion,Telefono,Email,Estado,IDRol)values('Bryan Gonzales','Vinol #25','6674213362','ejem@ej.com','Activo',2)

select * from usuarios
------Tabla de Catergorias-----
create table categorias(
ID integer primary key identity(1,1),
Nombre varchar(30),
Descripcion varchar(40),
Estado varchar(10)
);
insert into categorias(Nombre,Descripcion,Estado)values('Jugetes','Area de productos infantiles','Activo')
insert into categorias(Nombre,Descripcion,Estado)values('Almacen','Area de guardado de articulos','Activo')
insert into categorias(Nombre,Descripcion,Estado)values('Linea Blanca','Area de productos para Hogar','Activo')
select * from categorias
------Tabla de Productos-----
create table productos(
ID integer primary key identity(1,1),
IDCategoria integer foreign key references categorias(ID)
on update cascade
on delete cascade,
Codigo varchar(25),
Nombre varchar(30),
PrecioVenta money,
Existencia integer,
Descripcion varchar(50),
Estado varchar(10)
);
insert into productos(IDCategoria,Codigo,Nombre,PrecioVenta,Existencia,Descripcion,Estado)values(2,'45wf4wefs4','Mesa de madera',5680.21,2,'Mesa de madera solida color Marron','Activo')
insert into productos(IDCategoria,Codigo,Nombre,PrecioVenta,Existencia,Descripcion,Estado)values(3,'6+5gfdfgg5','Cobos de lego',4523.25,8,'Cubos de lego para construccion','Activo')
insert into productos(IDCategoria,Codigo,Nombre,PrecioVenta,Existencia,Descripcion,Estado)values(1,'987ds56f4f','Reclinable',2345.05,1,'Sillon reclinable color marron','Activo')
insert into productos(IDCategoria,Codigo,Nombre,PrecioVenta,Existencia,Descripcion,Estado)values(2,'123ad4ffd5','Casita de muñecas',1564.50,1,'Casita para muñecas con gran variedad e accesorios','Activo')
select * from productos

------Tabla de Ventas------
create table ventas(
ID integer primary key identity(1,1),
IDCliente integer foreign key references clientes(ID)
on update cascade
on delete cascade,
IDUsuario integer foreign key references usuarios(ID)
on update cascade
on delete cascade,
NumFactura varchar(30),
FechaHora datetime,
Impuesto money,
Total money
);
insert into ventas(IDCliente,IDUsuario,NumFactura,FechaHora,Impuesto,Total)values(2,3,'ew654wer66',getdate(),458.52,18856.36)
insert into ventas(IDCliente,IDUsuario,NumFactura,FechaHora,Impuesto,Total)values(1,2,'yjgh545645',getdate(),4654.02,89455.12)
insert into ventas(IDCliente,IDUsuario,NumFactura,FechaHora,Impuesto,Total)values(4,1,'jyf454456h',getdate(),560.00,9842.12)
insert into ventas(IDCliente,IDUsuario,NumFactura,FechaHora,Impuesto,Total)values(3,1,'hdf+656655',getdate(),445.12,8000.02)
insert into ventas(IDCliente,IDUsuario,NumFactura,FechaHora,Impuesto,Total)values(5,2,'ht5f445df6',getdate(),10000.11,102564.00)
select * from ventas
drop table ventas
drop table detallesventas
drop table clientes
------Tablas de Detalles ventas------
create table detallesventas(
ID integer primary key identity(1,1),
IDVenta integer foreign key references ventas(ID)
on update cascade
on delete cascade,
IDProducto integer foreign key references productos(ID)
on update cascade
on delete cascade,
Cantidad integer,
Precio money,
Descuento money,
Estado Varchar(10)
);
insert into detallesventas(IDVenta,IDProducto,Cantidad,Precio,Descuento,Estado)values(5,4,6,454.55,450.52,'Vendido')
insert into detallesventas(IDVenta,IDProducto,Cantidad,Precio,Descuento,Estado)values(2,1,2,65482.55,45.52,'Vendido')
insert into detallesventas(IDVenta,IDProducto,Cantidad,Precio,Descuento,Estado)values(3,3,3,1235.55,456.52,'Vendido')
insert into detallesventas(IDVenta,IDProducto,Cantidad,Precio,Descuento,Estado)values(1,2,1,1400.55,4510.52,'Vendido')
insert into detallesventas(IDVenta,IDProducto,Cantidad,Precio,Descuento,Estado)values(4,1,5,1254.55,564.52,'Vendido')
select * from detallesventas
