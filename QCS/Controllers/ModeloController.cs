using Negocio.Interfaces;
using Negocio.Modelos;
using Negocio.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QCS.Controllers
{
    public class ModeloController : Controller
    {
        private IRepoModelo _repoModelo;
        private IRepoColor _repoColor;

        public ModeloController()
        {
            if(_repoModelo == null) { 
            
            _repoModelo= new RepoModelo();
            }

            if (_repoColor == null)
            {

                _repoColor = new RepoColor();
            }
        }
        

        // GET: Modelo
        public ActionResult Index()
        {
            var listamodelos = _repoModelo.ListarModelos();
            return View(listamodelos);
        }


        // GET: colors/Create
        public ActionResult Create()
        {

            ViewBag.lista = _repoColor.ListarColores();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ModelModelo modelo, List<int> selectedColors)
        {
            if (ModelState.IsValid)
            {
                _repoModelo.AgregarModelo(modelo, selectedColors);
                return RedirectToAction("Index");
            }

            // Si la validación falla, recupera la lista de colores y vuelve a mostrar la vista
            ViewBag.lista = _repoColor.ListarColores();
            return View(modelo);
        }

        // GET: colors/Edit/5
        public ActionResult Edit(int id)
        {

            var modelo = _repoModelo.BuscarModelo(id);
            if (modelo == null)
            {
                return HttpNotFound();
            }

            var colores = _repoColor.ListarColores();
            ViewBag.Colores = colores;


            return View(modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ModelModelo modelo, List<int> selectedColors)
        {

            _repoModelo.EditarModelo(modelo, selectedColors);
            return RedirectToAction("Index");


        }

        public ActionResult Delete(int? id)
        {
            var modelo = _repoModelo.BuscarModelo((int)id);
            if (modelo == null)
            {
                return HttpNotFound();
            }
            return View(modelo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Home");
            }
            _repoModelo.DeleteModelo((int)id);
            return RedirectToAction("Index");

        }

    }
}