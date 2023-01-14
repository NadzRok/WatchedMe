namespace WatchedMe.Models {
    public class Movie {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public string Url { get; set; } = string.Empty;
    }
}
