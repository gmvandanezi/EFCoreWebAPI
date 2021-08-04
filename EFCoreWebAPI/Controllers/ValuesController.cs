using EFCore.Domain;
using EFCore.Repo;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public readonly HeroiContext _context;
        public ValuesController(HeroiContext context)
        {
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public ActionResult Get()
        {
            //var listHeroi = _context.Herois.ToList(); // LINQ METHOD
            var listHeroi = (from heroi in _context.Herois select heroi).ToList(); // LINQ SQL
            return Ok(listHeroi);
        }

        // GET api/values
        [HttpGet("filtro/{nome}")]
        public ActionResult GetFiltro(string nome)
        {
            var listHeroi = _context.Herois
                            .Where(x => x.Nome.Contains(nome))
                            .ToList(); // LINQ METHOD
            //var listHeroi = (from heroi in _context.Herois
            //                 where heroi.Nome.Contains(nome)
            //                 select heroi).ToList(); // LINQ SQL

            /*
            Não faça isso! Conexão aberta

            foreach (var item in _context.Herois)
            {
                realizeCalculo();
                criaArquivos();
                salvaRelatorio();
            }
            _____________________________________________

            Modo correto!

            foreach (var item in listHeroi)
            {
                realizeCalculo();
                criaArquivos();
                salvaRelatorio();
            }
            
            */

            return Ok(listHeroi);
        }

        // GET api/values/5
        [HttpGet("Atualizar/{nameHero}")]
        public ActionResult Get(string nameHero)
        {
            var heroi = _context.Herois
                        .Where(x => x.Id == 4)
                        .FirstOrDefault();
            heroi.Nome = "Homem Aranha";
            _context.SaveChanges();
            return Ok();
        }

        // GET api/values/5
        [HttpGet("AddRange")]
        public ActionResult GetAddRange()
        {
            _context.AddRange(
                    new Heroi { Nome = "Capitão América" },
                    new Heroi { Nome = "Doutor Estranho" },
                    new Heroi { Nome = "Pantera Negra" },
                    new Heroi { Nome = "Viúva Negra" },
                    new Heroi { Nome = "Hulk" },
                    new Heroi { Nome = "Gavião Arqueiro" },
                    new Heroi { Nome = "Capitã Marvel" }
                    );
            _context.SaveChanges();

            return Ok();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpGet("Delete/{id}")]
        public void Delete(int id)
        {
            var heroi = _context.Herois
                        .Where(x => x.Id == id)
                        .Single();
            _context.Herois.Remove(heroi);
            _context.SaveChanges();
        }
    }
}
