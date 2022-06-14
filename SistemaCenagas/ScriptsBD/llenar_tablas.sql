use cenagas;

SET SQL_SAFE_UPDATES = 0;
#SET GLOBAL FOREIGN_KEY_CHECKS=0;

#select * from roles
#delete from roles;
#describe roles;
insert into roles (nombre) values
('Superadmin'),
('Administrador'),
('Responsable de la administración de cambio'), #ADC
('Responsable de la revisión de seguridad del pre-arranque'), #PA
('Suplente'),
('Lider de equipo verificador'), #ADC y Pre Arranque
('Equipo verificador'), #ADC y Pre Arranque
('Empleado');

#select * from puestos;
#delete from puestos;
insert into puestos (nombre) values
('Director de seguridad industrial'),
('Director ejecutivo de operación'),
('Director ejecutivo de mantenimiento y seguridad'),
('Residente'),
('Coordinador'),
('Gerente'),
('Supervisor'),
('Direccion ejecutiva comercial de los permisos de transporte y cumplimiento regulatorio'),
('Jefe de la unidad de transporte y almacenamiento'),
('Otro');


#select * from usuarios where Id_Usuario = 6
#delete from usuarios where Id_Usuario = 6
#update usuarios set Estatus = 'Habilitado' where Id_Usuario = 1
insert into usuarios (Username, Email, Password, Id_Rol, Id_Puesto, Estatus) values
('superadmin', 'superadmin@cenagas.gob.mx', '123', 1,10, 'Habilitado'),
('admin', 'admin@cenagas.gob.mx', '123', 2,10, 'Habilitado');
insert into usuarios (Username, Email, Password, Id_Rol, Id_Puesto, Estatus, Nombre, Paterno, Materno, Titulo, Observaciones) values
('ahdzt.97', 'ahdzt.97@gmail.com', '123', 3, 10, 'Habilitado', 'armando', 'hernandez', 'muñiz', 'Ingeniero', 'N/A'),
('carlosg', 'carlosg@cenagas.gob.mx', '123', 4,10, 'Habilitado', 'carlos', 'g', 'g', 'Ingeniero', 'N/A'),
('cesarm', 'cesarm@cenagas.gob.mx', '123', 5,10, 'Habilitado', 'Cesar Jaciel', 'Suro', 'Manjarrez', 'Ingeniero', 'N/A'),
('efraint', 'efraint@cenagas.gob.mx', '123', 6,10, 'Habilitado', 'Efraín', 'Torres', 'Torres', 'Ingeniero', 'N/A'),
('titoh', 'titoh@cenagas.gob.mx', '123', 6,10, 'Habilitado', 'Tito Ulices', 'Saldierna', 'Hernández', 'Ingeniero', 'N/A'),
('josem', 'josem@cenagas.gob.mx', '123', 7,10, 'Habilitado', 'José Guadalupe', 'Moya', 'Mcclaugherty', 'Ingeniero', 'N/A');
#('luish', 'luish@cenagas.gob.mx', '123', 3, 'Habilitado', 'luis', 'h', 'm', 'Ingeniero', 'N/A'),
#('armandoh', 'armandoh@cenagas.gob.mx', '123', 1, 'Habilitado', 'armando', 'h', 'm', 'Ingeniero', 'N/A'),

#select * from anexos
insert into adc_anexos (Nombre) values 
('Anexo 1. Propuesta y análisis de cambios'),
('Anexo 2. Lista de control de administración del cambio'),
('Anexo 3. Proyecto de la administración del cambio'),
('Anexo 4. Autorización del inicio de operación del cambio'),
('Anexo 5. Solicitud de retiro de cambios temporales'),
('Anexo 6. Acta de cierre de la administración del cambio'),
#Pre-Arranque
('Anexo 1. Programa de Pre-Arranque'),
('Anexo 2. Revisión de Seguridad de Pre-Arranque'),
('Anexo 3. Descripción de criterios para la Revisión Documental y Física de la Revisión de Seguridad de Pre-Arranque.');


