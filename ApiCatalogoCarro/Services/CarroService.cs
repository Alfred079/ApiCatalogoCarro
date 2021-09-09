using ApiCatalogoCarro.Entities;
using ApiCatalogoCarro.Exceptions;
using ApiCatalogoCarro.InputModel;
using ApiCatalogoCarro.Repositorio;
using ApiCatalogoCarro.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoCarro.Services
{
    public class CarroService : ICarroService
    {

        private readonly ICarrosRepository _carroRepository;

        public CarroService(ICarrosRepository carroRepository)
        {
            _carroRepository = carroRepository;
        }

        public async Task<List<CarroViewModel>> Obter(int pagina, int quantidade)
        {
            var carross = await _carroRepository.Obter(pagina, quantidade);

            return carross.Select(carro => new CarroViewModel
            {
                Id = carro.Id,
                Nome = carro.Nome,
                Fabricante = carro.Fabricante,
                Preco = carro.Preco
            })
                               .ToList();
        }

        public async Task<CarroViewModel> Obter(Guid id)
        {
            var carro = await _carroRepository.Obter(id);

            if (carro == null)
                return null;

            return new CarroViewModel
            {
                Id = carro.Id,
                Nome = carro.Nome,
                Fabricante = carro.Fabricante,
                Preco = carro.Preco
            };
        }

        public async Task<CarroViewModel> Inserir(CarroInputModel carro)
        {
            var entidadeCarro = await _carroRepository.Obter(carro.Nome, carro.Fabricante);

            if (entidadeCarro.Count > 0)
                throw new CarroJaCadastradoException();

            var carroInsert = new Carro
            {
                Id = Guid.NewGuid(),
                Nome = carro.Nome,
                Fabricante = carro.Fabricante,
                Preco = carro.Preco
            };

            await _carroRepository.Inserir(carroInsert);

            return new CarroViewModel
            {
                Id = carroInsert.Id,
                Nome = carro.Nome,
                Fabricante = carro.Fabricante,
                Preco = carro.Preco
            };
        }

        public async Task Atualizar(Guid id, CarroInputModel carro)
        {
            var entidadeCarro = await _carroRepository.Obter(id); 

            if (entidadeCarro == null)
                throw new CarroNaoCadastradoException();

            entidadeCarro.Nome = carro.Nome;
            entidadeCarro.Fabricante = carro.Fabricante;
            entidadeCarro.Preco = carro.Preco;

            await _carroRepository.Atualizar(entidadeCarro);
        }

        public async Task Atualizar(Guid id, double preco)
        {
            var entidadeCarro = await _carroRepository.Obter(id);

            if (entidadeCarro == null)
                throw new CarroNaoCadastradoException();

            entidadeCarro.Preco = preco;

            await _carroRepository.Atualizar(entidadeCarro);
        }

        public async Task Remover(Guid id)
        {
            var carro = await _carroRepository.Obter(id);

            if (carro == null)
                throw new CarroNaoCadastradoException();

            await _carroRepository.Remover(id);
        }

        public void Dispose()
        {
            _carroRepository?.Dispose();
        }

    }
}
