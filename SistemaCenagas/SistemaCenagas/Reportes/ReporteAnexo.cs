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
        public string html_anexo4 { get; set; }
        public string html_anexo5 { get; set; }
        public string html_anexo6 { get; set; }
        public string html_anexo3_1 { get; set; }
        public string html_anexo3_2 { get; set; }
        public string CLAVE_PROYECTO { get; set; }
        public string FECHA_EMISION { get; set; }
        public string REVISION { get; set; }
        public string FECHA_REVISION { get; set; }
        public string FECHA_PROXIMA_REVISION { get; set; }
        public string CLAVE_ADC { get; set; }
        public string Dir_LOGO { get; set; }
        public string mark_yes { get; set; }
        public string mark_no { get; set; }

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
            html_anexo4 = System.IO.File.ReadAllText(ubicacion + "\\FormatosHTML\\Anexo4.html");

            ubicacion = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Plantillas"));
            html_anexo5 = System.IO.File.ReadAllText(ubicacion + "\\FormatosHTML\\Anexo5.html");

            ubicacion = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Plantillas"));
            html_anexo6 = System.IO.File.ReadAllText(ubicacion + "\\FormatosHTML\\Anexo6.html");

            ubicacion = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Plantillas"));
            html_anexo3_1 = System.IO.File.ReadAllText(ubicacion + "\\FormatosHTML\\Formato_Anexo3_1.html");

            ubicacion = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Plantillas"));
            html_anexo3_2 = System.IO.File.ReadAllText(ubicacion + "\\FormatosHTML\\Formato_Anexo3_2.html");


            Dir_LOGO = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Plantillas\\FormatosHTML\\"));

            mark_yes = "<span>&#9746;</span>";
            mark_no = "<span>&#9744;</span>";

            Anexo2_REGISTROS = "";
        }

        #region ANEXO 1

        public byte[] Anexo1_PDF(Proyectos proyecto, int idADC)
        {
            var anexo1 = Consultas.VistaAnexo1(context, idADC);
            var adc = Consultas.VistaADC(context).Where(a => a.adc.Id == idADC).FirstOrDefault();
            html_anexo1 = html_anexo1.Replace("#C_LOGO", Dir_LOGO + "logo.png");
            html_anexo1 = html_anexo1.Replace("#C_0_", "PRO-CEN-UTA-020-F01");
            html_anexo1 = html_anexo1.Replace("#C_1_", anexo1.anexo1.Tipo_Cambio.Equals("Temporal") ? mark_yes : mark_no);
            html_anexo1 = html_anexo1.Replace("#C_2_", anexo1.anexo1.Tipo_Cambio.Equals("Permanente") ? mark_yes : mark_no);
            html_anexo1 = html_anexo1.Replace("#C_3_", anexo1.anexo1.Fecha);
            html_anexo1 = html_anexo1.Replace("#C_4_", anexo1.residencia);
            html_anexo1 = html_anexo1.Replace("#C_5_", anexo1.gasoducto);
            html_anexo1 = html_anexo1.Replace("#C_6_", anexo1.tramo);
            html_anexo1 = html_anexo1.Replace("#C_7_", anexo1.anexo1.Proceso);
            html_anexo1 = html_anexo1.Replace("#C_8_", anexo1.anexo1.Prestacion_Servicio);
            html_anexo1 = html_anexo1.Replace("#C_9_", "<br/>" + anexo1.anexo1.Descripcion + "<br/><br/>");
            html_anexo1 = html_anexo1.Replace("#C_10", "<br/>" + anexo1.anexo1.Resultados_Propuesta + "<br/><br/>");
            html_anexo1 = html_anexo1.Replace("#C_11", "<br/>" + adc.proponente + "<br/><br/><br/>");
            html_anexo1 = html_anexo1.Replace("#C_12", "<br/>" + adc.responsable + "<br/><br/><br/>");
            html_anexo1 = html_anexo1.Replace("#C_13", "<br/>" + anexo1.anexo1.Resultados_Analisis + "<br/>");
            html_anexo1 = html_anexo1.Replace("#C_14", anexo1.anexo1.Estatus.Equals("Aceptado") ? mark_yes : mark_no);
            html_anexo1 = html_anexo1.Replace("#C_15", anexo1.anexo1.Estatus.Equals("Rechazado") ? mark_yes : mark_no);


            /*
            var dir = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Plantillas\\FormatosEXCEL\\Prueba.xlsx"));
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook sheet = excel.Workbooks.Open(dir);
            Microsoft.Office.Interop.Excel.Worksheet x = excel.ActiveSheet as Microsoft.Office.Interop.Excel.Worksheet;

            Microsoft.Office.Interop.Excel.Range userRange = x.UsedRange;
            int countRecords = userRange.Rows.Count;
            int add = countRecords + 1;

            x.Cells[17, 1] = "ARMANDO ISAAC HERNANDEZ MUNIZ";

            var dir2 = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Plantillas\\FormatosEXCEL\\Prueba2.xlsx"));
            sheet.SaveAs(dir2);
            sheet.Close(true, Type.Missing, Type.Missing);
            excel.Quit();
            */

            /*
            var dir = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Plantillas\\PDF.html"));
            System.IO.File.WriteAllText(dir, html_anexo1);

            var p = new System.Diagnostics.Process();
            p.StartInfo = new System.Diagnostics.ProcessStartInfo(dir)
            {
                UseShellExecute = true
            };
            p.Start();
            */


            HtmlToPdf htmlToPdf = new HtmlToPdf();
            htmlToPdf.Options.PdfPageSize = PdfPageSize.A4;
            htmlToPdf.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            htmlToPdf.Options.WebPageWidth = 1024;
            //htmlToPdf.Options.web
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

            html_anexo2 = html_anexo2.Replace("#C_LOGO", Dir_LOGO + "logo.png");
            html_anexo2 = html_anexo2.Replace("#CLAVE", proyecto.Clave + "-F02");
            html_anexo2 = html_anexo2.Replace("#REVISION", "1");
            html_anexo2 = html_anexo2.Replace("#REGISTROS", Anexo2_REGISTROS);

            /*
            var dir = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Plantillas\\PDF.html"));
            System.IO.File.WriteAllText(dir, html_anexo2);

            var p = new System.Diagnostics.Process();
            p.StartInfo = new System.Diagnostics.ProcessStartInfo(dir)
            {
                UseShellExecute = true
            };
            p.Start();
            */

            HtmlToPdf htmlToPdf = new HtmlToPdf();
            htmlToPdf.Options.PdfPageSize = PdfPageSize.A4;
            htmlToPdf.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            htmlToPdf.Options.WebPageWidth = 1024;
            //htmlToPdf.Options.web
            SelectPdf.PdfDocument doc = htmlToPdf.ConvertHtmlString(html_anexo2);
            byte[] pdf = doc.Save();
            doc.Close();
            return pdf;

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
                var puesto = context.Puestos.Where(p => p.Id == user.Id_Puesto).Select(p => p.Nombre).FirstOrDefault();
                NOMBRES += $"{user.Nombre} {user.Paterno} {user.Materno} <br />";
                PUESTOS += $"{puesto} <br />";
            }

            html_anexo3 = html_anexo3.Replace("#C_LOGO", Dir_LOGO + "logo.png");
            html_anexo3 = html_anexo3.Replace("#C_1_", proyecto.Clave + "-F01");
            html_anexo3 = html_anexo3.Replace("#C_2_", adc.adc.Folio);
            html_anexo3 = html_anexo3.Replace("#C_3_", DateTime.Now.ToShortDateString());
            html_anexo3 = html_anexo3.Replace("#C_4_", anexo1.anexo1.Tipo_Cambio.Equals("Temporal") ? mark_yes : mark_no);
            html_anexo3 = html_anexo3.Replace("#C_5_", anexo1.anexo1.Tipo_Cambio.Equals("Permanente") ? mark_yes : mark_no);
            html_anexo3 = html_anexo3.Replace("#C_6_", anexo3.Tipo_ADC.Equals("Nuevo") ? mark_yes : mark_no);
            html_anexo3 = html_anexo3.Replace("#C_7_", anexo3.Tipo_ADC.Equals("Rehabilitado") ? mark_yes : mark_no);
            html_anexo3 = html_anexo3.Replace("#C_8_", anexo3.Tipo_ADC.Equals("Modificado") ? mark_yes : mark_no);
            html_anexo3 = html_anexo3.Replace("#C_9_", anexo1.residencia);
            html_anexo3 = html_anexo3.Replace("#C_10", adc.lider);

            html_anexo3 = html_anexo3.Replace("#C_11", NOMBRES);
            html_anexo3 = html_anexo3.Replace("#C_12", PUESTOS);

            html_anexo3 = html_anexo3.Replace("#C_13", anexo3.Equipo.Equals("true") ? mark_yes : mark_no);
            html_anexo3 = html_anexo3.Replace("#C_14", anexo3.Instrumento.Equals("true") ? mark_yes : mark_no);
            html_anexo3 = html_anexo3.Replace("#C_15", anexo3.Componente_o_Dispositivo.Equals("true") ? mark_yes : mark_no);
            html_anexo3 = html_anexo3.Replace("#C_16", anexo3.Valvula.Equals("true") ? mark_yes : mark_no);
            html_anexo3 = html_anexo3.Replace("#C_17", anexo3.Accesorio_o_Ducto.Equals("true") ? mark_yes : mark_no);
            html_anexo3 = html_anexo3.Replace("#C_18", anexo3.Estacion_Compresion.Equals("true") ? mark_yes : mark_no);
            html_anexo3 = html_anexo3.Replace("#C_19", anexo3.Estacion_Medicion_y_Regulacion.Equals("true") ? mark_yes : mark_no);
            html_anexo3 = html_anexo3.Replace("#C_20", anexo3.Trampa_Envios_y_Recibo_Diablos.Equals("true") ? mark_yes : mark_no);
            html_anexo3 = html_anexo3.Replace("#C_21", anexo3.Otro_Elemento.Equals("true") ? mark_yes : mark_no);

            html_anexo3 = html_anexo3.Replace("#C_22", anexo3.Numero_Identificacion);
            html_anexo3 = html_anexo3.Replace("#C_23", anexo3.Descripcion_Riesgo);
            html_anexo3 = html_anexo3.Replace("#C_24", $"${anexo3.Inversion_Cambio} {(anexo3.Tipo_Moneda == "PESO" ? "pesos" : "dolares")}");
            html_anexo3 = html_anexo3.Replace("#C_25", anexo3.Fecha_Inicio);
            html_anexo3 = html_anexo3.Replace("#C_26", anexo3.Fecha_Termino);
            html_anexo3 = html_anexo3.Replace("#C_27", anexo3.Justificacion);
            html_anexo3 = html_anexo3.Replace("#C_28", anexo3.Descripcion_Cambio);

            var documentacion = context.ADC_Anexo3_Documentacion.Where(a => a.Id_Anexo3 == id).ToList();

            int x = 29, y = 30;

            for (int i = 0; i < documentacion.Count; i++)
            {
                html_anexo3 = html_anexo3.Replace($"#C_{x}", documentacion[i].Check == "true" ? mark_yes : mark_no);
                html_anexo3 = html_anexo3.Replace($"#C_{y}", $"{documentacion[i].Anotaciones}<br /><b>{documentacion[i].Responsable_Area}</b>");
                x += 2;
                y += 2;
            }

            html_anexo3 = html_anexo3.Replace("#OTRO_ESPECIFICAR", "");
            html_anexo3 = html_anexo3.Replace("#C_73", context.Usuarios.Where(u => u.Id == anexo3.Id_Responsable_ADC).Select( u => $"{u.Titulo} {u.Nombre} {u.Paterno} {u.Materno}").FirstOrDefault());
            html_anexo3 = html_anexo3.Replace("#C_74", context.Usuarios.Where(u => u.Id == anexo3.Id_Director_Seguridad_Industrial).Select( u => $"{u.Titulo} {u.Nombre} {u.Paterno} {u.Materno}").FirstOrDefault());
            html_anexo3 = html_anexo3.Replace("#C_75", context.Usuarios.Where(u => u.Id == anexo3.Id_Director_Ejecutivo_Operacion).Select( u => $"{u.Titulo} {u.Nombre} {u.Paterno} {u.Materno}").FirstOrDefault());
            html_anexo3 = html_anexo3.Replace("#C_76", context.Usuarios.Where(u => u.Id == anexo3.Id_Director_Ejecutivo_Mantenimiento_y_Seguridad).Select( u => $"{u.Titulo} {u.Nombre} {u.Paterno} {u.Materno}").FirstOrDefault());


            /*
            var dir = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Plantillas\\PDF.html"));
            System.IO.File.WriteAllText(dir, html_anexo3);

            //return dir;
            //System.Diagnostics.Process.Start(dir);
            var p = new System.Diagnostics.Process();
            p.StartInfo = new System.Diagnostics.ProcessStartInfo(dir)
            {
                UseShellExecute = true
            };
            p.Start();
            */

            HtmlToPdf htmlToPdf = new HtmlToPdf();
            //htmlToPdf.Options.
            htmlToPdf.Options.PdfPageSize = PdfPageSize.A4;
            htmlToPdf.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            htmlToPdf.Options.WebPageWidth = 1024;
            htmlToPdf.Options.MarginTop = 20;
            htmlToPdf.Options.MarginBottom = 20;
            htmlToPdf.Options.MarginLeft = 20;
            htmlToPdf.Options.MarginRight = 20;
            //htmlToPdf.Options.web
            SelectPdf.PdfDocument doc = htmlToPdf.ConvertHtmlString(html_anexo3);
            byte[] pdf = doc.Save();
            doc.Close();
            return pdf;
        }

        #endregion

        #region ANEXO 4

        public byte[] Anexo4_PDF(int id)
        {
            var anexo4 = context.ADC_Anexo4.Find(id);
            var anexo1 = Consultas.VistaAnexo1(context, anexo4.Id_Anexo1);
            var adc = Consultas.VistaADC(context).Where(a => a.adc.Id == anexo1.anexo1.Id).FirstOrDefault();
            
            var anexo3 = context.ADC_Anexo3.Find(anexo4.Id_Anexo3);

            html_anexo4 = html_anexo4.Replace("#C_LOGO", Dir_LOGO + "logo.png");
            html_anexo4 = html_anexo4.Replace("#C_1_", adc.adc.Folio);
            html_anexo4 = html_anexo4.Replace("#C_2_", anexo1.gasoducto);
            html_anexo4 = html_anexo4.Replace("#C_3_", anexo1.tramo);
            html_anexo4 = html_anexo4.Replace("#C_4_", anexo1.anexo1.Proceso);
            html_anexo4 = html_anexo4.Replace("#C_5_", anexo1.anexo1.Prestacion_Servicio);
            html_anexo4 = html_anexo4.Replace("#C_6_", anexo1.anexo1.Tipo_Cambio == "Temporal" ? mark_yes : mark_no);
            html_anexo4 = html_anexo4.Replace("#C_7_", anexo1.anexo1.Tipo_Cambio == "Permanente" ? mark_yes : mark_no);
            html_anexo4 = html_anexo4.Replace("#C_8_", anexo4.Fecha_Retiro_Cambio_Temporal);
            html_anexo4 = html_anexo4.Replace("#C_9_", anexo4.Tiempo_Estimado);
            html_anexo4 = html_anexo4.Replace("#C_10", anexo4.Propuesta_Inicio_Operacion);


            var responsable = context.Usuarios.Where(u => u.Id == anexo3.Id_Responsable_ADC).Select(u => $"{u.Titulo} {u.Nombre} {u.Paterno} {u.Materno}").FirstOrDefault();
            var lider = context.Usuarios.Where(u => u.Id == adc.adc.Id_LiderEquipoVerificador).Select(u => $"{u.Titulo} {u.Nombre} {u.Paterno} {u.Materno}").FirstOrDefault();
            var deo = context.Usuarios.Where(u => u.Id == anexo3.Id_Director_Ejecutivo_Operacion).Select(u => $"{u.Titulo} {u.Nombre} {u.Paterno} {u.Materno}").FirstOrDefault();
            var dems = context.Usuarios.Where(u => u.Id == anexo3.Id_Director_Ejecutivo_Mantenimiento_y_Seguridad).Select(u => $"{u.Titulo} {u.Nombre} {u.Paterno} {u.Materno}").FirstOrDefault();
            var residente = context.Usuarios.Where(u => u.Id == anexo4.Id_Residente).Select(u => $"{u.Titulo} {u.Nombre} {u.Paterno} {u.Materno}").FirstOrDefault();

            var EV = context.ADC_Equipo_Verificador.Where(a => a.Id_ADC == anexo3.Id_Anexo1).FirstOrDefault();//OrderBy(a => a.Id_Equipo_Verificador).LastOrDefault();
            var integrantesEV = context.ADC_Equipo_Verificador_Integrantes.Where(a => a.Id_Equipo_Verificador_ADC == EV.Id);

            IEnumerable<Usuarios> usuarios = (from u in context.Usuarios
                                              join ev in context.ADC_Equipo_Verificador_Integrantes on u.Id equals ev.Id_Usuario
                                              where ev.Id_Equipo_Verificador_ADC == EV.Id
                                              select u).ToList();

            string NOMBRES = "";
            //string PUESTOS = "";

            foreach (var user in usuarios)
            {
                NOMBRES += $"{user.Nombre} {user.Paterno} {user.Materno} <br />";
                //PUESTOS += "N/A <br />";
            }

            html_anexo4 = html_anexo4.Replace("#C_11", responsable);
            html_anexo4 = html_anexo4.Replace("#C_12", lider);
            html_anexo4 = html_anexo4.Replace("#C_13", NOMBRES);
            html_anexo4 = html_anexo4.Replace("#C_14", deo);
            html_anexo4 = html_anexo4.Replace("#C_15", dems);
            html_anexo4 = html_anexo4.Replace("#C_16", residente);

            /*
            var dir = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Plantillas\\PDF.html"));
            System.IO.File.WriteAllText(dir, html_anexo4);

            var p = new System.Diagnostics.Process();
            p.StartInfo = new System.Diagnostics.ProcessStartInfo(dir)
            {
                UseShellExecute = true
            };
            p.Start();
            */

            HtmlToPdf htmlToPdf = new HtmlToPdf();
            htmlToPdf.Options.PdfPageSize = PdfPageSize.A4;
            htmlToPdf.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            htmlToPdf.Options.WebPageWidth = 1024;
            htmlToPdf.Options.MarginTop = 20;
            htmlToPdf.Options.MarginBottom = 20;
            htmlToPdf.Options.MarginLeft = 20;
            htmlToPdf.Options.MarginRight = 20;
            //htmlToPdf.Options.web
            SelectPdf.PdfDocument doc = htmlToPdf.ConvertHtmlString(html_anexo4);
            byte[] pdf = doc.Save();
            doc.Close();
            return pdf;


        }

        #endregion

        #region ANEXO 5
        public byte[] Anexo5_PDF(int id)
        {
            var anexo5 = context.ADC_Anexo5.Find(id);
            var anexo1 = Consultas.VistaAnexo1(context, anexo5.Id_Anexo1);
            var anexo3 = context.ADC_Anexo3.Where(a => a.Id == anexo5.Id_Anexo3).FirstOrDefault();
            var anexo4 = context.ADC_Anexo4.Where(a => a.Id_Anexo1 == anexo5.Id_Anexo1).FirstOrDefault();
            var adc = Consultas.VistaADC(context).Where(a => a.adc.Id == anexo1.anexo1.Id).FirstOrDefault();

            var responsable = context.Usuarios.Where(u => u.Id == anexo5.Id_Responsable_Cambio_Temporal)
                .Select(u => $"{u.Titulo} {u.Nombre} {u.Paterno} {u.Materno}").FirstOrDefault();

            var r_operacion = context.Usuarios.Where(u => u.Id == anexo3.Id_Director_Ejecutivo_Operacion)
                .Select(u => $"{u.Titulo} {u.Nombre} {u.Paterno} {u.Materno}").FirstOrDefault();
            var r_mantenimiento = context.Usuarios.Where(u => u.Id == anexo3.Id_Director_Ejecutivo_Mantenimiento_y_Seguridad)
                .Select(u => $"{u.Titulo} {u.Nombre} {u.Paterno} {u.Materno}").FirstOrDefault();
            var r_seguridad = context.Usuarios.Where(u => u.Id == anexo3.Id_Director_Seguridad_Industrial)
                .Select(u => $"{u.Titulo} {u.Nombre} {u.Paterno} {u.Materno}").FirstOrDefault();

            html_anexo5 = html_anexo5.Replace("#C_LOGO", Dir_LOGO + "logo.png");
            html_anexo5 = html_anexo5.Replace("#C_1_", anexo1.gasoducto);
            html_anexo5 = html_anexo5.Replace("#C_2_", anexo1.tramo);
            html_anexo5 = html_anexo5.Replace("#C_3_", anexo1.anexo1.Proceso);
            html_anexo5 = html_anexo5.Replace("#C_4_", anexo1.anexo1.Prestacion_Servicio);
            html_anexo5 = html_anexo5.Replace("#C_5_", anexo4.Fecha_Retiro_Cambio_Temporal);
            html_anexo5 = html_anexo5.Replace("#C_6_", $"{anexo5.Mes_Cambio}/{anexo5.Dia_Cambio}/{anexo5.Anio_Cambio}");
            html_anexo5 = html_anexo5.Replace("#C_7_", anexo5.Descripcion);
            html_anexo5 = html_anexo5.Replace("#C_8_", "");
            html_anexo5 = html_anexo5.Replace("#C_9_", responsable);
            html_anexo5 = html_anexo5.Replace("#C_10", anexo5.Dia_Cambio);
            html_anexo5 = html_anexo5.Replace("#C_11", anexo5.Mes_Cambio);
            html_anexo5 = html_anexo5.Replace("#C_12", anexo5.Anio_Cambio);
            html_anexo5 = html_anexo5.Replace("#C_13", anexo5.Dia_Retiro);
            html_anexo5 = html_anexo5.Replace("#C_14", anexo5.Mes_Retiro);
            html_anexo5 = html_anexo5.Replace("#C_15", anexo5.Anio_Retiro);
            html_anexo5 = html_anexo5.Replace("#C_16", r_operacion);
            html_anexo5 = html_anexo5.Replace("#C_17", r_mantenimiento);
            html_anexo5 = html_anexo5.Replace("#C_18", r_seguridad);

            HtmlToPdf htmlToPdf = new HtmlToPdf();
            htmlToPdf.Options.PdfPageSize = PdfPageSize.A4;
            htmlToPdf.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            htmlToPdf.Options.WebPageWidth = 1024;
            htmlToPdf.Options.MarginTop = 20;
            htmlToPdf.Options.MarginBottom = 20;
            htmlToPdf.Options.MarginLeft = 20;
            htmlToPdf.Options.MarginRight = 20;
            //htmlToPdf.Options.web
            SelectPdf.PdfDocument doc = htmlToPdf.ConvertHtmlString(html_anexo5);
            byte[] pdf = doc.Save();
            doc.Close();
            return pdf;
        }
        #endregion

        #region ANEXO 6
        public byte[] Anexo6_PDF(int id)
        {
            var anexo6 = context.ADC_Anexo6.Find(id);
            var anexo6_doc = context.ADC_Anexo6_Documentacion.Where(a => a.Id_Anexo6 == id).ToList();
            var anexo1 = Consultas.VistaAnexo1(context, anexo6.Id_Anexo1);
            var anexo3 = context.ADC_Anexo3.Where(a => a.Id == anexo6.Id_anexo3).FirstOrDefault();
            var adc = Consultas.VistaADC(context).Where(a => a.adc.Id == anexo1.anexo1.Id).FirstOrDefault();

            var responsable = context.Usuarios.Where(u => u.Id == anexo3.Id_Responsable_ADC).Select(u => $"{u.Titulo} {u.Nombre} {u.Paterno} {u.Materno}").FirstOrDefault();
            var lider = context.Usuarios.Where(u => u.Id == adc.adc.Id_LiderEquipoVerificador).Select(u => $"{u.Titulo} {u.Nombre} {u.Paterno} {u.Materno}").FirstOrDefault();
            var deo = context.Usuarios.Where(u => u.Id == anexo3.Id_Director_Ejecutivo_Operacion).Select(u => $"{u.Titulo} {u.Nombre} {u.Paterno} {u.Materno}").FirstOrDefault();
            var dems = context.Usuarios.Where(u => u.Id == anexo3.Id_Director_Ejecutivo_Mantenimiento_y_Seguridad).Select(u => $"{u.Titulo} {u.Nombre} {u.Paterno} {u.Materno}").FirstOrDefault();

            var EV = context.ADC_Equipo_Verificador.Where(a => a.Id_ADC == anexo3.Id_Anexo1).FirstOrDefault();//OrderBy(a => a.Id_Equipo_Verificador).LastOrDefault();
            var integrantesEV = context.ADC_Equipo_Verificador_Integrantes.Where(a => a.Id_Equipo_Verificador_ADC == EV.Id);
            IEnumerable<Usuarios> usuarios = (from u in context.Usuarios
                                              join ev in context.ADC_Equipo_Verificador_Integrantes on u.Id equals ev.Id_Usuario
                                              where ev.Id_Equipo_Verificador_ADC == EV.Id
                                              select u).ToList();
            string NOMBRES = "";
            //string PUESTOS = "";

            foreach (var user in usuarios)
            {
                var puesto = context.Puestos.Where(p => p.Id == user.Id_Puesto).Select(p => p.Nombre).FirstOrDefault();
                NOMBRES += $"{user.Nombre} {user.Paterno} {user.Materno} --- {puesto} <br />";
                //PUESTOS += "N/A <br />";
            }

            html_anexo6 = html_anexo6.Replace("#C_LOGO", Dir_LOGO + "logo.png");
            html_anexo6 = html_anexo6.Replace("#C_1_", adc.adc.Folio);
            html_anexo6 = html_anexo6.Replace("#C_2_", anexo6.Fecha_Recepcion);
            html_anexo6 = html_anexo6.Replace("#C_3_", NOMBRES);
            html_anexo6 = html_anexo6.Replace("#C_4_", lider);
            html_anexo6 = html_anexo6.Replace("#4", anexo3.Fecha_Inicio);
            html_anexo6 = html_anexo6.Replace("#C_5_", anexo3.Fecha_Termino);

            for(int i = 0; i < anexo6_doc.Count; i++)
            {
                html_anexo6 = html_anexo6.Replace($"#C_{i+6}", anexo6_doc[i].Check == "true" ? mark_yes : mark_no);
            }

            html_anexo6 = html_anexo6.Replace("#C_32", "");
            html_anexo6 = html_anexo6.Replace("#C_33", responsable);
            html_anexo6 = html_anexo6.Replace("#C_34", lider);
            html_anexo6 = html_anexo6.Replace("#C_35", deo);
            html_anexo6 = html_anexo6.Replace("#C_36", dems);
            html_anexo6 = html_anexo6.Replace("#C_37", NOMBRES);


            HtmlToPdf htmlToPdf = new HtmlToPdf();
            htmlToPdf.Options.PdfPageSize = PdfPageSize.A4;
            htmlToPdf.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            htmlToPdf.Options.WebPageWidth = 1024;
            htmlToPdf.Options.MarginTop = 20;
            htmlToPdf.Options.MarginBottom = 20;
            htmlToPdf.Options.MarginLeft = 20;
            htmlToPdf.Options.MarginRight = 20;
            //htmlToPdf.Options.web
            SelectPdf.PdfDocument doc = htmlToPdf.ConvertHtmlString(html_anexo6);
            byte[] pdf = doc.Save();
            doc.Close();
            return pdf;
        }
        #endregion

    }
}
