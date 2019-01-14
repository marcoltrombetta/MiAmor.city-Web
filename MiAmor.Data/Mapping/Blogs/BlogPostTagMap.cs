using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class BlogPostTagMap : MiAmorEntityTypeConfiguration<BlogPostTag>
    {
        public BlogPostTagMap()
        {
            this.ToTable("BlogPostTag");
            this.HasKey(a => a.Id);

        }
    }
}
