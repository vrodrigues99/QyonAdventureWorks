using Domain.Entities;
using Infrastructure.Data.Extensions;
using Infrastructure.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using System.Reflection.Emit;

namespace Infrastructure.Data.Context
{
    public class DataContext : DbContext
    {
        #region Entities
        public DbSet<Competidores> Competidores { get; set; }
        public DbSet<PistaCorrida> PistaCorrida { get; set; }
        public DbSet<HistoricoCorrida> HistoricoCorrida { get; set; }
        #endregion Entities

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var Schema = config["Schema"];

            #region Mappings
            modelBuilder.AddConfiguration(new CompetidoresMapping(Schema));
            modelBuilder.AddConfiguration(new PistaCorridaMapping(Schema));
            modelBuilder.AddConfiguration(new HistoricoCorridaMapping(Schema));
            #endregion

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddUserSecrets<DataContext>()
                .AddJsonFile("appsettings.json")
                .Build();

            var secretProvider = config.Providers.First();
            secretProvider.TryGet("DbPassword", out var secretPass);

            optionsBuilder.UseNpgsql(config.GetConnectionString("DefaultConnection").Replace("#{UserPassword}#", secretPass));
        }
    }
}