using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class BlogSettingMap : MiAmorEntityTypeConfiguration<BlogSettings>
    {
        public BlogSettingMap()
        {
            this.ToTable("BlogSettings");
            this.HasKey(a => a.Id);

        }
    }
}
