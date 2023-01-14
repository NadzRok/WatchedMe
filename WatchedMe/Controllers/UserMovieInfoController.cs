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
        public IActionResult GetAllUserInfo(Guid UserId) {
            return Ok(new UserMovieInfoReply() { ErrorInfo = new ErrorReply() { IsError = false, Message = "" }, UserMovieInfoList = _DbContext.UsersMovieInfo.Where(umil => umil.UserId == UserId && umil.Active != false).ToList() });
        }

        // GET: api/UserMovieInfo/getusermovie?userid={UserId}?movieid={MovieId}
        [HttpGet("getusermovie")]
        public async Task<IActionResult> GetUserInfo(Guid UserId, Guid MovieId) {
            var userMovieReturn = await _DbContext.UsersMovieInfo.FirstOrDefaultAsync(umr => umr.UserId == UserId && umr.Id == MovieId);
            var result = new UserMovieInfoReply();
            if(userMovieReturn != null) {
                result = new UserMovieInfoReply() {
                    ErrorInfo = new ErrorReply() {
                        IsError = false,
                        Message = ""
                    },
                    SingleUserMovieInfo = userMovieReturn
                };
            } else {
                result = new UserMovieInfoReply() {
                    ErrorInfo = new ErrorReply() {
                        IsError = true,
                        Message = "Movie information Not Found."
                    }
                };
            }
            return Ok(result);
        }

        // POST: api/UserMovieInfo/addmovie
        [HttpPost("addmovie")]
        public async Task<IActionResult> PostUserInfo([FromBody] UserMovieInfo UserInfoToAdd) {
            if(UserInfoToAdd == null) {
                return BadRequest(new UserMovieInfoReply() { ErrorInfo = new ErrorReply() { IsError = true, Message = "No data to add." } });
            }
            var userInfoCheck = await _DbContext.UsersMovieInfo.FirstOrDefaultAsync(uic => uic.MovieId == UserInfoToAdd.MovieId && uic.UserId == UserInfoToAdd.UserId);
            if(userInfoCheck != null) {
                if(userInfoCheck.Active == false) {
                    userInfoCheck.Active = true;
                    await _DbContext.SaveChangesAsync();
                    return Ok(new UserMovieInfoReply() { ErrorInfo = new ErrorReply() { IsError = false, Message = "" }, SingleUserMovieInfo = userInfoCheck });
                }
                return Ok(new UserMovieInfoReply() { ErrorInfo = new ErrorReply() { IsError = true, Message = "Movie has already been added to your profile." } });
            }
            UserInfoToAdd.Id = Guid.NewGuid();
            UserInfoToAdd.Created = DateTime.Now;
            UserInfoToAdd.ModifideDate = UserInfoToAdd.Created;
            UserInfoToAdd.Active = true;
            try {
                await _DbContext.AddAsync(UserInfoToAdd);
                await _DbContext.SaveChangesAsync();
                return Ok(new UserMovieInfoReply() { ErrorInfo = new ErrorReply() { IsError = false, Message = "" }, SingleUserMovieInfo = UserInfoToAdd });
            } catch {
                return BadRequest(new UserMovieInfoReply() { ErrorInfo = new ErrorReply() { IsError = true, Message = "Failed to add movie." } });
            }
        }

        // PUT: api/UserMovieInfo/updateusermovieinfo
        [HttpPut("updateusermovieinfo")]
        public async Task<IActionResult> PutUserInfo([FromBody] UserMovieInfo UserInfoToUpdate) {
            if(UserInfoToUpdate == null) {
                return BadRequest(new UserMovieInfoReply() { ErrorInfo = new ErrorReply() { IsError = true, Message = "No data to update." } });
            }
            var userInfoUpdate = await _DbContext.UsersMovieInfo.FirstOrDefaultAsync(uiu => uiu.Id == UserInfoToUpdate.Id && uiu.UserId == UserInfoToUpdate.UserId && uiu.Active != false);
            if(userInfoUpdate == null) {
                return Ok(new UserMovieInfoReply() { ErrorInfo = new ErrorReply() { IsError = true, Message = "Movie does not exist." } });
            }
            userInfoUpdate.UserId = UserInfoToUpdate.UserId;
            userInfoUpdate.MovieId = UserInfoToUpdate.MovieId;
            userInfoUpdate.UserRating= UserInfoToUpdate.UserRating;
            userInfoUpdate.Created = UserInfoToUpdate.Created;
            userInfoUpdate.ModifideDate = DateTime.Now;
            userInfoUpdate.Notes = UserInfoToUpdate.Notes;
            userInfoUpdate.Active = true;
            try {
                await _DbContext.SaveChangesAsync();
                return Ok(new UserMovieInfoReply() { ErrorInfo = new ErrorReply() { IsError = false, Message = "" }, SingleUserMovieInfo = userInfoUpdate });
            } catch {
                return BadRequest(new UserMovieInfoReply() { ErrorInfo = new ErrorReply() { IsError = true, Message = "Failed to add movie." } });
            }
        }

        // DELETE: api/UserMovieInfo/deletemovie?userid={UserId}?movieid={MovieId}
        [HttpDelete("deleteusermovieinfo")]
        public async Task<IActionResult> DeleteMovie(Guid UserId, Guid MovieId) {
            if(MovieId == Guid.Empty || UserId == Guid.Empty) {
                return BadRequest(new UserMovieInfoReply() { ErrorInfo = new ErrorReply() { IsError = true, Message = "No data to update." } });
            }
            var UserInfoDelete = _DbContext.UsersMovieInfo.FirstOrDefault(uid => uid.Id == MovieId && uid.UserId == UserId && uid.Active != false);
            if(UserInfoDelete == null) {
                return Ok(new UserMovieInfoReply() { ErrorInfo = new ErrorReply() { IsError = true, Message = "User's movie info does not exist." } });
            }
            UserInfoDelete.ModifideDate = DateTime.Now;
            UserInfoDelete.Active = false;
            try {
                await _DbContext.SaveChangesAsync();
                return Ok(new UserMovieInfoReply() { ErrorInfo = new ErrorReply() { IsError = false, Message = "" }, SingleUserMovieInfo = UserInfoDelete });
            } catch {
                return BadRequest(new UserMovieInfoReply() { ErrorInfo = new ErrorReply() { IsError = true, Message = "Failed to remove movie." } });
            }
        }
    }
}
