using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class LanguageMap : MiAmorEntityTypeConfiguration<Language>
    {
        public LanguageMap()
        {
            this.ToTable("Language");
            this.HasKey(a => a.Id);

          
        }
    }
}
