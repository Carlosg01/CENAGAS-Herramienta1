use cenagas;
update roles set eliminado = 0;
update puestos set eliminado = 0;
update usuarios set eliminado = 0;
update usuarios set Notificacion_Proyecto = 'false';
update usuarios set Notificacion_Tarea = 'false';
update usuarios set Notificacion_ADC = 'false';
update adc_anexos set eliminado = 0;
update residencias set eliminado = 0;
update gasoductos set eliminado = 0;
update tramos set eliminado = 0;
update instalaciones set eliminado = 0;

update adc_actividades set eliminado = 0;
update adc_normativas set eliminado = 0;
update adc_procesos set terminado = 'false';
update adc_procesos set confirmado = 'false';
update adc_procesos set activo = 'false';
#update adc set prearranque = 'No';

update prearranque_actividades set eliminado = 0;
update prearranque_normativas set eliminado = 0;
update prearranque_procesos set terminado = 'false';
update prearranque_procesos set confirmado = 'false';
update prearranque_procesos set activo = 'false';

update estados set eliminado = 0;

update usuarios set token = null;


#update adc_anexo3_documentacion as a3 set a3.Check = 'false';

