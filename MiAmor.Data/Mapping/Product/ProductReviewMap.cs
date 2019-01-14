using MiAmor.Core;

namespace MiAmor.Data.Mapping
{
    public partial class ProductReviewMap : MiAmorEntityTypeConfiguration<ProductReview>
    {
        public ProductReviewMap()
        {
            this.ToTable("ProductReview");
            this.HasKey(pr => pr.Id);

            //this.HasRequired(pr => pr.Product)
            //    .WithMany(p => p.ProductReviews)
            //    .HasForeignKey(pr => pr.ProductId);

            //this.HasRequired(pr => pr.Customer)
            //    .WithMany()
            //    .HasForeignKey(pr => pr.CustomerId);
        }
    }
}