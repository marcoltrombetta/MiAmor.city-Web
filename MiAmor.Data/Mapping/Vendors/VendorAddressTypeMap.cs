using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class VendorAddressTypeMap : MiAmorEntityTypeConfiguration<VendorAddressType>
    {
        public VendorAddressTypeMap()
        {
            this.ToTable("VendorAddressType");
            this.HasKey(a => a.Id);
           
        }
    }
}
