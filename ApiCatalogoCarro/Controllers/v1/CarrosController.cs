using ApiCatalogoCarro.Exceptions;
using ApiCatalogoCarro.InputModel;
using ApiCatalogoCarro.Services;
using ApiCatalogoCarro.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoCarro.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CarrosController : ControllerBase
    {

        private readonly ICarroService _carroService;

public CarrosController(ICarroService carroService)
        {
            _carroService = carroService;
        }
        /// <summary>
        /// Buscar todos os carros de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possível retornar os carros sem paginação
        /// </remarks>
        /// <param name="pagina">Indica qual página está sendo consultada. Mínimo 1</param>
        /// <param name="quantidade">Indica a quantidade de reistros por página. Mínimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de carros</response>
        /// <response code="204">Caso não haja carros</response> 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarroViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        { 
            var carros = await _carroService.Obter(pagina, quantidade);

            if (carros.Count() == 0)
                return NoContent();

            return Ok(carros);
        }

        /// <summary>
        /// Buscar um carros pelo seu Id
        /// </summary>
        /// <param name="idCarro">Id do carro buscado</param>
        /// <response code="200">Retorna o carro filtrado</response>
        /// <response code="204">Caso não haja carro com este id</response>   

        [HttpGet("{idJogo:guid}")]
        public async Task<ActionResult<CarroViewModel>> Obter([FromRoute] Guid idCarro)
        {
            var carro = await _carroService.Obter(idCarro);

            if (carro == null)
                return NoContent();

            return Ok(carro);
        }

        [HttpPost]
        public async Task<ActionResult<CarroViewModel>> InserirJogo([FromBody] CarroInputModel jogoInputModel)
        {
            try
            {
                var carro = await _carroService.Inserir(jogoInputModel);

                return Ok(carro);
            }
            catch (CarroJaCadastradoException ex)
           
            {
                return UnprocessableEntity("Já existe um jogo com este nome para esta produtora");
            }
        }

       
        [HttpPut("{idJogo:guid}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid idCarro, [FromBody] CarroInputModel jogoInputModel)
        {
            try
            {
                await _carroService.Atualizar(idCarro, jogoInputModel);

                return Ok();
            }
            catch (CarroNaoCadastradoException ex)
            {
                return NotFound("Não existe este jogo");
            }
        }

        
        [HttpPatch("{idJogo:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid idCarro, [FromRoute] double preco)
        {
            try
            {
                await _carroService.Atualizar(idCarro, preco);

                return Ok();
            }
            catch (CarroNaoCadastradoException ex)
            
            {
                return NotFound("Não existe este jogo");
            }
        }


        [HttpDelete("{idJogo:guid}")]
        public async Task<ActionResult> ApagarJogo([FromRoute] Guid idJogo)
        {
            try
            {
                await _carroService.Remover(idJogo);

                return Ok();
            }
            catch (CarroNaoCadastradoException ex)
            {
                return NotFound("Não existe este jogo");
            }
        }
        [HttpDelete("idCarro:guid}")]
        public async Task<ActionResult> ApagarCarro(Guid idCarro)
        {
            return Ok();
        }


    }
}
