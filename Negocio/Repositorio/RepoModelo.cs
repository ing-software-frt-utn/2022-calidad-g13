using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Negocio.Modelos;
using Negocio.Interfaces;
using Datos;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace Negocio.Repositorio
{
        public class RepoModelo : IRepoModelo
        {

        public void AgregarModelo(ModelModelo modelo, List<int> codigosColores)
        {
            using (var db = new TFI_ControlCalidadEntities())
            {
                var nuevoModelo = new Modelo
                {
                    SKU = modelo.SKU,
                    Denominacion = modelo.Denominacion,
                    LimiteInferiorObs= modelo.LimiteInferiorObs,
                    LimiteSuperiorObs= modelo.LimiteSuperiorObs,
                    LimiteInferiorRep= modelo.LimiteInferiorRep,
                    LimiteSuperiorRep= modelo.LimiteSuperiorRep,
                    Color = db.Color.Where(c => codigosColores.Contains(c.codigo)).ToList()
                };

                db.Modelo.Add(nuevoModelo);
                db.SaveChanges();
            }
        }



        public void DeleteModelo(int? SKU)
            {
            using (var db = new TFI_ControlCalidadEntities())
            {
                var modelo = db.Modelo.Find(SKU);
                var m = db.Modelo.Include("Color").SingleOrDefault(model => model.SKU == modelo.SKU);
                m.Color.Clear();
                db.Modelo.Remove(modelo);
                db.SaveChanges();

            }
        }


        public ModelModelo BuscarModelo(int SKU)
        {
            using (var db = new TFI_ControlCalidadEntities())
            {

                return MapearDB(db.Modelo.Find(SKU));

            }
        }


       public void EditarModelo(ModelModelo modelo, List<int> codigosColores)
     {
        using (var db = new TFI_ControlCalidadEntities())
       {
        var editar = db.Modelo.Include("Color").SingleOrDefault(m => m.SKU == modelo.SKU);

        editar.SKU = modelo.SKU;
        editar.Denominacion = modelo.Denominacion;
        editar.LimiteInferiorRep = modelo.LimiteInferiorRep;
        editar.LimiteInferiorObs = modelo.LimiteInferiorObs;
        editar.LimiteSuperiorObs = modelo.LimiteSuperiorObs;
        editar.LimiteSuperiorRep = modelo.LimiteSuperiorRep;

        // Eliminamos los colores 
        editar.Color.Clear();

         // Agregamos los colores seleccionados
        List<Color> coloresSeleccionados = db.Color.Where(c => codigosColores.Contains(c.codigo)).ToList();

         editar.Color = coloresSeleccionados;

                db.SaveChanges();
          }

    }


        public List<ModelModelo> ListarModelos()
            {
            using (var db = new TFI_ControlCalidadEntities())
            {

                return db.Modelo.Select(MapearDB).ToList();

            }
        }



        private List<Color> ObtenerColoresSeleccionados(List<int> codigos)
        {
            using (var db = new TFI_ControlCalidadEntities())
            {
                List<Color> coloresSeleccionados = db.Color.Where(c => codigos.Contains(c.codigo)).ToList();
                return coloresSeleccionados;

            }


        }

        public int CantidadModelos()
        {
            using (var db = new TFI_ControlCalidadEntities())
            {
                int cantModelos = db.Modelo.Count();
                return cantModelos;
            }

        }


        //private Modelo EnviarDB(ModelModelo modelo)
        //{
        //    return new Modelo()
        //    {
        //        SKU = modelo.SKU,
        //        Denominacion = modelo.Denominacion,
        //        LimiteInferiorObs = modelo.LimiteInferiorObs,
        //        LimiteSuperiorObs = modelo.LimiteSuperiorObs,
        //        LimiteInferiorRep = modelo.LimiteInferiorRep,
        //        LimiteSuperiorRep = modelo.LimiteSuperiorRep,
        //        Color = (ICollection<Color>)modelo.Colores,
        //    };
        //}

        private ModelModelo MapearDB(Modelo tabla)
        {

            if (tabla == null)
            {
                return null;
            }

            List<ModeloColor> colores = tabla.Color.Select(c => new ModeloColor { Descripcion = c.descripcion, Codigo = c.codigo }).ToList();

            return new ModelModelo()
            {
                SKU = tabla.SKU,
                Denominacion = tabla.Denominacion,
                LimiteInferiorObs = (int)tabla.LimiteInferiorObs,
                LimiteSuperiorObs = (int)tabla.LimiteSuperiorObs,
                LimiteInferiorRep = (int)tabla.LimiteInferiorRep,
                LimiteSuperiorRep = (int)tabla.LimiteSuperiorRep,
                Colores = colores != null ? colores : new List<ModeloColor>(),
            };
        }


        //Validaciones






    }

}


    
