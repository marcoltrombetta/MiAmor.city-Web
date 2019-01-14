/// <reference path="../../../vendor/angular-file-upload/angular-file-upload.js" />
/**=========================================================
 * Module: constants.js
 * Define constants to inject across the application
 =========================================================*/
App
  .constant('APP_COLORS', {
    'primary':                '#5d9cec',
    'success':                '#27c24c',
    'info':                   '#23b7e5',
    'warning':                '#ff902b',
    'danger':                 '#f05050',
    'inverse':                '#131e26',
    'green':                  '#37bc9b',
    'pink':                   '#f532e5',
    'purple':                 '#7266ba',
    'dark':                   '#3a3f51',
    'yellow':                 '#fad732',
    'gray-darker':            '#232735',
    'gray-dark':              '#3a3f51',
    'gray':                   '#dde6e9',
    'gray-light':             '#e4eaec',
    'gray-lighter':           '#edf1f2'
  })
  .constant('APP_MEDIAQUERY', {
    'desktopLG':             1200,
    'desktop':                992,
    'tablet':                 768,
    'mobile':                 480
  })
  .constant('APP_REQUIRES', {
    // jQuery based and standalone scripts
      scripts: {
        'classyloader': ['vendor/jquery-classyloader/js/jquery.classyloader.min.js'],
        'modernizr': ['vendor/modernizr/modernizr.js'],
        'animo': ['vendor/animo.js/animo.js'],
        'animate': ['vendor/animate.css/animate.min.css'],
        'icons': ['vendor/fontawesome/css/font-awesome.min.css',
                             'vendor/simple-line-icons/css/simple-line-icons.css'],
        'inputmask': ['vendor/jquery.inputmask/dist/jquery.inputmask.bundle.min.js'],
        'taginput': ['vendor/bootstrap-tagsinput/dist/bootstrap-tagsinput.css',
                            'vendor/bootstrap-tagsinput/dist/bootstrap-tagsinput.min.js'],
        'parsley': ['vendor/parsleyjs/dist/parsley.min.js'],
        'filestyle': ['vendor/bootstrap-filestyle/src/bootstrap-filestyle.js'],
        'flot-chart': ['vendor/Flot/jquery.flot.js'],
        'flot-chart-plugins': ['vendor/flot.tooltip/js/jquery.flot.tooltip.min.js',
                               'vendor/Flot/jquery.flot.resize.js',
                               'vendor/Flot/jquery.flot.pie.js',
                               'vendor/Flot/jquery.flot.time.js',
                               'vendor/Flot/jquery.flot.categories.js',
                               'vendor/flot-spline/js/jquery.flot.spline.min.js'],
    },
    // Angular based script (use the right module name)
    modules: [
    
          {name: 'ui.grid', files: ['vendor/angular-ui-grid/ui-grid.min.css?v=2',
                                   'vendor/angular-ui-grid/ui-grid.min.js',
                                   'vendor/angular-ui-grid/csv.js',
                                   'vendor/angular-ui-grid/pdfmake.js',
                                    'vendor/angular-ui-grid/vfs_fonts.js']},
         {name: 'ui.select',                 files: ['vendor/angular-ui-select/dist/select.js',
                                            'vendor/angular-ui-select/dist/select.css']
         },
          { name: 'angularFileUpload', files: ['vendor/angular-file-upload/angular-file-upload.js'] },
          {name: 'localytics.directives',     files: ['vendor/chosen_v1.2.0/chosen.jquery.min.js',
                                                 'vendor/chosen_v1.2.0/chosen.min.css',
                                                 'vendor/angular-chosen-localytics/chosen.js']}
    ]
  })
;