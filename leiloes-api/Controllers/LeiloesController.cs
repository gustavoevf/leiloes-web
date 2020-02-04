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

        /// <summary>
        /// Cria um novo leilão
        /// </summary>
        /// <param name="leilao"></param>
        [HttpPost("criar")]
        public IActionResult Register([FromBody]LeilaoDto leilao)
        {
            // map dto to entity
            var leilaoEntity = _mapper.Map<Leilao>(leilao);

            try 
            {
                // save 
                _leilaoService.Criar(leilaoEntity);
                return Ok(leilaoEntity);
            } 
            catch(AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Obtém todos os leilões
        /// </summary>
        [HttpGet]
        public IActionResult ObterTodos()
        {
            var leiloes =  _leilaoService.ObterTodos();
            var leiloesDtos = _mapper.Map<IList<LeilaoDto>>(leiloes);
            return Ok(leiloesDtos);
        }

        /// <summary>
        /// Obtém o leilão por id
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var leilao =  _leilaoService.ObterPorId(id);
            var leilaoDto = _mapper.Map<LeilaoDto>(leilao);
            return Ok(leilaoDto);
        }

        /// <summary>
        /// Atualiza um leilão
        /// </summary>
        /// <param name="id"></param>
        /// <param name="leilao"></param>
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody]LeilaoDto leilao)
        {
            // map dto to entity and set id
            var leilaoEntity = _mapper.Map<Leilao>(leilao);
            leilaoEntity.Id = id;

            try 
            {
                // save 
                _leilaoService.Atualizar(leilaoEntity);
                return Ok();
            } 
            catch(AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Remove um leilão
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _leilaoService.Deletar(id);
            return Ok();
        }
    }
}
