using Microsoft.SqlServer.Server;
using Negocio.Modelos;
using Negocio.Repositorio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Servicios
{
   public  class ServicioInspeccion
    {
        public RegistrarHallazgo(int id, string tipoPie, string tipoDefecto, TimeSpan horaRegistro, int usuario_id)
        {
            var hallazgo = new ModeloHallazgo();
            var repoHallazgo = new RepoHallazgo();
            var ultimoid = repoHallazgo.ObtenerUltimoHallazgo();
            var repoJL = new RepoJornadaLaboral();
            var id_jornadaactual = repoJL.ObtenerJornada(usuario_id);


            var ultimoID = repoJL.ObtenerUltimaJornada();
            hallazgo.ID_Hallazgo = ++ultimoid;
            hallazgo.Defecto_id = id;
            hallazgo.Pie = tipoPie;
            hallazgo.Hora_Registro = horaRegistro;
            hallazgo.Fecha_Hallazgo = DateTime.Now;
            hallazgo.ID_JL = id_jornadaactual;

            return 

        }





    }
}
