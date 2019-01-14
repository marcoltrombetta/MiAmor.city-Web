using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class VendorMap : MiAmorEntityTypeConfiguration<Vendor>
    {
        public VendorMap()
        {
            this.ToTable("Vendor");
            this.HasKey(a => a.Id);
            
           
            Property(v => v.Name).IsRequired().HasMaxLength(255);
            Property(v => v.VendorTemplateId).IsOptional();
            this.HasRequired(t => t.VendorOpeningHours)
            .WithRequiredPrincipal(t => t.Vendor);
            
            //this.HasMany(v => v.VendorPictures).WithRequired(s => s.Vendor_Id).HasForeignKey(a => a.SliderId);
            //this.HasRequired(a => a.VendorAddress).WithMany(x => x.VendorAddressId);
            //this.HasOptional(a => a.TemplateByCategory).WithMany().HasForeignKey(x => x.VendorTemplateId).WillCascadeOnDelete(false);   
        }
    }
}
