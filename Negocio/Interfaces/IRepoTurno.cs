using Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Interfaces
{
    public interface IRepoTurno
    {
        int ObtenerTurno(TimeSpan horaInicio);
        ModeloTurno BuscarTurno(int id_turno);

    }
}
