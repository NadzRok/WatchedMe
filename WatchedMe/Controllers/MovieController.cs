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
        public IActionResult GetMovies() {
            return Ok(new MovieReturnMessage() { IsError = false, Message = "", MovieList = _DbContext.Movies.ToList() });
        }

        // GET: api/movie/getamovie?movieid={MovieId}
        [HttpGet("getamovie")]
        public async Task<IActionResult> GetMovie(Guid MovieId) {
            var movieReturn = await _DbContext.Movies.FirstOrDefaultAsync(mr => mr.Id == MovieId);
            var result = new MovieReturnMessage();
            if (movieReturn != null) {
                result = new MovieReturnMessage() {
                    IsError = false,
                    Message = "",
                    Movie = movieReturn
                };
            } else {
                result = new MovieReturnMessage() {
                    IsError = true,
                    Message = "Movie Not Found."
                };
            }
            return Ok(result);
        }

        // POST: api/movie/addmovie
        [HttpPost("addmovie")]
        public async Task<IActionResult> PostMovie([FromBody]Movie MovieToAdd) {
            if (MovieToAdd == null) { 
                return BadRequest(new MovieReturnMessage() { IsError = true, Message = "No data to add."});
            }
            var movieCheck = await _DbContext.Movies.FirstOrDefaultAsync(mc => mc.Title == MovieToAdd.Title);
            if (movieCheck != null) {
                if (movieCheck.Active == false) {
                    movieCheck.Active = true;
                    await _DbContext.SaveChangesAsync();
                    return Ok(new MovieReturnMessage() { IsError = false, Message = "", Movie = MovieToAdd });
                }
                return Ok(new MovieReturnMessage() { IsError = true, Message = "Movie has already been added." });
            }
            MovieToAdd.Id = Guid.NewGuid();
            MovieToAdd.Active = true;
            try {
                await _DbContext.Movies.AddAsync(MovieToAdd);
                await _DbContext.SaveChangesAsync();
                return Ok(new MovieReturnMessage() { IsError = false, Message = "", Movie = MovieToAdd });
            } catch {
                return BadRequest(new MovieReturnMessage() { IsError = true, Message = "Failed to add movie." });
            }
        }

        // PUT: api/movie/updatemovie
        [HttpPut("updatemovie")]
        public async Task<IActionResult> PutMovie([FromBody] Movie MovieToUpdate) {
            if(MovieToUpdate == null) {
                return BadRequest(new MovieReturnMessage() { IsError = true, Message = "No data to update."});
            }
            var movieUpdate = await _DbContext.Movies.FirstOrDefaultAsync(mu => mu.Id == MovieToUpdate.Id);
            if (movieUpdate == null) {
                return Ok(new MovieReturnMessage() { IsError = true, Message = "Movie does not exist." });
            }
            movieUpdate = MovieToUpdate;
            movieUpdate.Active = true;
            try {
                await _DbContext.SaveChangesAsync();
                return Ok(new MovieReturnMessage() { IsError = false, Message = "", Movie = movieUpdate });
            } catch {
                return BadRequest(new MovieReturnMessage() { IsError = true, Message = "Failed to add movie." });
            }
        }

        // DELETE: api/movie/deletemovie?movieid={MovieId}
        [HttpDelete("deletemovie")]
        public async Task<IActionResult> DeleteMovie(Guid MovieId) {
            if (MovieId == Guid.Empty) {
                return BadRequest(new MovieReturnMessage() { IsError = true, Message = "No data to update." });
            }
            var movieDelete = _DbContext.Movies.FirstOrDefault(mu => mu.Id == MovieId);
            if(movieDelete == null) {
                return Ok(new MovieReturnMessage() { IsError = true, Message = "Movie does not exist." });
            }
            movieDelete.Active = false;
            try { 
                await _DbContext.SaveChangesAsync();
                return Ok(new MovieReturnMessage() { IsError = false, Message = "", Movie = movieDelete });
            } catch {
                return BadRequest(new MovieReturnMessage() { IsError = true, Message = "Failed to remove movie." });
            }
        }
    }
}
