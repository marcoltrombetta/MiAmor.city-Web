using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class VendorPaymentTypeMap : MiAmorEntityTypeConfiguration<VendorPaymentType>
    {
        public VendorPaymentTypeMap()
        {
            this.ToTable("VendorPaymentType");
            this.HasKey(a => a.Id);

         

        }
    }
}
