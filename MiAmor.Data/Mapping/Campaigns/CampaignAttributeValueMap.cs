using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class CampaignAttributeValueMap : MiAmorEntityTypeConfiguration<CampaignAttributeValue>
    {
        public CampaignAttributeValueMap()
        {
            this.ToTable("CampaignAttributeValue");
            this.HasKey(c => c.Id);

        }
    }
}
