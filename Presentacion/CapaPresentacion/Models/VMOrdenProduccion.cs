using Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CapaPresentacionAdmin.Models
{
    public class VMOrdenProduccion
    {
        [DisplayName("N° de Orden de Produccion")]
        public int Numero_OP { get; set; }

        [DisplayName("Numero de Linea")]
        public int Num_linea { get; set; }

        public Estado Estado { get; set; }

        [DisplayName("Color")]
        public int Codigo_color { get; set; }

        [DisplayName("Modelo")]
        public int Sku_modelo { get; set; }

        //[DisplayName("Fecha de  Inicio")]
        //public DateTime Fecha_I { get; set; }

        //[DisplayName("Fecha de Finalizacion")]
        //public Nullable<System.DateTime> Fecha_F { get; set; }

        //[DisplayName("Supervisor")]
        //public int Id_usuario { get; set; }

        //[DisplayName("N° de Jornada")]
        //public Nullable<int> Id_jornada { get; set; }




    }
}