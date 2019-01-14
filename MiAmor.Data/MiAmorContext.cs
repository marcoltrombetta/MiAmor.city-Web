using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using MiAmor.Data.Mapping;
using MiAmor.Core;
using System.Web;
using System.Web.Mvc;
using Autofac.Integration.Mvc;
using Autofac;
using MiAmor.Data;


namespace MiAmor.Data
{
    public interface IContext
    {
 

        IDbSet<PriceList> PriceList { get; set; }
        IDbSet<PriceListItem> PriceListItem { get; set; }
        IDbSet<PriceListVendorCategory> PriceListVendorCategory { get; set; }
        IDbSet<BlogPost> BlogPost { get; set; }
        IDbSet<BlogPostPicture> BlogPostPicture { get; set; }
        IDbSet<BlogPostTag> BlogPostTag { get; set; }
        IDbSet<BlogComment> BlogComment { get; set; }
        IDbSet<BlogSettings> BlogSettings { get; set; }
        IDbSet<BlogPostTagBlogPost> BlogPostTagBlogPost { get; set; }
        IDbSet<BlogPostCategoryBlogPost> BlogPostCategoryBlogPost { get; set; }
        IDbSet<CampaignDelivery> CampaignDelivery { get; set; }
        IDbSet<Campaign> Campaign { get; set; }
        IDbSet<Vendor> Vendor { get; set; }
        IDbSet<VendorAddress> VendorAddress { get; set; }
        IDbSet<VendorAddressType> VendorAddressType { get; set; }
        IDbSet<VendorCategory> VendorCategory { get; set; }
        IDbSet<VendorCategoryVendor> VendorCategoryVendor { get; set; }
        IDbSet<VendorOpeningHours> VendorOpeningHours { get; set; }
        IDbSet<PaymentType> PaymentType { get; set; }
        IDbSet<VendorPaymentType> VendorPaymentType { get; set; }
        IDbSet<VendorContactPerson> VendorContactPerson { get; set; }
        IDbSet<Slider> Slider { get; set; }
        IDbSet<Slide> Slide { get; set; }
        IDbSet<VendorCategoryCount> VendorCategoryCount { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        IDbSet<TokenSettings> TokenSettings { get; set; }
        IDbSet<TokenType> TokenType { get; set; }
        IDbSet<Token> Token { get; set; }
        IDbSet<Customer> Customer { get; set; }
        IDbSet<Picture> Picture { get; set; }
        IDbSet<VendorPicture> VendorPicture { get; set; }
        
        IDbSet<ProductCategory> ProductCategory { get; set; }
        IDbSet<ProductCategoryProduct> ProductCategoryProduct { get; set; }
        IDbSet<Manufacturer> Manufacturer { get; set; }
        IDbSet<Product> Product { get; set; }
        IDbSet<ProductAttribute> ProductAttribute { get; set; }
        IDbSet<ProductAttributeCombination> ProductAttributeCombination { get; set; }
        IDbSet<ProductAttributeMapping> ProductAttributeMapping { get; set; }
        IDbSet<ProductAttributeValue> ProductAttributeValue { get; set; }
        //IDbSet<ProductCategoryProduct> ProductCategoryProduct { get; set; }
        IDbSet<ProductManufacturer> ProductManufacturer { get; set; }
        IDbSet<ProductPicture> ProductPicture { get; set; }
        IDbSet<ProductReview> ProductReview { get; set; }
        IDbSet<ProductReviewHelpfulness> ProductReviewHelpfulness { get; set; }
        IDbSet<ProductSpecificationAttribute> ProductSpecificationAttribute { get; set; }
        IDbSet<ProductTag> ProductTag { get; set; }
        IDbSet<ProductTemplate> ProductTemplate { get; set; }
        IDbSet<RelatedProduct> RelatedProduct { get; set; }
        IDbSet<ProductVendor> ProductVendor { get; set; }
        IDbSet<CustomerBookmark> CustomerBookmarks { get; set; }

        IDbSet<CustomerMessage> CustomerMessage { get; set; }
        IDbSet<CustomerMessageType> CustomerMessageType { get; set; }

        int SaveChanges();
    }


    public class MiAmorContext : DbContext, IContext
    {

        public MiAmorContext()
            : base("Name=MiAmorContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false; 

        }
       
