using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
    public class ModeloTurno
    {
        public int ID_Turno { get; set; }

        [DisplayName("Hora de Inicio")]
        public TimeSpan Hr_Inicio { get; set; }

        [DisplayName("Horario de Finalizacion")]
        public TimeSpan HR_Fin { get; set; }

        public string Descripcion { get; set; } 
    }
}
