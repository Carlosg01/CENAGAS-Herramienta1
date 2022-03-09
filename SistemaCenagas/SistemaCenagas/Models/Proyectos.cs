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
        public int Id_Proyecto { get; set; }

        [MaxLength(20)]
        public string Clave { get; set; }

        [MaxLength(300)]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }
        public int Id_Lider { get; set; }
        public string ADC { get; set; }
    }
}
