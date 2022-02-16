use bd_cenagas


describe usuario
insert into usuario (Nombre, Apellido, Email, Password, Confirm_Password)
values 
('Jose','Luis Esparza', 'joselp@gmail.com', '123','123'),
('Jose','Luis Razo', 'joselr@gmail.com', '123','123'),
('Rodrigo','Espinoza Lara', 'rodrigoel@gmail.com', '123','123')
('Jessica','Ortíz Ochoterena', 'jessicaoo@gmail.com','123','123')
select * from usuario


describe empleado
insert into empleado(Id_Usuario,nombre,paterno,materno,titulo,observaciones)
values
	(1,'Jose','Luis','Esparza','Ingeniero','Tiene más de 15 años dentreo de la empresa CENEGAS y es una persona de mucha confinza ya que ha logrado darle estructura a todos los servicios que han tenido problema so incenvenientes'),
    (2,'Jose','Luis','Razo','Ing.','Tiene más de 15 años dentreo de la empresa CENEGAS y es una persona de mucha confinza ya que ha logrado darle estructura a todos los servicios que han tenido problema so incenvenientes'),
    (3,'Rodrigo','Espinoza','Lara','Ingeniero','Tiene más de 15 años dentreo de la empresa CENEGAS y es una persona de mucha confinza ya que ha logrado darle estructura a todos los servicios que han tenido problema so incenvenientes'),
    (4,'Jessica','Ortíz','Ochoterena','ing.','Tiene más de 15 años dentreo de la empresa CENEGAS y es una persona de mucha confinza ya que ha logrado darle estructura a todos los servicios que han tenido problema so incenvenientes')
    /*,
    (5,'Laura','Valencia','Salas','Ingeniero','Tiene más de 15 años dentreo de la empresa CENEGAS y es una persona de mucha confinza ya que ha logrado darle estructura a todos los servicios que han tenido problema so incenvenientes'),
    (6,'Evelyn','Mendoza','Licona','Ing.','Tiene más de 15 años dentreo de la empresa CENEGAS y es una persona de mucha confinza ya que ha logrado darle estructura a todos los servicios que han tenido problema so incenvenientes'),
    (7,'Salvador','Blancas','Guzmaán','Ingeniero','Tiene más de 15 años dentro de la empresa CENEGAS y es una persona de mucha confinza ya que ha logrado darle estructura a todos los servicios que han tenido problema so incenvenientes'),
    (8,'Agustin','Castillo','Mendieta','Ingeniero','Tiene más de 15 años dentreo de la empresa CENEGAS y es una persona de mucha confinza ya que ha logrado darle estructura a todos los servicios que han tenido problema so incenvenientes'),
    (9,'Ingrid','Valadez','Pastrana','ing.','Tiene más de 15 años dentreo de la empresa CENEGAS y es una persona de mucha confinza ya que ha logrado darle estructura a todos los servicios que han tenido problema so incenvenientes'),
    (10,'María Eugenia','Madrigal','Posadas','Ing.','Tiene más de 15 años dentreo de la empresa CENEGAS y es una persona de mucha confinza ya que ha logrado darle estructura a todos los servicios que han tenido problema so incenvenientes'),
    (11,'Sandra','Cuevas','Rojas','Ing.','Tiene más de 15 años dentreo de la empresa CENEGAS y es una persona de mucha confinza ya que ha logrado darle estructura a todos los servicios que han tenido problema so incenvenientes'),
    (12,'Ulises','Portilla','Saldivar','Ingeniero','Tiene más de 15 años dentro de la empresa CENEGAS y es una persona de mucha confinza ya que ha logrado darle estructura a todos los servicios que han tenido problema so incenvenientes'),
    (13,'Juan Carlos','Estrada','Peralta','Ingeniero','Tiene más de 15 años dentreo de la empresa CENEGAS y es una persona de mucha confinza ya que ha logrado darle estructura a todos los servicios que han tenido problema so incenvenientes'),
    (14,'Sergio','Ochoterena','Solís','ing.','Tiene más de 15 años dentreo de la empresa CENEGAS y es una persona de mucha confinza ya que ha logrado darle estructura a todos los servicios que han tenido problema so incenvenientes'),
    (15,'Miguel Ángel','Rueda','Cervantes','Ing.','Tiene más de 15 años dentreo de la empresa CENEGAS y es una persona de mucha confinza ya que ha logrado darle estructura a todos los servicios que han tenido problema so incenvenientes'),
    (16,'Verónica','Verona','Petras','Ing.','Tiene más de 15 años dentreo de la empresa CENEGAS y es una persona de mucha confinza ya que ha logrado darle estructura a todos los servicios que han tenido problema so incenvenientes') 
    */
