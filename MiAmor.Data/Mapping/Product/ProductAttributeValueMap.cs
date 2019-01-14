using MiAmor.Core;

namespace MiAmor.Data.Mapping
{
    public partial class ProductAttributeValueMap : MiAmorEntityTypeConfiguration<ProductAttributeValue>
    {
        public ProductAttributeValueMap()
        {
            this.ToTable("ProductAttributeValue");
            this.HasKey(pav => pav.Id);
            this.Property(pav => pav.Name).IsRequired().HasMaxLength(400);
            this.Property(pav => pav.ColorSquaresRgb).HasMaxLength(100);

            this.Property(pav => pav.PriceAdjustment).HasPrecision(18, 4);
            this.Property(pav => pav.WeightAdjustment).HasPrecision(18, 4);
            this.Property(pav => pav.Cost).HasPrecision(18, 4);
       

         
        }
    }
}