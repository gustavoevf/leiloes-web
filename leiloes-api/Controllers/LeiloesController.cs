using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using WebApi.Dtos;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class LeiloesController : ControllerBase
    {
        private ILeilaoService _leilaoService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public LeiloesController(
            ILeilaoService leilaoService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _leilaoService = leilaoService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [HttpPost("criar")]
        public IActionResult Register([FromBody]LeilaoDto leilaoDto)
        {
            // map dto to entity
            var leilao = _mapper.Map<Leilao>(leilaoDto);

            try 
            {
                // save 
                _leilaoService.Criar(leilao);
                return Ok(leilao);
            } 
            catch(AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult ObterTodos()
        {
            var leiloes =  _leilaoService.ObterTodos();
            var leiloesDtos = _mapper.Map<IList<LeilaoDto>>(leiloes);
            return Ok(leiloesDtos);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var leilao =  _leilaoService.ObterPorId(id);
            var leilaoDto = _mapper.Map<LeilaoDto>(leilao);
            return Ok(leilaoDto);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody]LeilaoDto leilaoDto)
        {
            // map dto to entity and set id
            var leilao = _mapper.Map<Leilao>(leilaoDto);
            leilao.Id = id;

            try 
            {
                // save 
                _leilaoService.Atualizar(leilao);
                return Ok();
            } 
            catch(AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _leilaoService.Deletar(id);
            return Ok();
        }
    }
}
