using MiAmor.Core;


namespace MiAmor.Data.Mapping
{
    public partial class VendorReviewMap : MiAmorEntityTypeConfiguration<VendorReview>
    {
        public VendorReviewMap()
        {
            this.ToTable("VendorReview");
            this.HasKey(a => a.Id);

        

            //this.HasRequired(a => a.Country).WithMany().HasForeignKey(x => x.CountryId).WillCascadeOnDelete(false);
            //this.HasOptional(a => a.StateProvince).WithMany().HasForeignKey(x => x.StateProvinceId).WillCascadeOnDelete(false);
            //this.HasRequired(a => a.City).WithMany().HasForeignKey(x => x.CityId).WillCascadeOnDelete(false);
            //this.HasOptional(a => a.Neighbourhood).WithMany().HasForeignKey(x => x.NeighbourhoodId).WillCascadeOnDelete(false);

        }
    }
}
