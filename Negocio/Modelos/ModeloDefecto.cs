using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
    public class ModeloDefecto
    {
        public int ID_Defecto { get; set; }

        [DisplayName("Defecto")]
        public string Descripcion { get; set; }

        [DisplayName("Tipo de Defecto")]
        public string Tipo_Defecto { get; set; }


    }
}
