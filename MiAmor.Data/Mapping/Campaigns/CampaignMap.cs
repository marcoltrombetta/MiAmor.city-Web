using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class CampaignMap : MiAmorEntityTypeConfiguration<Campaign>
    {
        public CampaignMap()
        {
            this.ToTable("Campaign");
            this.HasKey(c => c.Id);

        }
    }
}
