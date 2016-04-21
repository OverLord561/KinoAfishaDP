using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KinoAfishaDP.Models
{
    public class Session
    {
        public int SessionId { get; set; }

        [Display(Name = "Час показу")]
        [Required(ErrorMessage = "Дата має відповідати формату 12/24/2004 12:42:25 AM")]
        public DateTime SessionTimePokaz { get; set; }


        public int FilmId { get; set; }
        public int MovieHouseId { get; set; }
        public Film Film { get; set; }
        public MovieHouse MovieHouse { get; set; }
    }
}