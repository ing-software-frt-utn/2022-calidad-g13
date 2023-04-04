using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
    public class ModeloColor
    {

        public int Codigo { get; set; }

        [DisplayName("Color")]
        public string Descripcion { get; set; }


    }
}
