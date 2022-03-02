use bd_cenagas
create table Instalaciones(
	id int auto_increment,
    instalacion varchar (200),
    ut_instalacion varchar (200),  
    clase varchar (200),
    km float,
    residencia varchar (200),
    region varchar (200),
    ut_tramo varchar (200),
    sistema varchar (200),
    longitud_X_decimal float,
    latitud_Y_decimal float,
    altitud_Z_decimal float,
    sector_pemex varchar (200),
    gmas_pemex varchar (200),
    primary key (id)    
)
select * from instalaciones where id = 1314
drop table instalaciones