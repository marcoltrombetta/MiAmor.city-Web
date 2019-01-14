using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class SlideMap : MiAmorEntityTypeConfiguration<Slide>
    {
        public SlideMap()
        {
            this.ToTable("Slide");
            this.HasKey(a => a.Id);

            Property(v => v.Name).IsRequired().HasMaxLength(255);

          //  this.HasRequired(a => a.Slider).WithMany(s => s.Slides).HasForeignKey(a=> a.SliderId);   
        }
    }
}
