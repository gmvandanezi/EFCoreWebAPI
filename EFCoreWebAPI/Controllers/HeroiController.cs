using EFCore.Domain;
using EFCore.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace EFCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroiController : ControllerBase
    {
        private readonly IEFCoreRepository _repo;

        public HeroiController(IEFCoreRepository repo)
        {
            _repo = repo;
        }

        // GET: api/<HeroiController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var herois = await _repo.GetAllHerois();
                return Ok("Sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            
        }

        // GET api/<HeroiController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var heroi = await _repo.GetHeroiById(id,true);

                return Ok(heroi);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // POST api/<HeroiController>
        [HttpPost]
        public async Task<IActionResult> Post(Heroi model)
        {
            try
            {
                _repo.Add(model);
                if (await _repo.SaveChangeAsync())
                {
                    return Ok("SALVO COM SUCESSO");
                }

            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }

            return Ok("Não Salvou");
        }

        // PUT api/<HeroiController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Heroi model)
        {
            try
            {
                var heroi = await _repo.GetHeroiById(id);
                if (heroi != null)
                {
                    _repo.Update(model);
                    if(await _repo.SaveChangeAsync())
                    return Ok("Deletado");
                }

            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }

            return BadRequest("Não Salvou");
        }

        // DELETE api/<HeroiController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var heroi = await _repo.GetHeroiById(id);
                if (heroi != null)
                {
                    _repo.Delete(heroi);
                    if (await _repo.SaveChangeAsync())
                    {
                        return Ok("Deletado");
                    } 
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }

            return Ok("Não deletado");
        }
    }
}
