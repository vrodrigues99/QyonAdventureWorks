using Domain.Entities;
using Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Mapping
{
    public class CompetidoresMapping : EntityTypeConfiguration<Competidores>
    {
        public CompetidoresMapping(string schema) : base(schema)
        {
        }

        public override void Map(EntityTypeBuilder<Competidores> builder)
        {
            builder.HasKey(x => x.Id);

            RemoveAutoFields(builder);
        }
    }
}
