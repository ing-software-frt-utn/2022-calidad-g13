using CapaPresentacionAdmin.Models;
using Datos;
using Microsoft.Ajax.Utilities;
using Negocio.Interfaces;
using Negocio.Modelos;
using Negocio.Repositorio;
using Negocio.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QCS.Controllers
{
    public class Asociar_AbandonarOPController : Controller
    {
        private Servicio_OP service_op;
        private Servicio_JL service_jl;
        private Servicio_Modelo service_modelo;
        private Servicio_Color service_color;
        private IRepoOrdenProduccion _repoOP;
        private IRepoModelo _repoModelo;
        private IRepoColor _repoColor;
        private IRepoLinea _repoLinea;
        private IRepoUsuario _repoUsuario;

        public Asociar_AbandonarOPController()
        {
            if (service_op == null)
            {

               service_op = new Servicio_OP();
            }
            if (service_modelo == null)
            {

                service_modelo = new Servicio_Modelo();
            }
            if (service_color == null)
            {

                service_color = new Servicio_Color();
            }
            if (service_jl == null)
            {

                service_jl = new Servicio_JL();
            }
            if (_repoOP == null)
            {

                _repoOP = new RepoOrdenProduccion();
            }
            if (_repoModelo == null)
            {

                _repoModelo = new RepoModelo();
            }

            if (_repoColor == null)
            {

                _repoColor = new RepoColor();
            }
            if (_repoLinea == null)
            {

                _repoLinea = new RepoLinea();
            }
            if (_repoUsuario == null)
            {

                _repoUsuario = new RepoUsuario();
            }
        }

        // GET: Asociar_AbandonarOP
        public ActionResult Index()
        {
            var repoOP = new RepoOrdenProduccion();
            var usuario = Session["Usuario"] as ModeloUsuario;



            ////// Verificar si el usuario está asociado a una OP en curso
            //var resultado = service_op.VerificarUsuarioAsociadoAOP(usuario.Legajo);
            //ViewBag.UsuarioAsociado = resultado.exito;

            // obtener todas las OP en proceso
            var opEnProceso = service_op.BuscarOP_EP();

            // crear un SelectList con los números de OP en proceso
            var opEnProcesoSelectList = new SelectList(opEnProceso, "Numero_OP", "Numero_OP");

            // asignar el SelectList a ViewBag
            ViewBag.numeroOP = opEnProcesoSelectList;

            return View();
        }

        [HttpPost]
        public ActionResult Index([Bind(Prefix = "numeroOP")] int numeroOP)
        {
            //var repoOP = new RepoOrdenProduccion();

            // buscar la OP por el número seleccionado
            var opSeleccionada = service_op.BuscarOP(numeroOP);

            // crear un SelectList con los números de OP en proceso
            var opEnProceso = service_op.BuscarOP_EP();
            var opEnProcesoSelectList = new SelectList(opEnProceso, "Numero_OP", "Numero_OP");

            // asignar la OP seleccionada y el SelectList a ViewBag para mostrarlos en la vista
            ViewBag.OPSeleccionada = opSeleccionada;
            ViewBag.ModeloOP = service_modelo.BuscarModelo(ViewBag.OPSeleccionada.Sku_modelo);
            ViewBag.ColorOP = service_color.BuscarColor(ViewBag.OPSeleccionada.Codigo_color);
            ViewBag.numeroOP = opEnProcesoSelectList;

            return View();
        }

        public ActionResult AsociarUsuarioOP(int idOP)
        {
            // Obtener usuario de la sesión
            var usuario = Session["Usuario"] as ModeloUsuario;

          

            //// Verificar si el usuario está asociado a una OP en curso
            var resultado = service_op.VerificarUsuarioAsociadoAOP(usuario.Legajo);
            if (resultado.exito)
            {
                TempData["Mensaje"] = resultado.mensaje;
                return RedirectToAction("Index");

            }

          
            // Crear la jornada laboral
            var jornada = new ModeloJornadaLaboral();
            service_op.IniciarJornada(jornada, usuario.Legajo, idOP);

            return RedirectToAction("Index", "Inspeccionar");

        }

        public ActionResult AbandonarOP(int idOP)
        {
            // Obtener usuario de la sesión
            var usuario = Session["Usuario"] as ModeloUsuario;

            // Obtener la jornada laboral asociada al usuario y a la OP
            var idJornada = service_op.ObtenerJornada(usuario.Legajo);

            if (idJornada != 0)
            {
                // Actualizar la fecha fin de la jornada laboral
                service_jl.ActualizarFechaFinJornada(idJornada);
            }

            // Agregar mensaje a TempData
            TempData["Mensaje"] = "Usted ya no se encuentra asociado a la Orden de Produccion.";

            // Redirigir a la página de inicio
            return RedirectToAction("Index", "Asociar_AbandonarOP");
        }


    }


}