        public IDbSet<PriceList> PriceList { get; set; }
        public IDbSet<PriceListItem> PriceListItem { get; set; }
        public IDbSet<PriceListVendorCategory> PriceListVendorCategory { get; set; }
        public IDbSet<BlogPostTag> BlogPostTag { get; set; }
        public IDbSet<BlogPostPicture> BlogPostPicture { get; set; }
        public IDbSet<BlogPost> BlogPost { get; set; }
        public IDbSet<BlogSettings> BlogSettings { get; set; }
        public IDbSet<BlogPostTagBlogPost> BlogPostTagBlogPost { get; set; }
        public IDbSet<BlogComment> BlogComment { get; set; }
        public IDbSet<BlogPostCategoryBlogPost> BlogPostCategoryBlogPost { get; set; }
        public IDbSet<CampaignDelivery> CampaignDelivery { get; set; }
        public IDbSet<Campaign> Campaign { get; set; }
        public IDbSet<Vendor> Vendor { get; set; }
        public IDbSet<VendorAddressType> VendorAddressType { get; set; }
        public IDbSet<VendorCategory> VendorCategory { get; set; }
        public IDbSet<VendorCategoryVendor> VendorCategoryVendor { get; set; }
        public IDbSet<VendorOpeningHours> VendorOpeningHours { get; set; }
        public IDbSet<PaymentType> PaymentType { get; set; }
        public IDbSet<VendorPaymentType> VendorPaymentType { get; set; }
        public IDbSet<VendorContactPerson> VendorContactPerson { get; set; }
        public IDbSet<Slider> Slider { get; set; }
        public IDbSet<Slide> Slide { get; set; }
        public IDbSet<VendorCategoryCount> VendorCategoryCount { get; set; }
        public IDbSet<VendorAddress> VendorAddress { get; set; }
        public IDbSet<TokenSettings> TokenSettings { get; set; }
        public IDbSet<TokenType> TokenType { get; set; }
        public IDbSet<Token> Token { get; set; }
        public IDbSet<Customer> Customer { get; set; }
        public IDbSet<Picture> Picture { get; set; }
        public IDbSet<VendorPicture> VendorPicture { get; set; }

        public IDbSet<ProductCategory> ProductCategory { get; set; }
        public IDbSet<ProductCategoryProduct> ProductCategoryProduct { get; set; }
        public IDbSet<Manufacturer> Manufacturer { get; set; }
        public IDbSet<Product> Product { get; set; }
        public IDbSet<ProductAttribute> ProductAttribute { get; set; }
        public IDbSet<ProductAttributeCombination> ProductAttributeCombination { get; set; }
        public IDbSet<ProductAttributeMapping> ProductAttributeMapping { get; set; }
        public IDbSet<ProductAttributeValue> ProductAttributeValue { get; set; }
        //public IDbSet<ProductCategoryProduct> ProductCategoryProduct { get; set; }
        public IDbSet<ProductManufacturer> ProductManufacturer { get; set; }
        public IDbSet<ProductPicture> ProductPicture { get; set; }
        public IDbSet<ProductReview> ProductReview { get; set; }
        public IDbSet<ProductReviewHelpfulness> ProductReviewHelpfulness { get; set; }
        public IDbSet<ProductSpecificationAttribute> ProductSpecificationAttribute { get; set; }
        public IDbSet<ProductTag> ProductTag { get; set; }
        public IDbSet<ProductTemplate> ProductTemplate { get; set; }
        public IDbSet<RelatedProduct> RelatedProduct { get; set; }
        public IDbSet<ProductVendor> ProductVendor { get; set; }
        public IDbSet<CustomerBookmark> CustomerBookmarks { get; set; }

        public IDbSet<CustomerMessage> CustomerMessage { get; set; }
        public IDbSet<CustomerMessageType> CustomerMessageType { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => !String.IsNullOrEmpty(type.Namespace))
            .Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
                type.BaseType.GetGenericTypeDefinition() == typeof(MiAmorEntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            //...or do it manually below. For example,
            //modelBuilder.Configurations.Add(new LanguageMap());

            base.OnModelCreating(modelBuilder);
        }

        //public override int SaveChanges()
        //{
        //    var modifiedEntries = ChangeTracker.Entries()
        //        .Where(x => x.Entity is IAuditableEntity
        //            && (x.State == System.Data.Entity.EntityState.Added || x.State == System.Data.Entity.EntityState.Modified));

        //    foreach (var entry in modifiedEntries)
        //    {
        //        IAuditableEntity entity = entry.Entity as IAuditableEntity;
        //        if (entity != null)
        //        {
        //            string identityName = Thread.CurrentPrincipal.Identity.Name;
        //            DateTime now = DateTime.UtcNow;

        //            if (entry.State == System.Data.Entity.EntityState.Added)
        //            {
        //                entity.CreatedBy = identityName;
        //                entity.CreatedDate = now;
        //            }
        //            else
        //            {
        //                base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
        //                base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
        //            }

        //            entity.UpdatedBy = identityName;
        //            entity.UpdatedDate = now;
        //        }
        //    }

        //    return base.SaveChanges();
        //}
    }
}
