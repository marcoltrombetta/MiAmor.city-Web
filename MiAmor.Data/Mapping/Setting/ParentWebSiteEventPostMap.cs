using MiAmor.Core;



namespace MiAmor.Data.Mapping
{
    public partial class ParentWebSiteBlogPostMap : MiAmorEntityTypeConfiguration<ParentWebSiteBlogPost>
    {
        public ParentWebSiteBlogPostMap()
        {
            this.ToTable("ParentWebSiteBlogPost");
            this.HasKey(a => a.Id);

        }
    }
}
