using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidades;
using Negocio;
using Presentacion.Security;

namespace Presentacion.Controllers
{
    [ValidarUsuario]
    public class BecasAlumnosController : Controller

    {

        // GET: BecasAlumnos
        NBecasAlumnos _metodos = new NBecasAlumnos();
        BecasAlumnos _becas = new BecasAlumnos();
        
        public ActionResult Index()
        {
            string token = (string)Session["token"];

            NAlumnos alumno = new NAlumnos();
            List<Alumnos> _listAlumnos = alumno.ConsultarTodos(token);

            
            ViewBag.ListAlumnos = _listAlumnos;
            

            List<BecasAlumnos> becas = _metodos.ConsultarTodos(token);
           

            var _ListaSinBeca = _listAlumnos.Where(alu => becas.Exists(beca => alu.nombre == beca.NombreAlumno)==false).ToList();
            
            ViewBag.SinBeca = _ListaSinBeca;

            return View(becas);
        }

        // GET: BecasAlumnos/Details/5
        public ActionResult Details(int id,string tokenn)
        {
            string token = (string)Session["token"];
            tokenn = token;
            _becas = _metodos.Consultar(id,token);
            return View(_becas);
        }

        // GET: BecasAlumnos/Create
        public ActionResult Create()
        {
            string token = (string)Session["token"];
            NAlumnos alumno = new NAlumnos();
            NBecas alum = new NBecas();
            NTipo ntipo = new NTipo();
            List<Alumnos> _listAlumnos = alumno.ConsultarTodos(token);
            List<Becas> _listBecas = alum.ConsultarTodos(token);
            List<TipoBecas> _listTipo = ntipo.ConsultarTodos(token);
            ViewBag.ListAlumnos = _listAlumnos;
            ViewBag.Becas = _listBecas;
            ViewBag.Tipo = _listTipo;
            return View();
        }

        // POST: BecasAlumnos/Create
        [HttpPost]
        public ActionResult Create(BecasAlumnos becasAlumnos,string tokenn)
        {
            try
            {
                string token = (string)Session["token"];
                tokenn = token;
                //TODO: Add insert logic here
                CustomResponse cr =  _metodos.Agregar(becasAlumnos, tokenn);
                if (cr.status==200)
                {
                    return RedirectToAction("Index");
                }

                else if (cr.status == 408)
                {
                   
                    ViewBag.error = cr.message;
                    NAlumnos alumno = new NAlumnos();
                    NBecas alum = new NBecas();
                    NTipo ntipo = new NTipo();
                    List<Alumnos> _listAlumnos = alumno.ConsultarTodos(token);
                    List<Becas> _listBecas = alum.ConsultarTodos(token);
                    List<TipoBecas> _listTipo = ntipo.ConsultarTodos(token);
                    ViewBag.ListAlumnos = _listAlumnos;
                    ViewBag.Becas = _listBecas;
                    ViewBag.Tipo = _listTipo;

                    return View();
                }
                else if(cr.status==407)
                {
                    
                    ViewBag.error = cr.message;
                    NAlumnos alumno = new NAlumnos();
                    NBecas alum = new NBecas();
                    NTipo ntipo = new NTipo();
                    List<Alumnos> _listAlumnos = alumno.ConsultarTodos(token);
                    List<Becas> _listBecas = alum.ConsultarTodos(token);
                    List<TipoBecas> _listTipo = ntipo.ConsultarTodos(token);
                    ViewBag.ListAlumnos = _listAlumnos;
                    ViewBag.Becas = _listBecas;
                    ViewBag.Tipo = _listTipo;

                    return View();
                }
                else if (cr.status == 406)
                {
                    
                    ViewBag.error = cr.message;
                    NAlumnos alumno = new NAlumnos();
                    NBecas alum = new NBecas();
                    NTipo ntipo = new NTipo();
                    List<Alumnos> _listAlumnos = alumno.ConsultarTodos(token);
                    List<Becas> _listBecas = alum.ConsultarTodos(token);
                    List<TipoBecas> _listTipo = ntipo.ConsultarTodos(token);
                    ViewBag.ListAlumnos = _listAlumnos;
                    ViewBag.Becas = _listBecas;
                    ViewBag.Tipo = _listTipo;

                    return View();
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: BecasAlumnos/Edit/5
        public ActionResult Edit(int id, string tokenn)
        {
            string token = (string)Session["token"];
            NAlumnos alumno = new NAlumnos();
            NBecas alum = new NBecas();
            NTipo ntipo = new NTipo();
            List<Alumnos> _listAlumnos = alumno.ConsultarTodos(token);
            List<Becas> _listBecas = alum.ConsultarTodos(token);
            List<TipoBecas> _listTipo = ntipo.ConsultarTodos(token);
            var detalles = _becas = _metodos.Consultar(id, token);
            ViewBag.Detalles = detalles;
            ViewBag.ListAlumnos = _listAlumnos;
            ViewBag.Becas = _listBecas;
            ViewBag.Tipo = _listTipo;
       
            tokenn = token;
           _metodos.Consultar(id, tokenn);
            return View(_becas);
        }

        // POST: BecasAlumnos/Edit/5
        [HttpPost]
        public ActionResult Edit(BecasAlumnos becas,string tokenn)
        {
            try
            {

                string token = (string)Session["token"];
                // TODO: Add update logic here
                NAlumnos alumno = new NAlumnos();
                NBecas alum = new NBecas();
                NTipo ntipo = new NTipo();
                List<Alumnos> _listAlumnos = alumno.ConsultarTodos(token);
                List<Becas> _listBecas = alum.ConsultarTodos(token);
                List<TipoBecas> _listTipo = ntipo.ConsultarTodos(token);
                ViewBag.ListAlumnos = _listAlumnos;
                ViewBag.Becas = _listBecas;
                ViewBag.Tipo = _listTipo;
         

                tokenn = token;
                 _metodos.Actualizar(becas,tokenn);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BecasAlumnos/Delete/5
        public ActionResult Delete(int id)
        {
            BecasAlumnos ba = new BecasAlumnos();
            string token = (string)Session["token"];
            ba = _metodos.Consultar(id,token);

            return View(ba);
        }

        // POST: BecasAlumnos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                string token = (string)Session["token"];
                // TODO: Add delete logic here
                _metodos.Eliminar(id,token);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