;
select * from empleado

describe proyectos
insert into proyectos(Id_Proyecto,Folio_adc,Nombre,Instalacion_Area,Tipo_Cambio,Descripcion)
values
(100,'PRO-CEN-UTA-020-F03','Construcción de la estación de compresión Tecolutla en el sistema de Transporte GSD de 48 Catus-San','instalación área','permanente','todas las instalaciones o centros de trabajo nuevos, modificados, rehabilitados o intervenidos por la ocurrenca de un Evento, se deben de someter a un Pre-Arranque'),
    (101,'PRO-CEN-UTA-020-F04','Construcción de la estación de compresión Tecolutla','instalación área','permanente','todas las instalaciones o centros de trabajo nuevos, modificados, rehabilitados o intervenidos por la ocurrenca de un Evento, se deben de someter a un Pre-Arranque'),
    (102,'PRO-CEN-UTA-020-F05','Construcción de la Estación de Compresión Lerdo en el Sistema de Transporte GSD de 48” Cactus – San Fernando','instalación área','permanente','todas las instalaciones o centros de trabajo nuevos, modificados, rehabilitados o intervenidos por la ocurrenca de un Evento, se deben de someter a un Pre-Arranque'),
    (103,'PRO-CEN-UTA-020-F06','Interconexión entre el STGN El Encino – La Laguna o Sistema FERMACA y el Sistema Nacional de Gasoductos (SNG) operado por CENAGAS','instalación área','permanente','todas las instalaciones o centros de trabajo nuevos, modificados, rehabilitados o intervenidos por la ocurrenca de un Evento, se deben de someter a un Pre-Arranque'),
    (104,'PRO-CEN-UTA-020-F07','Construcción de la Trampa de Envió y Recibo de Diablos Las Huertas','instalación área','permanente','El procedimiento de Administración de Cambios Temporales o Definitivos con Clave: PRO-CEN-UTA-020,  debe utilizarse como mecanismo para administrar todos los cambios temporales y definitivos antes de la puesta en operación de un Sistema de Transporte nuevo, rehabilitado o modificado con el propósito de identificar y controlar los riesgos asociados a las actividades que puedan ser causa de incidentes, confirmando que los elementos de Seguridad Industrial, Seguridad Operativa y Protección al Medio Ambiente del Sistema de Transporte han sido construidos o instalados conforme al diseño y proporciona la certeza de que la instalación es segura para el inicio de operaciones. La ADC debe ser aplicada y cumplirse estrictamente a todos los niveles del  Ser el primer documento emitido cuando se propone un cambio (no debe tomarse como "papeleo de rutina" posterior al cambio).')
;
select * from proyectos


describe asignacion
insert into asignacion(Id_Asignacion,Id_Empleado,Id_Proyecto,Id_Residencia,Fecha_alta,Fecha_baja,Funcion)
values
(1000,1,100,0,'20220101','20220301','lider de equipo verificador'),
(1001,2,100,0,'20220101','20220301','suplente'),
(1002,3,100,0,'20220101','20220301','responsable del prearranque'),
(1003,4,101,0,'20220401','20220728','lider de equipo verificador')/*,
(1004,5,101,0,'20220401','20220728','suplente'),
(1005,6,101,0,'20220401','20220728','responsable del prearranque')*/
;
select * from asignacion


