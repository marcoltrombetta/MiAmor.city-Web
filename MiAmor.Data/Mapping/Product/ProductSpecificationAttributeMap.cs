using MiAmor.Core;

namespace MiAmor.Data.Mapping
{
    public partial class ProductSpecificationAttributeMap : MiAmorEntityTypeConfiguration<ProductSpecificationAttribute>
    {
        public ProductSpecificationAttributeMap()
        {
            this.ToTable("Product_SpecificationAttribute_Mapping");
            this.HasKey(psa => psa.Id);

            this.Property(psa => psa.CustomValue).HasMaxLength(4000);

          

        

        //    this.HasRequired(psa => psa.Product)
        //        .WithMany(p => p.ProductSpecificationAttributes)
        //        .HasForeignKey(psa => psa.ProductId);
        }
    }
}