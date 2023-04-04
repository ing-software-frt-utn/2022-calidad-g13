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
        //private AlquilerVehiculosEntities db = new AlquilerVehiculosEntities();

        public ActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        //public ActionResult VistaDashboard()
        //{
        //    int totalCliente = db.Usuario.Where(u => u.EsAdministrador == false).Count();

        //    return Json(new { TotalClientes = totalCliente }, JsonRequestBehavior.AllowGet);
        //}
    }
}