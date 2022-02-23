use bd_cenagas

select *
from empleado as e
inner join asignacion as a on e.Id_Empleado = a.Id_Empleado
inner join detalleproyecto as dp on a.Id_Asignacion = dp.Id_Asignacion
where a.Funcion like '%Lider%'