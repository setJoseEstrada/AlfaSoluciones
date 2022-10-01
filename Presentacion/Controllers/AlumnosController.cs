using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Negocio;
using Presentacion.Security;


namespace Presentacion.Controllers
{
    [ValidarUsuario]
    public class AlumnosController : Controller
    {
        
        NAlumnos _metodos = new NAlumnos();
        Alumnos _alumno = new Alumnos();    
        // GET: Alumnos
        public ActionResult Index()
        {
            string token = (string)Session["token"];

            List<Alumnos> _listAlumnos = _metodos.ConsultarTodos(token);
            return View(_listAlumnos);
        }

        // GET: Alumnos/Details/5
        public ActionResult Details(int id)
        {
            _alumno = _metodos.ConsultarUno(id);


            return View(_alumno);
        }

        // GET: Alumnos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Alumnos/Create
        [HttpPost]
        public ActionResult Create(Alumnos alumnos)
        {
            try
            {
                // TODO: Add insert logic here
                _metodos.Agregar(alumnos);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Alumnos/Edit/5
        public ActionResult Edit(int id) => View(_metodos.ConsultarUno(id));
      

        // POST: Alumnos/Edit/5
        [HttpPost]
        public ActionResult Edit(Alumnos alumnmos)
        {
            try
            {
                // TODO: Add update logic here
                _metodos.Actualizar(alumnmos);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Alumnos/Delete/5
        public ActionResult Delete(int id)
        {
            _alumno = _metodos.ConsultarUno(id);
            return View(_alumno);
        }

        // POST: Alumnos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Alumnos alumnos)
        {
            try
            {
                // TODO: Add delete logic here
                _metodos.Eliminar(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
