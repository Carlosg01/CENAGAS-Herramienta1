use bd_cenagas

SET SQL_SAFE_UPDATES = 0

select * from usuarios where Id_Usuario = 2
insert into usuarios (Username, Email, Password, Rol, Estatus) values
('admin', 'admin@cenagas.gob.mx', '123', 'Superadmin', 'Habilitado')
insert into usuarios (Username, Email, Password, Rol, Estatus, Nombre, Paterno, Materno, Titulo, Observaciones) values
('carlosg', 'carlosg@cenagas.gob.mx', '123', 'Lider de equipo verificador', 'Habilitado', 'carlos', 'g', 'g', 'Ingeniero', 'N/A'),
('luish', 'luish@cenagas.gob.mx', '123', 'Responsable de la administración de cambio', 'Habilitado', 'luis', 'h', 'm', 'Ingeniero', 'N/A'),
('armandoh', 'armandoh@cenagas.gob.mx', '123', 'Empleado', 'Habilitado', 'armando', 'h', 'm', 'Ingeniero', 'N/A')


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

