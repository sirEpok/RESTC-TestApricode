#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApricodeTest.Models;

namespace ApricodeTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoGameController : ControllerBase
    {
        private readonly InfoGameContext _context;

        public InfoGameController(InfoGameContext context)
        {
            _context = context;
        }

        // GET: api/InfoGame
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InfoGame>>> GetInfoGame()
        {
            return await _context.InfoGames.ToListAsync();
        }

        // PUT: api/InfoGame/5
        [HttpPut("{name}")]
        public async Task<IActionResult> PutInfoGame(string name, InfoGame infoGame)
        {
            if (name != infoGame.Name)
            {
                return BadRequest();
            }

            _context.Entry(infoGame).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameInfoExists(name))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/InfoGame
        [HttpPost]
        public async Task<ActionResult<InfoGame>> PostInfoGame(InfoGame infoGame)
        {
            _context.InfoGames.Add(infoGame);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInfoGame", new { name = infoGame.Name }, infoGame);
        }

        // DELETE: api/InfoGame/5
        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteInfoGame(string name)
        {
            var infoGame = await _context.InfoGames.FindAsync(name);
            if (infoGame == null)
            {
                return NotFound();
            }

            _context.InfoGames.Remove(infoGame);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GameInfoExists(string name)
        {
            return _context.InfoGames.Any(e => e.Name.Equals(name));
        }

        // GET: api/InfoGameGenre/...
        [HttpGet("{genre}")]
        public async Task<ActionResult<IEnumerable<InfoGame>>> GetInfoGameGenre(string genre)
        {
            List<InfoGame> ig = await _context.InfoGames.ToListAsync();
            List<String> genres = null;
            List<InfoGame> infoGameGenre = null;

            foreach(InfoGame infoGame in ig)
            {
                genres = infoGame.Genres;
                foreach(string genresName in genres)
                {
                    if(genresName.Equals(genre))
                    {
                        infoGameGenre.Add(infoGame);
                    }
                }
            }

            return infoGameGenre;
        }
    }
}
