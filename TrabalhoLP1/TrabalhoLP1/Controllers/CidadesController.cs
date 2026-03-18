using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrabalhoLP1.Entidades;
using TrabalhoLP1.Services;

namespace TrabalhoLP1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CidadesController : ControllerBase
    {
        private readonly CidadeService _services;
        public CidadesController(CidadeService cidadeService)=>
            _services = cidadeService;
        [HttpPost("importar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Importar(IFormFile arquivo)
        {
            try
            {
                if (arquivo == null || arquivo.Length == 0)
                {
                    return BadRequest("Arquivo inválido.");
                }

                var ok = _services.Importar(arquivo);

                if (ok)
                {
                    return Ok("Importação realizada com sucesso.");
                }

                return BadRequest("Não foi possível importar o arquivo.");
            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Erro inesperado",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ObterTodos()
        {
            try
            {
                var cidades = _services.ObterTodos();
                return Ok(cidades);
            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Erro inesperado",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
        }

        [HttpGet("total")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ObterTotal()
        {
            try
            {
                return Ok(_services.TotalCidades());
            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Erro inesperado",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Obter(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Id inválido.");
                }

                var cidade = _services.Obter(id);

                if (cidade == null)
                {
                    return NotFound("Cidade não encontrada.");
                }

                return Ok(cidade);
            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Erro inesperado",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
        }

        [HttpGet("estados")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ObterEstados()
        {
            try
            {
                var estados = _services.ObterEstados();
                return Ok(estados);
            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Erro inesperado",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
        }

        [HttpGet("estado/{uf}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ObterPorEstado(string uf)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(uf))
                {
                    return BadRequest("UF inválida.");
                }

                var cidades = _services.ObterPorUF(uf);
                return Ok(cidades);
            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Erro inesperado",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Alterar(int id, Cidade request)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Id inválido.");
                }

                if (_services.CidadeExistente(id) == false)
                {
                    return NotFound("Cidade não encontrada.");
                }

                request.CidadeId = id;

                _services.Alterar(request);

                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Erro inesperado",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Excluir(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Id inválido.");
                }

                if (_services.CidadeExistente(id) == false)
                {
                    return NotFound("Cidade não encontrada.");
                }

                _services.Excluir(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Erro inesperado",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
        }
    }
}
