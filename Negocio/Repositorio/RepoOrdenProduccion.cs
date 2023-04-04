using Datos;
using Negocio.Interfaces;
using Negocio.Modelos;
using Negocio.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Repositorio
{
    public class RepoOrdenProduccion : IRepoOrdenProduccion
    {


        public void IniciarOP(ModeloOP op)
        {

            using (var db = new TFI_ControlCalidadEntities())
            {
                db.Orden_Produccion.Add(EnviarDB(op));
                db.SaveChanges();

            }
        }

        public List<ModeloOP> ListarOP()
        {
            using (var db = new TFI_ControlCalidadEntities())
            {
                return db.Orden_Produccion.Select(MapearDB).ToList();
            }

        }


        public ModeloOP BuscarOP(int num_op)
        {

            using (var db = new TFI_ControlCalidadEntities())
            {

                return MapearDB(db.Orden_Produccion.Find(num_op));

            }
        }

        public void IniciarJornada(ModeloJornadaLaboral jornada)
        {

            using (var db = new TFI_ControlCalidadEntities())
            {
                db.JornadaLaboral.Add(EnviarDBJ(jornada));
                db.SaveChanges();


            }

        }


        public List<ModeloOP> BuscarOP_EP()
        {
            using (var db = new TFI_ControlCalidadEntities())
            {
                var opEnProceso = db.Orden_Produccion.Where(op => op.Estado == "En_Proceso").ToList();
                var modelosOP = new List<ModeloOP>();

                foreach (var op in opEnProceso)
                {
                    // Revisamos si alguna jornada tiene la misma OP y no tiene fecha fin
                    var jornada = db.JornadaLaboral.FirstOrDefault(j => j.num_op == op.Numero_OP && j.FechaFin == null);

                    if (jornada == null)
                    {
                        var modeloOP = MapearDB(op);
                        modeloOP.Num_linea = (int)op.num_linea;
                        modeloOP.Codigo_color = (int)op.codigo_color;
                        modeloOP.Sku_modelo = (int)op.sku_modelo;
                        modelosOP.Add(modeloOP);
                    }
                }

                return modelosOP;
            }
        }


        public void PausarOP(int numero_op)
        {
            using (var db = new TFI_ControlCalidadEntities())
            {

                var op = db.Orden_Produccion.Find(numero_op);
                op.Estado = Estado.Pausada.ToString();
                db.SaveChanges();

            }
        }

        public void ReanudarOP(int numero_op, int legajo)
        {
            using (var db = new TFI_ControlCalidadEntities())
            {
                var op = db.Orden_Produccion.Find(numero_op);
                op.Estado = Estado.En_Proceso.ToString();
                db.SaveChanges();
            }
        }

        public void FinalizarOP(int numero_op)
        {
            using (var db = new TFI_ControlCalidadEntities())
            {
                var op = db.Orden_Produccion.Find(numero_op);
                op.Fecha_F = DateTime.Now;
                op.Estado = Estado.Finalizada.ToString();
                db.SaveChanges();

            }


        }



        private Orden_Produccion EnviarDB(ModeloOP op)
        {
            return new Orden_Produccion()
            {
                Numero_OP = op.Numero_OP,
                Fecha_I = op.Fecha_I,
                Fecha_F = op.Fecha_F.HasValue ? op.Fecha_F : null,
                Estado = op.Estado_OP.ToString(),
                num_linea = op.Num_linea,
                codigo_color = op.Codigo_color,
                sku_modelo = op.Sku_modelo,
                id_usuario = op.Id_usuario,


            };
        }

        private ModeloOP MapearDB(Orden_Produccion tabla)
        {
            if (tabla == null)
            {
                return null;
            }

            return new ModeloOP()
            {
                Numero_OP = tabla.Numero_OP,
                Fecha_I = tabla.Fecha_I ?? DateTime.MinValue,
                Fecha_F = tabla.Fecha_F ?? null,
                Estado_OP = (Estado)Enum.Parse(typeof(Estado), tabla.Estado),
                Num_linea = tabla.num_linea ?? 0,
                Codigo_color = tabla.codigo_color ?? 0,
                Sku_modelo = tabla.sku_modelo ?? 0,
                Id_usuario = tabla.id_usuario ?? 0,

            };

        }

        private JornadaLaboral EnviarDBJ(ModeloJornadaLaboral jornada)
        {
            return new JornadaLaboral()
            {
                ID_JL = jornada.ID_JL,
                FechaInicio = jornada.FechaInicio,
                FechaFin = jornada.FechaFin.HasValue ? jornada.FechaFin : null,
                Total_PP = jornada.Total_PP != 0 ? jornada.Total_PP : (int?)null,
                Id_Turno = jornada.Id_Turno,
                id_usuario = jornada.id_usuario,
                num_op = jornada.num_op,

            };
        }

        private JornadaLaboral MapearJornadaLaboral(ModeloJornadaLaboral jornada)
        {
            var jornadaDB = new JornadaLaboral();

            // Asignar las propiedades de ModeloJornadaLaboral a JornadaLaboral
            jornadaDB.ID_JL = jornada.ID_JL;
            jornadaDB.FechaFin = jornada.FechaFin;
            jornadaDB.FechaInicio = jornada.FechaInicio;
            jornadaDB.id_usuario = jornada.id_usuario;
            jornadaDB.Id_Turno = jornada.Id_Turno;
            jornadaDB.Total_PP = jornada.Total_PP;
            jornadaDB.num_op = jornada.num_op;
            // ...

            return jornadaDB;
        }
       
        /// VALIDACIONES ///
        public bool ComprobarUnicidad(int num_op)
        {
            using (var db = new TFI_ControlCalidadEntities())
            {
                // Se busca una orden de producción que tenga el número proporcionado
                var orden = db.Orden_Produccion.FirstOrDefault(op => op.Numero_OP == num_op);

                // Si se encuentra una orden con ese número, no se puede crear otra con el mismo número
                if (orden != null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public Orden_Produccion BuscarOPPorJornada(int Id_jornada)
        {
            using (var db = new TFI_ControlCalidadEntities())
            {
                var op = (from o in db.Orden_Produccion
                          join j in db.JornadaLaboral on o.Numero_OP equals j.num_op
                          where j.ID_JL == Id_jornada
                          select o).FirstOrDefault();

                return op;
            }

        }

        //public int ObtenerOPAsociada(int idUsuario)
        //{
        //    using (var db = new TFI_ControlCalidadEntities())
        //    {
        //        var jornada = db.JornadaLaboral.FirstOrDefault(j => j.id_usuario == idUsuario && j.FechaFin == null);
        //        if (jornada != null)
        //        {
        //            return jornada.numero_op;
        //        }
        //        else
        //        {
        //            return -1; // O algún otro valor que indique que el usuario no está asociado a ninguna OP
        //        }
        //    }

        //}
    }
}
