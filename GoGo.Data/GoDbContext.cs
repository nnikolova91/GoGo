using System;
using System.Collections.Generic;
using System.Text;
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

        public DbSet<Acsesoar> Acsesoaries { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Destination> Destinations { get; set; }

        public DbSet<Story> Stories { get; set; }

        public DbSet<DestinationPhoto> DestinationPhotos { get; set; }

        public DbSet<DestinationsUsers> DestinationsUsers { get; set; }

        public DbSet<PeopleStories> PeopleStories { get; set; }

        public DbSet<Cource> Cources { get; set; }

        public DbSet<CourcesUsers> CourcesUsers { get; set; }

        public DbSet<TeamLevelGame> TeamLevelGames { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Level> Levels { get; set; }
        
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("server=DESKTOP-JVV1OQ7\\SQLEXPRESS;database=GoGo;Integrated Security=true").UseLazyLoadingProxies();
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           // modelBuilder.Entity<DestinationPhoto>().HasBaseType<Photo>();
           // modelBuilder.Entity<GoUserPhoto>().HasBaseType<Photo>();
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Destination>()
                .HasMany<Comment>(s => s.Comments)
                .WithOne(c => c.Destination);
            //.OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CourcesUsers>()
               .HasKey(x => new { x.CourceId, x.ParticipantId });

            modelBuilder.Entity<CourcesUsers>()
                .HasOne(bc => bc.Cource)
                .WithMany(b => b.Participants)
                .HasForeignKey(bc => bc.CourceId);

            modelBuilder.Entity<CourcesUsers>()
                .HasOne(bc => bc.Participant)
                .WithMany(c => c.Cources)
                .HasForeignKey(bc => bc.ParticipantId);
            //-------------------------------------

            modelBuilder.Entity<TeamLevelGame>()
               .HasKey(x => new { x.TeamId, x.GameId, x.LevelId });

            modelBuilder.Entity<TeamLevelGame>()
                .HasOne(bc => bc.Game)
                .WithMany(b => b.Levels)
                .HasForeignKey(bc => bc.GameId);
            
            //-------------------------
            modelBuilder.Entity<DestinationsUsers>()                
                .HasKey(x => new { x.DestinationId, x.ParticipantId });
           
            modelBuilder.Entity<DestinationsUsers>()
                .HasOne(bc => bc.Destination)
                .WithMany(b => b.Participants)
                .HasForeignKey(bc => bc.DestinationId);
           
            modelBuilder.Entity<DestinationsUsers>()
                .HasOne(bc => bc.Participant)
                .WithMany(c => c.Destinations)
                .HasForeignKey(bc => bc.ParticipantId);
            //-----------------------
            modelBuilder.Entity<Destination>()
                .HasOne(x => x.Creator)
                .WithMany(p => p.CreatedDestinations)
                .HasForeignKey(x=>x.CreatorId);


            //modelBuilder.Entity<GoUserPhoto>().ToTable("GoUserPhoto")
            //     .HasOne(x => x.User)
            //     .WithOne(p => p.Image)
            //     .HasForeignKey<GoUserPhoto>(r => r.UserId);

            // modelBuilder.Entity<Destination>()
            //    .HasMany(s => s.Photos)
            //    .WithOne(c => c.Destination);

           
            modelBuilder.Entity<Story>()
                .HasOne(bc => bc.Author)
                .WithMany(c => c.CreatedStories)
                .HasForeignKey(x => x.AuthorId);
                
           
            modelBuilder.Entity<PeopleStories>()
               .HasKey(x => new { x.StoryId, x.UserId });
           
            modelBuilder.Entity<PeopleStories>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.Stories)
                .HasForeignKey(bc => bc.UserId);
           
            modelBuilder.Entity<PeopleStories>()
                .HasOne(bc => bc.Story)
                .WithMany(c => c.PeopleWhosLikeThis)
                .HasForeignKey(bc => bc.StoryId);

            modelBuilder.Entity<DestinationPhoto>().ToTable("DestinationPhoto")
              .HasOne(bc => bc.Destination)
              .WithMany(c => c.Photos)
              .HasForeignKey(x => x.DestinationId);


            modelBuilder.Entity<Comment>()
               .HasOne(bc => bc.Comentator)
               .WithMany(c => c.Comments)
               .HasForeignKey(x => x.ComentatorId);

            modelBuilder.Entity<Acsesoar>()
                 .HasOne(x => x.Destination)
                 .WithMany(p => p.Acsesoaries)
                 .HasForeignKey(r => r.DestinationId);
        }
    }
}
