/**=========================================================
 * Module: config.js
 * App routes and resources configuration
 sdfkjshdfkjhsdfkjshdkfj
 =========================================================*/

App.config(['$stateProvider', '$locationProvider', '$urlRouterProvider', 'RouteHelpersProvider',
function ($stateProvider, $locationProvider, $urlRouterProvider, helper) {
  'use strict';

  // Set the following to true to enable the HTML5 Mode
  // You may have to set <base> tag in index and a routing configuration in your server
  $locationProvider.html5Mode(false);

  // default route
  $urlRouterProvider.otherwise('/app/dashboard');

  // 
  // Application Routes
  // -----------------------------------   
    $stateProvider
    .state('app', {
        url: '/app',
        abstract: true,
        templateUrl: helper.basepath('app.html'),
        controller: 'AppController',
        resolve: helper.resolveFor('modernizr', 'icons', 'animo', 'classyloader')
    })
        //dashboard
    .state('app.dashboard', {
        url: '/dashboard',
        title: 'Dashboard',
        templateUrl: helper.basepath('dashboard.html'),
        resolve: helper.resolveFor('flot-chart', 'flot-chart-plugins')
    })
        //file upload
    .state('app.form-upload', {
        url: '/form-upload',
        title: 'Form upload',
        templateUrl: helper.basepath('form-upload.html'),
        resolve: helper.resolveFor('angularFileUpload', 'filestyle')
    })   
      //CUSTOMER
    .state('app.mainCustomer', {
        url: '/mainCustomer',
        templateUrl: helper.basepath('customer/mainCustomer.html'),
        resolve: helper.resolveFor('ui.grid')
    })
    .state('app.mainCustomer.details', {
        url: '/mainCustomer/details',
        title: 'Form Wizard',
        templateUrl: helper.basepath('customer/customerDetails.html?v=1'),
        resolve: helper.resolveFor('parsley')
    })
    .state('app.mainCustomer.details.Campaigns', {
        url: '/mainCustomer/details/Campaigns',
        templateUrl: helper.basepath('customer/customerCampaigns.html')
    })
    .state('app.mainCustomer.details.contact', {
        url: '/mainCustomer/details/contact',
        templateUrl: helper.basepath('customer/customerContact.html'),
        resolve: helper.resolveFor('ui.grid')
    })
    .state('app.mainCustomer.details.docs', {
        url: '/mainCustomer/details/docs',
        templateUrl: helper.basepath('customer/customerDocs.html'),
        resolve: helper.resolveFor('ui.grid')
    })
    .state('app.mainCustomer.gridCustomer', {
        url: '/mainCustomer/gridCustomer',
        templateUrl: helper.basepath('customer/gridCustomer.html'),
        resolve: helper.resolveFor('ui.grid')
    })   
    .state('app.mainCustomer.newCustomer', {
        url: '/newCustomer',
        templateUrl: helper.basepath('customer/newCustomer.html'),
        resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
    })
    .state('app.mainCustomer.editCustomer', {
          url: '/editCustomer?id',
        templateUrl: helper.basepath('customer/newCustomer.html'),
        resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
    })
    .state('app.table-vendor', {
        url: '/table-vendor',
        templateUrl: helper.basepath('table-vendor.html'),
        resolve: helper.resolveFor('ui.grid')
    })
    .state('app.buttons', {
        url: '/buttons',
        title: 'Buttons',
        templateUrl: helper.basepath('buttons.html')
    })
    .state('app.singleview', {
        url: '/singleview',
        title: 'Single View',
        templateUrl: helper.basepath('singleview.html')
    })
    .state('app.animations', {
        url: '/animations',
        title: 'Animations',
        templateUrl: helper.basepath('animations.html')
    })
    .state('app.submenu', {
        url: '/submenu',
        title: 'Submenu',
        templateUrl: helper.basepath('submenu.html')
    })

      //VENDOR
     .state('app.mainVendor', {
         url: '/mainVendor',
         templateUrl: helper.basepath('vendor/mainVendor.html'),
         resolve: helper.resolveFor('ui.grid')
     })
      .state('app.mainVendor.gridVendor', {
          url: '/mainVendor/gridVendor',
          templateUrl: helper.basepath('vendor/gridVendor.html'),
          resolve: helper.resolveFor('ui.grid')
      })
    .state('app.mainVendor.editVendor', {
        url: '/editVendor?id',
        templateUrl: helper.basepath('vendor/newVendor.html'),
        resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
    })
     .state('app.mainVendor.newVendor', {
         url: '/newVendor',
         templateUrl: helper.basepath('vendor/newVendor.html'),
         resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
     })

      //PRODUCT
    .state('app.mainProduct', {
        url: '/mainProduct',
        templateUrl: helper.basepath('product/mainProduct.html'),
        resolve: helper.resolveFor('ui.grid')
    })
    .state('app.mainProduct.gridProduct', {
        url: '/mainProduct/gridProduct',
        templateUrl: helper.basepath('product/gridProduct.html'),
        resolve: helper.resolveFor('ui.grid')
    })
     .state('app.mainProduct.editProduct', {
         url: '/editProduct?id',
         templateUrl: helper.basepath('product/newProduct.html'),
         resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
     })
     .state('app.mainProduct.newProduct', {
         url: '/newProduct',
         templateUrl: helper.basepath('product/newProduct.html'),
         resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
     })

      //MANUFACTURER
    .state('app.mainManufacturer', {
        url: '/mainManufacturer',
        templateUrl: helper.basepath('manufacturer/mainManufacturer.html'),
        resolve: helper.resolveFor('ui.grid')
    })
     .state('app.mainManufacturer.gridManufacturer', {
         url: '/mainManufacturer/gridManufacturer',
         templateUrl: helper.basepath('manufacturer/gridManufacturer.html'),
         resolve: helper.resolveFor('ui.grid')
     })
     .state('app.mainManufacturer.newManufacturer', {
         url: '/newManufacturer',
         templateUrl: helper.basepath('manufacturer/newManufacturer.html'),
         resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
     })
    .state('app.mainManufacturer.editManufacturer', {
        url: '/editManufacturer?id',
        templateUrl: helper.basepath('manufacturer/newManufacturer.html'),
        resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
    })

      //PRODUCT ATTRIBUTE
    .state('app.mainProductAttribute', {
        url: '/mainProductAttribute',
        templateUrl: helper.basepath('productAttribute/mainProductAttribute.html'),
        resolve: helper.resolveFor('ui.grid')
    })
     .state('app.mainProductAttribute.gridProductAttribute', {
         url: '/mainProductAttribute/gridProductAttribute',
         templateUrl: helper.basepath('productAttribute/gridProductAttribute.html'),
         resolve: helper.resolveFor('ui.grid')
     })
   .state('app.mainProductAttribute.newProductAttribute', {
       url: '/newProductAttribute',
       templateUrl: helper.basepath('productAttribute/newProductAttribute.html'),
       resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
   })
    .state('app.mainProductAttribute.editProductAttribute', {
        url: '/editProductAttribute?id',
        templateUrl: helper.basepath('productAttribute/newProductAttribute.html'),
        resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
    })

      //PRODUCT CATEGORY
     .state('app.mainProductCategory', {
         url: '/mainProductCategory',
         templateUrl: helper.basepath('productCategory/mainProductCategory.html'),
         resolve: helper.resolveFor('ui.grid')
     })
     .state('app.mainProductCategory.gridProductCategory', {
         url: '/mainProductCategory/gridProductCategory',
         templateUrl: helper.basepath('productCategory/gridProductCategory.html'),
         resolve: helper.resolveFor('ui.grid')
     })
   .state('app.mainProductCategory.newProductCategory', {
       url: '/newProductCategory',
       templateUrl: helper.basepath('productCategory/newProductCategory.html'),
       resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
   })
    .state('app.mainProductCategory.editProductCategory', {
        url: '/editProductCategory?id',
        templateUrl: helper.basepath('productCategory/newProductCategory.html'),
        resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
    })

      //VENDOR CATEGORY       
    .state('app.mainVendorCategory', {
             url: '/mainVendorCategory',
             templateUrl: helper.basepath('vendorCategory/mainVendorCategory.html'),
             resolve: helper.resolveFor('ui.grid')
     })
     .state('app.mainVendorCategory.gridVendorCategory', {
         url: '/mainVendorCategory/gridVendorCategory',
         templateUrl: helper.basepath('vendorCategory/gridVendorCategory.html'),
         resolve: helper.resolveFor('ui.grid')
     })
   .state('app.mainVendorCategory.newVendorCategory', {
       url: '/newVendorCategory',
       templateUrl: helper.basepath('vendorCategory/newVendorCategory.html'),
       resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
     })
   .state('app.mainVendorCategory.editVendorCategory', {
        url: '/editVendorCategory?id',
        templateUrl: helper.basepath('vendorCategory/newVendorCategory.html'),
        resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
   })
    
    //VENDOR CATEGORY COUNT
     .state('app.mainVendorCategoryCount', {
         url: '/mainVendorCategoryCount',
         templateUrl: helper.basepath('vendorCategoryCount/mainVendorCategoryCount.html'),
         resolve: helper.resolveFor('ui.grid')
     })
     .state('app.mainVendorCategoryCount.gridVendorCategoryCount', {
         url: '/mainVendorCategoryCount/gridVendorCategoryCount',
         templateUrl: helper.basepath('vendorCategoryCount/gridVendorCategoryCount.html'),
         resolve: helper.resolveFor('ui.grid')
     })
   .state('app.mainVendorCategoryCount.newVendorCategoryCount', {
       url: '/newVendorCategoryCount',
       templateUrl: helper.basepath('vendorCategoryCount/newVendorCategoryCount.html'),
       resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
   })
   .state('app.mainVendorCategoryCount.editVendorCategoryCount', {
       url: '/editVendorCategoryCount?id',
       templateUrl: helper.basepath('vendorCategoryCount/newVendorCategoryCount.html'),
       resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
   })

    //PRODUCT TAG
      .state('app.mainProductTag', {
          url: '/mainProductTag',
          templateUrl: helper.basepath('productTag/mainProductTag.html'),
          resolve: helper.resolveFor('ui.grid')
      })
     .state('app.mainProductTag.gridProductTag', {
         url: '/mainProductTag/gridProductTag',
         templateUrl: helper.basepath('productTag/gridProductTag.html'),
         resolve: helper.resolveFor('ui.grid')
     })
   .state('app.mainProductTag.newProductTag', {
       url: '/newProductTag',
       templateUrl: helper.basepath('productTag/newProductTag.html'),
       resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
   })
   .state('app.mainProductTag.editProductTag', {
       url: '/editProductTag?id',
       templateUrl: helper.basepath('productTag/newProductTag.html'),
       resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
   })
    //PRODUCT REVIEW
      .state('app.mainProductReview', {
          url: '/mainProductReview',
          templateUrl: helper.basepath('productReview/mainProductReview.html'),
          resolve: helper.resolveFor('ui.grid')
      })
     .state('app.mainProductReview.gridProductReview', {
         url: '/mainProductReview/gridProductReview',
         templateUrl: helper.basepath('productReview/gridProductReview.html'),
         resolve: helper.resolveFor('ui.grid')
     })
   .state('app.mainProductReview.newProductReview', {
       url: '/newProductReview',
       templateUrl: helper.basepath('productReview/newProductReview.html'),
       resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
   })
   .state('app.mainProductReview.editProductReview', {
       url: '/editProductReview?id',
       templateUrl: helper.basepath('productReview/newProductReview.html'),
       resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
   })

    //BLOG POST
      .state('app.mainBlogPost', {
          url: '/mainBlogPost',
          templateUrl: helper.basepath('blogPost/mainBlogPost.html'),
          resolve: helper.resolveFor('ui.grid')
      })
     .state('app.mainBlogPost.gridBlogPost', {
         url: '/mainProductReview/gridBlogPost',
         templateUrl: helper.basepath('blogPost/gridBlogPost.html'),
         resolve: helper.resolveFor('ui.grid')
     })
   .state('app.mainBlogPost.newBlogPost', {
       url: '/newBlogPost',
       templateUrl: helper.basepath('blogPost/newBlogPost.html'),
       resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
   })
   .state('app.mainBlogPost.editBlogPost', {
       url: '/editBlogPost?id',
       templateUrl: helper.basepath('blogPost/newBlogPost.html'),
       resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
   })

    //CAMPAIGN
      .state('app.mainCampaign', {
          url: '/mainCampaign',
          templateUrl: helper.basepath('campaign/mainCampaign.html'),
          resolve: helper.resolveFor('ui.grid')
      })
     .state('app.mainCampaign.gridCampaign', {
         url: '/mainCampaign/gridCampaign',
         templateUrl: helper.basepath('campaign/gridCampaign.html'),
         resolve: helper.resolveFor('ui.grid')
     })
   .state('app.mainCampaign.newCampaign', {
       url: '/newCampaign',
       templateUrl: helper.basepath('campaign/newCampaign.html'),
       resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
   })
   .state('app.mainCampaign.editCampaign', {
       url: '/editCampaign?id',
       templateUrl: helper.basepath('campaign/newCampaign.html'),
       resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
   })

    //CAMPAIGN DELIVERY
     .state('app.mainCampaignDelivery', {
         url: '/mainCampaignDelivery',
         templateUrl: helper.basepath('campaignDelivery/mainCampaignDelivery.html'),
              resolve: helper.resolveFor('ui.grid')
          })
     .state('app.mainCampaignDelivery.gridCampaignDelivery', {
         url: '/mainCampaignDelivery/gridCampaignDelivery',
         templateUrl: helper.basepath('campaignDelivery/gridCampaignDelivery.html'),
         resolve: helper.resolveFor('ui.grid')
     })
   .state('app.mainCampaignDelivery.newCampaignDelivery', {
       url: '/newCampaignDelivery',
       templateUrl: helper.basepath('campaignDelivery/newCampaignDelivery.html'),
       resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
   })
   .state('app.mainCampaignDelivery.editCampaignDelivery', {
       url: '/editCampaignDelivery?id',
       templateUrl: helper.basepath('campaignDelivery/newCampaignDelivery.html'),
       resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
   })
    //PRICE LIST
     .state('app.mainPriceList', {
         url: '/mainPriceList',
         templateUrl: helper.basepath('priceList/mainPriceList.html'),
         resolve: helper.resolveFor('ui.grid')
     })
     .state('app.mainPriceList.gridPriceList', {
         url: '/mainPriceList/gridPriceList',
         templateUrl: helper.basepath('priceList/gridPriceList.html'),
         resolve: helper.resolveFor('ui.grid')
     })
   .state('app.mainPriceList.newPriceList', {
       url: '/newPriceList',
       templateUrl: helper.basepath('priceList/newPriceList.html'),
       resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
   })
   .state('app.mainPriceList.editPriceList', {
       url: '/editPriceList?id',
       templateUrl: helper.basepath('priceList/newPriceList.html'),
       resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
   })

    //CAMPAIGN ATTRIBUTE
     .state('app.mainCampaignAttribute', {
         url: '/mainCampaignAttribute',
         templateUrl: helper.basepath('campaignAttribute/mainCampaignAttribute.html'),
         resolve: helper.resolveFor('ui.grid')
     })
     .state('app.mainCampaignAttribute.gridCampaignAttribute', {
         url: '/mainCampaignAttribute/gridCampaignAttribute',
         templateUrl: helper.basepath('campaignAttribute/gridCampaignAttribute.html'),
         resolve: helper.resolveFor('ui.grid')
     })
   .state('app.mainCampaignAttribute.newCampaignAttribute', {
       url: '/newCampaignAttribute',
       templateUrl: helper.basepath('campaignAttribute/newCampaignAttribute.html'),
       resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
   })
   .state('app.mainCampaignAttribute.editCampaignAttribute', {
       url: '/editCampaignAttribute?id',
       templateUrl: helper.basepath('campaignAttribute/newCampaignAttribute.html'),
       resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
   })

    //CAMPAIGN STATUS
         .state('app.mainCampaignStatus', {
             url: '/mainCampaignStatus',
             templateUrl: helper.basepath('campaignStatus/mainCampaignStatus.html'),
             resolve: helper.resolveFor('ui.grid')
         })
     .state('app.mainCampaignStatus.gridCampaignStatus', {
         url: '/mainCampaignStatus/gridCampaignStatus',
         templateUrl: helper.basepath('campaignStatus/gridCampaignStatus.html'),
         resolve: helper.resolveFor('ui.grid')
     })
   .state('app.mainCampaignStatus.newCampaignStatus', {
       url: '/newCampaignStatus',
       templateUrl: helper.basepath('campaignStatus/newCampaignStatus.html'),
       resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
   })
   .state('app.mainCampaignStatus.editCampaignStatus', {
       url: '/editCampaignStatus?id',
       templateUrl: helper.basepath('campaignStatus/newCampaignStatus.html'),
       resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
   })

    //CAMPAIGN TAG
             .state('app.mainCampaignTag', {
                 url: '/mainCampaignTag',
                 templateUrl: helper.basepath('campaignTag/mainCampaignTag.html'),
                 resolve: helper.resolveFor('ui.grid')
             })
     .state('app.mainCampaignTag.gridCampaignTag', {
         url: '/mainCampaignTag/gridCampaignTag',
         templateUrl: helper.basepath('campaignTag/gridCampaignTag.html'),
         resolve: helper.resolveFor('ui.grid')
     })
   .state('app.mainCampaignTag.newCampaignTag', {
       url: '/newCampaignTag',
       templateUrl: helper.basepath('campaignTag/newCampaignTag.html'),
       resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
   })
   .state('app.mainCampaignTag.editCampaignTag', {
       url: '/editCampaignTag?id',
       templateUrl: helper.basepath('campaignTag/newCampaignTag.html'),
       resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
   })

    //CAMPAIGN PAYMENTS
            .state('app.mainCampaignPayments', {
                url: '/mainCampaignPayments',
                templateUrl: helper.basepath('campaignPayments/mainCampaignPayments.html'),
                resolve: helper.resolveFor('ui.grid')
            })
     .state('app.mainCampaignPayments.gridCampaignPayments', {
         url: '/mainCampaignPayments/gridCampaignPayments',
         templateUrl: helper.basepath('campaignPayments/gridCampaignPayments.html'),
         resolve: helper.resolveFor('ui.grid')
     })
   .state('app.mainCampaignPayments.newCampaignPayments', {
       url: '/newCampaignPayments',
       templateUrl: helper.basepath('campaignPayments/newCampaignPayments.html'),
       resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
   })
   .state('app.mainCampaignPayments.editCampaignPayments', {
       url: '/editCampaignPayments?id',
       templateUrl: helper.basepath('campaignPayments/newCampaignPayments.html'),
       resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
   })

    //BLOG COMMENT
                .state('app.mainBlogComment', {
                    url: '/mainBlogComment',
                    templateUrl: helper.basepath('blogComment/mainBlogComment.html'),
                    resolve: helper.resolveFor('ui.grid')
                })
     .state('app.mainBlogComment.gridBlogComment', {
         url: '/mainBlogComment/gridBlogComment',
         templateUrl: helper.basepath('blogComment/gridBlogComment.html'),
         resolve: helper.resolveFor('ui.grid')
     })
   .state('app.mainBlogComment.newBlogComment', {
       url: '/newBlogComment',
       templateUrl: helper.basepath('blogComment/newBlogComment.html'),
       resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
   })
   .state('app.mainBlogComment.editBlogComment', {
       url: '/editBlogComment?id',
       templateUrl: helper.basepath('blogComment/newBlogComment.html'),
       resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
   })

    //BLOG POST TAG
     .state('app.mainBlogPostTag', {
         url: '/mainBlogPostTag',
         templateUrl: helper.basepath('blogPostTag/mainBlogPostTag.html'),
         resolve: helper.resolveFor('ui.grid')
     })
     .state('app.mainBlogPostTag.gridBlogPostTag', {
         url: '/mainBlogPostTag/gridBlogPostTag',
         templateUrl: helper.basepath('blogPostTag/gridBlogPostTag.html'),
         resolve: helper.resolveFor('ui.grid')
     })
   .state('app.mainBlogPostTag.newBlogPostTag', {
       url: '/newBlogPostTag',
       templateUrl: helper.basepath('blogPostTag/newBlogPostTag.html'),
       resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
   })
   .state('app.mainBlogPostTag.editBlogPostTag', {
       url: '/editBlogPostTag?id',
       templateUrl: helper.basepath('blogPostTag/newBlogPostTag.html'),
       resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
   })

    
    //EVENT POST 
     .state('app.mainEventPost', {
         url: '/mainEventPost',
         templateUrl: helper.basepath('eventPost/mainEventPost.html'),
         resolve: helper.resolveFor('ui.grid')
     })
     .state('app.mainEventPost.gridEventPost', {
         url: '/mainEventPost/gridEventPost',
         templateUrl: helper.basepath('eventPost/gridEventPost.html'),
         resolve: helper.resolveFor('ui.grid')
     })
   .state('app.mainEventPost.newEventPost', {
       url: '/newEventPost',
       templateUrl: helper.basepath('eventPost/newEventPost.html'),
       resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
   })
   .state('app.mainEventPost.editEventPost', {
       url: '/editEventPost?id',
       templateUrl: helper.basepath('eventPost/newEventPost.html'),
       resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
   })

    
    //EVENT POST TAG
     .state('app.mainEventPostTag', {
         url: '/mainEventPostTag',
         templateUrl: helper.basepath('eventPostTag/mainEventPostTag.html'),
         resolve: helper.resolveFor('ui.grid')
     })
     .state('app.mainEventPostTag.gridEventPostTag', {
         url: '/mainEventPostTag/gridEventPostTag',
         templateUrl: helper.basepath('eventPostTag/gridEventPostTag.html'),
         resolve: helper.resolveFor('ui.grid')
     })
   .state('app.mainEventPostTag.newEventPostTag', {
       url: '/newEventPostTag',
       templateUrl: helper.basepath('eventPostTag/newEventPostTag.html'),
       resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
   })
   .state('app.mainEventPostTag.editEventPostTag', {
       url: '/editEventPostTag?id',
       templateUrl: helper.basepath('eventPostTag/newEventPostTag.html'),
       resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
   })


    //EVENT COMMENT
      .state('app.mainEventComment', {
          url: '/mainEventComment',
          templateUrl: helper.basepath('eventComment/mainEventComment.html'),
          resolve: helper.resolveFor('ui.grid')
      })
     .state('app.mainEventComment.gridEventComment', {
         url: '/mainEventComment/gridEventComment',
         templateUrl: helper.basepath('eventComment/gridEventComment.html'),
         resolve: helper.resolveFor('ui.grid')
     })
   .state('app.mainEventComment.newEventComment', {
       url: '/newEventComment',
       templateUrl: helper.basepath('eventComment/newEventComment.html'),
       resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
   })
   .state('app.mainEventComment.editEventComment', {
       url: '/editEventComment?id',
       templateUrl: helper.basepath('eventComment/newEventComment.html'),
       resolve: helper.resolveFor('ui.select', 'taginput', 'inputmask', 'localytics.directives')
   })


    // 
    // CUSTOM RESOLVES
    //   Add your own resolves properties
    //   following this object extend
    //   method
    // ----------------------------------- 
    // .state('app.someroute', {
    //   url: '/some_url',
    //   templateUrl: 'path_to_template.html',
    //   controller: 'someController',
    //   resolve: angular.extend(
    //     helper.resolveFor(), {
    //     // YOUR RESOLVES GO HERE
    //     }
    //   )
    // })
    ;


}]).config(['$ocLazyLoadProvider', 'APP_REQUIRES', function ($ocLazyLoadProvider, APP_REQUIRES) {
    'use strict';

    // Lazy Load modules configuration
    $ocLazyLoadProvider.config({
      debug: false,
      events: true,
      modules: APP_REQUIRES.modules
    });

}]).config(['$controllerProvider', '$compileProvider', '$filterProvider', '$provide',
    function ( $controllerProvider, $compileProvider, $filterProvider, $provide) {
      'use strict';
      // registering components after bootstrap
      App.controller = $controllerProvider.register;
      App.directive  = $compileProvider.directive;
      App.filter     = $filterProvider.register;
      App.factory    = $provide.factory;
      App.service    = $provide.service;
      App.constant   = $provide.constant;
      App.value      = $provide.value;

}]).config(['$translateProvider', function ($translateProvider) {

    $translateProvider.useStaticFilesLoader({
        prefix: '/areas/Admin/app/i18n/',
        suffix : '.json'
    });
    $translateProvider.preferredLanguage('en');
    $translateProvider.useLocalStorage();
    $translateProvider.usePostCompiling(true);

}]).config(['cfpLoadingBarProvider', function(cfpLoadingBarProvider) {
    cfpLoadingBarProvider.includeBar = true;
    cfpLoadingBarProvider.includeSpinner = false;
    cfpLoadingBarProvider.latencyThreshold = 500;
    cfpLoadingBarProvider.parentSelector = '.wrapper > section';
}]).config(['$tooltipProvider', function ($tooltipProvider) {

    $tooltipProvider.options({appendToBody: true});

}])
;
