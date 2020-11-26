using Projeto01.Contexts;
using Projeto01.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Projeto01.Controllers
{
    public class FabricantesController : Controller
    {
        private EFContext context = new EFContext();

        // GET: Fabricantes
        public ActionResult Index()
        {
            return View(context.Fabricantes.OrderBy(f => f.Nome));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Fabricante fabricante)
        {
            context.Fabricantes.Add(fabricante);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Fabricante fabricante = context.Fabricantes.Find(id);

            if (fabricante == null)
                return HttpNotFound();

            return View(fabricante);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Fabricante fabricante)
        {
            if (ModelState.IsValid)
            {
                context.Entry(fabricante).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fabricante);
        }

        public ActionResult Details(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Fabricante fabricante = context.Fabricantes.Find(id);

            if (fabricante == null)
                return HttpNotFound();

            return View(fabricante);
        }

        public ActionResult Delete(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Fabricante fabricante = context.Fabricantes.Find(id);

            if (fabricante == null)
                return HttpNotFound();

            return View(fabricante);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            Fabricante fabricante = context.Fabricantes.Find(id);
            context.Fabricantes.Remove(fabricante);
            context.SaveChanges();
            TempData["Message"] = $"Fabricante {fabricante.Nome.ToUpper()} for excluído!";
            return RedirectToAction("Index");
        }
    }
}