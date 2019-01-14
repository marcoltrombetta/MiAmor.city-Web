using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class CampaignAttributeMap : MiAmorEntityTypeConfiguration<CampaignAttribute>
    {
        public CampaignAttributeMap()
        {
            this.ToTable("CampaignAttribute");
            this.HasKey(c => c.Id);

        }
    }
}
