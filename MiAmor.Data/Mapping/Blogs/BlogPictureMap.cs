using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class BlogPictureMap : MiAmorEntityTypeConfiguration<BlogPostPicture>
    {
        public BlogPictureMap()
        {
            this.ToTable("BlogPostPicture");
            this.HasKey(a => a.Id);

        }
    }
}
