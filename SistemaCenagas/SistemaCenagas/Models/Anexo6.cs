using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class Anexo6
    {
        [Key]
        public int Id_Anexo6 { get; set; }
        public string Fecha_Recepcion { get; set; }
        public int Id_Anexo1 { get; set; }
        public int Id_anexo3 { get; set; }
        //Periodo de tiempo real del cambio
        public string Fecha_Inicio { get; set; }
        public string Fecha_Termino { get; set; }

    }
}
