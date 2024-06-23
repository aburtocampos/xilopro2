using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
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

        public DbSet<Matchgame> Matches { get; set; }

        public DbSet<Lineup> Lineups { get; set; }

        public DbSet<CorrectionAction> CorrectionActions { get; set; }

        public DbSet<Parent> Parents { get; set; }

        public DbSet<PlayerFiles> PlayerFiles { get; set; }

        public DbSet<PlayerStatistics> PlayerStatistics { get; set; }

        public DbSet<Membership> Memberships { get; set; }

        public DbSet<Payments> Payments { get; set; }

        public DbSet<Attendance> Attendances { get; set; }


        //  public DbSet<UserCategory> UserCategory { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /***********************************************COUNTRY********************************************/
            builder.Entity<Country>().HasIndex(e => e.Country_Name).IsUnique();
            builder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Country_ID)
                    //.ValueGeneratedNever()
                    .HasColumnName("Country_ID");
            });
            builder.Entity<Country>(b =>
            {
                b.HasKey(e => e.Country_ID);
                b.Property(e => e.Country_ID).ValueGeneratedOnAdd();
            });

            /************************************************STATES************************************************/
            builder.Entity<State>().HasIndex(g => new { g.CountryId, g.State_Name }).IsUnique();// no repetir nombres de departamentos en el mismo pais
            builder.Entity<State>(b =>
            {
                b.HasKey(e => e.State_ID);
                b.Property(e => e.State_ID).ValueGeneratedOnAdd();
            });

            /************************************************CITY***********************************************/

            builder.Entity<City>().HasIndex(g => new { g.IdState, g.City_Name }).IsUnique();// no repetir nombres de municipios en el mismo departamento

            /*******************************************************APPUSER**************************************/
            builder.Entity<AppUser>().HasIndex(p => p.Email).IsUnique();
           
            builder.Entity<AppUser>() //arreglo de enteros para las categorias asignadas a usuarios
            .Property(u => u.SelectedCategoryIds)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()
            );

            /***********************************************TEAM***********************************************/
            builder.Entity<Team>().HasIndex(e => e.Team_Name).IsUnique();
            builder.Entity<Team>(b =>
            {
                b.HasKey(e => e.Team_ID);
                b.Property(e => e.Team_ID).ValueGeneratedOnAdd();
            });

            /***************************************************CATEGORY******************************************/
            builder.Entity<Category>().HasIndex(x => x.Category_Name).IsUnique();

            /****************************************************PLAYERS*******************************************/
            builder.Entity<Player>().HasIndex(gd => new { gd.Player_Email }).IsUnique();
            builder.Entity<Player>().HasIndex(gd => new { gd.Player_Cedula }).IsUnique();
            builder.Entity<Player>().HasIndex(gd => new { gd.SelectedCategoryIds, gd.Player_Dorsal, gd.Teamid }).IsUnique();

            builder.Entity<Player>(b =>
            {
                b.HasKey(e => e.Player_ID);
                b.Property(e => e.Player_ID).ValueGeneratedOnAdd();
            });

            builder.Entity<Player>(entity =>
            {
                entity.HasOne(a => a.Team)
                .WithMany(p => p.Players)
                .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(a => a.Position)
               .WithMany(p => p.Players)
               .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(a => a.Country)
              .WithMany(p => p.Players)
              .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Player>()
            .Property(u => u.SelectedCategoryIds)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()
            );

         

            /************************************************************POSITION****************************************************/
            builder.Entity<Position>().HasIndex(p => p.Position_Name).IsUnique();
         
            /**************************************************************GROUPS****************************************************/
            builder.Entity<Groups>().HasIndex(g => new { g.torneoId, g.Group_Name }).IsUnique();// no repetir nombres de grupos en el mismo torneo
                                                                                                // builder.Entity<Groups>().HasIndex(p => p.Group_Name).IsUnique();
           /*************************************************************GROUPDETAILS************************************************/
            builder.Entity<GroupDetail>().HasIndex(gd => new { gd.groupId, gd.teamId }).IsUnique(); // no repetir equipos en detalles de grupos
                                                                                                    // Configuración de relaciones Detalle de Grupos, Team y Grupos
            builder.Entity<GroupDetail>()
                .HasKey(gd => gd.GroupDetail_ID); // Establecer la clave primaria

            builder.Entity<GroupDetail>()
                .HasOne(gd => gd.Groups)
                .WithMany(g => g.GroupDetails)
                .HasForeignKey(gd => gd.groupId); // Establecer la relación con Group

            builder.Entity<GroupDetail>()
                .HasOne(gd => gd.Team)
                .WithMany()
                .HasForeignKey(gd => gd.teamId); // Establecer la relación con Team


            /*************************************************************TORNEO*****************************************************/
            builder.Entity<Torneo>()
            .Property(u => u.SelectedCategoryIds)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()
            );

            //try to fix the isssue when add group to a torneo
            builder.Entity<Torneo>(b =>
            {
                b.HasKey(e => e.Torneo_ID);
                b.Property(e => e.Torneo_ID).ValueGeneratedOnAdd();
            });

            //para no repetir season, name en la misma categoria
            /*builder.Entity<Torneo>()
            .HasIndex(g => new { g.Torneo_Name, g.SelectedCategoryIds })
            .IsUnique()
            .HasFilter(null);*/

            builder.Entity<Torneo>()
                .HasIndex(g => new { g.Torneo_Name, g.SelectedCategoryIds, g.Torneo_Season })
                .IsUnique()
                .HasFilter(null);

            /******************************************PLAYERSTATIDISTICS*********************************************************/
            builder.Entity<PlayerStatistics>().HasIndex(gd => new { gd.MatchId, gd.PlayerId }).IsUnique();//no repetir stat de jugador por jornada

            /*********************************************MATCHGAME************************************************/
            builder.Entity<Matchgame>().HasIndex(gd => new { gd.TeamLocalId, gd.TeamVisitorId, gd.Jornada, gd.GroupsrId }).IsUnique();//no repetir jornada y local ni visitante
            builder.Entity<Matchgame>().HasIndex(gd => new { gd.TeamVisitorId, gd.TeamLocalId, gd.Jornada, gd.GroupsrId }).IsUnique();
            builder.Entity<Matchgame>().HasIndex(gd => new { gd.TeamLocalId, gd.TeamVisitorId, gd.GroupsrId }).IsUnique();
            //fix llamar Team dos veces en Matches
            builder.Entity<Matchgame>()
            .HasOne(u => u.TeamLocal)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Matchgame>()
           .HasOne(u => u.TeamVisitor)
           .WithMany()
           .OnDelete(DeleteBehavior.Restrict);

            //para guardar match
            builder.Entity<Matchgame>()
                     .HasKey(gd => gd.Match_ID);

            builder.Entity<Matchgame>()
               .HasOne(gd => gd.Groups)
               .WithMany(gd => gd.Matches)
               .HasForeignKey(gd => gd.GroupsrId);


            /**************************************MEMBERSHIP****************************************************/
            builder.Entity<Membership>().HasIndex(p => p.Membership_FullName).IsUnique();


            /***************************************************CORRECACTION**************************************************/
            builder.Entity<CorrectionAction>()
            .Property(u => u.Jornadasasancionar)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
            );


        }
    }
}

