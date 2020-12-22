using Modelo.Cadastros;
using System.Collections.Generic;
using System.ComponentModel;

namespace Modelo.Tabelas
{
    public class Categoria
    {
        [DisplayName("Id")]
        public long? CategoriaId { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<Produto> Produtos { get; set; }
    }
}