using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using WatchedMe.Models.NoDb;

namespace WatchedMe.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase {
        private readonly ApplicationDbContext _DbContext;

        public MovieController(ApplicationDbContext DbContext) {
            _DbContext = DbContext;
        }

        // GET: api/movie/getallmovies
        [HttpGet("getallmovies")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies() {
            if (_DbContext.Movies == null) {
                return NotFound();
            }
            return await _DbContext.Movies.ToListAsync();
        }

        // GET: api/movie/getamovie?movieid={MovieId}
        [HttpGet("getamovie")]
        public async Task<ActionResult<Movie>> GetMovie(Guid MovieId) {
            if(_DbContext.Movies == null) {
                return NotFound();
            }
            var movie = await _DbContext.Movies.FirstOrDefaultAsync(m => m.Id == MovieId);
            if (movie == null) {
                return NotFound();
            }
            return movie;
        }

        // POST: api/movie/addmovie
        [HttpPost("addmovie")]
        public async Task<ActionResult<Movie>> PostMovie([FromBody]Movie MovieToAdd) {
            MovieToAdd.Id = Guid.NewGuid();
            MovieToAdd.Created = DateTime.Now;
            MovieToAdd.ModifideDate = DateTime.Now;
            MovieToAdd.Active = true;
            _DbContext.Movies.Add(MovieToAdd);
            await _DbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMovie), MovieToAdd);
        }

        // PUT: api/movie/updatemovie
        [HttpPut("updatemovie")]
        public async Task<ActionResult> PutMovie([FromBody] Movie MovieToUpdate) {
            if(MovieToUpdate == null) {
                return NotFound();
            }
            _DbContext.Entry(MovieToUpdate).State = EntityState.Modified;
            try {
                await _DbContext.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                throw;
            }
            return Ok();
        }

        // DELETE: api/movie/deletemovie?movieid={MovieId}
        [HttpDelete("deletemovie")]
        public async Task<ActionResult> DeleteMovie(Guid MovieId) {
            if (MovieId == Guid.Empty) {
                return NotFound();
            }
            var movieDelete = await _DbContext.Movies.FirstOrDefaultAsync(md => md.Id == MovieId && md.Active != false);
            if(movieDelete == null) {
                return NotFound();
            }
            movieDelete.ModifideDate = DateTime.Now;
            movieDelete.Active = false;
            _DbContext.Entry(movieDelete).State = EntityState.Modified;
            try { 
                await _DbContext.SaveChangesAsync();
            } catch(DbUpdateConcurrencyException) {
                throw;
            }
            return Ok();
        }
    }
}
