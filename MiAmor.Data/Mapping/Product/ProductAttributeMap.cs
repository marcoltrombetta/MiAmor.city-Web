using MiAmor.Core;

namespace MiAmor.Data.Mapping
{
    public partial class ProductAttributeMap : MiAmorEntityTypeConfiguration<ProductAttribute>
    {
        public ProductAttributeMap()
        {
            this.ToTable("ProductAttribute");
            this.HasKey(pa => pa.Id);
            this.Property(pa => pa.Name).IsRequired();
        }
    }
}