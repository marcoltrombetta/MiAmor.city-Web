using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class StateProvinceMap : MiAmorEntityTypeConfiguration<StateProvince>
    {
        public StateProvinceMap()
        {
            this.ToTable("StateProvince");
            this.HasKey(a => a.Id);

            Property(c => c.Name).IsRequired().HasMaxLength(100);

         //   this.HasRequired(a => a.Country).WithMany().HasForeignKey(x => x.CountryId).WillCascadeOnDelete(false);           
        }
    }
}
