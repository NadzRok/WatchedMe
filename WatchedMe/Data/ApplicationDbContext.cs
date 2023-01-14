namespace WatchedMe.Data {
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser> {
        public ApplicationDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options, operationalStoreOptions) {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<UserMovieInfo> UsersMovieInfo { get; set; }
    }
}