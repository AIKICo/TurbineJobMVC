using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TurbineJobMVC.Models.CustomValidation
{
    public class IsExistsAR : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult(!string.IsNullOrEmpty(ErrorMessage)
                    ? ErrorMessage
                    : "شماره اموال وجود ندارد");
            var db = (PCStockDBContext) validationContext.GetService(typeof(PCStockDBContext));
            if (db.TahvilForms.Any(q => q.AmvalNo.ToString() == value.ToString()))
                return ValidationResult.Success;
            return new ValidationResult(!string.IsNullOrEmpty(ErrorMessage) ? ErrorMessage : "شماره اموال وجود ندارد");
        }
    }
}