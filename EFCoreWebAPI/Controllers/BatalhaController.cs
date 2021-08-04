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
    public class BatalhaController : ControllerBase
    {
        private readonly IEFCoreRepository _repo;

        public BatalhaController(IEFCoreRepository repo)
        {
            _repo = repo;
        }

        // GET: api/<BatalhaController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var batalhas = await _repo.GetAllBatalhas();

                return Ok(batalhas);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro:{ex}");
            }
        }

        // GET api/<BatalhaController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var batalhas = await _repo.GetBatalhaById(id,true);

                return Ok(batalhas);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro:{ex}");
            }
        }

        // POST api/<BatalhaController>
        [HttpPost]
        public async Task<IActionResult> Post(Batalha model)
        {
            try
            {
                _repo.Add(model);

                if (await _repo.SaveChangeAsync())
                {
                    return Ok("SUCESSO");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro:{ex}");
            }

                return BadRequest("Não Salvou");
        }

        // PUT api/<BatalhaController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Batalha model)
        {
            try
            {
                var batalha = await _repo.GetBatalhaById(id);

                if (batalha != null)
                {
                    _repo.Update(model);
                    if (await _repo.SaveChangeAsync())
                    {
                        return Ok("SUCESSO");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro:{ex}");
            }

            return BadRequest("Não Salvou");
        }

        // DELETE api/<BatalhaController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var batalha = await _repo.GetBatalhaById(id);

                if (batalha != null)
                {
                    _repo.Delete(batalha);
                    if (await _repo.SaveChangeAsync())
                    {
                        return Ok("SUCESSO");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro:{ex}");
            }

            return BadRequest("Não Deletado");
        }
    }
}
