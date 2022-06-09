using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class Anexo3_CatalogoTipoDocumentacion
    {
        [Key]
        public int Id { get; set; }
        public string TipoDocumentacion { get; set; }
    }
}
