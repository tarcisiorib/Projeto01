using Modelo.Cadastros;
using Persistencia.Contexts;
using System.Linq;
using System;
using System.Data.Entity;

namespace Persistencia.DAL.Tabelas
{
    public class FabricanteDAL
    {
        private EFContext context = new EFContext();

        public IQueryable<Fabricante> ObterFabricantesClassificadosPorNome()
        {
            return context.Fabricantes.OrderBy(f => f.Nome);
        }

        public void GravarFabricante(Fabricante fabricante)
        {
            if (fabricante.FabricanteId == null)
                context.Fabricantes.Add(fabricante);
            else
                context.Entry(fabricante).State = EntityState.Modified;

            context.SaveChanges();
        }

        public Fabricante ObterFabricantePorId(long id)
        {
            return context.Fabricantes.Where(f => f.FabricanteId == id).Include("Produtos.Categoria").First();
        }

        public Fabricante EliminarFabricantePorId(long id)
        {
            var fabricante = context.Fabricantes.Find(id);
            context.Fabricantes.Remove(fabricante);
            context.SaveChanges();
            return fabricante;
        }
    }
}
