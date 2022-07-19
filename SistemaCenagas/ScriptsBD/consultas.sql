use cenagas;

select * from anexo3;
select * from adc;
select * from adc_procesos;
select * from adc_archivos;
select * from roles;
select * from puestos;

select * from prearranque;

select * from usuarios;

select * from proyectos;
select * from proyecto_miembros; 

#------CATALOGOS--------
select * from residencias;
select * from adc_anexos;
select * from gasoductos;
select * from tramos;
select * from instalaciones;
select * from roles;
select * from adc_actividades;
select * from adc_normativas;

select * from adc_equipo_verificador;
select * from adc_equipo_verificador_integrantes;
select * from proyecto_miembros;

select * from anexo3_documentacion as a3
where a3.check = 'true';


update anexo1 set id_residencia = 13
where id_propuestacambio = 1;

update usuarios set id_rol = 2
where id_usuario = 4;


use cenagas;
SET SQL_SAFE_UPDATES = 0;

select * from prearranque_anexo1_actividades_acciones;
select * from prearranque_anexo1_actividades;
select * from prearranque_anexo1;
select * from prearranque_anexo2_seccion2;
select * from prearranque_anexo2;
select * from prearranque_procesos;
select * from prearranque;




SET SQL_SAFE_UPDATES = 0;

delete from prearranque_procesos;
delete from prearranque_anexo1;
delete from prearranque_anexo2;
delete from prearranque;

select * from usuarios;
select * from adc_anexo3_documentacionresponsable;
select * from adc_anexo3_documentacion;
select * from ADC_Anexo3_CatalogoTipoDocumentacion;
select * from adc_equipo_verificador;
select * from adc_equipo_verificador_integrantes;


select * from prearranque_anexo2_seccion2_elementosrevision_catalogo;
select * from prearranque_anexo2_seccion2_catalogo;

select * from prearranque_anexo2_seccion2;
select * from prearranque_anexo2_seccion2_elementosrevision;

select * from adc_anexo4;
select * from adc_anexo5;
select * from adc_anexo6_documentacion;
