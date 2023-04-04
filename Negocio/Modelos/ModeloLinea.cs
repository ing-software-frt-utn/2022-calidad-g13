using Negocio.Modelos.Enumeraciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
    public class ModeloLinea
    {

        public int Numero_Linea { get; set; }
        public virtual Estado_Linea Estado { get; set; }



    }
}
