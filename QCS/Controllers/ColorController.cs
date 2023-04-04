using Negocio.Interfaces;
using Negocio.Modelos;
using Negocio.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace QCS.Controllers
{
    public class ColorController : Controller
    {
        private IRepoColor _repoColor;

        public ColorController()
        {
            if (_repoColor == null)
            {

                _repoColor = new RepoColor();
            }

        }

        // GET: Color
        public ActionResult Index()
        {
            var listarColores = _repoColor.ListarColores();

            return View(listarColores);
        }

        // GET: colors/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ModeloColor color)
        {

            if (ModelState.IsValid)
            {
                _repoColor.AgregarColor(color);
            }

            return RedirectToAction("Index");
        }

        // GET: colors/Edit/5
        public ActionResult Edit(int id)
        {
          
            var color = _repoColor.BuscarColor(id);
            if (color == null)
            {
                return HttpNotFound();
            }
            return View(color);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ModeloColor color)
        {
            _repoColor.EditarColor(color);
            return RedirectToAction("Index");


        }

        public ActionResult Delete(int? id)
        {
            var color = _repoColor.BuscarColor((int)id);
            if (color == null)
            {
                return HttpNotFound();
            }
            return View(color);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Home");
            }
            _repoColor.DeleteColor((int)id);
            return RedirectToAction("Index");

        }

    }
}