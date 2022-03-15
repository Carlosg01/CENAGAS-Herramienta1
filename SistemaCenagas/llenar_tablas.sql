use bd_cenagas

SET SQL_SAFE_UPDATES = 0

select * from roles
update roles set registro_eliminado = 0
insert into roles (nombre) values
('Empleado'),
('Lider de equipo verificador'),
('Responsable de la administración de cambio'),
('Suplente'),
('Superadmin'),
('Administrador')

select * from usuarios where Id_Usuario = 2
update usuarios set Estatus = 'Habilitado' where Id_Usuario = 1
insert into usuarios (Username, Email, Password, Id_Rol, Estatus) values
('admin', 'admin@cenagas.gob.mx', '123', 5, 'Habilitado')
insert into usuarios (Username, Email, Password, Id_Rol, Estatus, Nombre, Paterno, Materno, Titulo, Observaciones) values
('carlosg', 'carlosg@cenagas.gob.mx', '123', 2, 'Habilitado', 'carlos', 'g', 'g', 'Ingeniero', 'N/A'),
('luish', 'luish@cenagas.gob.mx', '123', 3, 'Habilitado', 'luis', 'h', 'm', 'Ingeniero', 'N/A'),
('armandoh', 'armandoh@cenagas.gob.mx', '123', 1, 'Habilitado', 'armando', 'h', 'm', 'Ingeniero', 'N/A')
insert into usuarios (Username, Email, Password, Id_Rol, Estatus, Nombre, Paterno, Materno, Titulo, Observaciones) values
('ahdzt', 'ahdzt.97@gmail.com', '123', 2, 'Habilitado', 'armando', 'hernandez', 'muñiz', 'Ingeniero', 'N/A'),
update usuarios set registro_eliminado = 0


select * from anexos
insert into anexos (Nombre) values 
('Anexo 1. Propuesta y análisis de cambios'),
('Anexo 2. Lista de control de administración del cambio'),
('Anexo 3. Proyecto de la administración del cambio'),
('Anexo 4. Autorización del inicio de operación del cambio'),
('Anexo 5. Solicitud de retiro de cambios temporales'),
('Anexo 6. Acta de cierre de la administración del cambio')
update anexos set registro_eliminado = 0

describe residencias
select * from residencias
insert into residencias (Nombre) values
('Cárdenas'), ('Chihuahua'), ('Ciudad de México'), ('Hermosillo'),('Madero'),('Mendoza'),('Minatitlán'),('Monterrey'),('Reynosa'),
('Salamanca'), ('Tlaxcala'),('Torreón'),('Veracruz')
update residencias set registro_eliminado = 0

