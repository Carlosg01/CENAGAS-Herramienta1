﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class ADC_Equipo_Verificador
    {
        [Key]
        public int Id_Equipo_Verificador { get; set; }
        public int Id_ADC { get; set; }
        public int Id_Lider { get; set; }
    }
}
