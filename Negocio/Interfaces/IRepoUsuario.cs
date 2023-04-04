using Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Interfaces
{
    public interface IRepoUsuario
    {

        ModeloUsuario ObtenerUsuario(string username, string password);
        ModeloUsuario BuscarUsuario(int legajo);
        bool ComprobarDisponibilidad(int legajo);

    }
}
