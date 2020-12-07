using Modelo.Tabelas;
using Persistencia.Contexts;
using System.Linq;
using System;
using System.Data.Entity;

namespace Persistencia.DAL.Tabelas
{
    public class CategoriaDAL
    {
        private EFContext context = new EFContext();

        public IQueryable<Categoria> ObterCategoriasClassificadasPorNome()
        {
            return context.Categorias.OrderBy(c => c.Nome);
        }

        public void GravarCategoria(Categoria categoria)
        {
            if (categoria.CategoriaId == null)
                context.Categorias.Add(categoria);
            else
                context.Entry(categoria).State = EntityState.Modified;

            context.SaveChanges();
        }

        public Categoria ObterCategoriaPorId(long id)
        {
            return context.Categorias.Where(c => c.CategoriaId == id).Include(p => p.Produtos).First();
        }

        public Categoria EliminarCategoriaPorId(long id)
        {
            var categoria = ObterCategoriaPorId(id);
            context.Categorias.Remove(categoria);
            context.SaveChanges();
            return categoria;
        }
    }
}
