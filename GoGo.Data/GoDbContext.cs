using GoGo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GoGo.Data
{
    public class GoDbContext : IdentityDbContext<GoUser, ApplicationRole, string>
    {
        public GoDbContext(DbContextOptions<GoDbContext> options)
            : base(options)
        {
        }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Destination> Destinations { get; set; }

        public DbSet<Story> Stories { get; set; }

        public DbSet<DestinationPhoto> DestinationPhotos { get; set; }

        public DbSet<DestinationsUsers> DestinationsUsers { get; set; }

        public DbSet<PeopleStories> PeopleStories { get; set; }

        public DbSet<GameLevelParticipant> LevelsParticipants { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<CoursesUsers> CoursesUsers { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Level> Levels { get; set; }

        public DbSet<Theme> Thems { get; set; }

        public DbSet<ThemComment> ThemComments { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("server=DESKTOP-JVV1OQ7\\SQLEXPRESS;database=GoGo;Integrated Security=true").UseLazyLoadingProxies();
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ThemComment>()
                .HasOne(s => s.Them)
                .WithMany(c => c.ThemComments)
                .HasForeignKey(t => t.ThemId); ;

            modelBuilder.Entity<Destination>()
                .HasMany<Comment>(s => s.Comments)
                .WithOne(c => c.Destination);

            modelBuilder.Entity<CoursesUsers>()
               .HasKey(x => new { x.CourseId, x.ParticipantId });

            modelBuilder.Entity<CoursesUsers>()
                .HasOne(bc => bc.Course)
                .WithMany(b => b.Participants)
                .HasForeignKey(bc => bc.CourseId);

            modelBuilder.Entity<CoursesUsers>()
                .HasOne(bc => bc.Participant)
                .WithMany(c => c.Courses)
                .HasForeignKey(bc => bc.ParticipantId);
            //-------------------------------------
            modelBuilder.Entity<GameLevelParticipant>()
               .HasKey(x => new { x.ParticipantId, x.GameId, x.LevelId });

            modelBuilder.Entity<GameLevelParticipant>()
                .HasOne(bc => bc.Game)
                .WithMany(b => b.LevelsParticipants)
                .HasForeignKey(bc => bc.GameId);
            //-------------------------
            modelBuilder.Entity<DestinationsUsers>()
                .HasKey(x => new { x.DestinationId, x.ParticipantId });

            modelBuilder.Entity<DestinationsUsers>()
                .HasOne(bc => bc.Destination)
                .WithMany(b => b.Participants)
                .HasForeignKey(bc => bc.DestinationId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DestinationsUsers>()
                .HasOne(bc => bc.Participant)
                .WithMany(c => c.Destinations)
                .HasForeignKey(bc => bc.ParticipantId).OnDelete(DeleteBehavior.Restrict);
            //-----------------------
            modelBuilder.Entity<Destination>()
                .HasOne(x => x.Creator)
                .WithMany(p => p.CreatedDestinations)
                .HasForeignKey(x => x.CreatorId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Story>()
               .HasOne(x => x.Destination)
               .WithMany(p => p.Stories)
               .HasForeignKey(x => x.DestinationId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Story>()
                .HasOne(bc => bc.Author)
                .WithMany(c => c.CreatedStories)
                .HasForeignKey(x => x.AuthorId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PeopleStories>()
               .HasKey(x => new { x.StoryId, x.UserId });

            modelBuilder.Entity<PeopleStories>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.Stories)
                .HasForeignKey(bc => bc.UserId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PeopleStories>()
                .HasOne(bc => bc.Story)
                .WithMany(c => c.PeopleWhosLikeThis)
                .HasForeignKey(bc => bc.StoryId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DestinationPhoto>().ToTable("DestinationPhoto")
              .HasOne(bc => bc.Destination)
              .WithMany(c => c.Photos)
              .HasForeignKey(x => x.DestinationId);

            modelBuilder.Entity<Comment>()
               .HasOne(bc => bc.Comentator)
               .WithMany(c => c.Comments)
               .HasForeignKey(x => x.ComentatorId);
        }
    }
}
