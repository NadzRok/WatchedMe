namespace WatchedMe.Models {
    public class UserMovieInfo {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid MovieId { get; set; }
        public int UserRating { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime ModifideDate { get; set; }
        public string Notes { get; set; } = string.Empty;
        [Required]
        public bool Active { get; set; }
    }
}
