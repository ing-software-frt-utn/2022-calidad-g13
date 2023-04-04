using Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
    public class ModeloJornadaLaboral
    {
        [DisplayName("Jornada N°")]
        public int ID_JL { get; set; }

        [DisplayName("Fecha de  Inicio")]
        public DateTime FechaInicio { get; set; }

        [DisplayName("Fecha de Finalizacion")]
        public DateTime? FechaFin { get; set; }

        [DisplayName("Total de Pares de Primera")]
        public int Total_PP { get; set; }

        [DisplayName("Total de Pares de Segunda")]
        public int Total_PS { get; set; }

        [DisplayName("Turno")]
        public int Id_Turno { get; set; }

        public int Id_Hallazgo { get; set; }

        [DisplayName("Supervisor")]
        public int id_usuario { get; set; }

        public int num_op { get; set; }

        public int TotalDR { get; set; }

        public int TotalDO { get; set; }


        public ModeloHallazgo Hallazgo { get; set; }
        public ModeloTurno Turno { get; set; }
        public ModeloUsuario Usuario { get; set; }

        public static implicit operator ModeloJornadaLaboral(int v)
        {
            throw new NotImplementedException();
        }
    }
}
