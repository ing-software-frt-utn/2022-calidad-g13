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
    public class RepoTurno : IRepoTurno
    {
        public ModeloTurno BuscarTurno(int id_turno)
        {

            using (var db = new TFI_ControlCalidadEntities())
            {

                return MapearTurno(db.Turno.Find(id_turno));

            }
        }

        public int ObtenerTurno(TimeSpan horaInicio)
        {
            using (var db = new TFI_ControlCalidadEntities())
            {
                var turnos = db.Turno.ToList();
                var turno = turnos.FirstOrDefault(t => t.Hr_Inicio < horaInicio && t.HR_Fin > horaInicio);
                var idturno = turno.ID_Turno;
                return idturno;

            }
        }


        private ModeloTurno MapearTurno(Turno t)
        {

            return new ModeloTurno()
            {
                ID_Turno = t.ID_Turno,
                HR_Fin = (TimeSpan)t.HR_Fin,
                Hr_Inicio = (TimeSpan)t.Hr_Inicio,
                Descripcion = t.Descripcion,


            };

        }


    }
}

