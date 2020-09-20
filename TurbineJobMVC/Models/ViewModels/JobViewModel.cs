using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TurbineJobMVC.Models.CustomValidation;

namespace TurbineJobMVC.Models.ViewModels
{
    public class JobViewModel
    {
        [Display(Name = "شماره اموال")]
        [Required(ErrorMessage = "شماره اموال را وارد نمایید.")]
        [RegularExpression(@"\d{10}", ErrorMessage = "شماره اموال را به صورت صحیح وارد نمایید مثال 2142020025")]
        [IsExistsAR(ErrorMessage = "شماره اموال وارد شده ثبت نشده است")]
        [IsDublicateNotRateAR]
        [IsDublicateActiveAR]
        public string AR { get; set; }

        [Display(Name = "شرح نیاز")]
        [Required(ErrorMessage = "شرح نیاز را وارد نمایید.")]
        [StringLength(1000, MinimumLength = 20, ErrorMessage = "شرح خود را به صورت مختصر و با مفهوم بیان نمایید")]
        [NotMapped]
        public string Description { get; set; }

        [Display(Name = "تلفن تماس")]
        [Required(ErrorMessage = "شماره تماس را وارد نمایید.")]
        [RegularExpression(@"\d{5}", ErrorMessage = "شماره تلفن را به صورت صحیح وارد نمایید مثال 24068")]
        [NotMapped]
        public string PhoneNumber { get; set; }

        [Display(Name = "شرح نیاز")] public string NeedDescription => $"{Description}\r\n(شماره تماس:{PhoneNumber})";

        [NotMapped] public bool defaultAR { get; set; }

        [NotMapped] public bool defaultDes { get; set; }
    }
}