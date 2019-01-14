using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class ParentWebSiteSettingMap : MiAmorEntityTypeConfiguration<ParentWebSiteSetting>
    {
        public ParentWebSiteSettingMap()
        {
            this.ToTable("ParentWebSiteSetting");
            this.HasKey(a => a.Id);

        }
    }
}
