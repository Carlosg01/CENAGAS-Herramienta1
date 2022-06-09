use cenagas;
update roles set registro_eliminado = 0;
update usuarios set registro_eliminado = 0;
update usuarios set Notificacion_Proyecto = 'false';
update usuarios set Notificacion_Tarea = 'false';
update usuarios set Notificacion_ADC = 'false';
update anexos set registro_eliminado = 0;
update residencias set registro_eliminado = 0;
update gasoductos set registro_eliminado = 0;
update tramos set registro_eliminado = 0;
update instalaciones set registro_eliminado = 0;
update adc_actividades set registro_eliminado = 0;
update adc_normativas set registro_eliminado = 0;
update adc_procesos set terminado = 'false';
update adc_procesos set confirmado = 'false';
update adc_procesos set activo = 'false';

update anexo3_documentacion as a3 set a3.Check = 'false';

