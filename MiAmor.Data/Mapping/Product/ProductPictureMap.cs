using MiAmor.Core;

namespace MiAmor.Data.Mapping
{
    public partial class ProductPictureMap : MiAmorEntityTypeConfiguration<ProductPicture>
    {
        public ProductPictureMap()
        {
            this.ToTable("Product_Picture_Mapping");
            this.HasKey(pp => pp.Id);
            
            //this.HasRequired(pp => pp.Picture)
            //    .WithMany(p => p.ProductPictures)
            //    .HasForeignKey(pp => pp.PictureId);


            //this.HasRequired(pp => pp.Product)
            //    .WithMany(p => p.ProductPictures)
            //    .HasForeignKey(pp => pp.ProductId);
        }
    }
}