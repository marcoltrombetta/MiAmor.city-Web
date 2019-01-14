using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class VendorPictureMap : MiAmorEntityTypeConfiguration<VendorPicture>
    {
        public VendorPictureMap()
        {
            this.ToTable("VendorPicture");
            this.HasKey(a => a.Id);

            //this.HasRequired(a => a.Vendor).WithMany().HasForeignKey(x => x.VendorId).WillCascadeOnDelete(false);

        }
    }
}
