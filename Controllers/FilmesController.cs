
using DOTNET_DAPPER.Models;
using DOTNET_DAPPER.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DOTNET_DAPPER.Controllers;

        [ApiController]
        [Route("api/[controller]")]
        public class FilmesController : ControllerBase      
    {
        private readonly IFilmeRepository _repository;
          public FilmesController(IFilmeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var filmes = await _repository.BuscaFilmesAsync();

            return filmes.Any() ? Ok(filmes) : NoContent();
        }

         [HttpGet("id")]
        public async Task<IActionResult> Get(int id)
        {
            var filme = await _repository.BuscaFilmeAsync(id);

            return filme != null
                ? Ok(filme)
                : NotFound("filme não encontrado");
        }

        [HttpPost]
        public async Task<IActionResult> Post(FilmeRequest request)
        {
            if (string.IsNullOrEmpty(request.Nome) || request.Ano <= 0 || request.ProdutoraId <= 0)
            {
                return BadRequest("Informações inválidas");
            }

            var adicionado = await _repository.AdicionaAsync(request);

            return adicionado ? Ok("Flme Adicionado com sucesso")
            : BadRequest("Erro ao adicionar filme");
        }

    }
