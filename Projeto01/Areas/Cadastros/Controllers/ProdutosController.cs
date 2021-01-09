using Servico.Cadastros;
using Modelo.Cadastros;
using System.Net;
using System.Web.Mvc;
using Servico.Tabelas;
using System.Web;
using System;
using System.IO;

namespace Projeto01.Areas.Cadastros.Controllers
{
    public class ProdutosController : Controller
    {
        private ProdutoServico produtoServico = new ProdutoServico();
        private CategoriaServico categoriaServico = new CategoriaServico();
        private FabricanteServico fabricanteServico = new FabricanteServico();

        // GET: Produtos
        public ActionResult Index()
        {
            return View(produtoServico.ObterProdutosClassificadosPorNome());
        }

        // GET: Produtos/Details/5
        public ActionResult Details(long? id)
        {
            return ObterVisaoProdutoPorId(id);
        }

        // GET: Produtos/Create
        public ActionResult Create()
        {
            PopularViewBag();
            return View();
        }

        // POST: Produtos/Create
        [HttpPost]
        public ActionResult Create(Produto produto, HttpPostedFileBase file = null)
        {
            return GravarProduto(produto, file, null);
        }

        // GET: Produtos/Edit/5
        public ActionResult Edit(long? id)
        {
            PopularViewBag(produtoServico.ObterProdutoPorId((long)id));
            return ObterVisaoProdutoPorId(id);
        }

        // POST: Produtos/Edit/5
        [HttpPost]
        public ActionResult Edit(Produto produto, HttpPostedFileBase file = null, string chkRemover = null)
        {
            return GravarProduto(produto, file, chkRemover);
        }

        // GET: Produtos/Delete/5
        public ActionResult Delete(long? id)
        {
            return ObterVisaoProdutoPorId(id);
        }

        // POST: Produtos/Delete/5
        [HttpPost]
        public ActionResult Delete(long id)
        {
            try
            {
                Produto produto = produtoServico.EliminarProdutoPorId(id);
                TempData["msg"] = $"Produto {produto.Nome.ToUpper()} for excluído";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private ActionResult ObterVisaoProdutoPorId(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Produto produto = produtoServico.ObterProdutoPorId((long)id);

            if (produto == null)
            {
                return HttpNotFound();
            }

            return View(produto);
        }

        private void PopularViewBag(Produto produto = null)
        {
            if (produto == null)
            {
                ViewBag.CategoriaId = new SelectList(categoriaServico.ObterCategoriasClassificadasPorNome(), "CategoriaId", "Nome");
                ViewBag.FabricanteId = new SelectList(fabricanteServico.ObterFabricantesPorNome(), "FabricanteId", "Nome");
            }
            else
            {
                ViewBag.CategoriaId = new SelectList(categoriaServico.ObterCategoriasClassificadasPorNome(), "CategoriaId", "Nome", produto.CategoriaId);
                ViewBag.FabricanteId = new SelectList(fabricanteServico.ObterFabricantesPorNome(), "FabricanteId", "Nome", produto.FabricanteId);
            }
        }

        private ActionResult GravarProduto(Produto produto, HttpPostedFileBase file, string chkRemover)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (chkRemover != null)
                        produto.Arquivo = null;

                    if (file != null)
                    {
                        produto.NomeArquivo = file.FileName;
                        produto.TamanhoArquivo = file.ContentLength;
                        produto.TipoArquivo = file.ContentType;
                        produto.Arquivo = SetFileContent(file);
                    }
                    produtoServico.GravarProduto(produto);
                    return RedirectToAction("Index");
                }
                PopularViewBag(produto);
                return View(produto);
            }
            catch
            {
                PopularViewBag(produto);
                return View(produto);
            }
        }

        private byte[] SetFileContent(HttpPostedFileBase file)
        {
            var bytes = new byte[file.ContentLength];
            file.InputStream.Read(bytes, 0, file.ContentLength);
            return bytes;
        }

        public FileContentResult GetFile(long id)
        {
            Produto p = produtoServico.ObterProdutoPorId(id);
            if (p != null)
                return File(p.Arquivo, p.TipoArquivo);

            return null;
        }

        public ActionResult Download(long id)
        {
            Produto p = produtoServico.ObterProdutoPorId(id);
            FileStream fl = new FileStream(Server.MapPath($"~/TempData/{p.NomeArquivo}"), FileMode.Create, FileAccess.Write);
            fl.Write(p.Arquivo, 0, Convert.ToInt32(p.TamanhoArquivo));
            fl.Close();
            return File(fl.Name, p.TipoArquivo, p.NomeArquivo);
        }
    }
}
