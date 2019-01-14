using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class CampaignAttributeMappingMap : MiAmorEntityTypeConfiguration<CampaignAttributeMapping>
    {
        public CampaignAttributeMappingMap()
        {
            this.ToTable("CampaignAttributeMapping");
            this.HasKey(c => c.Id);

        }
    }
}
