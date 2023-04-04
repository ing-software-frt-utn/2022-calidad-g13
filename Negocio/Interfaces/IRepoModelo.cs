using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Negocio.Modelos;

namespace Negocio.Interfaces
{
    public interface IRepoModelo
    {
        void AgregarModelo(ModelModelo modelos, List<int> codigosColores);
        void EditarModelo(ModelModelo modelo, List<int> codigosColores);
        void DeleteModelo(int? SKU);
        List<ModelModelo> ListarModelos();
        ModelModelo BuscarModelo(int SKU);
        int CantidadModelos();
    }
}
