using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ClipItch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClipesController : BaseController
    {
        private readonly IMapper _mapper;

        public ClipesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("inicio")]
        public IActionResult Index()
        {            
            return Ok("Teste");
        }

        [HttpGet("obterTodos")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(string.Format("Teste {0}", 1));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("adicionar")]
        public async Task<IActionResult> Insert()
        {
            try
            {
                return Ok(string.Format("Teste {0}", 1));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}