using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinoAfishaDP.Models
{
    public class Comment
    {
        public int FilmId { get; set; }
        public string User_Name { get; set; }
        public string User_Photo { get; set; }
        public string User_LabelText { get; set; }


        public DateTime Date { get; set; }
    }
}