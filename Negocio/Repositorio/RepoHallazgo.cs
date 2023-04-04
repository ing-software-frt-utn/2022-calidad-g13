using Datos;
using Negocio.Interfaces;
using Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Repositorio
{
    public  class RepoHallazgo : IRepoHallazgo
    {
       

       public int ObtenerUltimoHallazgo()
        {
            using (var db = new TFI_ControlCalidadEntities())
            {
                var ultimoHallazgo = db.Hallazgo.Max(h => (int?)h.ID_Hallazgo) ?? 0;
                return ultimoHallazgo;
            }
        }


        public void RegistrarHallazgo(ModeloHallazgo hallazgo)
        {

            using (var db = new TFI_ControlCalidadEntities())
            {
                db.Hallazgo.Add(EnviarDB(hallazgo));
                db.SaveChanges();

            }
        }

        private Hallazgo EnviarDB(ModeloHallazgo h)
        {
            return new Hallazgo()
            {
                ID_Hallazgo = h.ID_Hallazgo,
                Fecha_Hallazgo = h.Fecha_Hallazgo,
                Hora_Registro = h.Hora_Registro,
                Pie = h.Pie,
                Defecto_id = h.Defecto_id,
                ID_JL = h.ID_JL,

            };
        }



    }
}
