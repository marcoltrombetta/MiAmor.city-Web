using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class CampaignStatusMap : MiAmorEntityTypeConfiguration<CampaignStatus>
    {
        public CampaignStatusMap()
        {
            this.ToTable("CampaignStatus");
            this.HasKey(c => c.Id);

        }
    }
}
