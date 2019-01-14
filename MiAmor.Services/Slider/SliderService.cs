using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiAmor.Core;
using MiAmor.Data;
using System.Data.Entity;


namespace MiAmor.Services
{

    public interface ISliderService : IEntityService<Slider>
    {
        Slider GetById(int Id);
        Slider GetHomeSlider();
       
    }

    public partial class SliderService : EntityService<Slider>, ISliderService
    {
        #region Fields
        private IEncryption _Encryption;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public SliderService(IContext context, IEncryption encription)
            : base(context)
        {
            _context = context;
            _Encryption = encription;
            _dbset = _context.Set<Slider>();

        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public Slider GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        public Slider GetHomeSlider()
        {
                    
            Slider Slider = (from c in _dbset
                                .Include(c => c.Slides)
                                .Include(c => c.Slides.Select (sl=>sl.Picture))
                                select c).SingleOrDefault();

            return Slider;
        }

        #endregion

    }
}
