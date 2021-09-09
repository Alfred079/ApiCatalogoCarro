using ApiCatalogoCarro.Entities;
using ApiCatalogoCarro.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoCarro.Repositories
{
    public class CarroRepository : ICarrosRepository
    {
        private static Dictionary<Guid, Carro> carros = new Dictionary<Guid, Carro>()
        {
            {Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), new Carro{ Id = Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), Nome = "Hilux", Fabricante = "Toyota", Preco = 200} },
            {Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), new Carro{ Id = Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), Nome = "Racts", Fabricante = "Toyota", Preco = 190} },
            {Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), new Carro{ Id = Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), Nome = "RunX", Fabricante = "Toyota", Preco = 180} },
            {Guid.Parse("da033439-f352-4539-879f-515759312d53"), new Carro{ Id = Guid.Parse("da033439-f352-4539-879f-515759312d53"), Nome = "Tiguan", Fabricante = "Volkswagen", Preco = 170} },
            {Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), new Carro{ Id = Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), Nome = "Triton", Fabricante = "Mitsubishi", Preco = 80} },
            {Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), new Carro{ Id = Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), Nome = "A4", Fabricante = "Audi", Preco = 190} }
        };

        public Task<List<Carro>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(carros.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<Carro> Obter(Guid id)
        {
            if (!carros.ContainsKey(id))
                return Task.FromResult<Carro>(null);

            return Task.FromResult(carros[id]);
        }

        public Task<List<Carro>> Obter(string nome, string fabricante)
        {
            return Task.FromResult(carros.Values.Where(carro => carro.Nome.Equals(nome) && carro.Fabricante.Equals(fabricante)).ToList());
        }

        public Task<List<Carro>> ObterSemLambda(string nome, string fabricante)
        {
            var retorno = new List<Carro>();

            foreach (var carro in carros.Values)
            {
                if (carro.Nome.Equals(nome) && carro.Fabricante.Equals(fabricante))
                    retorno.Add(carro);
            }

            return Task.FromResult(retorno);
        }

        public Task Inserir(Carro carro)
        {
            carros.Add(carro.Id, carro);
            return Task.CompletedTask;
        }

        public Task Atualizar(Carro carro)
        {
            carros[carro.Id] = carro;
            return Task.CompletedTask;
        }

        public Task Remover(Guid id)
        {
            carros.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            //Fechar conexão com o banco
        }
    }
}
