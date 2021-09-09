using ApiCatalogoCarro.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoCarro.Repositorio
{
    public interface ICarrosRepository : IDisposable
    {
        Task<List<Carro>> Obter(int pagina, int quantidade);
        Task<Carro> Obter(Guid id);
        Task<List<Carro>> Obter(string nome, string produtora);
        Task Inserir(Carro carro);
        Task Atualizar(Carro carro);
        Task Remover(Guid id);
    }
}
