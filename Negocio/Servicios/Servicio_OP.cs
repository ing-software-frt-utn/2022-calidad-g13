using Datos;
using Negocio.Interfaces;
using Negocio.Modelos;
using Negocio.Modelos.Enumeraciones;
using Negocio.Repositorio;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Servicios
{
    public class Servicio_OP
    {
        private readonly IRepoLinea _repoLinea;
        private readonly IRepoOrdenProduccion _repoOP;
        private readonly IRepoUsuario _repoUsuario;
        private readonly IRepoTurno _repoTurno;


        public Servicio_OP(IRepoOrdenProduccion repoOrdenProduccion, IRepoLinea repoLinea, IRepoUsuario repoUsuario)
        {
            this._repoOP = repoOrdenProduccion;
            this._repoLinea = repoLinea;
            this._repoUsuario = repoUsuario;
        }

        public Servicio_OP()
        {
            if (_repoLinea == null)
            {

                _repoLinea = new RepoLinea();
            }
            if (_repoOP == null)
            {

                _repoOP = new RepoOrdenProduccion();
            }
            if (_repoUsuario == null)
            {

                _repoUsuario = new RepoUsuario();
            }
            if (_repoTurno == null)
            {

                _repoTurno = new RepoTurno();
            }

        }




        public (bool exito, string mensaje) CrearOP(ModeloOP op, int legajo_usuario)
        {

            var resultado = ValidarOrdenProduccion(op, op.Num_linea, legajo_usuario);

            if (resultado.exito)
            {
                IniciarOP(op, legajo_usuario);
                resultado.mensaje = "Orden de producción creada exitosamente.";

            }

            return resultado;

        }

        private void IniciarOP(ModeloOP orden, int user)
        {


            orden.Id_usuario = user;
            orden.Fecha_I = DateTime.Now; // Establecer la propiedad Fecha_I con la fecha y hora actual
            orden.Estado_OP = Estado.En_Proceso;
            _repoOP.IniciarOP(orden);


            var linea = _repoLinea.BuscarLinea(orden.Num_linea);
            linea.Estado = Estado_Linea.No_Disponible;
            _repoLinea.ActualizarLinea(linea);

        }

        private (bool exito, string mensaje) ValidarOrdenProduccion(ModeloOP op, int numLinea, int legajo_usuario)
        {

            var resultado = (exito: false, mensaje: "");

            // Verificar si el número de OP es único
            var esUnico = _repoOP.ComprobarUnicidad(op.Numero_OP);
            if (!esUnico)
            {
                resultado.mensaje = "El número de orden de producción ingresado ya existe. Por favor ingrese otro";
                return resultado;
            }

            // Verificar si la línea está disponible
            var lineaDisponible = _repoLinea.ComprobarEstado(numLinea);
            if (!lineaDisponible)
            {
                resultado.mensaje = "La línea seleccionada ya no está disponible";
                return resultado;
            }

            // Verificar si el usuario ya tiene una OP asignada
            var estaDisponible = _repoUsuario.ComprobarDisponibilidad(legajo_usuario);
            if (!estaDisponible)
            {
                resultado.mensaje = "Usted ya se encuentra asociado a una Orden de Producción";
                return resultado;
            }

            resultado.exito = true;
            return resultado;
        }

        public ModeloOP BuscarOP(int num_op)
        {

            return _repoOP.BuscarOP(num_op);
        }

        public List<ModeloOP> BuscarOP_EP()
        {
           

            return _repoOP.BuscarOP_EP();

        }

        public Orden_Produccion BuscarOPPorJornada(int usuario)
        {
            var repoJL = new RepoJornadaLaboral();
            var jornada_id = repoJL.ObtenerJornada(usuario);

            return _repoOP.BuscarOPPorJornada(jornada_id);
        }


        public void IniciarJornada(ModeloJornadaLaboral jornada, int usuario,int op_num )
        {
            var repoJL = new RepoJornadaLaboral();
            var ultimoID = repoJL.ObtenerUltimaJornada();

            jornada.ID_JL = ++ultimoID;
            jornada.id_usuario = usuario;
            jornada.FechaInicio = DateTime.Now;
            var idTurno = _repoTurno.ObtenerTurno(jornada.FechaInicio.TimeOfDay);
            jornada.Id_Turno = idTurno;
            jornada.num_op = op_num;

            _repoOP.IniciarJornada(jornada);

            
        }



        //public void ActualizarOP(ModeloOP op, ModeloJornadaLaboral  jornada)
        //{
           
        //   var repoOP = new RepoOrdenProduccion();
        //   repoOP.ActualizarOP(op, jornada);

        //}

        public void PausarOP(int numero_op)
        {
            _repoOP.PausarOP(numero_op);

        }


        public void ReanudarOP(int numero_op, int legajo)
        {
            _repoOP.ReanudarOP(numero_op, legajo);


        }

        public void FinalizarOP(int numero_OP)
        {
           
            _repoOP.FinalizarOP(numero_OP);

            var linea = _repoLinea.BuscarLinea(numero_OP);
            linea.Estado = Estado_Linea.Disponible;
            _repoLinea.ActualizarLinea(linea);

        }


        public int ObtenerJornada(int idUsuario)
        {
            var repoJL = new RepoJornadaLaboral();
            return repoJL.ObtenerJornada(idUsuario);
        }


        public (bool exito, string mensaje) VerificarUsuarioAsociadoAOP(int idUsuario)
        {
            using (var db = new TFI_ControlCalidadEntities())
            {
                var jornada = db.JornadaLaboral.FirstOrDefault(j => j.id_usuario == idUsuario && j.FechaFin == null);
                if (jornada != null)
                {
                    return (true, "El usuario ya se encuentra asociado a una orden de producción");
                }
                else
                {
                    return (false, "");
                }
            }
        }

        public (bool exito, string mensaje) VerificarOPAsociadaAJornada(int numeroOP)
        {
            using (var db = new TFI_ControlCalidadEntities())
            {
                var jornada = db.JornadaLaboral.FirstOrDefault(j => j.num_op == numeroOP && j.FechaFin == null);
                if (jornada != null)
                {
                    return (true, "La orden de producción ya está asociada a una jornada laboral");
                }
                else
                {
                    return (false, "");
                }
            }
        }


    }
}
