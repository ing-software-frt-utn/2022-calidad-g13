using Datos;
using Negocio.Modelos;
using Negocio.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Servicios
{
    public class Servicio_Linea
    {
        public void ActualizarLinea(Modelos.ModeloLinea linea)
        {
            var repoLinea = new RepoLinea();
            repoLinea.ActualizarLinea(linea);
        }

        public ModeloLinea BuscarLinea(int num_linea)
        {
            var repoLinea = new RepoLinea();
            return repoLinea.BuscarLinea(num_linea);

        }

        public List<ModeloLinea> ListarLineas()
        {
            var repoLinea = new RepoLinea();
            return repoLinea.ListarLineas();

        }

        public List<Linea> ObtenerLineasDisponibles()
        {
            var repoLinea = new RepoLinea();
            return repoLinea.ObtenerLineasDisponibles();
        }






    }
}
