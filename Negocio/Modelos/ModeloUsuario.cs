using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
    public class ModeloUsuario
    {
        public int Legajo { get; set; }

        public string Contraseña { get; set; }

        [DisplayName("Usuario")]
        public string Nombre_Apellido { get; set; }

        public string Email { get; set; }

        public string Rol { get; set; }


    }
}
