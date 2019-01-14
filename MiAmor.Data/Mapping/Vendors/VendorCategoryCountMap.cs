using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class VendorCategoryCountMap : MiAmorEntityTypeConfiguration<VendorCategoryCount>
    {
        public VendorCategoryCountMap()
        {

            this.ToTable("VendorCategoryCount");
            this.HasKey(a => a.Id);
            Property(v => v.VendorCategoryId).IsRequired();
            Property(v => v.CountVendors).IsRequired();
            //this.HasOptional(a => a.ParentCategory)
            //.WithMany(a => a.VendorCategories)
            //.HasForeignKey(x => x.ParentCategoryId)
            //.WillCascadeOnDelete(false);          

        }
    }
}
