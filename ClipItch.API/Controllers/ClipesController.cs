using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ClipItch.API.Interface;
using ClipItch.API.Models;
using ClipItch.API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ClipItch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClipesController : BaseController
    {
        private readonly IClipeInterface _clipeInterface;
        private readonly IMapper _mapper;

        public ClipesController(IClipeInterface clipeInterface, IMapper mapper)
        {
            _clipeInterface = clipeInterface;
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
                return Ok(await _clipeInterface.GetClipes());
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