namespace WatchedMe.Models.NoDb {
    public class MovieReturnMessage {
        public ErrorReply ErrorInfo { get; set; } = new ErrorReply();
        public List<Movie> MovieList { get; set; } = new List<Movie>();
        public Movie SingleMovie { get; set; } = new Movie();
    }
}
