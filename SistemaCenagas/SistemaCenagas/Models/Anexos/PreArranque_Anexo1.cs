using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class PreArranque_Anexo1
    {
        [Key]
        public int Id { get; set; }
        public string Fecha_Elaboracion { get; set; }
        public int Id_Prearranque { get; set; }

    }

    public class PreArranque_Anexo1_Actividades
    {
        [Key]
        public int Id { get; set; }
        public string Accion_Descriptiva { get; set; }
        public string Responsable { get; set; }
        public int Id_Responsable { get; set; }
        public int Id_Anexo1 { get; set; } //FK
    }

    public class PreArranque_Anexo1_Actividades_Acciones
    {
        [Key]
        public int Id { get; set; }
        public int Id_Anexo1_Actividades { get; set; } //FK
        public string Actividad { get; set; }
        public string Fecha_Inicio { get; set; }
        public string Fecha_Termino { get; set; }
        public string Evidencia { get; set; }
        public float Avance { get; set; }
        public string Responsable { get; set; }
        public string Concluida { get; set; }
        
    }

    public class PreArranque_Anexo1_Model
    {
        public PreArranque_Anexo1 anexo1 { get; set; }
        public string Proyecto { get; set; }
        public List<PreArranque_Anexo1_Avtividades_Model> actividadesModel { get; set; }
        public List<string> miembros { get; set; }
        public List<int> idMiembro { get; set; }
        //public ADC_Anexo3_Documentacion[] documentacion { get; set; }
    }
    public class PreArranque_Anexo1_Actividades_Acciones_ArchivosEvidencia
    {
        [Key]
        public int Id { get; set; }
        public int Id_Accion { get; set; }
        public string Actividad { get; set; }
        public int Id_Usuario { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Extension { get; set; }
        public float Size { get; set; }
        public string Ubicacion { get; set; }
        public int Eliminado { get; set; }
    }
    public class PreArranque_Anexo1_Avtividades_Model
    {
        public PreArranque_Anexo1_Actividades accion { get; set; }
        public List<PreArranque_Anexo1_Actividades_Acciones> actividaes { get; set; }
        public int Num_actividades_actuales { get; set; }
    }
}
