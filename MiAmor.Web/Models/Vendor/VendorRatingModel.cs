using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiAmor.Web.Models
{
    public class VendorRatingModel
    {
        private int TotalRatingNum;

        public int ApprovedRatingSum { get; set; }

        public int ApprovedRatingHalfStarSum { get; set; }

        public int NumberOfLeftRating { get; set; }

        public VendorRatingModel(int SetRating)
        {
            TotalRatingNum = 5;
            ApprovedRatingSum = SetRating / 2;
            ApprovedRatingHalfStarSum = SetRating % 2;
            NumberOfLeftRating = TotalRatingNum - ((ApprovedRatingHalfStarSum==1) ? ApprovedRatingSum + 1 : ApprovedRatingSum);

        }
    }
}