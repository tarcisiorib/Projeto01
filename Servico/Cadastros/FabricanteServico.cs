using Modelo.Cadastros;
using Persistencia.DAL.Tabelas;
using System.Linq;
using System;

namespace Servico.Cadastros
{
    public class FabricanteServico
    {
        private FabricanteDAL fabricanteDAL = new FabricanteDAL();

        public IQueryable<Fabricante> ObterFabricantesPorNome()
        {
            return fabricanteDAL.ObterFabricantesClassificadosPorNome();
        }

        public void GravarFabricante(Fabricante fabricante)
        {
            fabricanteDAL.GravarFabricante(fabricante);
        }

        public Fabricante ObterFabricantePorId(long id)
        {
            return fabricanteDAL.ObterFabricantePorId(id);
        }

        public Fabricante EliminarFabricantePorId(long id)
        {
            return fabricanteDAL.EliminarFabricantePorId(id);
        }
    }
}
