using ApiCatalogoCarro.InputModel;
using ApiCatalogoCarro.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoCarro.Services
{
    public interface ICarroService : IDisposable
    {
        Task<List<CarroViewModel>> Obter(int pagina, int quantidade);
        Task<CarroViewModel> Obter(Guid id);
        Task<CarroViewModel> Inserir(CarroInputModel carro);
        Task Atualizar(Guid id, CarroInputModel carro);
        Task Atualizar(Guid id, double preco);
        Task Remover(Guid id);
    }
}
