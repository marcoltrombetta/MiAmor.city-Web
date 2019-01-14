using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class VendorBlogPostMap : MiAmorEntityTypeConfiguration<VendorBlogPost>
    {
        public VendorBlogPostMap()
        {
            this.ToTable("VendorBlogPost");
            this.HasKey(a => a.Id);

        }
    }
}
