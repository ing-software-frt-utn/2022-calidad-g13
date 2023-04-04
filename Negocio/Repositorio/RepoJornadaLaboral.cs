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
    public class RepoJornadaLaboral : IRepoJorndaLaboral
    {

        public ModeloJornadaLaboral BuscarJornada(int id_jornada)
        {
            using (var db = new TFI_ControlCalidadEntities())
            {
                return MapearDB(db.JornadaLaboral.Find(id_jornada));
            }

          
        }

        public int ObtenerUltimaJornada()
        {

            using (var db = new TFI_ControlCalidadEntities())
            {
                var ultimJornada = db.JornadaLaboral.Max(j => (int?)j.ID_JL) ?? 0;
                return ultimJornada;
            }
        }


        public int ObtenerJornada(int usuario_id)
        {
            using (var db = new TFI_ControlCalidadEntities())
            {
                {
                    var jornada = db.JornadaLaboral.FirstOrDefault(j => j.id_usuario == usuario_id && j.FechaFin == null);

                    return jornada.ID_JL;
                }

            }


        }


        public void ActualizarJornada(ModeloJornadaLaboral jornada)
        {
            using (var db = new TFI_ControlCalidadEntities())
            {

                var jornadaExistente = db.JornadaLaboral.FirstOrDefault(j => j.ID_JL == jornada.ID_JL);
                if (jornadaExistente != null)
                {
                    jornadaExistente.FechaFin = jornada.FechaFin;
                    db.SaveChanges();
                }
            }
        }


        public void ActualizarDatosJornada(ModeloJornadaLaboral jornada)
        {
            using (var db = new TFI_ControlCalidadEntities())
            {

                var jornadaExistente = db.JornadaLaboral.FirstOrDefault(j => j.ID_JL == jornada.ID_JL);
                if (jornadaExistente != null)
                {
                    jornadaExistente.Total_PP = jornada.Total_PP;
                    jornadaExistente.TotalDO = jornada.TotalDO;
                    jornadaExistente.TotalDR = jornada.TotalDR;
                    db.SaveChanges();
                }
            }
        }

        private ModeloJornadaLaboral MapearDB(JornadaLaboral tabla)
        {
            if (tabla == null)
            {
                return null;
            }

            return new ModeloJornadaLaboral()
            {
               ID_JL = tabla.ID_JL,
               FechaInicio = (DateTime)tabla.FechaInicio,
               FechaFin = tabla.FechaFin,
               Total_PP = (int)tabla.Total_PP,
               Id_Turno = (int)tabla.Id_Turno,
               id_usuario = (int)tabla.id_usuario,
               num_op = (int)tabla.num_op,
               TotalDO = (int)tabla.TotalDO,
               TotalDR = (int)tabla.TotalDR,
            };

        }

    }
}
