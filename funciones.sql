use bd_cenagas
describe adc_normativas
insert into usuarios (Username, Email, Password, Rol, Confirmacion_email) values
('admin', 'admin@cenagas.gob.mx', '123', 'Superadmin', 'Confirmado')

update usuarios set rol = 'superadmin' where id_usuario = 1

select * from usuarios


drop database bd_cenagas