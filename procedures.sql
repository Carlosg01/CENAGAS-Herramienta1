delimiter //
create procedure Proc_DetallesProyectos(in id_proyecto int)
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
    where dp.Id_Proyecto = id_proyecto;
end //
delimiter ;

delimiter //
create procedure Proc_usuariosProyecto(in id_proyecto int)
begin
	select u.*
	from usuario as u
	inner join asignacion as a on u.Id_Usuario = a.Id_Usuario
	inner join detalleproyecto as dp on a.Id_proyecto = dp.Id_Proyecto
	where a.Id_Proyecto = id_proyecto
    group by u.Id_Usuario
    order by a.Funcion;
end //
delimiter ;
call Proc_usuariosProyecto(100)


delimiter //
create procedure DetalleProyectosUsuario(in id_usuario int, in id_proyecto int, in id_asignacion int)
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
	inner join usuario as u on a.Id_Usuario = u.Id_Usuario
	where u.Id_Usuario = id_usuario and p.Id_Proyecto = id_proyecto and dp.Id_Asignacion = id_asignacion
    group by dp.Id_DetalleProyecto;
end //
delimiter ;

delimiter //
create procedure ProyectosUsuario(in id_usuario int)
begin
	select  p.Id_Proyecto,
			a.Id_Asignacion,
			p.Folio_adc,
			p.Nombre,
			p.Instalacion_Area,
			p.Tipo_Cambio,
			p.Descripcion
	from proyectos as p
	inner join asignacion as a on a.Id_Proyecto = p.Id_Proyecto
	where a.Id_Usuario = id_usuario
    group by a.Id_Asignacion;
end //
delimiter ;
call ProyectosUsuario(1)

delimiter //
create procedure AsignacionProyectosUsuario(in id_usuario int)
begin
	select  a.Id_Asignacion,
			a.Id_Usuario,
            a.Id_Proyecto,
            a.Id_Residencia,
            a.Fecha_alta,
            a.Fecha_baja,
            a.Funcion
	from asignacion as a
	inner join proyectos as p on a.Id_Proyecto = p.Id_Proyecto
	where a.Id_Usuario = id_usuario
    group by a.Id_Asignacion;
end //
delimiter ;
select * from asignacion

call ProyectosEmpleado(1)
call AsignacionProyectosEmpleado(1)
call DetalleProyectosEmpleado(1,100,1000)
call ProyectosUsuario(4)