using System.Collections.Generic;
using System.ComponentModel;

namespace Modelo.Cadastros
{
    public class Fabricante
    {
        [DisplayName("Id")]
        public long? FabricanteId { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<Produto> Produtos { get; set; }
    }
}