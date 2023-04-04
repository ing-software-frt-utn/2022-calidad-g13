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
using System.Web.Services.Description;

namespace QCS.Controllers
{
    public class OrdenProduccionController : Controller
    {
       
        private readonly Servicio_OP op_service;
        private IRepoOrdenProduccion _repoOP;
        private IRepoModelo _repoModelo;
        private IRepoColor _repoColor;
        private IRepoLinea _repoLinea;
        private IRepoUsuario _repoUsuario;

        public OrdenProduccionController()
        {
            if (op_service == null)
            {

                op_service= new Servicio_OP();
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
        public ActionResult Create(VMOrdenProduccion orden)
        {
            if (!ModelState.IsValid)
            {
                return View(orden);
            }

            var usuario = Session["Usuario"] as ModeloUsuario;
            var op = new ModeloOP
            {
                Numero_OP = orden.Numero_OP,
                Sku_modelo = orden.Sku_modelo,
                Codigo_color = orden.Codigo_color,
                Num_linea = orden.Num_linea,
            };

            var resultadoCreacion = op_service.CrearOP(op, usuario.Legajo);

            if (!resultadoCreacion.exito)
            {
                return HandleCreateErrors(orden, resultadoCreacion.mensaje);
            }

            return RedirectToAction("Index");
        }


        public ActionResult ReanudarOP(int numero_op, int legajo)

        {
            var usuario = Session["Usuario"] as ModeloUsuario;
            op_service.ReanudarOP(numero_op, usuario.Legajo);
            return RedirectToAction("Index");

        }

        public ActionResult FinalizarOP(int numero_op)

        {
            op_service.FinalizarOP(numero_op);
            return RedirectToAction("Index");

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

        private ActionResult HandleCreateErrors(VMOrdenProduccion orden, string mensaje)
        {
            ViewBag.Mensaje = mensaje;
            var listaModelos = _repoModelo.ListarModelos();
            var listaLineas = _repoLinea.ObtenerLineasDisponibles();
            ViewBag.listamodelos = new SelectList(listaModelos, "SKU", "Denominacion");
            ViewBag.listalineas = new SelectList(listaLineas, "Numero_Linea", "Numero_Linea");
            return View(orden);
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
    
