using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class VendorAddressMap : MiAmorEntityTypeConfiguration<VendorAddress>
    {
        public VendorAddressMap()
        {
            this.ToTable("VendorAddress");
            this.HasKey(a => a.Id);

            Property(c => c.Active).IsRequired();

            //this.HasRequired(a => a.Country).WithMany().HasForeignKey(x => x.CountryId).WillCascadeOnDelete(false);
            //this.HasOptional(a => a.StateProvince).WithMany().HasForeignKey(x => x.StateProvinceId).WillCascadeOnDelete(false);
            //this.HasRequired(a => a.City).WithMany().HasForeignKey(x => x.CityId).WillCascadeOnDelete(false);
            //this.HasOptional(a => a.Neighbourhood).WithMany().HasForeignKey(x => x.NeighbourhoodId).WillCascadeOnDelete(false);
            
        }
    }
}
