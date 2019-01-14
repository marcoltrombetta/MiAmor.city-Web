using System;
using System.Linq;
using MiAmor.Core;
using MiAmor.Services;
using MiAmor.Data;
using System.Globalization;

namespace MiAmor.Services
{
    /// <summary>
    /// Affiliate service
    /// </summary>
    public partial class LanguageService : EntityService<Language>, ILanguageService
    {
        #region Fields


        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public LanguageService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<Language>();

        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public Language GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        public string GetValue(string resourceKey, CultureInfo culture)
        {

            if (string.IsNullOrEmpty(resourceKey))
            {
                throw new ArgumentNullException("resourceKey");
            }

            if (culture == null)
            {
                culture = CultureInfo.CurrentUICulture;
            }

            return GetResourceByCultureAndKey(resourceKey, culture);

        }

        public string GetResourceByCultureAndKey(string resourceKey, CultureInfo culture) {
            string res = _dbset.FirstOrDefault(x => x.StringName == resourceKey).English;
            
            switch(culture.DisplayName){
                case "English":
                    return _dbset.FirstOrDefault(x => x.StringName == resourceKey).English;
                    break;
                case "Spanish":
                    return _dbset.FirstOrDefault(x => x.StringName == resourceKey).Spanish;
                    break;
                case "Hebrew":
                    return _dbset.FirstOrDefault(x => x.StringName == resourceKey).Hebrew;
                    break;
            }

            return res;
        }

        #endregion

    }

}
