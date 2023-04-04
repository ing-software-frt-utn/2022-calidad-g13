using Negocio.Interfaces;
using Negocio.Modelos;
using Negocio.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Servicios
{
    public class Servicios_Usuario
    {
        public ModeloUsuario BuscarUsuario(int legajo)
        {
            var repoUsuario = new RepoUsuario();
            return repoUsuario.BuscarUsuario(legajo);

        }

     







    }
}
