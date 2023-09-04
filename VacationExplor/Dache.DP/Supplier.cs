using System;
using System.ComponentModel.DataAnnotations;

namespace Dache.DP
{
    public class Supplier
    {
        public int ID { get; set; }

        [Display(Name = "שם")]
        [Required(ErrorMessage = "שדה חובה")]
        public String Name { get; set; }

        [Display(Name = "כתובת")]
        [Required(ErrorMessage = "שדה חובה")]
        public string Addrese { get; set; }

        [Display(Name ="טלפון")]
        [Required(ErrorMessage = "שדה חובה")]
        [MaxLength(12)]
        public string Phone { get; set; }

        [Display(Name = "דואר אלקטרוני")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "כתובת אימייל שגוי")]
        [Required(ErrorMessage = "שדה חובה")]
        public string Email { get; set; }

        [Display(Name = "בחר שם משתמש להתחברות")]
        [Required(ErrorMessage = "שדה חובה")]
        public string UserName { get; set; }

        [Display(Name = "סיסמא")]
        [Required(ErrorMessage = "שדה חובה")]
        [MinLength(6, ErrorMessage = "סיסמא קצרה מדי ,לפחות 6 תווים")]
        [MaxLength(25 ,ErrorMessage ="אורך מוגזם של סיסמא ,עד 25 תווים")]
        public string Password { get; set; } 
        public string LogoUrl { get; set; }
        public DateTime LastAccsesTime { get; set; }
        public string SupplierLogoUrl { get; set; }
    }
}
