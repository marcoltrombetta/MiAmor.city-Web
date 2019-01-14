using System;
using System.Linq;
using MiAmor.Core;
using MiAmor.Services;
using MiAmor.Data;
using System.Collections.Generic;

namespace MiAmor.Services
{
    public interface IVendorOpeningHoursService : IEntityService<VendorOpeningHours>
    {
        VendorOpeningHours GetById(int Id);
        bool GetIsVendorOpenByVendorId(int Id);
    }
    /// <summary>
    /// Affiliate service
    /// </summary>
    public partial class VendorOpeningHoursService : EntityService<VendorOpeningHours>, IVendorOpeningHoursService
    {
        #region Fields


        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public VendorOpeningHoursService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<VendorOpeningHours>();

        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an VendorOpeningHours
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public VendorOpeningHours GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        public bool GetIsVendorOpenByVendorId(int Id)
        {
            DayOfWeek CurrDayName = DateTime.Now.DayOfWeek;
             VendorOpeningHours oph ;
             bool open=false;

             switch (CurrDayName.ToString().ToUpper())
             {
                 case "SUNDAY":
                     oph = _dbset.FirstOrDefault(x => x.VendorId == Id);
                     if (oph != null)
                     {
                         open = validateOpen(oph.Sunday.Split(' '));
                     }
                     
                     break;
                 case "MONDAY":
                     oph = _dbset.FirstOrDefault(x => x.VendorId == Id);
                     if (oph != null)
                     {
                         open = validateOpen(oph.Monday.Split(' '));
                     }
                     break;
                 case "TUESDAY":
                     oph = _dbset.FirstOrDefault(x => x.VendorId == Id);
                     if (oph != null)
                     {
                         open = validateOpen(oph.Tuesday.Split(' '));
                     }
                     break;
                 case "WEDNESDAY":
                     oph = _dbset.FirstOrDefault(x => x.VendorId == Id);
                     if (oph != null)
                     {
                         open = validateOpen(oph.Wednesday.Split(' '));
                     }
                     break;
                 case "THURSDAY":
                     oph = _dbset.FirstOrDefault(x => x.VendorId == Id);
                     if (oph != null)
                     {
                         open = validateOpen(oph.Thursday.Split(' '));
                     }
                     break;
                 case "SATURDAY":
                     oph = _dbset.FirstOrDefault(x => x.VendorId == Id);
                     if (oph != null)
                     {
                         open = validateOpen(oph.Saturday.Split(' '));
                     }
                     break;
             }
            return true;

        }

        private bool validateOpen(string[] data){
            string current = DateTime.Now.ToString("HH:mm");

            DateTime dateTimeNow = convert(current);

             double res=0;
             if (data.Length == 4)
                {
                    DateTime mondayfrom = convert(data[0]);
                    DateTime mondayto = convert(data[2]);

                    res = dateTimeNow.Subtract(mondayfrom).TotalMinutes + mondayto.Subtract(dateTimeNow).TotalMinutes;
                }
             else if (data.Length == 7)
                {
                    DateTime mondayfrom = convert(data[0]);
                    DateTime mondayto = convert(data[2]);

                    double aux = mondayfrom.Subtract(dateTimeNow).TotalMinutes + mondayto.Subtract(dateTimeNow).TotalMinutes;

                    mondayfrom = convert(data[3]);
                    mondayto = convert(data[5]);

                    double aux1 = mondayfrom.Subtract(dateTimeNow).TotalMinutes + mondayto.Subtract(dateTimeNow).TotalMinutes;

                    if (aux1 >= 0)
                    {
                        res = aux1;
                    }
                    else
                    {
                        res = aux;
                    }

                }

             return res>0;
        }

         private DateTime convert(string time) {
           return DateTime.ParseExact(time, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
        }

        #endregion

    }

}
