using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dache.DP
{
    public class Service
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "שדה חובה")]
        [Display(Name = "שם העסק")]
        [MaxLength(60, ErrorMessage = "מוגבל ל60 תווים")]
        public string Name { get; set; }

        [Display(Name = "קטגוריה")]
        public string Catrgory { get; set; }

        [MaxLength(150,ErrorMessage = "מוגבל ל150 תווים")]

        [Display(Name = "כותרת ראשית על השרות שלך")]
        public string Head { get; set; }

        [Required(ErrorMessage = "שדה חובה")]
        [Display(Name = "תאור")]
        [MaxLength(300, ErrorMessage = "מוגבל ל300 תווים")]
        public string Description { get; set; }

        [Display(Name = "אזור")]
        public string Area { get; set; }

        [Display(Name = "כתובת או עיר")]
        public string Addrese { get; set; }

        [Required(ErrorMessage ="שדה חובה")]
        [Display(Name = "טלפון")]
        [MaxLength(12 ,ErrorMessage = "מוגבל ל 12 תווים")]
        [MinLength(4, ErrorMessage = "מינימום 4 תווים")]
        public string Phone { get; set; }

        [Display(Name ="פרטים נוספים ,שעות פתיחה מחיר וכו")]
        public string MorDetiles { get; set; }

        [Display(Name ="הוסף תמונה")]
        public string ImgUrl { get; set; }
        public int SupplierID { get; set; }
        public string SupplierLogoUrl { get; set; }

    }
}

