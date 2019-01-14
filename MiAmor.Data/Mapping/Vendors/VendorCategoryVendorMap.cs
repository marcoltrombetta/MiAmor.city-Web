using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class VendorCategoryVendorMap : MiAmorEntityTypeConfiguration<VendorCategoryVendor>
    {
        public VendorCategoryVendorMap()
        {

            this.ToTable("VendorCategoryVendor");
            this.HasKey(a => a.Id);
           
            //this.HasOptional(a => a.ParentCategory)
            //.WithMany(a => a.VendorCategories)
            //.HasForeignKey(x => x.ParentCategoryId)
            //.WillCascadeOnDelete(false);          

        }
    }
}
