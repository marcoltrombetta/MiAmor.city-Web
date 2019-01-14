using System;
using System.Linq;
using MiAmor.Core;
using MiAmor.Services;
using MiAmor.Data;
using System.Collections.Generic;



namespace MiAmor.Services
{
    public interface IPictureService : IEntityService<Picture>
    {
        Picture GetById(int Id);
        bool InsertPictureAndRelevantTable(PictureUpdateTableData PictureUpdateTableData);
    }
    /// <summary>
    /// Affiliate service
    /// </summary>
    public partial class PictureService : EntityService<Picture>, IPictureService
    {
        #region Fields
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public PictureService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<Picture>();

        }

        #endregion

        #region Methods

        public bool InsertPictureAndRelevantTable(PictureUpdateTableData PictureUpdateTableData)
        {
            Picture Picture = new Picture();
            Picture.BaseUrl = PictureUpdateTableData.url;
            Picture.PictureSizeTypeId = 1;
            this.Create(Picture);
            MiAmorContext db = new MiAmorContext();
            db.Database.ExecuteSqlCommand(
                @"Insert " + PictureUpdateTableData.TableName + " (PictureId, " + PictureUpdateTableData .ColName +
                ") Values('" + Picture.Id + "','" + PictureUpdateTableData.ColValue + "')"
                );
            return true;
        }

        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public Picture GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        #endregion

    }

}
