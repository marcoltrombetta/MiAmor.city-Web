using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class CampaignDeliveryMap : MiAmorEntityTypeConfiguration<CampaignDelivery>
    {
        public CampaignDeliveryMap()
        {
            this.ToTable("CampaignDelivery");
            this.HasKey(c => c.Id);

        }
    }
}
