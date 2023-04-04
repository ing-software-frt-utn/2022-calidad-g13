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
    public class Servicio_Modelo
    {
        public void AgregarModelo(ModelModelo modelo, List<int> codigosColores)
        {

            var repoModelo = new RepoModelo();

            repoModelo.AgregarModelo(modelo, codigosColores);

        }

        public void DeleteModelo(int? SKU)
        {
            var repoModelo = new RepoModelo();
            repoModelo.DeleteModelo(SKU);


        }

        public ModelModelo BuscarModelo(int SKU)
        {
            var repoModelo = new RepoModelo();
            return repoModelo.BuscarModelo(SKU);

        }

        public void EditarModelo(ModelModelo modelo, List<int> codigosColores)
        {
            var repoModelo = new RepoModelo();
            repoModelo.EditarModelo(modelo, codigosColores);


        }

        public List<ModelModelo> ListarModelos()
        {
            var repoModelo = new RepoModelo();
            return repoModelo.ListarModelos();

        }

        public int CantidadModelos()
        {
            var repoModelo = new RepoModelo();

            return repoModelo.CantidadModelos();
        }


    }
}
