namespace WatchedMe.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UserMovieInfoController : ControllerBase {
        private readonly ApplicationDbContext _DbContext;

        public UserMovieInfoController(ApplicationDbContext DbContext) {
            _DbContext = DbContext;
        }

        // GET: api/movie/getallmovies?userid={UserId}
        //[HttpGet("getallmovies")]
        //public IActionResult GetAllUserInfo(Guid UserId) {
        //    return Ok(new UserMovieInfoReply() { ErrorInfo = new ErrorReply() { IsError = false, Message = "" }, UserMovieInfoList = _DbContext.UserMovie.Where(umil => umil.).ToList() });
        //}
    }
}
