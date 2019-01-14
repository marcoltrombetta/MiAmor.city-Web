using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class VendorOpeningHoursMap : MiAmorEntityTypeConfiguration<VendorOpeningHours>
    {
        public VendorOpeningHoursMap()
        {
            this.ToTable("VendorOpeningHours");
            this.HasKey(a => a.Id);

            //this.HasRequired(a => a.Vendor).WithMany().HasForeignKey(x => x.VendorId).WillCascadeOnDelete(false);

        }
    }
}
