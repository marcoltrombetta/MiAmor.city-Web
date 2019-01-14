using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class CustomerMessageTypeMap : MiAmorEntityTypeConfiguration<CustomerMessageType>
    {
        public CustomerMessageTypeMap()
        {
            this.ToTable("CustomerMessageType");
            this.HasKey(c => c.Id);

        }
    }
}
