use cenagas;
drop database cenagas;
SET SQL_SAFE_UPDATES = 0;
delete from instalaciones;

delete from proyectos;
delete from residencias;

delete from adc_equipo_verificador;
delete from adc_equipo_verificador_integrantes;
delete from adc_anexo3_documentacionresponsable;
delete from adc_anexo3_documentacion;
delete from adc_anexo3;
delete from adc_archivos;
delete from adc_procesos;
delete from adc_anexo1;
delete from adc;


delete from prearranque_anexo1_actividades_acciones;
delete from prearranque_anexo1_actividades;
delete from prearranque_anexo1;
delete from prearranque_anexo2_seccion3;
#delete from prearranque_anexo2_seccion2_elementosrevision_catalogo;
#delete from prearranque_anexo2_seccion2_catalogo;
delete from prearranque_anexo2_seccion2_elementosrevision;
delete from prearranque_anexo2_seccion2;
delete from prearranque_anexo2;
delete from prearranque_procesos;
delete from prearranque;