﻿// <auto-generated />
using System;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Domain.Entities.Competidores", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<decimal>("Altura")
                        .HasColumnType("numeric");

                    b.Property<string>("Nome")
                        .HasColumnType("text");

                    b.Property<decimal>("Peso")
                        .HasColumnType("numeric");

                    b.Property<char>("Sexo")
                        .HasColumnType("character(1)");

                    b.Property<decimal>("TemperaturaMediaCorpo")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Competidores");
                });

            modelBuilder.Entity("Domain.Entities.HistoricoCorrida", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CompetidorId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DataCorrida")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("PistaCorridaId")
                        .HasColumnType("integer");

                    b.Property<decimal>("TempoGasto")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("CompetidorId");

                    b.HasIndex("PistaCorridaId");

                    b.ToTable("HistoricoCorrida");
                });

            modelBuilder.Entity("Domain.Entities.PistaCorrida", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Descricao")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PistaCorrida");
                });

            modelBuilder.Entity("Domain.Entities.HistoricoCorrida", b =>
                {
                    b.HasOne("Domain.Entities.Competidores", "Competidor")
                        .WithMany()
                        .HasForeignKey("CompetidorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.PistaCorrida", "PistaCorrida")
                        .WithMany()
                        .HasForeignKey("PistaCorridaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Competidor");

                    b.Navigation("PistaCorrida");
                });
#pragma warning restore 612, 618
        }
    }
}
