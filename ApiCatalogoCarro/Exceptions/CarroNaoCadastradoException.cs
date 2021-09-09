using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoCarro.Exceptions
{
    public class CarroNaoCadastradoException : Exception
    {
        public CarroNaoCadastradoException()
            : base("Este carro não está cadastrado")
        { }
    }
}
