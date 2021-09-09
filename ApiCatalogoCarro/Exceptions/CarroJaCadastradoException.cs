using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoCarro.Exceptions
{
    public class CarroJaCadastradoException : Exception
    {
        public CarroJaCadastradoException()
            : base("Este carro já está cadastrado")
        { }
    }
}
