using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class Model_ProyectoMiembro
    {
        public Proyectos proyecto { get; set; }
        public List<string> miembros { get; set; }
        public List<int> idMiembro { get; set; }
    }
}
