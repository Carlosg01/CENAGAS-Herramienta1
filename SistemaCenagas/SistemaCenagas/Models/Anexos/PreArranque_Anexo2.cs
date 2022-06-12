﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class PreArranque_Anexo2
    {
        [Key]
        public int Id { get; set; }

        //Seccion 1: Datos generales
        public int Numero_Revisiones { get; set; }
        public int Revision_Actual { get; set; }
        public string Fecha_Elaboracion { get; set; }
        public int Id_Residencia { get; set; } //FK
        public string Ut_Gasoducto { get; set; } //FK
        public string Ut_Tramo { get; set; } //FK 
        public int Id_Prearranque { get; set; }

    }

    public class PreArranque_Anexo2_Seccion2
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Id_Anexo2 { get; set; } //FK
    }

    public class PreArranque_Anexo2_Seccion2_ElementosRevision
    {
        [Key]
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Elemento_Revision { get; set; }
        public string Tipo_Revision { get; set; }
        public string Tipo_Hallazgo { get; set; }
        public string Atendido { get; set; }
        public string Observacion { get; set; }
        public int Id_Anexo2_Seccion2 { get; set; } //FK
    }

    public class PreArranque_Anexo2_Seccion3
    {
        [Key]
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Descripcion_Hallazgo { get; set; }
        public string Riesgo { get; set; }
        public string Descripcion_Recomendacion { get; set; }
        public int Id_Responsable { get; set; }

    }
}
