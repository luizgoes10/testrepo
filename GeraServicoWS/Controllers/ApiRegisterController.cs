using GeraServicoWS.Models;
using GeraServicoWS.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GeraServicoWS.Controllers
{
    public class ApiRegisterController : Controller
    {
        DataImpl data = new DataImpl();

        //Get: ApiRegister
        public JsonResult Index([Bind(Include = "Login, Senha")]Owner owner)
        {
            if (owner.Login != null || owner.Senha != null)
            {
                var logOn = data.GetOwner(owner.Login);
                if (logOn.Senha == owner.Senha && logOn.Login == owner.Login)
                {
                    return Json(data.GetTableApi(owner.Login), JsonRequestBehavior.AllowGet);
                }
                return Json("Dados inválidos, por favor tente novamente.", JsonRequestBehavior.AllowGet);
            }
            return Json(new HttpStatusCodeResult(HttpStatusCode.NotFound, "Os valores do recurso estão ausentes."), JsonRequestBehavior.AllowGet);
        }

     

        // GET: ApiRegister/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ApiRegister/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ApiRegister/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                string[] col = new string[25];
                // TODO: Add insert logic here
               for(int i = 0; i < collection.Count; i++)
                {
                    col[i] = collection[i];
                }
                var login = User.Identity.Name;
                var owner = data.GetOwner(login);
                data.SaveNewApi(owner, col);
                return RedirectToAction("Index", new { login = owner.Login, senha = owner.Senha });
            }
            catch(Exception e)
            {
                return View();
            }
        }

        // GET: ApiRegister/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ApiRegister/Edit/5
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

        // GET: ApiRegister/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ApiRegister/Delete/5
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
