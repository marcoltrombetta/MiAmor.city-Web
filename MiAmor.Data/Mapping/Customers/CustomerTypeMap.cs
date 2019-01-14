using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class CustomerTypeMap : MiAmorEntityTypeConfiguration<CustomerType>
    {
        public CustomerTypeMap()
        {
            this.ToTable("CustomerType");
            this.HasKey(c => c.Id);

        }
    }
}
