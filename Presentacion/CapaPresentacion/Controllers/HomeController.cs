using Negocio.Interfaces;
using Negocio.Repositorio;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacionAdmin.Controllers
{
    public class HomeController : Controller
    {
        private IRepoModelo _repoModelo;
        private IRepoColor _repoColor;

        public HomeController()
        {
            if (_repoModelo == null)
            {

                _repoModelo = new RepoModelo();
            }

            if (_repoColor == null)
            {

                _repoColor = new RepoColor();
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult VistaDashboard()
        {
            int totalModelos = _repoModelo.CantidadModelos();
            int totalColores = _repoColor.CantidadColores();

            return Json(new { TotalModelos = totalModelos, TotalColores = totalColores }, JsonRequestBehavior.AllowGet);
        }

    }
}