using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Produces("application/json")] Ao habilitar essa anotação, o Swagger UI otimiza o JSON e remove a formatação.
    public class ClipsController : Controller
    {
        private readonly IClipsService _clipsService;

        public ClipsController(IClipsService clipsService)
        {
            _clipsService = clipsService;
        }

        /// <summary>
        /// Busca a lista completa de clips.
        /// A lista é ordenada de acordo com o número de visualizações.
        /// </summary>
        /// <returns>JSON com todos os clips.</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET api/v1/clips
        ///     
        /// </remarks>
        /// <response code="200">JSON retornado com sucesso.</response>
        /// <response code="400">Erro no cliente.</response>
        /// <response code="404">Lista de clips vazia.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllClips()
        {
            try
            {
                var clipsList = await _clipsService.GetClipsAsync(null);

                if (clipsList is null) return NotFound("Lista de clips vazia.");

                return Ok(clipsList);
            }

            catch (Exception)
            {
                return BadRequest("Erro na requisição.");
            }
        }

        /// <summary>
        /// Busca a lista de clips.
        /// A quantidade de clips deve ser passada na rota da requisição.
        /// A lista é ordenada de acordo com o número de visualizações.
        /// </summary>
        /// <param name="quantity">Quantidade de clips a serem retornados.</param>
        /// <returns>JSON com a quantidade de clips solicitada.</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET api/v1/clips/10
        ///     
        /// </remarks>
        /// <response code="200">JSON retornado com sucesso.</response>
        /// <response code="400">Erro no cliente.</response>
        /// <response code="404">Lista de clips vazia.</response>
        [HttpGet("{quantity:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetClips([FromRoute] int quantity)
        {
            try
            {
                var clipsList = await _clipsService.GetClipsAsync(quantity);

                if (clipsList is null) return NotFound("Lista de clips vazia.");

                return Ok(clipsList);
            }

            catch (Exception)
            {
                return BadRequest("Erro na requisição.");
            }
        }

        /// <summary>
        /// Busca a lista clips de um determinado jogo.
        /// O Id do jogo deve ser passado na rota da requisição.
        /// A lista é ordenada de acordo com o número de visualizações.
        /// </summary>
        /// <param name="id">Id do jogo</param>
        /// <returns>JSON com a lista de clips de um determinado jogo.</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET api/v1/clips/game/509658
        ///     
        /// </remarks>
        /// <response code="200">JSON retornado com sucesso.</response>
        /// <response code="400">Erro no cliente.</response>
        /// <response code="404">Lista de clips vazia.</response>
        [HttpGet("game/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllClipsByGameId([FromRoute] string id)
        {
            try
            {
                var clipsList = await _clipsService.GetClipsByGameIdAsync(id, null);

                if (clipsList is null) return NotFound("Lista de clips vazia.");

                return Ok(clipsList);
            }

            catch (Exception)
            {
                return BadRequest("Erro na requisição.");
            }
        }

        /// <summary>
        /// Busca a lista de clips de um determinado jogo.
        /// O Id do jogo e a quantidade de clips devem ser passados na rota da requisição.
        /// A lista é ordenada de acordo com o número de visualizações.
        /// </summary>
        /// <param name="id">Id do jogo</param>
        /// <param name="quantity">Quantidade de clips a serem retornados.</param>
        /// <returns>JSON com uma lista de clips com quantidade definida de um determinado jogo.</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET api/v1/clips/game/509658/10
        ///     
        /// </remarks>
        /// <response code="200">JSON retornado com sucesso.</response>
        /// <response code="400">Erro no cliente.</response>
        /// <response code="404">Lista de clips vazia.</response>
        [HttpGet("game/{id}/{quantity:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetClipsByGameId([FromRoute] string id, int quantity)
        {
            try
            {
                var clipsList = await _clipsService.GetClipsByGameIdAsync(id, quantity);

                if (clipsList is null) return NotFound("Lista de clips vazia.");

                return Ok(clipsList);
            }

            catch (Exception)
            {
                return BadRequest("Erro na requisição.");
            }
        }

        /// <summary>
        /// Busca a lista de clips mais visualizados do dia.
        /// A lista é ordenada de acordo com o número de visualizações.
        /// </summary>
        /// <returns>JSON com a lista de clips mais visualizados do dia.</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET api/v1/clips/daily
        ///     
        /// </remarks>
        /// <response code="200">JSON retornado com sucesso.</response>
        /// <response code="400">Erro no cliente.</response>
        /// <response code="404">Lista de clips vazia.</response>
        [HttpGet("daily")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllDailyClips()
        {
            try
            {
                var clipsList = await _clipsService.GetDailyClipsAsync(null);

                if (clipsList is null) return NotFound("Lista de clips vazia.");

                return Ok(clipsList);
            }

            catch (Exception)
            {
                return BadRequest("Erro na requisição.");
            }
        }

        /// <summary>
        /// Busca a lista de clips mais visualizados do dia.
        /// A quantidade de clips deve ser passada na rota da requisição.
        /// A lista é ordenada de acordo com o número de visualizações.
        /// </summary>
        /// <param name="quantity">Quantidade de clips a serem retornados.</param>
        /// <returns>JSON com a lista de clips mais visualizados do dia, com quantidade definida. </returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET api/v1/clips/daily/10
        ///     
        /// </remarks>
        /// <response code="200">JSON retornado com sucesso.</response>
        /// <response code="400">Erro no cliente.</response>
        /// <response code="404">Lista de clips vazia.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("daily/{quantity:int}")]
        public async Task<IActionResult> GetDailyClips([FromRoute] int quantity)
        {
            try
            {
                var clipsList = await _clipsService.GetDailyClipsAsync(quantity);

                if (clipsList is null) return NotFound("Lista de clips vazia.");

                return Ok(clipsList);
            }

            catch (Exception)
            {
                return BadRequest("Erro na requisição.");
            }
        }

        /// <summary>
        /// Busca a lista de clips mais visualizados da semana.
        /// A lista é ordenada de acordo com o número de visualizações.
        /// </summary>
        /// <returns>JSON com a lista de clips mais visualizados da semana.</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET api/v1/clips/weekly
        ///     
        /// </remarks>
        /// <response code="200">JSON retornado com sucesso.</response>
        /// <response code="400">Erro no cliente.</response>
        /// <response code="404">Lista de clips vazia.</response>
        [HttpGet("weekly")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllWeeklyClips()
        {
            try
            {
                var clipsList = await _clipsService.GetWeeklyClipsAsync(null);

                if (clipsList is null) return NotFound("Lista de clips vazia.");

                return Ok(clipsList);
            }

            catch (Exception)
            {
                return BadRequest("Erro na requisição.");
            }
        }

        /// <summary>
        /// Busca a lista de clips mais visualizados da semana.
        /// A quantidade de clips deve ser passada na rota da requisição.
        /// A lista é ordenada de acordo com o número de visualizações.
        /// </summary>
        /// <param name="quantity">Quantidade de clips a serem retornados.</param>
        /// <returns>JSON com a lista de clips mais visualizados da semana, com quantidade definida. </returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET api/v1/clips/weekly/10
        ///     
        /// </remarks>
        /// <response code="200">JSON retornado com sucesso.</response>
        /// <response code="400">Erro no cliente.</response>
        /// <response code="404">Lista de clips vazia.</response>
        [HttpGet("weekly/{quantity:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetWeeklyClips([FromRoute] int quantity)
        {
            try
            {
                var clipsList = await _clipsService.GetWeeklyClipsAsync(quantity);

                if (clipsList is null) return NotFound("Lista de clips vazia.");

                return Ok(clipsList);
            }

            catch (Exception)
            {
                return BadRequest("Erro na requisição.");
            }
        }

        /// <summary>
        /// Busca o clipe pelo id.
        /// O id do clip deve ser passado na requisição.
        /// </summary>
        /// <param name="idClip">Id do clipe.</param>
        /// <returns>JSON com o clipe solicitado.</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET api/v1/clips/TacitAmericanJaguarTooSpicy-Xy5mBaVC8k8VEFEG
        ///     
        /// </remarks>
        /// <response code="200">JSON retornado com sucesso.</response>
        /// <response code="400">Erro no cliente.</response>
        /// <response code="404">Retorno vazio.</response>
        [HttpGet("{idClip:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetClipById([FromRoute] string idClip)
        {
            try
            {
                var clip = await _clipsService.GetClipById(idClip);

                if (clip is null) return NotFound("Retorno sem dados, favor reavaliar os inputs fornecidos.");

                return Ok(clip);
            }

            catch (Exception)
            {
                return BadRequest("Erro na requisição.");
            }
        }
    }
}