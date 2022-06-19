using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class Proyectos
    {
        [Key]
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Estado_ADC { get; set; }
        public string Estado_PreArranque { get; set; }
        public int Eliminado { get; set; }
    }
}
