using Domain.Entities;
using Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Mapping
{
    public class PistaCorridaMapping : EntityTypeConfiguration<PistaCorrida>
    {
        public PistaCorridaMapping(string schema) : base(schema)
        {
        }

        public override void Map(EntityTypeBuilder<PistaCorrida> builder)
        {
            builder.HasKey(x => x.Id);

            RemoveAutoFields(builder);
        }
    }
}
