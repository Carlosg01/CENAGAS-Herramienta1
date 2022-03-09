use bd_cenagas

delimiter //
create procedure vistaProyectos()
begin
	select *
    from proyectos as p
    inner join usuarios as u on p.Id_Lider = u.Id_Usuario
end // 
delimiter ;