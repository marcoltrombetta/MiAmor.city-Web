using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class VendorCategoryMap : MiAmorEntityTypeConfiguration<VendorCategory>
    {
        public VendorCategoryMap()
        {

            this.ToTable("VendorCategory");
            this.HasKey(a => a.Id);
            Property(v => v.Name).IsRequired().HasMaxLength(255);
            Property(v => v.Description).IsOptional();
            //this.HasOptional(a => a.ParentCategory)
            //.WithMany(a => a.VendorCategories)
            //.HasForeignKey(x => x.ParentCategoryId)
            //.WillCascadeOnDelete(false);          

        }
    }
}
