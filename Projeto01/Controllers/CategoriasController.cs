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
    public class CategoriasController : Controller
    {
        private EFContext context = new EFContext();

        // GET: Categorias
        public ActionResult Index()
        {
            return View(context.Categorias);
        }

        // GET: Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categoria categoria)
        {
            context.Categorias.Add(categoria);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Categoria categoria = context.Categorias.Find(id);

            if (categoria == null)
                return HttpNotFound();

            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                context.Entry(categoria).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoria);
        }

        public ActionResult Details(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Categoria categoria = context.Categorias.Find(id);

            if (categoria == null)
                return HttpNotFound();

            return View(categoria);
        }

        public ActionResult Delete(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Categoria categoria = context.Categorias.Find(id);

            if (categoria == null)
                return HttpNotFound();

            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            Categoria categoria = context.Categorias.Find(id);
            context.Categorias.Remove(categoria);
            context.SaveChanges();
            TempData["msg"] = $"Categoria {categoria.Nome.ToUpper()} excluída!";
            return RedirectToAction("Index");
        }
    }
}