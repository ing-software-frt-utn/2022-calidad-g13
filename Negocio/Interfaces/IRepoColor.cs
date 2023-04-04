using Datos;
using Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Interfaces
{
    public interface IRepoColor

    {
        void AgregarColor(ModeloColor color);
        void EditarColor(ModeloColor color);
        void DeleteColor(int codigo);
        List<ModeloColor> ListarColores();
        ModeloColor BuscarColor(int codigo);
        int CantidadColores();

    }
}