#describe residencias
#select * from residencias
insert into residencias (Nombre) values
('Cárdenas'),('Chihuahua'),('Ciudad de México'),('Hermosillo'),('Madero'),('Mendoza'),('Minatitlán'),('Monterrey'),('Patzcuaro'),('Reynosa'),
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

insert into adc_normativas (id_actividad, clave, responsable, descripcion, registro) values
#actividad 1
(1, '1.1', 'Proponente del cambio', '1.1 El "Proponente del Cambio"  inicia el proceso de Administración  de Cambios  (ADC) mediante el Anexo 1. Propuesta y Análisis de Cambio. Numerales 1 y 2 (6.1)', ''),
(1, '1.2', 'Responsable de la Administración del Cambio', '1.2 El Responsable de la Administración del Cambio  procede a analizar para determinar la factibilidad de la misma y/o proponer soluciones alternas, revisar si han existido propuestas similares, realizadas o rechazadas e identificar problemas potenciales. Y determinar si no es un reemplazo idéntico.  (6.3)', ''),
#actividad 2
(2, '2.1', 'Responsable de la Administración del Cambio', '2.1 Si el cambio es factible,  el Responsable de la Administración del Cambio debe convocar al personal que cuente con experiencia conforme al cambio propuesto y proceder al registro en el Anexo 3. Proyecto de la Administración del Cambio, PRO-CEN-UTA-020-F03. (6.4)', ''),
(2, '2.2', 'Equipo Verificador', '2.2 Se debe formalizar la integración del Equipo Verificador incluyendo el nombramiento de un líder en la Minuta de Trabajo, LIN-CEN-UTA-0008-F04.  (6.4)', ''),
#actividad 3
(3, '3.1', 'Responsable de la Administración del Cambio, Equipo Verificador', '3.1 El Responsable de la Administración del Cambio junto con el Equipo Verificador elaboran el Análisis de Riesgo de Proceso (ARP) conforme al Lineamiento de Identificación de Peligros y Análisis de Riesgo. El Equipo Verificador selecciona la metodología para elaborar o actualizar el análisis de riesgo utilizando el Anexo 1.', ''),
#actividad 4
(4, '4.1', 'Equipo Verificador', '4.1 El Equipo Verificador identificará los peligros inherentes y los posibles peligros que se pueden generar en situaciones específicas derivados de las propiedades fisicoquímicas o características de las sustancias peligrosas manejadas y transportadas, así como sus respectivas condiciones de proceso, evaluando las amenazas y/o formas de que dichos peligros puedan salirse de control, por lo que se identificarán escenarios de riesgos o posibles accidentes. El Líder de Equipo Verificador registra las propuestas de cambio aceptadas o rechazadas en el Anexo 2.', ''),
#actividad 5
(5, '5.1', 'Equipo Verificador', '5.1 El Equipo Verificador planea, programa y ejecuta las actividades y acciones que deben considerase de acuerdo al proyecto, asimismo, debe estimar el tiempo requerido de cada actividad mediante el Anexo 3. Proyecto de la Administración del Cambio.', ''),
(5, '5.2', 'Responsable de la Administración del Cambio', '5.2 El Responsable de la Administración del Cambio elabora un análisis Técnico-Económico, numeral 11 del Anexo 3. Proyecto de la Administración del Cambio, PRO-CEN-UTA-020-F03, que integre invariablemente  los siguientes conceptos:
• Problemática
• Alcance del cambio
• Análisis costo-beneficio
• Presupuesto estimado
• Recursos  económicos   estimados   para  el  cambio;   materiales,   mano  de  obra, equipos, capacitación, etc.', ''),
(5, '5.3', 'Director de Seguridad industrial', '5.3  Entrega al Director de Seguridad industrial para su revisión (6.10, 6.11)', ''),
#actividad 6
(6, '6.1', 'N/A', '“Revisión de seguridad Pre-rranque” en el numeral 13 del Anexo 3. Proyecto de la Administración del Cambio, se debe comprobar que las recomendaciones de los hallazgos o desviaciones tipo “A” y “B” fueron atendidas conforme con el Procedimiento para la Revisión de Seguridad de Pre-Arranque.
Por lo anterior, hasta que no se cuente con la atención de los hallazgos o desviaciones tipo “A” y “B” la Administración del Cambio no puede ser concluida.
El proceso de Administración del Cambio es complemento para el arranque o inicio de operación de un Sistema de Transporte nuevo, modificado o rehabilitado conforme con el Procedimiento para la Revisión de Seguridad de Pre-Arranque. (6.17).', ''),
#actividad 7
(7, '7.1', 'Responsable de la Administración del Cambio', 'El Responsable de la Administración del Cambio, solicita la autorización para el inicio de operación del cambio, mediante el uso del Anexo 4. Autorización de Inicio de Operación del Cambio', ''),
(7, '7.2', 'Líder de Equipo Verificador', 'El Líder de Equipo Verificador, junto con el Equipo Verificador Revisa y valida los registros relacionados con la revisión de Seguridad de Pre-Arranque y aprueba el inicio de operación del cambio, mediante el Anexo 4. Autorización de Inicio de Operación del Cambio,', ''),
#actividad 8
(8, '8.1', 'Responsable de la Administración del Cambio', 'Una vez autorizado el Inicio de Operación del Cambio, el responsable de la Administración del cambio ejecuta las maniobras correspondientes para el inicio de operación del proceso, equipo o instalación que fue sometido a un cambio, considerando las medidas de seguridad y lo establecido en los procedimientos operativos e instrucciones de trabajo correspondientes.', ''),
(8, '8.2', 'Líder de Equipo Verificador', 'El Líder del Equipo Verificador, notifica al Responsable de la Administración del Cambio la conclusión de la Administración del Cambio para la elaboración del Anexo 6. Acta de Cierre de la Administración del Cambio.', ''),
#actividad 9
(9, '9.1', 'Dirección de Seguridad Industrial', 'La Dirección de Seguridad Industrial revisa que la documentación seleccionada en el numeral 13 del Anexo 3. Proyecto de la Administración del Cambio, esté cargada en el sitio web indicado y archiva la Administración del Cambio y registra la fecha de cierre de esta en el Anexo 2, Lista de control de Administración del Cambio. Asimismo, cambia el estatus de “En elaboración” por “Concluida”, y difunde la Administración del Cambio al personal de la UTA.', '');


#Pre-Arranque
#select * from prearranque_actividades
insert into prearranque_actividades (actividad) values
('Integración del Equipo Verificador'),
('Anexo l. Programa de PreArranque, PROCEN-UTA-027-F0l.'),
('Anexo 2. Revisión de Seguridad de Pre-Arranque, PRO-CEN-UTA-02l-F02.'),
('Registro y Clasificación de los hallazgos detectados durante la revisión  Anexo 2. Revisión de Seguridad de Pre-Arranque PRO-CEN-UTA-027-F02.'),
(''),
('Duictamen de Pre-Arranque'),
('Aviso de inicio de operación'),
('Integración del expediente de la Administración del Cambio.');

#select * from prearranque_normativas
insert into prearranque_normativas (id_actividad, clave, responsable, descripcion, registro) values
#actividad 1
(1, '6.1', 'Responsable de la revisión de seguridad del pre-arranque', 
'Convoca al personal que cuente con experiencia conforme con el Proyecto del Sistema de Transporte nuevo, rehabilitado o modificado. Se debe formalizar la integración del Equipo Verificador incluyendo el nombramiento de un Líder del Equipo Verificador en la Minuta de Trabajo, LIN-CEN-UTA-0008-F04. El Equipo Verificador debe estar integrado por personal que cuente con experiencia y puede estar integrado por Directores, Residentes, Coordinadores, Gerentes, Supervisores de diferentes especialidades y Contratistas o Prestadores de Servicio o Proveedores a fin de llevar a buen término la Revisión de Seguridad de Pre-Arranque que corresponda. El Líder del Equipo Verificador debe ser una persona con autoridad suficiente para aproloar, evaluar, revisar, coordinar,', 
'Minuta de Trabajo, LI N-CEN-UTA-008-F04'),
#actividad 2
(2,'6.2',
'Responsable de la revisión de seguridad del pre-arranque y Equipo verificador',
'Elabora el Programa de Pre-Arranque, PRO-CEN-UTA-027-FOl (Anexo 1), este plan debe contener como mínimo lo siguiente: 
• Revisión Documental
• Revisión Física 
Este Programa de Pre-Arranque debe asegurar que las instalaciones o Centros de Trabajo están listos para iniciar su operación, mediante la verificación de las siguientes condiciones: 
• Cumplimiento de especificaciones de diseño y las recomendaciones de los fabricantes, durante las etapas de construcción, mantenimiento, modernización o modificación.
• Nuevas sustancias químicas o materiales usados en el proceso.
• Verificar que los sistemas de seguridad se encuentren conforme al diseño y disponibles. 
• Practicas Seguras de Trabajo, la actualización y comunicación de los procedimientos de seguridad, operación, mantenimiento y los planes de respuesta a emergencias sean actualizados.
• La capacitación necesaria al personal.
• Revisar las recomendaciones derivadas de la elaboración del análisis de riesgo.
• Revisar que la Administración de cambio exista y aplicar recomendaciones derivadas de dicho documento.
Todas las instalaciones o Centros de Trabajos nuevos, modificados, rehabilitados o intervenidos por la ocurrencia de un Evento, se deben someter a una revisión de seguridad de Pre-Arranque.',
'Anexo 1. Programa de Pre-Arranque, PRO-CEN-UTA-027-F0l'),
#actividad 3
(3,'6.3','Equipo verificador',
'Verifica el cumplimiento de la Revisión de Seguridad de Pre-Arranque mediante el uso del Anexo 2. Revisión de Seguridad de Pre-Arranque, PRO-CEN-UTA-02l-F02 al termino de las actividades en el Sistema de Transporte nuevo, rehabilitado o modificado que según corresponda. 
La Revisión de Seguridad de Pre-Arranque contiene los siguientes criterios mismos que son descritos en el Anexo 3.
Descripción de criterios para la Revisión Documental y Física de la Revisión de Seguridad de Pre-Arranque. 
Revisión documental y Física: 
• Identificación de Peligros y Análisis de Riesgos.
• Mejores Prácticas y Estándares.
• Control de Actividades y Procesos.
• Procedimientos de Operación y Prácticas Seguras.
• Competencia, Capacitación y Entrenamiento.
• Requisitos Legales.
• Integridad Mecánica y Aseguramiento de la Calidad.
• Sistemas de seguridad.
• Salud en el trabajo.
• Contratistas, Prestadores de Servicio y/o Proveedores',
'Anexo 2. Revisión de Seguridad de Pre-Arranque, PRO-CEN-UTA-021-F02. 
Anexo 3. Descripción de criterios para la revisión Documental y Física de la Revisión de Seguridad de Pre-Arranque.'),
#actividad 4
(4,'6.4','Equipo verificador',
'Registra en la Sección 3 del Anexo 2. Revisión de Seguridad de Pre-Arranque PRO-CEN-UTA-027-F02., los hallazgos encontrados. 
En caso de que NO se registren hallazgos tipo "A" o "B" pasar a la siguiente actividad. 
En caso de registrar hallazgos tipo "A" o "B", estos deben contar con su Plan de atención de hallazgos. 
Cada Plan de atención de hallazgos debe ser revisado por el Responsable indicado en la Sección 3 del Anexo 2. Revisión de Seguridad de Pre- Arranque PRO-CEN-UTA-027-F02. 
Este Responsable debe formar parte del Equipo Verificador, quien tiene la responsabilidad de verificar el cumplimiento de la atención del hallazgo conforme al Plan de Acción descrito. 
Hasta que los hallazgos sean verificados por este Responsable, la Revisión de Seguridad de Pre- Arranque (Anexo 2. Revisión de Seguridad de Pre- Arranque PRO-CEN-UTA-027-F02) no podrá ser aprobado por el Equipo Verificador. 
Los hallazgos tipo "A" tienen que ser atendidos para el inicio de operación del Sistema de Transporte nuevo, modificado o rehabilitado.',
'Anexo 2. Revisión de Seguridad de Pre-Arranque, PRO-CEN-UTA-021-F02.'),
#actividad 5
(5,'6.5','Equipo verificador',
'Aprueba en la Sección 4 del Anexo 2. Revisión de Seguridad de Pre-Arranque, PRO-CEN-UTA-021-F02.',
'Anexo 2. Revisión de Seguridad de Pre-Arranque, PRO-CEN-UTA-021-F02.'),
#actividad 6
(6,'6.6','Responsable de la revisión de seguridad del pre-arranque',
'Gestiona la contratación de una Unidad Verificadora para que emita el Dictamen de Pre-Arranque y/o Dictamen de Diseño conforme con la NOM-007-ASEA-2016.',
'Dictamen de Pre-Arranque y/o Dictamen de Diseño por una Unidad Verificadora'),
#actividad 7
(7,'6.7','Responsable de la revisión de seguridad del pre-arranque',
'Informa por medio de Oficio el inicio de operación del Sistema de Transporte nuevo, modificado o rehabilitado al Jefe de Unidad y sus Directores Ejecutivos que según correspondan. 
El oficio debe ir acompañado de la siguiente información. 
• Anexo 2. Revisión de Seguridad de Pre-Arranque PRO-CEN-UTA-021-F02 debidamente firmado por el Equipo Verificador.
• Dictamen de Pre-Arranque y/o Dictamen de Diseño.','Oficio'),
#actividad 8
(8,'6.8','Responsable de la revisión de seguridad del pre-arranque',
'Integra la siguiente información al expediente de la Administración del Cambio que según corresponda conforme con el Procedimiento de Administración de Cambios Temporales o Definitivos, PRO-CEN-UTA-020. 
• Anexo 2. Revisión de Seguridad de Pre-Arranque PRO-CEN-UTA-021-F02 debidamente firmado por el Equipo Verificador.
• Dictamen de Pre-Arranque y/o Dictamen de Diseño.',
'-Procedimiento de Administración de Cambios Temporales o Definitivos PRO-CEN-UTA-020');


insert into ADC_Anexo3_CatalogoTipoDocumentacion (TipoDocumentacion) values 
('Análisis de riesgo de proceso'),
('Análisis de riesgo del ducto'),
('Requerimientos normativos'),
('Requerimientos gubernamentales'),
('Dictamen de diseño emitido por una unidad verificadora'),
('Dictamen de pre-arranque emitido por una unidad verificadora'),
('Dictamen de verificación de instalaciones eléctricas por una unidad verificadora'),
('Revisión de seguridad de pre-arranque'),
('Inspecciones y pruebas por un tercero calificado'),
('Pruebas de hermeticidad'),
('Pruebas destructivas y no destructivas'),
('Certificados de los equipos e instrumentos'),
('Certificados de los equipos patrón'),
('Ingeniería de detalle'),
('Tecnología de proceso'),

('Diagramas de flujo de proceso'),
('Diagramas de tubería e instrumentación (DTI)'),
('Planos de construcción'),
('Planos de diseño'),
('Planos de clasificación eléctrica'),
('Diagramas eléctricos'),
('Manual de operación de conformidad con numeral 10.14 de la NOM-007-ASEA-2016'),
('Procedimientos de operación y mantenimiento'),
('Actualización de capacitación y entrenamiento del personal'),
('Materiales de construcción'),
('Especificaciones de tubería, accesorios, válvulas y conexiones'),
('Inventario de sustancias peligrosas'),
('Reportes de integridad mecánica'),
('Evaluación de desempeño de seguridad de contratistas'),
('Estructura organizacional');