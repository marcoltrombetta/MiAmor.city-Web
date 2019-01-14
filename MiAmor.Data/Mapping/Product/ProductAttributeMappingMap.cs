using MiAmor.Core;

namespace MiAmor.Data.Mapping
{
    public partial class ProductAttributeMappingMap : MiAmorEntityTypeConfiguration<ProductAttributeMapping>
    {
        public ProductAttributeMappingMap()
        {
            this.ToTable("Product_ProductAttribute_Mapping");
            this.HasKey(pam => pam.Id);
         
          
        }
    }
}