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
        public int Id_Anexo2_Seccion2 { get; set; }
        public int Id_Responsable { get; set; }
        public int Id_Anexo1 { get; set; } //FK
    }

    public class PreArranque_Anexo1_Actividades_Acciones
    {
        [Key]
        public int Id { get; set; }
        public int Id_Anexo1_Actividades { get; set; } //FK
        public string Fecha_Inicio { get; set; }
        public string Fecha_Termino { get; set; }
        public string Evidencia { get; set; }
        public float Avance { get; set; }
        public string Concluida { get; set; }
        
    }
}
