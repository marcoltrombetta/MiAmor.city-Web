using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class CampaignPictureMap : MiAmorEntityTypeConfiguration<CampaignPicture>
    {
        public CampaignPictureMap()
        {
            this.ToTable("CampaignPicture");
            this.HasKey(c => c.Id);

        }
    }
}
