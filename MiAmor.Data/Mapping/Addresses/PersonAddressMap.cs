using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class PersonAddressMap : MiAmorEntityTypeConfiguration<PersonAddress>
    {
        public PersonAddressMap()
        {
            this.ToTable("PersonAddress");
            this.HasKey(c => c.Id);

           

            //this.HasRequired(a => a.Country).WithMany().HasForeignKey(x => x.CountryId).WillCascadeOnDelete(false);
            //this.HasOptional(a => a.StateProvince).WithMany().HasForeignKey(x => x.StateProvinceId).WillCascadeOnDelete(false);         
        }
    }
}