describe detalleproyecto
insert into detalleproyecto(Id_DetalleProyecto,Id_Proyecto,Id_Residencia,Id_Asignacion,No_Desarrollo,Desarrollo,Descripcion_Actividad,Avance,Faltante_Comentarios,Comentarios,Plan_Accion,Anexos,Tipo_Proyecto)
values
(1000,100,0,1000,1,'Solicitud de cambio','1.1 El "Proponente del Cambio"  inicia el proceso de Administración  de Cambios  (ADC) mediante el Anexo 1. Propuesta y Análisis de Cambio, PRO-CEN-UTA-020-F01. Numerales 1 y 2 (6.1)
1.2 El Responsable de la Administración del Cambio  procede a analizar para determinar la factibilidad de la misma y/o proponer soluciones alternas, revisar si han existido propuestas similares, realizadas o rechazadas e identificar problemas potenciales. Y determinar si no es un reemplazo idéntico.  (6.3)
Nota: El documento de ADC debe emitirse antes de llevar a cabo cualquier cambio y no se debe elaborar  posterior al inicio de los trabajos.',100,'','Diseño y construcción de una TRD y TED.','Realizar AR con la metodología  HazOp y What If..?','aqui va un anexo','ADC'),
(1001,100,0,1000,2,'Análisis de la propuesta de cambio','2.1 Si el cambio es factible,  el Responsable de la Administración del Cambio, debe convocar al personal que cuente con experiencia conforme al cambio propuesto y proceder al registro en el Anexo 3. Proyecto de la Administración del Cambio, PRO-CEN-UTA-020-F03. (6.4)',100,'El proyecto es viable y debe dar continuidad con el Procedimiento de Administración del Cambio.','Convocar al Equipo Verificardor  para que se lleve a cabo la identificación de posibles Riesgos en los procesos y sistemas del CENAGAS mediente el Lineamiento de Identificación de Peligros y Análisis de Riesgo, LIN-CEN-UTA-002, seleccionando la metodología adecuada.  .','Realizar AR con la metodología  HazOp y What If..?','aqui va un anexo','ADC'),
(1002,100,0,1000,3,'Evaluación del Análisis de Riesgo de Proceso correspondiente a la Administración del Cambio','3.1 El Responsable de la Administración del Cambio junto con el Equipo Verificador, elaboran el Análisis de Riesgo  (AR) conforme con el Lineamiento de Identificación de Peligros y Análisis de Riesgo, LIN-CEN-UTA-002,',100,'El equipo de FERMACA, Convocó para realizar la actividad del Analisis de Riesgo mediante la metodología HazOp y What if? plan de atención a las recomendaciones.','Realizar el analisis de para selección de metodología para elaboración de riesgo, mediente el formato, LIN-CEN-UTA-002-F01,Una vez realizado el AR mediante las herramientas  HazOp y What if? se deberá  realizar un resumen de los riesgos y recomendaciones que resulten del AR, así como el plan de atención a las recomendaciones.','Realizar AR con la metodología  HazOp y What If..?','aqui va un anexo','ADC'),
(1003,100,0,1000,4,'Evaluación Preliminar de Riesgos','4.1 El Equipo Verificador identificará los peligros inherentes y los posibles peligros que se pueden generar en situaciones específicas derivados de las propiedades fisicoquímicas o características de las sustancias peligrosas manejadas y transportadas, así como sus respectivas condiciones de proceso, evaluando las amenazas y/o formas de que dichos peligros puedan salirse de control, por lo que se identificarán escenarios de riesgos o posibles accidentes. (Lineamiento de Identificación de Peligros y Análisis de Riesgo, LIN-CEN-UTA-002). El Líder de Equipo Verificador,  registra las propuestas de cambio aceptadas o rechazadas en el Anexo 2. Lista de Control de Administración del Cambio, PRO-CEN-UTA-020-F02, ',100,'El Equipo Multidisciplinario de Análisis y Evaluación de Riesgos (EMAER) entre FERMACA – CENAGAS desarrollaron sesiones de trabajo para realizar el Hazop / What if?;  con la finalidad de identificar los riesgos y peligros en la Interconexión del CENAGAS y continuar con la administración del cambio.','Realizar el registro de la propuesta de cambio aceptada en el anexo 2 lista de Control de Administración del Cambio. Presentar lista de control de cambios identificados en la ingenieria y aprobados para su construcción.','Realizar AR con la metodología  HazOp y What If..?','aqui va un anexo','ADC'),
(1004,100,0,1000,5,'Resultado del análisis técnico-económico','5.1 El Equipo Verificador, Planea, programa y ejecutan las actividades y acciones, que deben considerase de acuerdo al proyecto, asi mismo deben estimar el tiempo requerido de cada actividad, mediante el Anexo 3. Proyecto de la Administración del Cambio, PRO-CEN-UTA-020-F03 y Minuta de Trabajo, LIN-CEN-UTA-008-F04 (6.8)',100,'Para el caso de este proyecto no se requiere del  análisis Técnico-Económico, numeral 11 del Anexo 3. Proyecto de la Administración del Cambio, PRO-CEN-UTA-020-F03, ya que el proyecto es a cargo de la compañía FERMACA, quien desarrolla toda la construcción del  proyecto.','N/A','Realizar AR con la metodología  HazOp y What If..?','aqui va un anexo','ADC'),
(1005,100,0,1000,6,'Revisión de seguridad Pre-Arranque','“Revisión de seguridad Pre-rranque” en el numeral 13 del Anexo 3. Proyecto de la Administración del Cambio, PRO-CEN-UTA-020-F03, se debe comprobar que las recomendaciones de los hallazgos o desviaciones tipo “A” y “B” fueron atendidas conforme con el Procedimiento para la Revisión de Seguridad de Pre-Arranque, PRO-CEN-UTA-021.',100,'Con fecha 16 de abril se da inicio a la construcción del proyecto, así mismo se da inicio para integrar la carpeta de revisión del Pre-arranque.','Desarrollar las actividades para el Pre-Arranque llenando el  Anexos 1 Programa de Pre-Arranque y Anexo 2 Revisión de Seguridad de Pre-Arranque de conformidad al Procedimiento para la revisión de seguridad de Pre-Arranque PRO-CEN-UTA-021','Realizar AR con la metodología  HazOp y What If..?','aqui va un anexo','ADC'),
(1006,100,0,1000,7,'Autorización para el inicio de operación','El Responsable de la Administración del Cambio, solicita la autorización para el inicio de operación del cambio, mediante el uso del Anexo 4. Autorización de Inicio de Operación del Cambio, PRO-CEN-UTA-020-F04.',100,'Mediante el formato Anexo 4. Autorización de Inicio de Operación del Cambio, PRO-CEN-UTA-020-F04. el Ingeniero Cesar Jaciel Zuro Residente de Torreón,  deberá realizar la solicitud de auitorización para el inicio de operación del cambio.','Terminada la obra al 100% conforme al Contrato de Interconexión y de Medición Número
UTA/CIM/006/2018, que celebraron por una parte “FERMACA” y “CENAGAS” se procedera a firmar el Anexo 4','Realizar AR con la metodología  HazOp y What If..?','aqui va un anexo','ADC'),
(1007,100,0,1000,8,'Cierre de la Administración del Cambio','Una vez autorizado el Inicio de Operación del Cambio, El responsable de la Administración del cambio, ejecuta las maniobras correspondientes para el inicio de operación del proceso, equipo o instalación que fue sometido a un cambio, considerando las medidas de seguridad y lo establecido en los procedimientos operativos e instrucciones de trabajo correspondientes. El Líder del Equipo Verificador, notifica al Responsable de la Administración del Cambio la conclusión de la Administración del Cambio para la elaboración del Anexo 6. Acta de Cierre de la Administración del Cambio, PRO-CEN-UTA-020-F06.',100,'Por parte de la DSI se debe avisar de la conclusión de la Administración del Cambio, mediante el  Anexo 6. Acta de Cierre de la Administración del Cambio, PRO-CEN-UTA-020-F06.','Ejecutadas las maniobras correspondientes en la aapertura de la valvula se procedera a firmar el Anexo 6','Realizar AR con la metodología  HazOp y What If..?','aqui va un anexo','ADC'),
(1008,100,0,1000,9,'Control de cambios de centro de trabajo','La Dirección de Seguridad Industrial, Revisa que la documentación seleccionada en el numeral 13 del Anexo 3. Proyecto de la Administración del Cambio, PRO-CEN-UTA-020-F03 esté cargada en el sitio web indicado y Archiva la Administración del Cambio y registra la fecha de cierre de esta en el Anexo 2. Lista de control de Administración del Cambio, PRO-CEN-UTA-020-F02. Asimismo, cambia el estatus de “En elaboración” por “Concluida”, asi como debe Difundir la Administración del Cambio al personal de la UTA.',100,'La documentación a la que hace referencía el Anexo 3. Proyecto de la Administración del Cambio, PRO-CEN-UTA-020-F03, se encuentra cargada y revisada por el equipo verificado ren el sitio web indicado.','Integrar la información conforme al Anexo 3. Proyecto de la Administración del Cambio, PRO-CEN-UTA-020-F03. ','Realizar AR con la metodología  HazOp y What If..?','aqui va un anexo','ADC')
;
select * from detalleproyecto

select  p.Nombre,
		p.Instalacion_Area,
        p.Tipo_Cambio,
        p.Descripcion
from proyectos as p
inner join asignacion as a on a.Id_Proyecto = p.Id_Proyecto
where a.Id_Empleado = 2


SET SQL_SAFE_UPDATES = 0

/*delete from detalleproyecto
delete from asignacion
delete from empleado
delete from usuario

drop database bd_cenagas*/
