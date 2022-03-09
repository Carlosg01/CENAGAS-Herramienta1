use bd_cenagas
select * from usuarios where Id_Usuario = 2
insert into usuarios (Username, Email, Password, Rol, Confirmacion_email) values
('admin', 'admin@cenagas.gob.mx', '123', 'Superadmin', 'Confirmado')

select * from anexos
insert into anexos (Nombre) values 
('Anexo 1. Propuesta y análisis de cambios'),
('Anexo 2. Lista de control de administración del cambio'),
('Anexo 3. Proyecto de la administración del cambio'),
('Anexo 4. Autorización del inicio de operación del cambio'),
('Anexo 5. Solicitud de retiro de cambios temporales'),
('Anexo 6. Acta de cierre de la administración del cambio')

describe residencias
select * from residencias
insert into residencias (Nombre) values
('Cárdenas'), ('Chihuahua'), ('Ciudad de México'), ('Hermosillo'),('Madero'),('Mendoza'),('Minatitlán'),('Monterrey'),('Reynosa'),
('Salamanca'), ('Tlaxcala'),('Torreón'),('Veracruz')

select * from proyectos
describe proyectos

drop database bd_cenagas