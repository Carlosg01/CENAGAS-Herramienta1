delimiter //
create procedure DetalleProyectosEmpleado(in id_empleado int)
begin
	select
		dp.Id_DetalleProyecto,
        dp.Id_Proyecto,
        dp.Id_Residencia,
        dp.Id_Asignacion,
        dp.No_Desarrollo,
		dp.Desarrollo,
		dp.Descripcion_Actividad, 
        dp.Avance,
        dp.Faltante_Comentarios, 
        dp.Comentarios,
        dp.Plan_Accion,
        dp.Anexos,
        dp.Tipo_Proyecto
	from detalleproyecto as dp
	inner join proyectos as p on dp.Id_Proyecto = p.Id_proyecto
	inner join asignacion as a on a.Id_Proyecto = dp.Id_Proyecto
	inner join empleado as e on a.Id_Empleado = e.Id_Empleado
	where e.Id_Empleado = id_empleado;
end //
delimiter ;

call DetalleProyectosEmpleado(2)

delimiter //
create procedure ProyectosEmpleado(in id_empleado int)
begin
	select  p.Id_Proyecto,
			p.Folio_adc,
			p.Nombre,
			p.Instalacion_Area,
			p.Tipo_Cambio,
			p.Descripcion
	from proyectos as p
	inner join asignacion as a on a.Id_Proyecto = p.Id_Proyecto
	where a.Id_Empleado = id_empleado;
end //
delimiter ;

call ProyectosEmpleado(2)
