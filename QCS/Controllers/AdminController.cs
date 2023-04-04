using Negocio.Interfaces;
using Negocio.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QCS.Controllers
{
    public class AdminController : Controller
    {
        private IRepoModelo _repoModelo;
        private IRepoColor _repoColor;

        public AdminController()
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

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        //public ActionResult VistaDashboard()
        //{
        //    int totalModelos = _repoModelo.CantidadModelos();
        //    int totalColores = _repoColor.CantidadColores();
        //}
    }
}
