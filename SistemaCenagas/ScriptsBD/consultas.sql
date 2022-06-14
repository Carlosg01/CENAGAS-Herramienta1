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
select * from prearranque_anexo2;
select * from prearranque_procesos;

delete from prearranque_procesos;
delete from prearranque_anexo2;
delete from prearranque;







