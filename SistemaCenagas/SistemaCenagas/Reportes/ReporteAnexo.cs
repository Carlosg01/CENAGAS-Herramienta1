using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SelectPdf;
using SistemaCenagas.Data;
using SistemaCenagas.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace SistemaCenagas.Reportes
{
    public class ReporteAnexos
    {
        #region General
        public string html_anexo1 { get; set; }
        public string html_anexo2 { get; set; }
        public string html_anexo3 { get; set; }
        public string html_anexo3_1 { get; set; }
        public string html_anexo3_2 { get; set; }
        public string CLAVE_PROYECTO { get; set; }
        public string FECHA_EMISION { get; set; }
        public string REVISION { get; set; }
        public string FECHA_REVISION { get; set; }
        public string FECHA_PROXIMA_REVISION { get; set; }
        public string CLAVE_ADC { get; set; }

        #endregion


        private readonly ApplicationDbContext context;
        private Global global;

        public ReporteAnexos(ApplicationDbContext _context, Global _global)
        {
            context = _context;
            global = _global;

            string ubicacion = "";
            
            ubicacion = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Plantillas"));
            html_anexo1 = System.IO.File.ReadAllText(ubicacion + "\\FormatosHTML\\Formato_Anexo1.html");

            ubicacion = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Plantillas"));
            html_anexo2 = System.IO.File.ReadAllText(ubicacion + "\\FormatosHTML\\Formato_Anexo2.html");

            ubicacion = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Plantillas"));
            html_anexo3 = System.IO.File.ReadAllText(ubicacion + "\\FormatosHTML\\Formato_Anexo3.html");

            ubicacion = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Plantillas"));
            html_anexo3_1 = System.IO.File.ReadAllText(ubicacion + "\\FormatosHTML\\Formato_Anexo3_1.html");

            ubicacion = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Plantillas"));
            html_anexo3_2 = System.IO.File.ReadAllText(ubicacion + "\\FormatosHTML\\Formato_Anexo3_2.html");


            Anexo2_REGISTROS = "";
        }

        #region ANEXO 1

        public byte[] Anexo1_PDF(Proyectos proyecto, int idADC)
        {
            var anexo1 = Consultas.VistaAnexo1(context, idADC);
            var adc = Consultas.VistaADC(context).Where(a => a.adc.Id == idADC).FirstOrDefault();
            html_anexo1 = html_anexo1.Replace("#CLAVE", proyecto.Clave + "-F01");
            html_anexo1 = html_anexo1.Replace("#TEMPORAL", anexo1.anexo1.Tipo_Cambio.Equals("Temporal") ? "X" : "");
            html_anexo1 = html_anexo1.Replace("#PERMANENTE", anexo1.anexo1.Tipo_Cambio.Equals("Permanente") ? "X" : "");
            html_anexo1 = html_anexo1.Replace("#FECHA", anexo1.anexo1.Fecha);
            html_anexo1 = html_anexo1.Replace("#RESIDENCIA", anexo1.residencia);
            html_anexo1 = html_anexo1.Replace("#SECTOR_AREA", anexo1.gasoducto);
            html_anexo1 = html_anexo1.Replace("#INSTALACION", anexo1.tramo);
            html_anexo1 = html_anexo1.Replace("#PROCESO", anexo1.anexo1.Proceso);
            html_anexo1 = html_anexo1.Replace("#PRESTACION_SERVICIO", anexo1.anexo1.Prestacion_Servicio);
            html_anexo1 = html_anexo1.Replace("#DESCRIPCION_CAMBIO", anexo1.anexo1.Descripcion);
            html_anexo1 = html_anexo1.Replace("#RESULTADO_ANALISIS", anexo1.anexo1.Resultados_Propuesta);
            html_anexo1 = html_anexo1.Replace("#PROPONENTE_CAMBIO", adc.proponente);
            html_anexo1 = html_anexo1.Replace("#RESPONSABLE_CAMBIO", adc.responsable);
            html_anexo1 = html_anexo1.Replace("#RESULTADO_PROPUESTA", anexo1.anexo1.Resultados_Analisis);
            html_anexo1 = html_anexo1.Replace("#ACEPTADO", anexo1.anexo1.Estatus.Equals("Aceptado") ? "X" : "");
            html_anexo1 = html_anexo1.Replace("#RECHAZADO", anexo1.anexo1.Estatus.Equals("Rechazado") ? "X" : "");

            HtmlToPdf htmlToPdf = new HtmlToPdf();
            SelectPdf.PdfDocument doc = htmlToPdf.ConvertHtmlString(html_anexo1);
            byte[] pdf = doc.Save();
            doc.Close();
            return pdf;
        }

        #endregion

        #region ANEXO 2
        public struct Anexo2_Registro
        {
            public string NoFolio { get; set; }
            public string tipoCambio1 { get; set; }
            public string tipoCambio2 { get; set; }
            public string lugar { get; set; }
            public string descripcion { get; set; }
            public string estatus { get; set; }
            public string fechaRegistro { get; set; }
            public string fechaCierre { get; set; }
            public string responsable { get; set; }
            public string lider { get; set; }
            public string estatusADC { get; set; }
            public string presentoARP { get; set; }
        }
        public string Anexo2_REGISTROS { get; set; }
        public void Anexo2_AgregarRegistros(int IdProyecto)
        {
            global.resumenADC = Consultas.VistaResumenADC(context).Where(r => r.id_proyecto == IdProyecto);

            IEnumerable<Anexo2_Registro> registros = (from p in context.Proyectos
                                               join adc in context.ADC on p.Id equals adc.Id_Proyecto
                                               join a1 in context.ADC_Anexo1 on adc.Id equals a1.Id
                                               join res in context.Usuarios on adc.Id_ResponsableADC equals res.Id
                                                      join lider in context.Usuarios on adc.Id_LiderEquipoVerificador equals lider.Id
                                                      join r in context.Residencias on a1.Id_Residencia equals r.Id
                                               //join resumen in global.resumenADC on p.Id_Proyecto equals resumen.id_proyecto
                                               where p.Id == IdProyecto
                                               select new Anexo2_Registro
                                               {
                                                   NoFolio = adc.Folio,
                                                   tipoCambio1 = a1.Tipo_Cambio,
                                                   tipoCambio2 = adc.Folio.Contains("-NV-") ? "Nuevo" : "Modificado",
                                                   lugar = r.Nombre,
                                                   descripcion = a1.Descripcion,
                                                   estatus = a1.Estatus,
                                                   fechaRegistro = a1.Fecha,
                                                   fechaCierre = adc.Fecha_Actualizacion,
                                                   responsable = $"{res.Nombre} {res.Paterno}",
                                                   lider = $"{lider.Nombre} {lider.Paterno}",
                                                   estatusADC = "En elaboración",
                                                   //estatusADC = resumen.avance_ADC >= 100 || a1.Estatus.Equals("Rechazado") ? "Concluida" : "En elaboración",
                                                   presentoARP = "N/A"

                                               }).ToList();

            int i = 0;
            foreach(var reg in registros)
            {
                var estatusADC = global.resumenADC.ElementAt(i).avance_ADC >= 100 || reg.estatus.Equals("Rechazado") ? "Concluida" : "En elaboración";
                var style = "style16";
                Anexo2_REGISTROS += "<tr class='row13'>";
                Anexo2_REGISTROS += $"<td class='column0 {style} null {style}'>{reg.NoFolio}</td>";
                Anexo2_REGISTROS += $"<td class='column1 {style} null {style}'>{reg.tipoCambio1}</td>";
                Anexo2_REGISTROS += $"<td class='column2 {style} null {style}'>{reg.tipoCambio2}</td>";
                Anexo2_REGISTROS += $"<td class='column3 {style} null {style}'>{reg.lugar}</td>";
                Anexo2_REGISTROS += $"<td class='column4 {style} null {style}'>{reg.descripcion}</td>";
                Anexo2_REGISTROS += $"<td class='column5 {style} null {style}'>{reg.estatus}</td>";
                Anexo2_REGISTROS += $"<td class='column6 {style} null {style}'>{reg.fechaRegistro.Split(' ')[0]}</td>";
                Anexo2_REGISTROS += $"<td class='column7 {style} null {style}'>{reg.fechaCierre.Split(' ')[0]}</td>";
                Anexo2_REGISTROS += $"<td class='column8 {style} null {style}'>{reg.responsable}</td>";
                Anexo2_REGISTROS += $"<td class='column9 {style} null {style}'>{reg.lider}</td>";
                Anexo2_REGISTROS += $"<td class='column10 {style} null {style}'>{estatusADC}</td>";
                Anexo2_REGISTROS += $"<td class='column11 {style} null {style}'>{reg.presentoARP}</td>";
                Anexo2_REGISTROS += "</tr>";
                i++;
            }
        }
        public byte[] Anexo2_PDF(Proyectos proyecto)
        {
            Anexo2_AgregarRegistros(proyecto.Id);

            html_anexo2 = html_anexo2.Replace("#CLAVE", proyecto.Clave + "-F02");
            html_anexo2 = html_anexo2.Replace("#REVISION", "1");
            html_anexo2 = html_anexo2.Replace("#REGISTROS", Anexo2_REGISTROS);
            
            HtmlToPdf htmlToPdf = new HtmlToPdf();
            SelectPdf.PdfDocument doc = htmlToPdf.ConvertHtmlString(html_anexo2);
            byte[] pdf = doc.Save();
            doc.Close();


            return pdf;//File(pdf, "applicaction/pdf", $"Anexo2-{proyecto.Nombre}.pdf");

        }

        #endregion

        #region ANEXO 3

        public byte[] Anexo3_PDF(int id)
        {
            var anexo3 = context.ADC_Anexo3.Find(id);
            var anexo1 = Consultas.VistaAnexo1(context, anexo3.Id_Anexo1);
            var adc = Consultas.VistaADC(context).Where(a => a.adc.Id == anexo3.Id_Anexo1).FirstOrDefault();
            var proyecto = Consultas.VistaProyectos(context).Where(p => p.Id == adc.adc.Id_Proyecto).FirstOrDefault();
            var EV = context.ADC_Equipo_Verificador.Where(a => a.Id_ADC == anexo3.Id_Anexo1).FirstOrDefault();//OrderBy(a => a.Id_Equipo_Verificador).LastOrDefault();
            var integrantesEV = context.ADC_Equipo_Verificador_Integrantes.Where(a => a.Id_Equipo_Verificador_ADC == EV.Id);

            IEnumerable<Usuarios> usuarios = (from u in context.Usuarios
                            join ev in context.ADC_Equipo_Verificador_Integrantes on u.Id equals ev.Id_Usuario
                            where ev.Id_Equipo_Verificador_ADC == EV.Id
                            select u).ToList();

            string NOMBRES = "";
            string PUESTOS = "";

            foreach (var user in usuarios)
            {
                NOMBRES += $"{user.Nombre} {user.Paterno} {user.Materno} <br />";
                PUESTOS += "N/A <br />";
            }


            html_anexo3 = html_anexo3.Replace("#CLAVE", proyecto.Clave + "-F01");
            html_anexo3 = html_anexo3.Replace("#FOLIO", adc.adc.Folio);
            html_anexo3 = html_anexo3.Replace("#FECHA_REGISTRO", DateTime.Now.ToShortDateString());
            html_anexo3 = html_anexo3.Replace("#TEMPORAL", anexo1.anexo1.Tipo_Cambio.Equals("Temporal") ? "X" : "");
            html_anexo3 = html_anexo3.Replace("#DEFINITIVO", anexo1.anexo1.Tipo_Cambio.Equals("Permanente") ? "X" : "");
            html_anexo3 = html_anexo3.Replace("#NUEVO", anexo3.Tipo_ADC.Equals("Nuevo") ? "X" : "");
            html_anexo3 = html_anexo3.Replace("#REHABILITADO", anexo3.Tipo_ADC.Equals("Rehabilitado") ? "X" : "");
            html_anexo3 = html_anexo3.Replace("#MODIFICADO", anexo3.Tipo_ADC.Equals("Modificado") ? "X" : "");
            html_anexo3 = html_anexo3.Replace("#RESIDENCIA", anexo1.residencia);
            html_anexo3 = html_anexo3.Replace("#LIDER_ADC", adc.lider);

            html_anexo3 = html_anexo3.Replace("#EQUIPO", anexo3.Equipo.Equals("true") ? "X" : "");
            html_anexo3 = html_anexo3.Replace("#INSTUMENTO", anexo3.Instrumento.Equals("true") ? "X" : "");
            html_anexo3 = html_anexo3.Replace("#COMPONENTE", anexo3.Componente_o_Dispositivo.Equals("true") ? "X" : "");
            html_anexo3 = html_anexo3.Replace("#VALVULA", anexo3.Valvula.Equals("true") ? "X" : "");
            html_anexo3 = html_anexo3.Replace("#ACCESORIO", anexo3.Accesorio_o_Ducto.Equals("true") ? "X" : "");
            html_anexo3 = html_anexo3.Replace("#COMPRESION", anexo3.Estacion_Compresion.Equals("true") ? "X" : "");
            html_anexo3 = html_anexo3.Replace("#MEDICION", anexo3.Estacion_Medicion_y_Regulacion.Equals("true") ? "X" : "");
            html_anexo3 = html_anexo3.Replace("#TRAMPA", anexo3.Trampa_Envios_y_Recibo_Diablos.Equals("true") ? "X" : "");
            html_anexo3 = html_anexo3.Replace("#OTRO", anexo3.Otro_Elemento.Equals("true") ? "X" : "");

            html_anexo3 = html_anexo3.Replace("#NO_IDENTIFICACION", anexo3.Numero_Identificacion);

            html_anexo3 = html_anexo3.Replace("#NOMBRES", NOMBRES);
            html_anexo3 = html_anexo3.Replace("#PUESTOS", PUESTOS);

            

            HtmlToPdf htmlToPdf = new HtmlToPdf();
            SelectPdf.PdfDocument doc = htmlToPdf.ConvertHtmlString(html_anexo3);
            byte[] pdf = doc.Save();
            doc.Close();
            return pdf;
        }

        #endregion

    }
}
