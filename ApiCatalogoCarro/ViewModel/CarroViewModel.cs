using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoCarro.ViewModel
{
    public class CarroViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Fabricante { get; set; }
        public double Preco { get; set; }
    }
}
