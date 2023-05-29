using BlazorCrud.Client.Pages;
using BlazorCrud.Server.Data;
using BlazorCrud.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;
        public SuperHeroController(DataContext context)
        {
            _context= context;
        }
        

        [HttpGet("comics")]
        public async Task<ActionResult<List<Comic>>> GetComics()
        {
            var Comics = await _context.Comics.ToListAsync();
            return Ok(Comics);
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetSuperHero()
        {
            var heroes= await _context.SuperHeroes.Include(sh=>sh.Comic).ToListAsync();
            return Ok(heroes);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<List<SuperHero>>> GetSuperHero(int id)
        {
            var hero = await _context.SuperHeroes
                .Include(c => c.Comic)
                .FirstOrDefaultAsync(h => h.Id == id);
            if (hero == null)
            {
                return NotFound("Sorry, No hero here,. :/");
            }
            return Ok(hero);
        }
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> CreateSuperHero(SuperHero hero)
        {
            hero.Comic = null;
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await GetDbHeroes());
        }
        private async Task<List<SuperHero>> GetDbHeroes()
        {
            return await _context.SuperHeroes.Include(sh => sh.Comic).ToListAsync();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<SuperHero>>> UpdateSuperHero(SuperHero hero, int id)
        {
            var dbhero = await _context.SuperHeroes
                .Include(sh => sh.Comic)
                .FirstOrDefaultAsync(sh => sh.Id == id);
            if (dbhero==null)
                   return NotFound("Sorry , But no hero for you.");

            dbhero.FirstName=hero.FirstName;
            dbhero.LastName =hero.LastName; 
            dbhero.HeroName =hero.HeroName;
            dbhero.ComicId = hero.ComicId;


            await _context.SaveChangesAsync();
            return Ok(await GetDbHeroes());
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteSuperHero(SuperHero hero, int id)
        {
            var dbhero = await _context.SuperHeroes
                .Include(sh => sh.Comic)
                .FirstOrDefaultAsync(sh => sh.Id == id);
            if (dbhero == null)
                return NotFound("Sorry , But no hero for you.");

            _context.SuperHeroes.Remove(dbhero);

            await _context.SaveChangesAsync();
            return Ok(await GetDbHeroes());
        }



    }

}
