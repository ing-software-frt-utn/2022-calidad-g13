using Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Interfaces
{
    public interface IRepoHallazgo
    {
        int ObtenerUltimoHallazgo();
        void RegistrarHallazgo(ModeloHallazgo hallazgo);
    }
}
