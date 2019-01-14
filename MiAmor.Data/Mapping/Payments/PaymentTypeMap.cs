using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class PaymentTypeMap : MiAmorEntityTypeConfiguration<PaymentType>
    {
        public PaymentTypeMap()
        {
            this.ToTable("PaymentType");
            this.HasKey(a => a.Id);

            Property(c => c.Name).IsRequired().HasMaxLength(150);

        }
    }
}
