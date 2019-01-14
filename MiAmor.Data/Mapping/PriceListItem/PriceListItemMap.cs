using MiAmor.Core;



namespace MiAmor.Data.Mapping
{
    public partial class PriceListItemMap : MiAmorEntityTypeConfiguration<PriceListItem>
    {
        public PriceListItemMap()
        {
            this.ToTable("PriceListItem");
            this.HasKey(a => a.Id);

        }
    }
}
