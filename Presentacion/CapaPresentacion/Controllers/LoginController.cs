using Negocio.Interfaces;
using Negocio.Repositorio;
using Negocio.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QCS.Controllers
{
    public class LoginController : Controller
    {
        private IRepoUsuario _repoUsuario;
        private Servicios_Usuario service_usuario;

        public LoginController()
        {
            if (_repoUsuario == null)
            {

                _repoUsuario = new RepoUsuario();
            }
            if (service_usuario == null)
            {

                service_usuario = new Servicios_Usuario();
            }

        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string email, string clave)
        {
            var usuario = _repoUsuario.ObtenerUsuario(email, clave);

            if (usuario != null)
            {
                // Crear sesión para el usuario
                Session["Usuario"] = usuario;

                if (usuario.Rol == "Supervisor de Linea")
                {
                    return RedirectToAction("Index", "OrdenProduccion");
                }
                else if (usuario.Rol == "Supervisor de Calidad")
                {
                    return RedirectToAction("Index", "Asociar_AbandonarOP");
                }
                else if (usuario.Rol == "Administrativo")
                {
                    return RedirectToAction("Index", "Modelo");
                }
                else
                {
                    // Acción de retorno predeterminada en caso de que el rol del usuario no coincida con ninguno de los especificados
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.Mensaje = "Correo o clave inválidos";
                return View();
            }

        }

        public ActionResult CerrarSesion()
        {
            Session["Usuario"] = null;
            return RedirectToAction("Index", "Login");
        }
    }
}
