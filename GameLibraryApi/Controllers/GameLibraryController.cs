using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GameLibraryApi.Models;
using GameLibraryApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GameLibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameLibraryController : ControllerBase
    {
        private readonly ApiContext _context;
        public GameLibraryController(ApiContext context)
        {
            _context = context;
        }

        [HttpPost("CreateEdit")]
        public JsonResult CreateEdit(GameLibrary game)
        {
            if (game.Id == 0)
            {
                _context.GameLibraries.Add(game);
            }
            else
            {
                var libraryInDb = _context.GameLibraries.Find(game.Id);

                if (libraryInDb == null)
                    return new JsonResult(NotFound());

                libraryInDb = game;
            }

            _context.SaveChanges();

            return new JsonResult(Ok(game));
        }

        [HttpGet("GetId")]
        public JsonResult Get(int id)
        {
            var result = _context.GameLibraries.Include(library => library.Genres).SingleOrDefault(library => library.Id == id);
            if (result == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(result));
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var result = _context.GameLibraries.Find(id);

            if (result == null)
                return new JsonResult(NotFound());

            _context.GameLibraries.Remove(result);
            _context.SaveChanges();

            return new JsonResult(NoContent());
        }

        [HttpGet("AllGetGames")]
        public JsonResult AllGetGames()
        {
            var result = _context.GameLibraries.Include(library => library.Genres).ToList();

            return new JsonResult(Ok(result));
        }

        [HttpGet("GetGamesGenre")]
        public JsonResult GetGamesGenre(string genre)
        {
            var games = _context.GameLibraries
            .Include(library => library.Genres)
            .Where(game => game.Genres.Any(gen => gen.Name == genre))
            .ToList();

            return new JsonResult(Ok(games));
        }
    }
}
