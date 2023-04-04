using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
    public class ModeloHallazgo
    {

        public int ID_Hallazgo { get; set; }

        [DisplayName("Fecha del Hallazgo")]
        public DateTime Fecha_Hallazgo { get; set; }
       
        [DisplayName("Hora de Registro")]
        public TimeSpan Hora_Registro { get; set; }
        public string Pie { get; set; }

        [DisplayName("ID del Defecto")]
        public int Defecto_id { get; set; }

        [DisplayName("Jornada N°")]
        public ModeloJornadaLaboral ID_JL { get; set; }
        
    }
}
