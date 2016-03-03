using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KinoAfishaDP.Models
{
    public class Film
    {
        public int FilmId { get; set; }

        [Required(ErrorMessage = "Вкажіть назву фільму")]
        public string FilmName { get; set; }

        [Required(ErrorMessage = "Вкажіть рейтинг")]
        public string FilmRating { get; set; }

        [Required(ErrorMessage = "Вкажіть країну походження")]
        public string FilmCountry { get; set; }

        [Required(ErrorMessage = "Вкажіть дату виходу")]
        public string FilmAge { get; set; }

        [Required(ErrorMessage = "Вкажіть тривалість фільму, хв")]
        public double FilmLength { get; set; }

        [Required(ErrorMessage = "Вкажіть жанр фільму")]
        public string FilmGenre { get; set; }

        [Required(ErrorMessage = "Вкажіть акторський склад")]
        public string FilmActors { get; set; }

        [StringLength(1000, MinimumLength = 7, ErrorMessage = "Потрібно вказати від 7 до 1000 символів")]
        public string FilmReview { get; set; }

        [Required(ErrorMessage = "Вкажіть місце знаходження зображеннь")]
        public string FilmPictures { get; set; }
        public int FilmPlus { get; set; }
        public int FilmMinus { get; set; }

        public List<Session> Session { get; set; }
    }
}