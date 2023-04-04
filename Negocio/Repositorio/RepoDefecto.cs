using Datos;
using Negocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Repositorio
{
    public class RepoDefecto : IRepoDefecto
    {
        public List<Defecto> GetDefectos()
        {
            using (var db = new TFI_ControlCalidadEntities())
            {
                return db.Defecto.ToList();
            }
        }
    }
}
