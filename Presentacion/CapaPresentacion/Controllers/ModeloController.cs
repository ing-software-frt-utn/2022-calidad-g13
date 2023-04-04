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
    public class ModeloController : Controller
    {
        private Servicio_Modelo service_modelo;
        private Servicio_Color service_color;
      
        public ModeloController()
        {
            if(service_color == null) { 
            
                service_color = new Servicio_Color();
            }
            if (service_modelo == null)
            {

                service_modelo = new Servicio_Modelo();
            }
        }
        

        // GET: Modelo
        public ActionResult Index()
        {
            var listamodelos = service_modelo.ListarModelos();
            return View(listamodelos);
        }


        // GET: colors/Create
        public ActionResult Create()
        {

            ViewBag.lista = service_color.ListarColores();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ModelModelo modelo, List<int> selectedColors)
        {
            if (ModelState.IsValid)
            {
                service_modelo.AgregarModelo(modelo, selectedColors);
                return RedirectToAction("Index");
            }

            // Si la validación falla, recupera la lista de colores y vuelve a mostrar la vista
            ViewBag.lista = service_color.ListarColores();
            return View(modelo);
        }

        // GET: colors/Edit/5
        public ActionResult Edit(int id)
        {

            var modelo = service_modelo.BuscarModelo(id);
            if (modelo == null)
            {
                return HttpNotFound();
            }

            var colores = service_color.ListarColores();
            ViewBag.Colores = colores;


            return View(modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ModelModelo modelo, List<int> selectedColors)
        {

            service_modelo.EditarModelo(modelo, selectedColors);
            return RedirectToAction("Index");


        }

        public ActionResult Delete(int? id)
        {
            var modelo = service_modelo.BuscarModelo((int)id);
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
            service_modelo.DeleteModelo((int)id);
            return RedirectToAction("Index");

        }

    }
}