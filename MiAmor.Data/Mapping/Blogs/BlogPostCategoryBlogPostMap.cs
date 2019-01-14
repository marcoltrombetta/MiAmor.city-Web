using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class BlogPostCategoryBlogPostMap : MiAmorEntityTypeConfiguration<BlogPostCategoryBlogPost>
    {
        public BlogPostCategoryBlogPostMap()
        {
            this.ToTable("BlogPostCategoryBlogPost");
            this.HasKey(a => a.Id);

        }
    }
}
