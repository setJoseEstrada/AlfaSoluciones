using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Entidades;
using Negocio;
using Newtonsoft.Json;

namespace Presentacion.Controllers
{
    public class LoginController : Controller
    {
        

        public ActionResult Index() { return View(); }
        // GET: Login

        NLogin enter = new NLogin();
        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            string correo = Convert.ToString(form["correo"]);
            string contrasena = Convert.ToString(form["contrasena"]);
            Login login = new Login();
            login.contrasena = contrasena;
            login.correo = correo;
            Respuesta res = enter.Login(login);
            if(res.Exito==1)
            {

                Session["token"] = res.Data.token;

                return RedirectToAction("Index", "BecasAlumnos");
            }
            else
            {
                res.Exito = 0;
                res.Mensaje = "Usuario o Contraseña Incorrecta";
            }

            
                return RedirectToAction("Index", "Login");
           
        }

    }
}