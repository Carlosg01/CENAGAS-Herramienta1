using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class PreArranque_Equipo_Verificador
    {
        [Key]
        public int Id { get; set; }
        public int Id_PreArranque { get; set; }
        public int Id_LiderEquipoVerificador { get; set; }
    }
}
