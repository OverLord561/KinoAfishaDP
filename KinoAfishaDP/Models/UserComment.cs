using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KinoAfishaDP.Models
{
    public class UserComment
    {
        public int Id { get; set; }

        [Display(Name = "Номер прем'єри")]
        public int ReviewID { get; set; }

        [Display(Name = "Логін")]
        public string UserNickName { get; set; }

        [Required(ErrorMessage = "Оставьте сообщение", AllowEmptyStrings = false)]
        [Display(Name = "Коментар")]
        public string LabelText { get; set; }

        [Required(ErrorMessage = "Введите e-mail", AllowEmptyStrings = false)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Введите правильный e-mail")]
        [Display(Name = "E-mail")]
        public string E_mail { get; set; }

        [Required(ErrorMessage = "Введите Ваше имя", AllowEmptyStrings = false)]
        [Display(Name = "Ім'я")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }
    }
}