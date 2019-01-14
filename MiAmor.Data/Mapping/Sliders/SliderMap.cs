using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class SliderMap : MiAmorEntityTypeConfiguration<Slider>
    {
        public SliderMap()
        {
            this.ToTable("Slider");
            this.HasKey(a => a.Id);

            Property(v => v.Name).IsRequired().HasMaxLength(255);

           // this.HasMany(a => a.Slides).WithRequired(s => s.Slider).HasForeignKey(a => a.SliderId);
        }
    }
}
