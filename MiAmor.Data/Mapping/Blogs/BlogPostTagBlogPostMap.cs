using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class BlogPostTagBlogPostMap : MiAmorEntityTypeConfiguration<BlogPostTagBlogPost>
    {
        public BlogPostTagBlogPostMap()
        {
            this.ToTable("BlogPostTagBlogPost");
            this.HasKey(a => a.Id);

        }
    }
}
