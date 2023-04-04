using Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Interfaces
{
    public interface IRepoJorndaLaboral
    {
        void ActualizarJornada(ModeloJornadaLaboral jornada);
        ModeloJornadaLaboral BuscarJornada(int id_jornada);
        int ObtenerUltimaJornada();
        int ObtenerJornada(int usuario_id);
        void ActualizarDatosJornada(ModeloJornadaLaboral jornada);
    }
}
