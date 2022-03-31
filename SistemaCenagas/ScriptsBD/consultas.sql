use cenagas;

select * from anexo1;
select * from adc;
select * from adc_procesos;

select * from usuarios;

select * from proyectos;
select * from proyecto_miembros; 

#------CATALOGOS--------
select * from anexos;
select * from gasoductos;
select * from tramos;
select * from instalaciones;
select * from roles;
select * from adc_actividades;
select * from adc_normativas;

select *
from Anexo1 as a1
inner join ADC as adc on a1.Id_PropuestaCambio = adc.Id_ADC
inner join Residencias as r on a1.Id_Residencia = r.Id_Residencia
inner join Proyectos as p on a1.Id_Proyecto = p.Id_Proyecto
inner join ADC_Procesos as proc on adc.Id_ADC = proc.Id_ADC
where adc.Registro_Eliminado = 0
group by adc.Id_ADC





