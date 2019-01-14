using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MiAmor.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
          name: "BlogPost", // Route name
          url: "Blog/BlogPost/{Id}",  // URL with parameters
          defaults: new { controller = "Blog", action = "BlogPost", Id = UrlParameter.Optional }  // Parameter defaults
              );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{CategoryId}/{PageNumber}",
                defaults: new { controller = "Home", action = "Index", CategoryId = UrlParameter.Optional, PageNumber = UrlParameter.Optional }
            );


            routes.MapRoute(
            name: "MainHeader", // Route name
            url: "Home/MainHeader",  // URL with parameters
            defaults: new { controller = "Home", action = "MainHeader"}  // Parameter defaults
                );
            
            routes.MapRoute(
                name: "CustomerDetailsLeftbar", // Route name
                url: "Customer/CustomerDetailsLeftbar",  // URL with parameters
                defaults: new { controller = "Customer", action = "CustomerDetailsLeftbar" }  // Parameter defaults
            );

           routes.MapRoute(
            name: "Vendor", // Route name
            url: "Vendor/{Id}/{ContentId}/{PageNumber}",  // URL with parameters
            defaults: new { controller = "Vendor", action = "Index", Id = UrlParameter.Optional, ContentId = UrlParameter.Optional, PageNumber = UrlParameter.Optional }  // Parameter defaults
                );

           routes.MapRoute(
            name: "Customer", // Route name
            url: "Customer/CategorySearchCombo",  // URL with parameters
            defaults: new { controller = "Customer", action = "Index"}  // Parameter defaults
                );

            routes.MapRoute(
                    name: "CategorySearchCombo", // Route name
                    url: "Home/CategorySearchCombo",  // URL with parameters
                    defaults: new { controller = "Home", action = "CategorySearchCombo" }  // Parameter defaults
             );

            routes.MapRoute(
                name: "MainFooter", // Route name
                url: "Home/MainFooter",  // URL with parameters
                defaults: new { controller = "Home", action = "MainFooter" }  // Parameter defaults
            );
                    //new
           routes.MapRoute(
            name: "CustomerBookmarks", // Route name
            url: "Customer/GetCustomerBookmarks/{PageNumber}",  // URL with parameters
            defaults: new { controller = "Customer", action = "GetCustomerBookmarks", PageNumber = UrlParameter.Optional }  // Parameter defaults
            );

           routes.MapRoute(
         name: "CustomerMessages", // Route name
         url: "Customer/GetCustomerMessages/{PageNumber}",  // URL with parameters
         defaults: new { controller = "Customer", action = "GetCustomerMessages", PageNumber = UrlParameter.Optional }  // Parameter defaults
         );

           routes.MapRoute(
         name: "GetCustomerQuickiesBookmarks", // Route name
         url: "Customer/GetCustomerQuickiesBookmarks/{PageNumber}",  // URL with parameters
         defaults: new { controller = "Customer", action = "GetCustomerQuickiesBookmarks", PageNumber = UrlParameter.Optional }  // Parameter defaults
         );

            routes.MapRoute(
            name: "GetCustomerQuickiesMessages", // Route name
            url: "Customer/GetCustomerQuickiesMessages/{PageNumber}",  // URL with parameters
            defaults: new { controller = "Customer", action = "GetCustomerQuickiesMessages", PageNumber = UrlParameter.Optional }  // Parameter defaults
            );

            routes.MapRoute(
          name: "VendorCompanyBlogItems", // Route name
          url: "Vendor/VendorCompanyBlogItems/{PageNumber}/{BlogPostType}",  // URL with parameters
          defaults: new { controller = "Vendor", action = "VendorCompanyBlogItems", PageNumber = UrlParameter.Optional, BlogPostType = UrlParameter.Optional }  // Parameter defaults
          );
            //new
            
        }
    }
}
