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


select * from prearranque;
select * from prearranque_anexo1;
select * from prearranque_anexo2;
select * from prearranque_procesos;

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

select count(*)
from ADC_Equipo_Verificador_Integrantes as evi
inner join ADC_Equipo_Verificador as ev on evi.id_equipo_verificador_adc = ev.id
inner join ADC_Anexo3 as a3 on ev.id_adc = a3.id_anexo1
#inner join ADC_Anexo3_Documentacion as doc on a3.id = doc.id_anexo3
inner join ADC_Anexo3_DocumentacionResponsable as docr on a3.id = docr.id_anexo3
where evi.estatus = 'Agregado'