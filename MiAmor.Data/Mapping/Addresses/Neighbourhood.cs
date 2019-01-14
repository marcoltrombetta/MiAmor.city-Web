using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class NeighbourhoodMap : MiAmorEntityTypeConfiguration<Neighbourhood>
    {
        public NeighbourhoodMap()
        {
            this.ToTable("Neighbourhood");
            this.HasKey(c => c.Id);
          
            Property(c => c.Name).IsRequired().HasMaxLength(55);
            Property(c => c.Published).IsRequired();
           

        }
    }
}
