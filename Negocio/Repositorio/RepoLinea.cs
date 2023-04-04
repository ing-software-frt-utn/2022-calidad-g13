using Datos;
using Negocio.Interfaces;
using Negocio.Modelos;
using Negocio.Modelos.Enumeraciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Repositorio
{
    public class RepoLinea :IRepoLinea
    {
        public void ActualizarLinea(ModeloLinea linea)
        {
            using (var db = new TFI_ControlCalidadEntities())
            {
                var actualizar_linea = db.Linea.Find(linea.Numero_Linea);
                actualizar_linea.Estado = linea.Estado.ToString();
                db.SaveChanges();

            }
        }

        public ModeloLinea BuscarLinea(int num_linea)
        {

            using (var db = new TFI_ControlCalidadEntities())
            {

                return MapearDB(db.Linea.Find(num_linea));

            }


        }

        public List<ModeloLinea> ListarLineas()
        {
            using (var db = new TFI_ControlCalidadEntities())
            {

                return db.Linea.Select(MapearDB).ToList();

            }
        }

        public List<Linea> ObtenerLineasDisponibles()
        {

            using (var db = new TFI_ControlCalidadEntities())
            {

                return db.Linea.Where(l => l.Estado == "Disponible").ToList();


            }

        } 


        private ModeloLinea MapearDB(Linea tabla)
        {
            return new ModeloLinea()
            {
                Numero_Linea = tabla.Numero_Linea,
                Estado = (Estado_Linea)Enum.Parse(typeof(Estado_Linea), tabla.Estado),
            };
        }

        //Validaciones
        public bool ComprobarEstado(int num_linea)
        {
            using (var db = new TFI_ControlCalidadEntities())
            {
                // Se busca una  línea proporcionada al OP que no tenga estado "No Disponible"
                //var OP = db.Orden_Produccion.FirstOrDefault(op => op.Linea.Numero_Linea == num_linea && op.Estado != "En_Proceso");
                var OP = db.Orden_Produccion.FirstOrDefault(op => op.Linea.Numero_Linea == num_linea && (op.Estado == "Finalizada" || op.Estado == "Pausada"));


                // Si se encuentra una linea asignada a la  orden de produccióncon con estado diferente a "En_Proceo", la línea está disponible
                if (OP != null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }




    }
}
