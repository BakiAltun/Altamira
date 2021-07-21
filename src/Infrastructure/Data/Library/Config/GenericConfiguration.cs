using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vimo.Infrastructure.Data.Library.Config
{
    public class GenericConfiguration<T> : IEntityTypeConfiguration<T> where T : class
    {
        public static IEntityTypeConfiguration<T> Instance => new GenericConfiguration<T>();
        public static IEntityTypeConfiguration<T> InstanceWithName(string tableName) => new GenericConfiguration<T>(tableName);

        private readonly string _tableName;
        public GenericConfiguration(string tableName)
        {
            _tableName = tableName;
        }

        public GenericConfiguration()
        {

        }

        public void Configure(EntityTypeBuilder<T> builder)
        {
            if (string.IsNullOrEmpty(_tableName))
                builder.ToTable(typeof(T).Name);
            else
                builder.ToTable(_tableName);
        }
    }
}