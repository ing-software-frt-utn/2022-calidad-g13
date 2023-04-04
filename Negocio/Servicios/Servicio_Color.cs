using Negocio.Modelos;
using Negocio.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Servicios
{
    public class Servicio_Color
    {
        public void AgregarColor(ModeloColor color)
        {
            var repoColor = new RepoColor();
            repoColor.AgregarColor(color);

        }

        public ModeloColor BuscarColor(int codigo)
        {
            var repoColor = new RepoColor();
            return  repoColor.BuscarColor(codigo);

        }


        public void DeleteColor(int codigo)
        {
            var repoColor = new RepoColor();
             repoColor.DeleteColor(codigo);


        }

        public void EditarColor(ModeloColor color)
        {
            var repoColor = new RepoColor();
            repoColor.EditarColor(color);

        }

        public List<ModeloColor> ListarColores()
        {
            var repoColor = new RepoColor();
            return repoColor.ListarColores();

        }

        public int CantidadColores()
        {
            var repoColor = new RepoColor();
            return repoColor.CantidadColores();

        }


    }
}
