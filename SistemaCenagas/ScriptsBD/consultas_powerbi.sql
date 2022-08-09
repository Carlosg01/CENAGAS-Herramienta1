#use cenagas;

/*
-------Prearranque--------
Clave
Folio
Descripción
Acciones
Actividades
Fecha inicio
Fecha fin
Avance

--------ADC---------
Proyecto
Responsable
Fecha inicio
Avance
Residencia
Tareas
*/
delimiter $$
CREATE PROCEDURE actividadesPrearranque()
BEGIN
    #---ACTIVIDADES PRE-ARRANQUE---
	select pre_proc.id_prearranque,
		   pre_act.actividad,
		   pre_proc.avance
	from prearranque_procesos as pre_proc
	inner join prearranque_actividades as pre_act on pre_proc.id_actividad = pre_act.id;
END $$

delimiter $$
CREATE PROCEDURE InfoPrearranque()
BEGIN
    #---PRE-ARRANQUE---
	select pre.Id as clave,
	   pro.nombre as proyecto,
       pro.descripcion as descripcion,
       concat(resp.nombre, " ", resp.paterno, " ", resp.materno) as Responsable,
       avg(pre_proc.avance) as avance,
       pre_a1.fecha_elaboracion as fecha_inicio,
       pre.fecha_actualizacion as fecha_fin
	from prearranque as pre
	inner join prearranque_procesos as pre_proc on pre_proc.id_prearranque = pre.id
	inner join prearranque_anexo1 as pre_a1 on pre.id = pre_a1.id_prearranque
	inner join proyectos as pro on pre.id_proyecto = pro.id
	inner join usuarios as resp on pre.id_responsable = resp.id;
END $$

delimiter $$
CREATE PROCEDURE actividadesADC()
BEGIN
    #---PRE-ARRANQUE---
    select adc_proc.id_adc,
		   adc_act.actividad,
           adc_proc.avance
    from adc_procesos as adc_proc
    inner join adc_actividades as adc_act on adc_proc.id_actividad = adc_act.id;	
END $$

delimiter $$
CREATE PROCEDURE InfoADC()
BEGIN
    #---PRE-ARRANQUE---
    select adc.id,
		   adc.folio,
           pro.nombre as proyecto,
           pro.descripcion,
           concat(u.nombre," ", u.paterno," ", u.materno) as responsable,
           avg(proc.avance) as avance,
           res.nombre as residencia,
           a1.fecha as fecha_inicio,
           adc.fecha_actualizacion as fecha_fin
    from adc
    inner join adc_procesos as proc on adc.id = proc.id_adc
    inner join adc_anexo1 as a1 on adc.id = a1.id
    inner join residencias as res on a1.id_residencia = res.id
    inner join proyectos as pro on adc.id_proyecto = pro.id
    inner join usuarios as u on adc.id_responsableadc = u.id;
END $$

call actividadesPrearranque();
call InfoPrearranque();
call actividadesADC();
call InfoADC();

#drop procedure actividadesPrearranque;
