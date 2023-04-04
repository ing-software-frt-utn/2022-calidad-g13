using Datos;
using Microsoft.Ajax.Utilities;
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
    public class OrdenProduccionController : Controller
    {
       

        private IRepoOrdenProduccion _repoOP;
        private IRepoModelo _repoModelo;
        private IRepoColor _repoColor;
        private IRepoLinea _repoLinea;
        private IRepoUsuario _repoUsuario;

        public OrdenProduccionController()
        {
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

        // GET: OrdeProduccion
        public ActionResult Index()
        {
            var listaOP = _repoOP.ListarOP();
            foreach (var op in listaOP)
            {
                op.Modelo = _repoModelo.BuscarModelo(op.Sku_modelo);
                op.Color = _repoColor.BuscarColor(op.Codigo_color);
                op.Usuario = _repoUsuario.BuscarUsuario(op.Id_usuario);
            }
            return View(listaOP);
        }


        public ActionResult Create()
        {
            var listaModelos = _repoModelo.ListarModelos();
            var listaLineas = _repoLinea.ObtenerLineasDisponibles();
            


            ViewBag.listamodelos = new SelectList(listaModelos, "SKU", "Denominacion");
            ViewBag.listalineas = new SelectList(listaLineas, "Numero_Linea", "Numero_Linea");

            return View();
        }


        // POST: OrdeProduccion/Create
        [HttpPost]
        public ActionResult Create(ModeloOP op)
        {
            if (!ModelState.IsValid)
            {
                return View(op);

            }

            var usuario = Session["Usuario"] as ModeloUsuario;
            op.Id_usuario = usuario.Legajo;
            op.Fecha_I = DateTime.Now; // Establecer la propiedad Fecha_I con la fecha y hora actual
            op.Estado = "En Proceso";


            var lineaDisponible = _repoLinea.ComprobarEstado(op.Num_linea);
            var esUnico = _repoOP.ComprobarUnicidad(op.Numero_OP);
            var estaDisponible = _repoUsuario.ComprobarDisponibilidad(op.Id_usuario);

            

            if(!ModelState.IsValid)
            {
                return View(op);

            }

            if(esUnico != true)
            {
                ViewBag.Mensaje = "El numero de orden de produccion ingresado ya existe. Por favor ingrese otro";
                var listaModelos = _repoModelo.ListarModelos();
                var listaLineas = _repoLinea.ObtenerLineasDisponibles();
                ViewBag.listamodelos = new SelectList(listaModelos, "SKU", "Denominacion");
                ViewBag.listalineas = new SelectList(listaLineas, "Numero_Linea", "Numero_Linea");
                return View(op);
            }
            if(lineaDisponible != true)
            {
                ViewBag.Mensaje = "La línea seleccionada ya no está disponible";
                var listaModelos = _repoModelo.ListarModelos();
                var listaLineas = _repoLinea.ObtenerLineasDisponibles();
                ViewBag.listamodelos = new SelectList(listaModelos, "SKU", "Denominacion");
                ViewBag.listalineas = new SelectList(listaLineas, "Numero_Linea", "Numero_Linea");
                return View(op);

            }
            if(estaDisponible != true)
            {
                ViewBag.Mensaje = "Usted ya se encuentra asociado a una Ordem de Produccion";
                var listaModelos = _repoModelo.ListarModelos();
                var listaLineas = _repoLinea.ObtenerLineasDisponibles();
                ViewBag.listamodelos = new SelectList(listaModelos, "SKU", "Denominacion");
                ViewBag.listalineas = new SelectList(listaLineas, "Numero_Linea", "Numero_Linea");
                return View(op);
            }

            if (estaDisponible == true && lineaDisponible == true && esUnico == true)
            { 
                _repoOP.IniciarOP(op);
                var linea = _repoLinea.BuscarLinea(op.Num_linea);
                linea.Estado = "No Disponible";
                _repoLinea.ActualizarLinea(linea);
                return RedirectToAction("Index");
            }


            return View();
            
        }


        public ActionResult GetColordelModelo(int sku_model)
        {
            var colores = _repoModelo.BuscarModelo(sku_model).Colores
                                          .Select(c => new SelectListItem
                                          {
                                              Value = c.Codigo.ToString(),
                                              Text = c.Descripcion
                                          }).ToList();
            return Json(colores, JsonRequestBehavior.AllowGet);
        }



        //// GET: OrdeProduccion/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: OrdeProduccion/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: OrdeProduccion/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: OrdeProduccion/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }


        //}




    }




}
    
