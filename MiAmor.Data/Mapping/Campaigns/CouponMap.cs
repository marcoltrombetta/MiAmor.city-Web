using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class CouponMap : MiAmorEntityTypeConfiguration<Coupon>
    {
        public CouponMap()
        {
            this.ToTable("Cupon");
            this.HasKey(c => c.Id);

        }
    }
}
