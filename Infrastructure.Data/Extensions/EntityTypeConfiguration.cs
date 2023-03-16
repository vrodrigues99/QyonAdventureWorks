using Domain;
using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Extensions
{
    public abstract class EntityTypeConfiguration<TEntity> where TEntity : class
    {
        public string Schema { get; private set; }

        public int MyProperty { get; set; }

        public EntityTypeConfiguration(string schema)
        {
            this.Schema = schema;
        }

        public abstract void Map(EntityTypeBuilder<TEntity> builder);

        public void RemoveAutoFields(EntityTypeBuilder<TEntity> builder)
        {
            builder.Ignore("ValidationResult");
            builder.Ignore("CascadeMode");
            builder.Ignore("RuleLevelCascadeMode");
            builder.Ignore("ClassLevelCascadeMode");
        }
    }
}
