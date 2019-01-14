using MiAmor.Core;

namespace MiAmor.Data.Mapping
{
    public partial class ProductVendorMap : MiAmorEntityTypeConfiguration<ProductVendor>
    {
        public ProductVendorMap()
        {
            this.ToTable("ProductVendor");
            this.HasKey(c => c.Id);
            //this.HasMany(p => p.ProductCategories)
            //    .WithMany(pt => pt.Product)
            //    .Map(m => m.ToTable("Product_ProductTag_Mapping"));
        }
    }
}