using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KinoAfishaDP.Models
{
    public class MovieHouse
    {
        public int MovieHouseId { get; set; }

        public string MovieHouseImage { get; set; }

        [Required(ErrorMessage = "Вкажіть назву кінотеатру")]
        public string MovieHouseName { get; set; }

        [Required(ErrorMessage = "Вкажіть телефонний номер кінотеатру")]
        [Display(Name="Телефон")]
        [RegularExpression(@"[0-9]{3}-[0-9]{3}-[0-9]{4}", ErrorMessage = "Телефонний номер номер має відповідати формату (xxx)-(xxx)-(xxxx)")]
        public string MovieHouseTelephone { get; set; }

        [Required(ErrorMessage = "Вкажіть адрес кінотеатру")]
        public string MovieHouseAdress { get; set; }

        [Display(Name = "Рейтинг")]
        [Required(ErrorMessage = "Вкажіть рейтинг кінотеатру")]
        public double MovieHouseRating { get; set; }

        [Required(ErrorMessage = "Вкажіть місце знаходження зображеннь")]
        public string MovieHouseStars { get; set; }

        [Required(ErrorMessage = "Довгота для карти google")]
        public double GeoLong { get; set; }

        [Required(ErrorMessage = "Широта для карти google")]
        public double GeoLat { get; set; }

        public List<Session> Session { get; set; }
    }
}