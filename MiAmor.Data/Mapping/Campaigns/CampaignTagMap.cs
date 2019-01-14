using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class CampaignTagMap : MiAmorEntityTypeConfiguration<CampaignTag>
    {
        public CampaignTagMap()
        {
            this.ToTable("CampaignTag");
            this.HasKey(c => c.Id);

        }
    }
}
