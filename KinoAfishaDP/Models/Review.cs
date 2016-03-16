using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinoAfishaDP.Models
{
    public class Review
    {
        public int FilmId_Review { get; set; }
        public string FilmName_Review { get; set; }

        public double FilmRating_Review { get; set; }
        public string FilmCountry_Review { get; set; }
        public string FilmAge_Review { get; set; }
        

        public double FilmLength_Review { get; set; }
        public string FilmGenre_Review { get; set; }

        public string FilmActors_Review { get; set; }
        public string FilmReview_Review { get; set; }
        public string FilmPictures_Review { get; set; }

        public int Plus_Review { get; set; }
        public int Minus_Review { get; set; }
    }
}