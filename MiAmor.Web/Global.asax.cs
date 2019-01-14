using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac.Integration.Mvc;
using Autofac;
using MiAmor.Builder;
using System.Data.Entity;
using MiAmor.Data;
using System.Web.Optimization;
using AutoMapper;
using MiAmor.Core;
using MiAmor.Web.Models;
using MiAmor.Services;
using MiAmor.Web.Areas.Admin.Models;


namespace MiAmor.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            setBuilder();
            setAutoMapperModels();
            Database.SetInitializer<MiAmorContext>(new CreateDatabaseIfNotExists<MiAmorContext>());
            //using (var cat = new MiAmorContext())
            //{
            //    cat.Database.Initialize(false);
            //}
        }
        #region set functions
        private void setBuilder()
        {
            var builder = new Autofac.ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();


            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new EFModule());
            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().Named<ICacheManager>("miamor_cache_static").SingleInstance();

            
            builder.RegisterType<Encryption>().As<IEncryption>().SingleInstance();
            builder.Register(c => new Encryption(c.Resolve<ITokenSettingsService>()));
            //builder.RegisterType<Encryption>().UsingConstructor(typeof(ITokenSettingsService)).SingleInstance();           
            
            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
        private void setAutoMapperModels()
        {
            Mapper.CreateMap<Vendor, VendorBoxModel>();
            Mapper.CreateMap<Vendor, VendorBoxModelMobile>();
            Mapper.CreateMap<Customer, CustomerRegistration>();
            Mapper.CreateMap<CustomerRegistration, Customer>();
            Mapper.CreateMap<CustomerDetails, Customer>();
            Mapper.CreateMap<Customer, CustomerDetails>();
            Mapper.CreateMap<CustomerCrudModel, Customer>();
            Mapper.CreateMap<Customer, CustomerCrudModel>();
            Mapper.CreateMap<VendorCrudModel, Vendor>();
            Mapper.CreateMap<Vendor, VendorCrudModel>();
            Mapper.CreateMap<Campaign, CampaignModel>();
            Mapper.CreateMap<CampaignModel, Campaign>();
            Mapper.CreateMap<VendorReview, VendorReviewModel>();
            Mapper.CreateMap<VendorReviewModel, VendorReview>();
            Mapper.CreateMap<VendorAddressCrudModel, VendorAddress>();
            Mapper.CreateMap<VendorAddress, VendorAddressCrudModel>();
            Mapper.CreateMap<CustomerProfile, Customer>();
            Mapper.CreateMap<Customer, CustomerProfile>();
            Mapper.CreateMap<CustomerReviewVendorModel, VendorReview>();
            Mapper.CreateMap<VendorReview, CustomerReviewVendorModel>();
            Mapper.CreateMap<CustomerMessage, CustomerMessageModel>();
            Mapper.CreateMap<CustomerMessageModel, CustomerMessage>();
            Mapper.CreateMap<NeighbourhoodModel, Neighbourhood>();
            Mapper.CreateMap<Neighbourhood, NeighbourhoodModel>();
            Mapper.CreateMap<ProductCrudModel, Product>();
            Mapper.CreateMap<Product, ProductCrudModel>();
            Mapper.CreateMap<ManufacturerCrudModel, Manufacturer>();
            Mapper.CreateMap<Manufacturer, ManufacturerCrudModel>();
            Mapper.CreateMap<ProductAttributeCrudModel, ProductAttribute>();
            Mapper.CreateMap<ProductAttribute, ProductAttributeCrudModel>();
            Mapper.CreateMap<ProductCategoryCrudModel, ProductCategory>();
            Mapper.CreateMap<ProductCategory, ProductCategoryCrudModel>();
            Mapper.CreateMap<VendorCategoryCrudModel, VendorCategory>();
            Mapper.CreateMap<VendorCategory, VendorCategoryCrudModel>();
            Mapper.CreateMap<VendorCategoryCountCrudModel, VendorCategoryCount>();
            Mapper.CreateMap<VendorCategoryCount, VendorCategoryCountCrudModel>();
            Mapper.CreateMap<ProductTagCrudModel, ProductTag>();
            Mapper.CreateMap<ProductTag, ProductTagCrudModel>();
            Mapper.CreateMap<ProductReviewCrudModel, ProductReview>();
            Mapper.CreateMap<ProductReview, ProductReviewCrudModel>();
            Mapper.CreateMap<CampaignCrudModel, Campaign>();
            Mapper.CreateMap<Campaign, CampaignCrudModel>();
            Mapper.CreateMap<CampaignDeliveryCrudModel, CampaignDelivery>();
            Mapper.CreateMap<CampaignDelivery, CampaignDeliveryCrudModel>();
            Mapper.CreateMap<BlogPost, BlogModel>();
            Mapper.CreateMap<BlogModel, BlogPost>();
            Mapper.CreateMap<BlogPost, BlogPostAppModel>();
            Mapper.CreateMap<BlogPostAppModel, BlogPost>();
            Mapper.CreateMap<Vendor, VendorAutocompleteModel>();
            Mapper.CreateMap<VendorAutocompleteModel, Vendor>();
            Mapper.CreateMap<Vendor, VendorAutocompleteModel>()
             .ForMember(dest => dest.vendorId, opt => opt.MapFrom(src => src.Id));
            
             Mapper.CreateMap<CityAutocompleteModel, City>();
             Mapper.CreateMap<City, CityAutocompleteModel>()
                .ForMember(dest => dest.cityId, opt => opt.MapFrom(src => src.Id));

             Mapper.CreateMap<NeighbourhoodAutocompleteModel, Neighbourhood>();
             Mapper.CreateMap<Neighbourhood, NeighbourhoodAutocompleteModel>()
                .ForMember(dest => dest.neighbourhoodId, opt => opt.MapFrom(src => src.Id));

             Mapper.CreateMap<ParentCategoryAutocompleteModel, ProductCategory>();
             Mapper.CreateMap<ProductCategory, ParentCategoryAutocompleteModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

             Mapper.CreateMap<PriceListCrudModel, PriceList>();
             Mapper.CreateMap<PriceList, PriceListCrudModel>();
             Mapper.CreateMap<CampaignAttributeCrudModel, CampaignAttribute>();
             Mapper.CreateMap<CampaignAttribute, CampaignAttributeCrudModel>();
             Mapper.CreateMap<CampaignStatusCrudModel, CampaignStatus>();
             Mapper.CreateMap<CampaignStatus, CampaignStatusCrudModel>();
             Mapper.CreateMap<CampaignTagCrudModel, CampaignTag>();
             Mapper.CreateMap<CampaignTag, CampaignTagCrudModel>();
             Mapper.CreateMap<CampaignPaymentsCrudModel, CampaignPayment>();
             Mapper.CreateMap<CampaignPayment, CampaignPaymentsCrudModel>();
             Mapper.CreateMap<CustomerAutocompleteModel, Customer>();
             Mapper.CreateMap<Customer, CustomerAutocompleteModel>()
                .ForMember(dest => dest.customerId, opt => opt.MapFrom(src => src.Id));

             Mapper.CreateMap<CampaignAutocompleteModel, Campaign>();
             Mapper.CreateMap<Campaign, CampaignAutocompleteModel>()
                .ForMember(dest => dest.campaignId, opt => opt.MapFrom(src => src.Id));

             Mapper.CreateMap<SliderModel, Slider>();
             Mapper.CreateMap<Slider, SliderModel>();

             Mapper.CreateMap<SlideModel, Slide>();
             Mapper.CreateMap<Slide, SlideModel>();

             Mapper.CreateMap<BlogCommentCrudModel, BlogComment>();
             Mapper.CreateMap<BlogComment, BlogCommentCrudModel>();

             Mapper.CreateMap<BlogPostAutocompleteModel, BlogPost>();
             Mapper.CreateMap<BlogPost, BlogPostAutocompleteModel>()
                .ForMember(dest => dest.blogPostId, opt => opt.MapFrom(src => src.Id));


             Mapper.CreateMap<BlogPostTagCrudModel, BlogPostTag>();
             Mapper.CreateMap<BlogPostTag, BlogPostTagCrudModel>();            
             Mapper.CreateMap<BlogPostCrudModel, BlogPost>();
             Mapper.CreateMap<BlogPost, BlogPostCrudModel>();

            
             Mapper.CreateMap<ParentWebSiteBlogPost, BlogModel>();
             Mapper.CreateMap<BlogModel, ParentWebSiteBlogPost>();


             Mapper.CreateMap<Message, ContactUsModel>();
             Mapper.CreateMap<ContactUsModel, Message>();
        }
        #endregion
    }
}
