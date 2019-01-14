using MiAmor.Core;

namespace MiAmor.Data.Mapping
{
    public partial class RelatedProductMap : MiAmorEntityTypeConfiguration<RelatedProduct>
    {
        public RelatedProductMap()
        {
            this.ToTable("RelatedProduct");
            this.HasKey(c => c.Id);
        }
    }
}