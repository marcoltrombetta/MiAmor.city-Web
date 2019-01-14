using MiAmor.Core;

namespace MiAmor.Data.Mapping
{
    public partial class ProductCategoryProductMap : MiAmorEntityTypeConfiguration<ProductCategoryProduct>
    {
        public ProductCategoryProductMap()
        {
            this.ToTable("ProductCategoryProduct");
            this.HasKey(c => c.Id);
           
        }
    }
}