use cenagas;

select * from anexo1;
select * from adc;
select * from adc_procesos;
select * from adc_archivos;

select * from usuarios;

select * from proyectos;
select * from proyecto_miembros; 

#------CATALOGOS--------
select * from residencias;
select * from anexos;
select * from gasoductos;
select * from tramos;
select * from instalaciones;
select * from roles;
select * from adc_actividades;
select * from adc_normativas;

select * from adc_equipoverificador;


update anexo1 set id_residencia = 13
where id_propuestacambio = 1;

update usuarios set id_rol = 2
where id_usuario = 4;






