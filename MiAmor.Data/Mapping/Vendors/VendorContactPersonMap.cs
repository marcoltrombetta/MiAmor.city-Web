using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class VendorContactPersonMap : MiAmorEntityTypeConfiguration<VendorContactPerson>
    {
        public VendorContactPersonMap()
        {
            this.ToTable("VendorContactPerson");
            this.HasKey(a => a.Id);

          
        }
    }
}
