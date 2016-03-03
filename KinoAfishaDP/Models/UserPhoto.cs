using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KinoAfishaDP.Models
{
    public class UserPhoto
    {

        public int UserPhotoId { get; set; }
        public string UserName { get; set; }
        public string Photo { get; set; }

    }
}