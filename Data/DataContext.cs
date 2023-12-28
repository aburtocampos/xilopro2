using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Xml;
using xilopro2.Data.Entities;

namespace xilopro2.Data
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }


       

        /*  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
          {
            //  base.OnConfiguring(optionsBuilder);
              optionsBuilder.UseSqlServer(@"server=H510MPC;database=xilodb2;Integrated Security=true;TrustServerCertificate=true;");
          }*/


        public DbSet<Country> Countries { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Torneo> Torneos { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Groups> Groups { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<GroupDetail> GroupDetails { get; set; }

        public DbSet<Match> Matches { get; set; }

        public DbSet<Lineup> Lineups { get; set; }

        public DbSet<CorrectionAction> CorrectionActions { get; set; }

        public DbSet<Parent> Parents { get; set; }

        public DbSet<PlayerFiles> PlayerFiles { get; set; }



        //  public DbSet<UserCategory> UserCategory { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Country>().HasIndex(e => e.Country_Name).IsUnique();
            builder.Entity<Team>().HasIndex(e => e.Team_Name).IsUnique();
            builder.Entity<Category>().HasIndex(x => x.Category_Name).IsUnique();
            builder.Entity<Position>().HasIndex(p => p.Position_Name).IsUnique();
            builder.Entity<AppUser>().HasIndex(p => p.Email).IsUnique();

            builder.Entity<AppUser>()
            .Property(u => u.SelectedCategoryIds)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()
            );

       /*     builder.Entity<Player>(b =>
            {
                b.HasKey(e => e.Player_ID);
                b.Property(e => e.Player_ID).ValueGeneratedOnAdd();
            });

            builder.Entity<Player>(entity =>
            {
                entity.HasOne(a => a.Team)
                .WithMany(p => p.Players)
                .HasForeignKey(p => p.TeamId)
                .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(a => a.Position)
               .WithMany(p => p.Players)
               .HasForeignKey(p => p.PositionId)
               .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(a => a.Country)
              .WithMany(p => p.Players)
              .HasForeignKey(p => p.Countryid)
              .OnDelete(DeleteBehavior.NoAction);

            });*/

            builder.Entity<Player>()
            .Property(u => u.SelectedCategoryIds)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()
            );



        }


    }
}

