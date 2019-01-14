using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class CustomerBookmarkMap : MiAmorEntityTypeConfiguration<CustomerBookmark>
    {
        public CustomerBookmarkMap()
        {
            this.ToTable("CustomerBookmark");
            this.HasKey(c => c.Id);

        }
    }
}
