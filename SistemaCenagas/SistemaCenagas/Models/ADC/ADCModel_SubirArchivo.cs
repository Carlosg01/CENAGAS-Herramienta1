﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class ADCModel_SubirArchivo
    {
        public int Id_ADC { get; set; }
        public int Id_Proceso { get; set; }
        public string Actividad { get; set; }
        public int Id_Usuario { get; set; }
        public IFormFile Archivo { get; set; }
    }
}
