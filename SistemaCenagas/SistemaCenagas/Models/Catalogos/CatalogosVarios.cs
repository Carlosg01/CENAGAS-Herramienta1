using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class ElementoS3S
    {
        [Key]
        public int Id { get; set; }
        public string Elemento { get; set; }
        public string Clave { get; set; }
        public string Descripcion { get; set; }
        public int Eliminado { get; set; }
    }
    public class Fuente_Deteccion
    {
        [Key]
        public int Id { get; set; }
        public string Fuente { get; set; }
        public string Abreviatura { get; set; }
        public int Eliminado { get; set; }
    }
    public class Etapa_Realizada
    {
        [Key]
        public int Id { get; set; }
        public string Etapa { get; set; }
        public int Avance { get; set; }
        public int Eliminado { get; set; }
    }
    public class Zonas
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Eliminado { get; set; }
    }
    public class Estados
    {
        [Key]
        public int Id { get; set; }
        public string Pais { get; set; }
        public string Estado { get; set; }
        public string Capital { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public int Eliminado { get; set; }
    }
    public class Residencias
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Id_Estado { get; set; }
        public string Codigo_Postal { get; set; }
        public int Eliminado { get; set; }
    }
    public class Especialidades
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Eliminado { get; set; }
    }
    public class Sistema
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Denominacion { get; set; }
        public int Eliminado { get; set; }
    }
    public class DDV
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Eliminado { get; set; }
    }
    public class Unidad
    {
        [Key]
        public int Id { get; set; }
        public string Abreviatura { get; set; }
        public string Nombre { get; set; }
        public int Eliminado { get; set; }
    }
    public class Direccion_Ejecutiva
    {
        [Key]
        public int Id { get; set; }
        public string Abreviatura { get; set; }
        public string Nombre { get; set; }
        public int Id_Unidad { get; set; }
        public int Eliminado { get; set; }
    }
    public class Direccion
    {
        [Key]
        public int Id { get; set; }
        public string Abreviatura { get; set; }
        public string Nombre { get; set; }
        public int Id_DireccionEjecutiva { get; set; }
        public int Eliminado { get; set; }

    }
    public class Tipo
    {
        [Key]
        public int Id { get; set; }
        public string Abreviatura { get; set; }
        public string Tipo_Instalacion { get; set; }
        public string Resumen { get; set; }
        public int Eliminado { get; set; }

    }


    //MODELOS
    public class ResidenciasIndex
    {
        public Residencias residencia { get; set; }
        public string estado { get; set; }
        public string capital { get; set; }
    }
    public class DireccionEjecutivaIndex
    {
        public Direccion_Ejecutiva direccionEjecutiva { get; set; }
        public string _Unidad { get; set; }
    }
    public class DireccionIndex
    {
        public Direccion direccion { get; set; }
        public string direccionEjecutiva { get; set; }
        public string _unidad { get; set; }
    }
}
