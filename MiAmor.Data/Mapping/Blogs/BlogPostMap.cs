using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class BlogPostMap : MiAmorEntityTypeConfiguration<BlogPost>
    {
        public BlogPostMap()
        {
            this.ToTable("BlogPost");
            this.HasKey(a => a.Id);

        }
    }
}
