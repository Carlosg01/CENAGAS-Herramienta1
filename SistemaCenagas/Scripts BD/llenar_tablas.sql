use bd_cenagas;

SET SQL_SAFE_UPDATES = 0;

#select * from roles
insert into roles (nombre) values
('Empleado'),
('Lider de equipo verificador'),
('Responsable de la administración de cambio'),
('Suplente'),
('Superadmin'),
('Administrador');

#select * from usuarios where Id_Usuario = 2
#update usuarios set Estatus = 'Habilitado' where Id_Usuario = 1
insert into usuarios (Username, Email, Password, Id_Rol, Estatus) values
('admin', 'admin@cenagas.gob.mx', '123', 5, 'Habilitado');
insert into usuarios (Username, Email, Password, Id_Rol, Estatus, Nombre, Paterno, Materno, Titulo, Observaciones) values
('carlosg', 'carlosg@cenagas.gob.mx', '123', 2, 'Habilitado', 'carlos', 'g', 'g', 'Ingeniero', 'N/A'),
('luish', 'luish@cenagas.gob.mx', '123', 3, 'Habilitado', 'luis', 'h', 'm', 'Ingeniero', 'N/A'),
('armandoh', 'armandoh@cenagas.gob.mx', '123', 1, 'Habilitado', 'armando', 'h', 'm', 'Ingeniero', 'N/A'),
('ahdzt', 'ahdzt.97@gmail.com', '123', 2, 'Habilitado', 'armando', 'hernandez', 'muñiz', 'Ingeniero', 'N/A');

#select * from anexos
insert into anexos (Nombre) values 
('Anexo 1. Propuesta y análisis de cambios'),
('Anexo 2. Lista de control de administración del cambio'),
('Anexo 3. Proyecto de la administración del cambio'),
('Anexo 4. Autorización del inicio de operación del cambio'),
('Anexo 5. Solicitud de retiro de cambios temporales'),
('Anexo 6. Acta de cierre de la administración del cambio');

#describe residencias
#select * from residencias
insert into residencias (Nombre) values
('Cárdenas'),('Chihuahua'),('Ciudad de México'),('Hermosillo'),('Madero'),('Mendoza'),('Minatitlán'),('Monterrey'),('Reynosa'),
('Salamanca'), ('Tlaxcala'),('Torreón'),('Veracruz');

#select * from adc_actividades
insert into adc_actividades (actividad) values
('Solicitud de cambio'),
('Análisis de la propuesta de cambio'),
('Evaluación del Análisis de Riesgo de Proceso correspondiente a la Administración del Cambio'),
('Evaluación Preliminar de Riesgos'),
('Resultado del análisis técnico-económico'),
('Revisión de seguridad Pre-Arranque'),
('Autorización para el inicio de operación'),
('Cierre de la Administración del Cambio'),
('Control de cambios de centro de trabajo');

