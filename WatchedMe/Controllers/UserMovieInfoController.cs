namespace WatchedMe.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UserMovieInfoController : ControllerBase {
        private readonly ApplicationDbContext _DbContext;

        public UserMovieInfoController(ApplicationDbContext DbContext) {
            _DbContext = DbContext;
        }

        // GET: api/UserMovieInfo/getallusermovies?userid={UserId}
        [HttpGet("getallusermovies")]
        public async Task<ActionResult<IEnumerable<UserMovieInfo>>> GetAllUserInfo(Guid UserId) {
            if(UserId == Guid.Empty) {
                return NotFound();
            }
            var usersMovies = await _DbContext.UsersMovieInfo.Where(um => um.UserId == UserId && um.Active != false).ToListAsync();
            if(usersMovies != null) {
                return usersMovies;
            } else {
                return NotFound();
            }
        }

        // GET: api/UserMovieInfo/getusermovie?userid={UserId}?movieid={MovieId}
        [HttpGet("getusermovie")]
        public async Task<ActionResult<UserMovieInfo>> GetUserInfo(Guid UserId, Guid MovieId) {
            if(UserId == Guid.Empty || MovieId == Guid.Empty) {
                return NotFound();
            }
            var userMovieReturn = await _DbContext.UsersMovieInfo.FirstOrDefaultAsync(umr => umr.UserId == UserId && umr.MovieId == MovieId && umr.Active != false);
            if(userMovieReturn != null) {
                return userMovieReturn;
            } else {
                return NotFound();
            }
        }

        // POST: api/UserMovieInfo/addmovie
        [HttpPost("addmovie")]
        public async Task<ActionResult<UserMovieInfo>> PostUserInfo([FromBody] UserMovieInfo UserInfoToAdd) {
            if(UserInfoToAdd == null) {
                return NotFound();
            }
            UserInfoToAdd.Id = Guid.NewGuid();
            UserInfoToAdd.Created = DateTime.Now;
            UserInfoToAdd.ModifideDate = DateTime.Now;
            UserInfoToAdd.Active = true;
            _DbContext.UsersMovieInfo.Add(UserInfoToAdd);
            await _DbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserInfo), UserInfoToAdd);
        }

        // PUT: api/UserMovieInfo/updateusermovieinfo
        [HttpPut("updateusermovieinfo")]
        public async Task<ActionResult> PutUserInfo([FromBody] UserMovieInfo UserInfoToUpdate) {
            if(UserInfoToUpdate == null) {
                return NotFound();
            }
            _DbContext.Entry(UserInfoToUpdate).State = EntityState.Modified;
            try {
                await _DbContext.SaveChangesAsync();
            } catch(DbUpdateConcurrencyException) {
                throw;
            }
            return Ok();
        }

        // DELETE: api/UserMovieInfo/deletemovie?userid={UserId}?movieid={MovieId}
        [HttpDelete("deleteusermovieinfo")]
        public async Task<ActionResult> DeleteMovie(Guid UserId, Guid MovieId) {
            if(MovieId == Guid.Empty || UserId == Guid.Empty) {
                return NotFound();
            }
            var userMovieDelete = await _DbContext.UsersMovieInfo.FirstOrDefaultAsync(umd => umd.UserId == UserId && umd.MovieId == MovieId && umd.Active != false);
            if(userMovieDelete == null) {
                return NotFound();
            }
            userMovieDelete.ModifideDate = DateTime.Now;
            userMovieDelete.Active = false;
            _DbContext.Entry(userMovieDelete).State = EntityState.Modified;
            try {
                await _DbContext.SaveChangesAsync();
            } catch(DbUpdateConcurrencyException) {
                throw;
            }
            return Ok();
        }
    }
}
