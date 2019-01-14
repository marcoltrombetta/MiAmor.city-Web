using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class CustomerMap : MiAmorEntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            this.ToTable("Customer");
            this.HasKey(c => c.Id);

        }
    }
}
