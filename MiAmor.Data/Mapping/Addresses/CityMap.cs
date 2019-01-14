using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class CityMap : MiAmorEntityTypeConfiguration<City>
    {
        public CityMap()
        {
            this.ToTable("City");
            this.HasKey(c => c.Id);
          
            Property(c => c.Name).IsRequired().HasMaxLength(55);
            Property(c => c.Published).IsRequired();
            Property(c => c.StateProvinceId).IsOptional();

            //this.HasRequired(a => a.Country).WithMany().HasForeignKey(x => x.CountryId).WillCascadeOnDelete(false);
            //this.HasOptional(a => a.StateProvince).WithMany().HasForeignKey(x => x.StateProvinceId).WillCascadeOnDelete(false);         
        }
    }
}
