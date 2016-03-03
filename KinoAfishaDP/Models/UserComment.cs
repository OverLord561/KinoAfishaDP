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
        public int ReviewID { get; set; }

        public string UserNickName { get; set; }

        [Required(ErrorMessage = "Оставьте сообщение", AllowEmptyStrings = false)]
        public string LabelText { get; set; }

        [Required(ErrorMessage = "Введите e-mail", AllowEmptyStrings = false)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Введите правильный e-mail")]
        public string E_mail { get; set; }
        [Required(ErrorMessage = "Введите Ваше имя", AllowEmptyStrings = false)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}