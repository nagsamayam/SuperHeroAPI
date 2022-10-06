using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Data;
using System.Xml.Linq;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {

        private static List<SuperHero> superHeros = new List<SuperHero>()
        {
             new SuperHero {
                Id = 1,
                Name = "Spider Man",
                FirstName = "Peter",
                LastName = "Parker",
                Place = "New York City"
             },
            new SuperHero
            {
                Id = 2,
                Name = "Iron Man",
                FirstName = "Tony",
                LastName = "Stark",
                Place = "London"
            },
        };

        private readonly DataContext _context;
        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _context.SuperHeros.ToListAsync());
           // return Ok(superHeros);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            //  var superHero = superHeros.Find(h => h.Id == id);
            var superHero = await _context.SuperHeros.FindAsync(id);
            if(superHero == null)
            {
                return BadRequest("Super Hero not found");
            }

            return Ok(superHero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddSuperHero(SuperHero superHero) {
            // superHeros.Add(superHero);
           // return Ok(superHeros);

            _context.SuperHeros.Add(superHero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeros.ToListAsync());
           
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SuperHero>> Put(int id, SuperHero request)
        {
            // var superHero = superHeros.Find(h => h.Id == id);
            var superHero = await _context.SuperHeros.FindAsync(id);
            if(superHero == null)
            {
                return BadRequest("Super Hero not found");
            }

            superHero.Name = request.Name;
            superHero.FirstName = request.FirstName;
            superHero.LastName = request.LastName;
            superHero.Place = request.Place;

            await _context.SaveChangesAsync();

          
            return Ok(superHero);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            // var superHero = superHeros.Find(h => h.Id == id);
            var superHero = await _context.SuperHeros.FindAsync(id);
            if(superHero == null)
            {
                return BadRequest("Super Hero not found");
            }

            // superHeros.Remove(superHero);
            _context.SuperHeros.Remove(superHero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeros.ToListAsync());

           // return Ok(superHeros);
        }
    }
}
