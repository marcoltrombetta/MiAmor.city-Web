using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class BlogCommentMap : MiAmorEntityTypeConfiguration<BlogComment>
    {
        public BlogCommentMap()
        {
            this.ToTable("BlogComment");
            this.HasKey(a => a.Id);

        }
    }
}
