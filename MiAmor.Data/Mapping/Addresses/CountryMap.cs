using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class CountryMap : MiAmorEntityTypeConfiguration<Country>
    {
        public CountryMap()
        {
            this.ToTable("Country");
            this.HasKey(a => a.Id);

            Property(c => c.Name).IsRequired().HasMaxLength(55);
            Property(c => c.DisplayOrder).IsRequired();
            //this.HasRequired(a => a.Language).WithMany().HasForeignKey(x => x.LanguageId).WillCascadeOnDelete(false);

        }
    }
}
