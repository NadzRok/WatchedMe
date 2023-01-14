namespace WatchedMe.Models.NoDb {
    public class UserMovieInfoReply {
        public ErrorReply ErrorInfo { get; set; } = new ErrorReply();
        public List<UserMovieInfo> UserMovieInfoList { get; set; } = new List<UserMovieInfo>();
        public UserMovieInfo SingleUserMovieInfo { get; set; } = new UserMovieInfo();
    }
}
