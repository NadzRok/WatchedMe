namespace WatchedMe.Models.NoDb {
    public class MovieReturnMessage {
        public bool IsError { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<Movie> MovieList { get; set; } = new List<Movie>();
        public Movie Movie { get; set; } = new Movie();
    }
}
