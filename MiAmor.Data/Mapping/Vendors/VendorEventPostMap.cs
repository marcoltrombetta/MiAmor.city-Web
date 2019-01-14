using MiAmor.Core;
using MiAmor.Core.Domain.Venodrs;

namespace MiAmor.Data.Mapping
{
    public partial class VendorEventPostMap : MiAmorEntityTypeConfiguration<VendorEventPost>
    {
        public VendorEventPostMap()
        {
            this.ToTable("VendorEventPost");
            this.HasKey(a => a.Id);
        }
    }
}
