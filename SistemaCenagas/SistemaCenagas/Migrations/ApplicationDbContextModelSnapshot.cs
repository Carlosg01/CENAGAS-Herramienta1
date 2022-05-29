﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SistemaCenagas.Data;

namespace SistemaCenagas.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.15");

            modelBuilder.Entity("SistemaCenagas.Models.ADC", b =>
                {
                    b.Property<int>("Id_ADC")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Fecha_Actualizacion")
                        .HasColumnType("longtext");

                    b.Property<string>("Folio")
                        .HasColumnType("longtext");

                    b.Property<int>("Id_Lider")
                        .HasColumnType("int");

                    b.Property<int>("Id_ProponenteCambio")
                        .HasColumnType("int");

                    b.Property<int>("Id_Proyecto")
                        .HasColumnType("int");

                    b.Property<int>("Id_ResponsableADC")
                        .HasColumnType("int");

                    b.Property<int>("Id_Suplente")
                        .HasColumnType("int");

                    b.Property<int>("Registro_Eliminado")
                        .HasColumnType("int");

                    b.HasKey("Id_ADC");

                    b.ToTable("ADC");
                });

            modelBuilder.Entity("SistemaCenagas.Models.ADC_Actividades", b =>
                {
                    b.Property<int>("Id_Actividad")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Actividad")
                        .HasColumnType("longtext");

                    b.Property<int>("Registro_Eliminado")
                        .HasColumnType("int");

                    b.HasKey("Id_Actividad");

                    b.ToTable("ADC_Actividades");
                });

            modelBuilder.Entity("SistemaCenagas.Models.ADC_Archivos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Actividad")
                        .HasColumnType("longtext");

                    b.Property<string>("Clave")
                        .HasColumnType("longtext");

                    b.Property<string>("Extension")
                        .HasColumnType("longtext");

                    b.Property<int>("Id_ADC")
                        .HasColumnType("int");

                    b.Property<int>("Id_Usuario")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

                    b.Property<int>("Registro_Eliminado")
                        .HasColumnType("int");

                    b.Property<float>("Size")
                        .HasColumnType("float");

                    b.Property<string>("Ubicacion")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("ADC_Archivos");
                });

            modelBuilder.Entity("SistemaCenagas.Models.ADC_Equipo_Verificador", b =>
                {
                    b.Property<int>("Id_Equipo_Verificador")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Id_ADC")
                        .HasColumnType("int");

                    b.Property<int>("Id_Lider")
                        .HasColumnType("int");

                    b.HasKey("Id_Equipo_Verificador");

                    b.ToTable("ADC_Equipo_Verificador");
                });

            modelBuilder.Entity("SistemaCenagas.Models.ADC_Equipo_Verificador_Integrantes", b =>
                {
                    b.Property<int>("Id_Equipo_Verificador_Integrantes")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Estatus")
                        .HasColumnType("longtext");

                    b.Property<int>("Id_Equipo_Verificador")
                        .HasColumnType("int");

                    b.Property<int>("Id_Usuario")
                        .HasColumnType("int");

                    b.HasKey("Id_Equipo_Verificador_Integrantes");

                    b.ToTable("ADC_Equipo_Verificador_Integrantes");
                });

            modelBuilder.Entity("SistemaCenagas.Models.ADC_Normativas", b =>
                {
                    b.Property<int>("Id_Normativa")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Clave")
                        .HasColumnType("longtext");

                    b.Property<string>("Descripcion")
                        .HasColumnType("longtext");

                    b.Property<int>("Id_Actividad")
                        .HasColumnType("int");

                    b.Property<int>("Id_Anexo")
                        .HasColumnType("int");

                    b.Property<int>("Registro_Eliminado")
                        .HasColumnType("int");

                    b.Property<string>("Responsable")
                        .HasColumnType("longtext");

                    b.HasKey("Id_Normativa");

                    b.ToTable("ADC_Normativas");
                });

            modelBuilder.Entity("SistemaCenagas.Models.ADC_Procesos", b =>
                {
                    b.Property<int>("Id_Proceso")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<float>("Avance")
                        .HasColumnType("float");

                    b.Property<string>("Confirmado")
                        .HasColumnType("longtext");

                    b.Property<string>("Faltante_Comentarios")
                        .HasColumnType("longtext");

                    b.Property<int>("Id_ADC")
                        .HasColumnType("int");

                    b.Property<int>("Id_Actividad")
                        .HasColumnType("int");

                    b.Property<string>("Plan_Accion")
                        .HasColumnType("longtext");

                    b.Property<int>("Registro_Eliminado")
                        .HasColumnType("int");

                    b.Property<string>("Terminado")
                        .HasColumnType("longtext");

                    b.HasKey("Id_Proceso");

                    b.ToTable("ADC_Procesos");
                });

            modelBuilder.Entity("SistemaCenagas.Models.Anexo1", b =>
                {
                    b.Property<int>("Id_PropuestaCambio")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasColumnType("longtext");

                    b.Property<string>("Estatus")
                        .HasColumnType("longtext");

                    b.Property<string>("EstatusADC")
                        .HasColumnType("longtext");

                    b.Property<string>("Fecha")
                        .HasColumnType("longtext");

                    b.Property<int>("Id_Anexo2")
                        .HasColumnType("int");

                    b.Property<int>("Id_Proyecto")
                        .HasColumnType("int");

                    b.Property<int>("Id_Residencia")
                        .HasColumnType("int");

                    b.Property<string>("PresentoARP")
                        .HasColumnType("longtext");

                    b.Property<string>("Prestacion_Servicio")
                        .HasColumnType("longtext");

                    b.Property<string>("Proceso")
                        .HasColumnType("longtext");

                    b.Property<int>("Registro_Eliminado")
                        .HasColumnType("int");

                    b.Property<string>("Resultados_Analisis")
                        .HasColumnType("longtext");

                    b.Property<string>("Resultados_Propuesta")
                        .HasColumnType("longtext");

                    b.Property<string>("Tipo_Cambio")
                        .HasColumnType("longtext");

                    b.Property<string>("Ut_Gasoducto")
                        .HasColumnType("longtext");

                    b.Property<string>("Ut_Tramo")
                        .HasColumnType("longtext");

                    b.HasKey("Id_PropuestaCambio");

                    b.ToTable("Anexo1");
                });

            modelBuilder.Entity("SistemaCenagas.Models.Anexo2", b =>
                {
                    b.Property<int>("Id_Anexo2")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Id_Proyecto")
                        .HasColumnType("int");

                    b.HasKey("Id_Anexo2");

                    b.ToTable("Anexo2");
                });

            modelBuilder.Entity("SistemaCenagas.Models.Anexo3", b =>
                {
                    b.Property<int>("Id_Anexo3")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Accesorio_Ducto")
                        .HasColumnType("longtext");

                    b.Property<string>("Componente_o_Dispositivo")
                        .HasColumnType("longtext");

                    b.Property<string>("Descripcion_Cambio")
                        .HasColumnType("longtext");

                    b.Property<string>("Descripcion_Riesgo")
                        .HasColumnType("longtext");

                    b.Property<string>("Equipo")
                        .HasColumnType("longtext");

                    b.Property<string>("Estacion_Compresion")
                        .HasColumnType("longtext");

                    b.Property<string>("Estacion_Medicion_y_Regulacion")
                        .HasColumnType("longtext");

                    b.Property<string>("FechaRegistro")
                        .HasColumnType("longtext");

                    b.Property<string>("Fecha_Inicio")
                        .HasColumnType("longtext");

                    b.Property<string>("Fecha_Termino")
                        .HasColumnType("longtext");

                    b.Property<int>("Id_Anexo1")
                        .HasColumnType("int");

                    b.Property<int>("Id_Anexo2")
                        .HasColumnType("int");

                    b.Property<string>("Id_Director_Ejecutivo_Mantenimiento_y_Seguridad")
                        .HasColumnType("longtext");

                    b.Property<string>("Id_Director_Ejecutivo_Operacion")
                        .HasColumnType("longtext");

                    b.Property<string>("Id_Director_Seguridad_Industrial")
                        .HasColumnType("longtext");

                    b.Property<string>("Id_Responsable_ADC")
                        .HasColumnType("longtext");

                    b.Property<string>("Instrumento")
                        .HasColumnType("longtext");

                    b.Property<double>("Inversion_Cambio")
                        .HasColumnType("double");

                    b.Property<string>("Justificacion")
                        .HasColumnType("longtext");

                    b.Property<string>("Numero_Identificacion")
                        .HasColumnType("longtext");

                    b.Property<string>("Otra_Documentacion")
                        .HasColumnType("longtext");

                    b.Property<string>("Otro_Elemento")
                        .HasColumnType("longtext");

                    b.Property<string>("TipoADC")
                        .HasColumnType("longtext");

                    b.Property<string>("Trampa_Envios_y_Recibo_Diablos")
                        .HasColumnType("longtext");

                    b.Property<string>("Valvula")
                        .HasColumnType("longtext");

                    b.HasKey("Id_Anexo3");

                    b.ToTable("Anexo3");
                });

            modelBuilder.Entity("SistemaCenagas.Models.Anexo3_Documentacion", b =>
                {
                    b.Property<int>("Id_Anexo3_Documentacion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Check")
                        .HasColumnType("longtext");

                    b.Property<int>("Id_Anexo3")
                        .HasColumnType("int");

                    b.Property<int>("Id_Responsable")
                        .HasColumnType("int");

                    b.Property<string>("Tipo")
                        .HasColumnType("longtext");

                    b.HasKey("Id_Anexo3_Documentacion");

                    b.ToTable("Anexo3_Documentacion");
                });

            modelBuilder.Entity("SistemaCenagas.Models.Anexo4", b =>
                {
                    b.Property<int>("Id_Anexo4")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Fecha_Retiro_Cambio_Temporal")
                        .HasColumnType("longtext");

                    b.Property<int>("Id_Anexo1")
                        .HasColumnType("int");

                    b.Property<int>("Id_Anexo3")
                        .HasColumnType("int");

                    b.Property<int>("Id_Residente")
                        .HasColumnType("int");

                    b.Property<string>("Propuesta_Inicio_Operacion")
                        .HasColumnType("longtext");

                    b.Property<string>("Tiempo_Estimado")
                        .HasColumnType("longtext");

                    b.HasKey("Id_Anexo4");

                    b.ToTable("Anexo4");
                });

            modelBuilder.Entity("SistemaCenagas.Models.Anexo5", b =>
                {
                    b.Property<int>("Id_Anexo5")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Anio_Cambio")
                        .HasColumnType("longtext");

                    b.Property<string>("Anio_Retiro")
                        .HasColumnType("longtext");

                    b.Property<string>("Descripcion_Cambio_Temporal")
                        .HasColumnType("longtext");

                    b.Property<string>("Dia_Cambio")
                        .HasColumnType("longtext");

                    b.Property<string>("Dia_Retiro")
                        .HasColumnType("longtext");

                    b.Property<int>("Id_Anexo1")
                        .HasColumnType("int");

                    b.Property<int>("Id_Anexo3")
                        .HasColumnType("int");

                    b.Property<int>("Id_Responsable_Cambio_Temporal")
                        .HasColumnType("int");

                    b.Property<string>("Mes_Cambio")
                        .HasColumnType("longtext");

                    b.Property<string>("Mes_Retiro")
                        .HasColumnType("longtext");

                    b.HasKey("Id_Anexo5");

                    b.ToTable("Anexo5");
                });

            modelBuilder.Entity("SistemaCenagas.Models.Anexo6", b =>
                {
                    b.Property<int>("Id_Anexo6")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Fecha_Inicio")
                        .HasColumnType("longtext");

                    b.Property<string>("Fecha_Recepcion")
                        .HasColumnType("longtext");

                    b.Property<string>("Fecha_Termino")
                        .HasColumnType("longtext");

                    b.Property<int>("Id_Anexo1")
                        .HasColumnType("int");

                    b.Property<int>("Id_anexo3")
                        .HasColumnType("int");

                    b.HasKey("Id_Anexo6");

                    b.ToTable("Anexo6");
                });

            modelBuilder.Entity("SistemaCenagas.Models.Anexo6_Documentacion", b =>
                {
                    b.Property<int>("Id_Anexo6_Documentacion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Check")
                        .HasColumnType("longtext");

                    b.Property<string>("Elemento")
                        .HasColumnType("longtext");

                    b.Property<int>("Id_Anexo6")
                        .HasColumnType("int");

                    b.Property<string>("Seccion")
                        .HasColumnType("longtext");

                    b.HasKey("Id_Anexo6_Documentacion");

                    b.ToTable("Anexo6_Documentacion");
                });

            modelBuilder.Entity("SistemaCenagas.Models.Anexos", b =>
                {
                    b.Property<int>("Id_Anexo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

                    b.Property<int>("Registro_Eliminado")
                        .HasColumnType("int");

                    b.HasKey("Id_Anexo");

                    b.ToTable("Anexos");
                });

            modelBuilder.Entity("SistemaCenagas.Models.Gasoductos", b =>
                {
                    b.Property<int>("Id_Gasoducto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Denominacion")
                        .HasColumnType("longtext");

                    b.Property<string>("Descripcion_Pemex")
                        .HasColumnType("longtext");

                    b.Property<float>("Diametro_o_pulgadas")
                        .HasColumnType("float");

                    b.Property<string>("Gasoducto")
                        .HasColumnType("longtext");

                    b.Property<float>("Longitud_Metros")
                        .HasColumnType("float");

                    b.Property<int>("Registro_Eliminado")
                        .HasColumnType("int");

                    b.Property<string>("Sistema")
                        .HasColumnType("longtext");

                    b.Property<string>("Ut_Ducto")
                        .HasColumnType("longtext");

                    b.Property<string>("Ut_Gasoducto")
                        .HasColumnType("longtext");

                    b.Property<string>("Ut_Pemex")
                        .HasColumnType("longtext");

                    b.HasKey("Id_Gasoducto");

                    b.ToTable("Gasoductos");
                });

            modelBuilder.Entity("SistemaCenagas.Models.Instalaciones", b =>
                {
                    b.Property<int>("Id_Instalacion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<float>("Altitud_Z_decimal")
                        .HasColumnType("float");

                    b.Property<string>("Clase")
                        .HasColumnType("longtext");

                    b.Property<string>("Gmas_Pemex")
                        .HasColumnType("longtext");

                    b.Property<string>("Instalacion")
                        .HasColumnType("longtext");

                    b.Property<float>("Km")
                        .HasColumnType("float");

                    b.Property<float>("Latitud_Y_decimal")
                        .HasColumnType("float");

                    b.Property<float>("Longitud_X_decimal")
                        .HasColumnType("float");

                    b.Property<string>("Region")
                        .HasColumnType("longtext");

                    b.Property<int>("Registro_Eliminado")
                        .HasColumnType("int");

                    b.Property<string>("Residencia")
                        .HasColumnType("longtext");

                    b.Property<string>("Sector_Pemex")
                        .HasColumnType("longtext");

                    b.Property<string>("Sistema")
                        .HasColumnType("longtext");

                    b.Property<string>("Ut_Instalacion")
                        .HasColumnType("longtext");

                    b.Property<string>("Ut_Tramo")
                        .HasColumnType("longtext");

                    b.HasKey("Id_Instalacion");

                    b.ToTable("Instalaciones");
                });

            modelBuilder.Entity("SistemaCenagas.Models.Proyecto_Miembros", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Estatus")
                        .HasColumnType("longtext");

                    b.Property<int>("Id_Proyecto")
                        .HasColumnType("int");

                    b.Property<int>("Id_Usuario")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Proyecto_Miembros");
                });

            modelBuilder.Entity("SistemaCenagas.Models.Proyectos", b =>
                {
                    b.Property<int>("Id_Proyecto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Clave")
                        .HasColumnType("longtext");

                    b.Property<string>("Descripcion")
                        .HasColumnType("longtext");

                    b.Property<string>("Estado_ADC")
                        .HasColumnType("longtext");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

                    b.Property<int>("Registro_Eliminado")
                        .HasColumnType("int");

                    b.HasKey("Id_Proyecto");

                    b.ToTable("Proyectos");
                });

            modelBuilder.Entity("SistemaCenagas.Models.Residencias", b =>
                {
                    b.Property<int>("Id_Residencia")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

                    b.Property<int>("Registro_Eliminado")
                        .HasColumnType("int");

                    b.HasKey("Id_Residencia");

                    b.ToTable("Residencias");
                });

            modelBuilder.Entity("SistemaCenagas.Models.Roles", b =>
                {
                    b.Property<int>("Id_Rol")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasColumnType("longtext");

                    b.Property<string>("Estado")
                        .HasColumnType("longtext");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

                    b.Property<string>("Privilegios")
                        .HasColumnType("longtext");

                    b.Property<int>("Registro_Eliminado")
                        .HasColumnType("int");

                    b.HasKey("Id_Rol");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("SistemaCenagas.Models.Tramos", b =>
                {
                    b.Property<int>("Id_Tramo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<float>("Diametro")
                        .HasColumnType("float");

                    b.Property<float>("Espesor_Nominal")
                        .HasColumnType("float");

                    b.Property<string>("Fecha_Construccion")
                        .HasColumnType("longtext");

                    b.Property<float>("Km_Fin")
                        .HasColumnType("float");

                    b.Property<float>("Km_Inicio")
                        .HasColumnType("float");

                    b.Property<float>("Longitud_Metros")
                        .HasColumnType("float");

                    b.Property<int>("Registro_Eliminado")
                        .HasColumnType("int");

                    b.Property<string>("Residencia")
                        .HasColumnType("longtext");

                    b.Property<float>("SMYS")
                        .HasColumnType("float");

                    b.Property<string>("Tramo")
                        .HasColumnType("longtext");

                    b.Property<string>("Ut_Gasoducto")
                        .HasColumnType("longtext");

                    b.Property<string>("Ut_Tramo")
                        .HasColumnType("longtext");

                    b.HasKey("Id_Tramo");

                    b.ToTable("Tramos");
                });

            modelBuilder.Entity("SistemaCenagas.Models.Usuarios", b =>
                {
                    b.Property<int>("Id_Usuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Confirmar_Password")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Estatus")
                        .HasColumnType("longtext");

                    b.Property<int>("Id_Rol")
                        .HasColumnType("int");

                    b.Property<string>("Image_Url")
                        .HasColumnType("longtext");

                    b.Property<string>("Materno")
                        .HasColumnType("longtext");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

                    b.Property<string>("Notificacion_ADC")
                        .HasColumnType("longtext");

                    b.Property<string>("Notificacion_Proyecto")
                        .HasColumnType("longtext");

                    b.Property<string>("Notificacion_Tarea")
                        .HasColumnType("longtext");

                    b.Property<string>("Nueva_Password")
                        .HasColumnType("longtext");

                    b.Property<string>("Observaciones")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<string>("Paterno")
                        .HasColumnType("longtext");

                    b.Property<string>("Puesto")
                        .HasColumnType("longtext");

                    b.Property<int>("Registro_Eliminado")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .HasColumnType("longtext");

                    b.Property<string>("Username")
                        .HasColumnType("longtext");

                    b.HasKey("Id_Usuario");

                    b.ToTable("Usuarios");
                });
#pragma warning restore 612, 618
        }
    }
}
