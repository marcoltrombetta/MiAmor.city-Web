using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class PriceListMap : MiAmorEntityTypeConfiguration<PriceList>
    {
        public PriceListMap()
        {
            this.ToTable("PriceList");
            this.HasKey(a => a.Id);

        }
    }
}
