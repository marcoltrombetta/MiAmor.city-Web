using MiAmor.Core;

namespace MiAmor.Data.Mapping
{
    public partial class CampaignPaymentsMap : MiAmorEntityTypeConfiguration<CampaignPayment>
    {
        public CampaignPaymentsMap()
        {
            this.ToTable("CampaignPayment");
            this.HasKey(c => c.Id);

        }
    }
}
