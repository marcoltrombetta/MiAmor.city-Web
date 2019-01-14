using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class PriceListVendorCategoryMap : MiAmorEntityTypeConfiguration<PriceListVendorCategory>
    {
        public PriceListVendorCategoryMap()
        {
            this.ToTable("PriceListVendorCategory");
            this.HasKey(a => a.Id);
        }
    }
}
