using Negocio.Modelos;
using Negocio.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Servicios
{
    public class ServicioTurno
    {


        public ModeloTurno GetTurno(TimeSpan hrActual)
        {
            var repoTurno = new RepoTurno();

            var turno_id = repoTurno.ObtenerTurno(hrActual);

            return repoTurno.BuscarTurno(turno_id);

        }


    }
}
