using Datos;
using Negocio.Interfaces;
using Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Repositorio
{
    public class RepoColor : IRepoColor
    {
        public void AgregarColor(ModeloColor color)
        {
            using (var db = new TFI_ControlCalidadEntities())
            {
                db.Color.Add(EnviarDB(color));
                db.SaveChanges();

            }
        }

        public ModeloColor BuscarColor(int codigo)
        {
            using (var db = new TFI_ControlCalidadEntities())
            {

                return MapearDB(db.Color.Find(codigo));

            }
        }

        public void DeleteColor(int codigo)
        {
            using (var db = new TFI_ControlCalidadEntities())
            {

                var eliminar = db.Color.Find(codigo);
                db.Color.Remove(eliminar);
                db.SaveChanges();

            }
        }

        public void EditarColor(ModeloColor color)
        {
            using (var db = new TFI_ControlCalidadEntities())
            {

                var editar = db.Color.Find(color.Codigo);
                editar.codigo = color.Codigo;
                editar.descripcion = color.Descripcion;
                db.SaveChanges();
            }
        }

        public List<ModeloColor> ListarColores()
        {
            using (var db = new TFI_ControlCalidadEntities())
            {

                return db.Color.Select(MapearDB).ToList();

            }
        }

        public int CantidadColores()
        {
            using (var db = new TFI_ControlCalidadEntities())
            {
                int cantColores = db.Color.Count();
                return cantColores;
            }

        }


        private Color EnviarDB(ModeloColor color)
        {
            return new Color()
            {
                descripcion = color.Descripcion,
                codigo = color.Codigo,
            };
        }

        private ModeloColor MapearDB(Color tabla)
        {
            if (tabla == null)
            {
                return null;
            }

            return new ModeloColor()
            {
                Codigo = tabla.codigo,
                Descripcion = tabla.descripcion,
            };
        }

        //Validaciones


    }
}