insert into adc_normativas (id_actividad, clave, responsable, descripcion, id_anexo) values
#actividad 1
(1, '1.1', 'Proponente del cambio', '1.1 El "Proponente del Cambio"  inicia el proceso de Administración  de Cambios  (ADC) mediante el Anexo 1. Propuesta y Análisis de Cambio. Numerales 1 y 2 (6.1)', 1),
(1, '1.2', 'Responsable de la Administración del Cambio', '1.2 El Responsable de la Administración del Cambio  procede a analizar para determinar la factibilidad de la misma y/o proponer soluciones alternas, revisar si han existido propuestas similares, realizadas o rechazadas e identificar problemas potenciales. Y determinar si no es un reemplazo idéntico.  (6.3)', 1),
#actividad 2
(2, '2.1', 'Responsable de la Administración del Cambio', '2.1 Si el cambio es factible,  el Responsable de la Administración del Cambio debe convocar al personal que cuente con experiencia conforme al cambio propuesto y proceder al registro en el Anexo 3. Proyecto de la Administración del Cambio, PRO-CEN-UTA-020-F03. (6.4)', 3),
(2, '2.2', 'Equipo Verificador', '2.2 Se debe formalizar la integración del Equipo Verificador incluyendo el nombramiento de un líder en la Minuta de Trabajo, LIN-CEN-UTA-0008-F04.  (6.4)', 3),
#actividad 3
(3, '3.1', 'Responsable de la Administración del Cambio, Equipo Verificador', '3.1 El Responsable de la Administración del Cambio junto con el Equipo Verificador elaboran el Análisis de Riesgo de Proceso (ARP) conforme al Lineamiento de Identificación de Peligros y Análisis de Riesgo. El Equipo Verificador selecciona la metodología para elaborar o actualizar el análisis de riesgo utilizando el Anexo 1.', 3),
#actividad 4
(4, '4.1', 'Equipo Verificador', '4.1 El Equipo Verificador identificará los peligros inherentes y los posibles peligros que se pueden generar en situaciones específicas derivados de las propiedades fisicoquímicas o características de las sustancias peligrosas manejadas y transportadas, así como sus respectivas condiciones de proceso, evaluando las amenazas y/o formas de que dichos peligros puedan salirse de control, por lo que se identificarán escenarios de riesgos o posibles accidentes. El Líder de Equipo Verificador registra las propuestas de cambio aceptadas o rechazadas en el Anexo 2.', 3),
#actividad 5
(5, '5.1', 'Equipo Verificador', '5.1 El Equipo Verificador planea, programa y ejecuta las actividades y acciones que deben considerase de acuerdo al proyecto, asimismo, debe estimar el tiempo requerido de cada actividad mediante el Anexo 3. Proyecto de la Administración del Cambio.', 3),
(5, '5.2', 'Responsable de la Administración del Cambio', '5.2 El Responsable de la Administración del Cambio elabora un análisis Técnico-Económico, numeral 11 del Anexo 3. Proyecto de la Administración del Cambio, PRO-CEN-UTA-020-F03, que integre invariablemente  los siguientes conceptos:
• Problemática
• Alcance del cambio
• Análisis costo-beneficio
• Presupuesto estimado
• Recursos  económicos   estimados   para  el  cambio;   materiales,   mano  de  obra, equipos, capacitación, etc.', 3),
(5, '5.3', 'Director de Seguridad industrial', '5.3  Entrega al Director de Seguridad industrial para su revisión (6.10, 6.11)', 3),
#actividad 6
(6, '6.1', 'N/A', '“Revisión de seguridad Pre-rranque” en el numeral 13 del Anexo 3. Proyecto de la Administración del Cambio, se debe comprobar que las recomendaciones de los hallazgos o desviaciones tipo “A” y “B” fueron atendidas conforme con el Procedimiento para la Revisión de Seguridad de Pre-Arranque.
Por lo anterior, hasta que no se cuente con la atención de los hallazgos o desviaciones tipo “A” y “B” la Administración del Cambio no puede ser concluida.
El proceso de Administración del Cambio es complemento para el arranque o inicio de operación de un Sistema de Transporte nuevo, modificado o rehabilitado conforme con el Procedimiento para la Revisión de Seguridad de Pre-Arranque. (6.17).', 3),
#actividad 7
(7, '7.1', 'Responsable de la Administración del Cambio', 'El Responsable de la Administración del Cambio, solicita la autorización para el inicio de operación del cambio, mediante el uso del Anexo 4. Autorización de Inicio de Operación del Cambio', 3),
(7, '7.2', 'Líder de Equipo Verificador', 'El Líder de Equipo Verificador, junto con el Equipo Verificador Revisa y valida los registros relacionados con la revisión de Seguridad de Pre-Arranque y aprueba el inicio de operación del cambio, mediante el Anexo 4. Autorización de Inicio de Operación del Cambio,', 3),
#actividad 8
(8, '8.1', 'Responsable de la Administración del Cambio', 'Una vez autorizado el Inicio de Operación del Cambio, el responsable de la Administración del cambio ejecuta las maniobras correspondientes para el inicio de operación del proceso, equipo o instalación que fue sometido a un cambio, considerando las medidas de seguridad y lo establecido en los procedimientos operativos e instrucciones de trabajo correspondientes.', 3),
(8, '8.2', 'Líder de Equipo Verificador', 'El Líder del Equipo Verificador, notifica al Responsable de la Administración del Cambio la conclusión de la Administración del Cambio para la elaboración del Anexo 6. Acta de Cierre de la Administración del Cambio.', 3),
#actividad 9
(9, '9.1', 'Dirección de Seguridad Industrial', 'La Dirección de Seguridad Industrial revisa que la documentación seleccionada en el numeral 13 del Anexo 3. Proyecto de la Administración del Cambio, esté cargada en el sitio web indicado y archiva la Administración del Cambio y registra la fecha de cierre de esta en el Anexo 2, Lista de control de Administración del Cambio. Asimismo, cambia el estatus de “En elaboración” por “Concluida”, y difunde la Administración del Cambio al personal de la UTA.', 3);

#select * from anexo1
#select * from adc
#select * from adc_procesos