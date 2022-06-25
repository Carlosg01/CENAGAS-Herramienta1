use cenagas;

insert into elementos3s (elemento, clave, descripcion) values 
('Política de Seguridad Industrial, Seguridad Operativa y protección al medio ambiente','P','Política de Seguridad Industrial, Seguridad Operativa y protección al medio ambiente'),
('La identificación de peligros y Análisis de riesgos','IPAR','La identificación de peligros y Análisis de riesgos' ),
('Requisitos legales','RL','Requisitos legales'),
('Metas, objetivos e Indicadores','MOI','Metas, objetivos e Indicadores'),
('Funciones, Responsabilidades y Autoridad','FRA','Funciones, Responsabilidades y Autoridad'),
('Competencia, Capacitación y Entrenamiento','CCE','Competencia, Capacitación y Entrenamiento'),
('Comunicación, Participación y Consulta','CPC','Comunicación, Participación y Consulta'),
('Control de Documentos y Registros','CDR','Control de Documentos y Registros'),
('Mejores Practicas y Estándares','MPE','Mejores Practicas y Estándares'),
('Control de Actividades y Procesos','CAP','Control de Actividades y Procesos'),
('Integridad Mecánica y Aseguramiento de la Calidad','IMAC','Integridad Mecánica y Aseguramiento de la Calidad'),
('Seguridad de Contratista','SC','Seguridad de Contratista'),
('Preparación y Respuesta a Emergencias','PRE','Preparación y Respuesta a Emergencias'),
('Monitoreo, Verificación y Evaluación','MOI','Monitoreo, Verificación y Evaluación'),
('Auditorias','A','Auditorias'),
('Investigación de Incidentes y Accidentes','IIA','Investigación de Incidentes y Accidentes'),
('Revisión de los Resultados','RR','Revisión de los Resultados'),
('Informes de desempeño','ID','Informes de desempeño');

insert into fuente_deteccion (fuente, abreviatura) values
('Administración Del Cambio (ADC)','ADC'),
('Análisis de Riesgo de Proceso (ARP)','ARP'),
('Auditoría Externa','AE'),
('Auditoría Interna','AI'),
('Cambio de Uso de Suelo en Terrero Forestal (CUSTF)','CUSTF'),
('Celaje Aéreo','CA'),
('Celaje Terrestre (Patrullaje)','CT'),
('Estudio de Riesgo Ambiental (ERA)','ERA'),
('Estudio Técnico Justificativo (ETJ)','ETJ'),
('Evaluación de Impacto Social (EvIS)','EvIS'),
('Evaluación de Protección Civil','EPC'),
('Inspección Preventiva de Riesgo (IPR)','IPR'),
('Investigación Causa Raiz (ICR)','ICR'),
('Manifestación de Impacto Ambiental (MIA)','MIA'),
('Pre-Arranque','PA'),
('Reaseguro','RA'),
('Simulacros','SIM'),
('Verificación NOM-007-ASEA-2016 (Transporte GN)','NOM-007'),
('Verificación NOM-009-ASEA-2017 (Integridad de ducto)','NOM-009');

insert into etapa_realizada (etapa, avance) values
('Aceptación de la recomendación',5),
('Clasificación y priorización',10),
('Designación del responsable',20),
('Elaboración del Plan de Acción',35),
('Ejecución de actividades',80),
('Solicitud cierre',85),
('Verificación atención recomendación',95),
('Autorización cierre',100);

insert into zonas (nombre) values
('Norte'),('Centro'),('Sur');

insert into estados (Pais, Estado, Capital, Latitud, Longitud) values 
('México','Aguascalientes','Aguascalientes',21.88,	-102.29),
('México','Baja California Norte','Mexicali',	32.65,	-115.45),
('México','Baja California Sur','La Paz',24.13,	-110.32),
('México','Campeche','San Francisco de Campeche',	19.85,	-90.52),
('México','Chihuahua','Chihuahua',	28.64,	-106.09),
('México','Chiapas','Tuxtla Gutiérrez',	16.75,	-93.12),
('México','Coahuila','Saltillo',	25.43,	-101.00),
('México','Colima','Colima',	19.24,	-103.73),
('México','Durango','Victoria de Durango',	24.03,	-104.65),
('México','Guanajuato','Guanajuato',	21.08,	-101.25),
('México','Guerrero','Chilpancingo de los Bravo',	17.55,	-99.50),
('México','Hidalgo','Pachuca de Soto',	20.12,	-98.75),
('México','Jalisco','Guadalajara',	20.67,	-103.34),
('México','Estado de México','Toluca de Lerdo',	19.29,	-99.65),
('México','Michoacán de Ocampo','Morelia',	19.70,	-101.19),
('México','Morelos','Cuernavaca',	18.93,	-99.23),
('México','Nayarit','Tepic',	21.50,	-104.90),
('México','Nuevo León','Monterrey',	25.67,	-100.31),
('México','Oaxaca','Oaxaca de Juárez',	17.07,	-96.72),
('México','Puebla','Puebla de Zaragoza',	19.04,	-98.21),
('México','Querétaro','Santiago de Querétaro',	20.59,	-100.39),
('México','Quintana Roo','Chetumal',	18.51,	-88.30),
('México','San Luis Potosí','San Luis Potosí',	22.16,	-100.99),
('México','Sinaloa','Culiacán Rosales',	24.80,	-107.43),
('México','Sonora','Hermosillo',	29.09,	-110.96),
('México','Tabasco','Villahermosa',	17.92,	-92.39),
('México','Tamaulipas','Ciudad Victoria',	23.74,	-99.14),
('México','Tlaxcala','Tlaxcala de Xicohténcatl',	19.31,	-98.24),
('México','Vecracruz','Xalapa-Enríquez',	19.54,	-96.91),
('México','Yucatán','Mérida',	20.97,	-89.62),
('México','Zacatecas','Zacatecas',	22.77,	-102.58);

