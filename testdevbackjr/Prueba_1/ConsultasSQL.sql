#Depurar solo los ID diferentes de 6,7,9 y 10 de la tabla usuarios (5 puntos)
delete from pruebas.usuarios where userId in (6,7,9,10);

#Actualizar el dato Sueldo en un 10 porciento a los empleados que tienen fechas entre el año 2000 y 2001 
update empleado 
inner join usuarios 
on empleado.userId = usuarios.userId 
set Sueldo = Sueldo*.10 
where year(fechaIngreso) in ('2000', '2001') ;

#traer el nombre de usuario y fecha de ingreso de los usuarios que gananen mas de 10000 y su apellido comience con T
select usuarios.Nombre, empleado.FechaIngreso 
from usuarios
inner join empleado
on usuarios.userId = empleado.userId
where Sueldo > 10000 and Paterno like 'T*';