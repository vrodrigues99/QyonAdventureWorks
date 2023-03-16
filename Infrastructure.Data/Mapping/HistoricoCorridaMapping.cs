using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Infrastructure.Data.Extensions;
using Domain.Entities;

namespace Infrastructure.Data.Mapping
{
    public class HistoricoCorridaMapping : EntityTypeConfiguration<HistoricoCorrida>
    {
        public HistoricoCorridaMapping(string schema) : base(schema)
        {
        }

        public override void Map(EntityTypeBuilder<HistoricoCorrida> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Competidor)
                .WithMany()
                .HasForeignKey(x => x.CompetidorId);

            builder.HasOne(x => x.PistaCorrida)
                .WithMany()
                .HasForeignKey(x => x.PistaCorridaId);

            RemoveAutoFields(builder);
        }
    }
}