insert into residencias (Nombre, Id_Estado) values
('Cárdenas',26),
('Chihuahua',5),
('Estado de México',14),
('Hermosillo',25),
('Madero', 27),
('Mendoza', 27),
('Minatitlán', 29),
('Monterrey', 18),
('Reynosa', 27),
('Salamanca', 13), 
('Tlaxcala', 28),
('Torreón',7),
('Veracruz',29);

insert into especialidades (nombre) values
('Corrosión'),
('Medición'),
('Operación'),
('Seguridad'),
('Compresión'),
('Tuberías y Obra Civil'),
('SCADA');

insert into sistema (nombre, denominacion) values
('SNG',	'Sistema Nacional de Gasoductos'),
('SNH',	'Sistema Naco Hermosillo');

insert into ddv (nombre) values 
('Compartido'),
('Privado'),
('Propio');

insert into unidad (abreviatura, nombre) values 
('UTA','Unidad de Transporte y Almacenamiento'),
('UFA','Unidad de Finanzas y Administración'),
('UTOI','Unidad de Tecnologías Operacionales y de Información'),
('UAJ','Unidad de Asuntos Jurídicos'),
('UGTP','Unidad de Gestión Técnica y Planeación'),
('','Arcelormmital'),
('','CSIPA'),
('','ARCMMT/CSIPA');

insert into direccion_ejecutiva (abreviatura, nombre, id_unidad) values
('DEMS','DEMS - Dirección Ejecutiva de Mantenimiento y Seguridad', 1),
('DEO','DEO - Dirección Ejecutiva de Operación', 1),
('DECPTCR','DECPTCR - Dirección Ejecutiva Comercial de los Permisos de Transporte y Cumplimiento Regulatorio',1),
('DENVC','DENVC - Dirección Ejecutiva de Normatividad y Validación Contractual', 1),
('','Arcelormmital', 1),
('','CSIPA', 1),
('','ARCMMT/CSIPA', 1);
#select * from direccion_ejecutiva;

insert into direccion (abreviatura, nombre, id_direccionejecutiva) values
('DSI','Dirección de Seguridad Industrial',1),
('DMS','Dirección de Mantenimiento Integral',1),
('DMEDA','Dirección de Mantenimiento de Equipo Dinámico y Automatización',1),
('DPE','Dirección de Planeación y Evaluación',1),
('DR','Dirección de Regionales',1),
('DO','Dirección de Operacion',2),
('DCM','Dirección de Confiabilidad de Medición',2),
('DM','Dirección de Medición',2),
('DN','Dirección de Normatividad',4),
('DATPT','Dirección de Administración Técnica de Permisos de Transporte',3),
('DGCPT','Dirección de Gestión Comercial de los Permisos de Transporte',3),
('','Arcelormmital',5),
('','CSIPA',6),
('','ARCMMT/CSIPA',7);

insert into tipo (abreviatura, tipo_instalacion, resumen) values
('Cruce','Cruzamiento','Si'),
('DDV','Derecho de Vía',''),
('EC','Estación de Compresión','Si'),
('ERM','Estación de Regulación y Medición',''),
('PMP','Punto de Medición de Potecial','Si'),
('Ramal','Ramal',''),
('RPC','Rectificador de Protección Catódica','Si'),
('TED','Trampa de Envío de Diablos','Si'),
('TRD','Trampa de Recibo de Diablos','Si'),
('VS','Válvula de Seccionamiento','Si'),
('VT','Válvula Troncal','Si'),
('GSD','Gasoducto','');






