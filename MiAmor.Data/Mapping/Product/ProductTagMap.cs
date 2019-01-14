using MiAmor.Core;

namespace MiAmor.Data.Mapping
{
    public partial class ProductTagMap : MiAmorEntityTypeConfiguration<ProductTag>
    {
        public ProductTagMap()
        {
            this.ToTable("ProductTag");
            this.HasKey(pt => pt.Id);
            this.Property(pt => pt.Name).IsRequired().HasMaxLength(400);
        }
    }
}