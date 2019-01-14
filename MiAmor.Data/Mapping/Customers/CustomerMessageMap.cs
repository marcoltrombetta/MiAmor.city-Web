using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class CustomerMessageMap : MiAmorEntityTypeConfiguration<CustomerMessage>
    {
        public CustomerMessageMap()
        {
            this.ToTable("CustomerMessage");
            this.HasKey(c => c.Id);

        }
    }
}
