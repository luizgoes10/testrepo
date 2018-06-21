using GeraServicoWS.Models;
using GeraServicoWS.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GeraServicoWS.Controllers
{
    public class OwnerController : Controller
    {
        DataImpl data = new DataImpl();
        //Get Login
        public ActionResult LogOn()
        {
            return View();
        }
        //Post Login
        [HttpPost]
        public ActionResult LogOn([Bind(Include = "Login, Senha, Remenber")]Owner owner)
        {
            if (ModelState.IsValid)
            {
                var logon = data.GetOwner(owner.Login);
                if(logon.Senha != owner.Senha && logon.Senha != null)
                {
                    ViewBag.Error = "Senha incorreta.";
                    return View();
                }
                if(logon.Login == null)
                {
                    ViewBag.Error = "Login não existe.";
                    return View();
                }
                FormsAuthentication.SetAuthCookie(owner.Login, false);
                Session.Add("Owner", owner);
                Session.Add("Login", owner.Login);
                Session.Add("Senha", owner.Senha);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }
        // GET: Owner
        public ActionResult Index()
        {
            return View(data.GetOwners().ToList());
        }

        // GET: Owner/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Owner/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Owner/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Email, Login, Senha, Remenber")]Owner owner)
        {
            if (ModelState.IsValid)
            {
                data.SaveOwner(owner);
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Owner/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Owner/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Owner/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Owner/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
