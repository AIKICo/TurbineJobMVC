using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using TurbineJobMVC.Services;

namespace TurbineJobMVC.Models.CustomValidation
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class IsDublicateActiveAR : ValidationAttribute, IClientModelValidator
    {
        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-isdublicateactivear",
                "برای این اموال یک درخواست ثبت شده که همچنان در دست اقدام می باشد");
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult(ErrorMessage != null ? ErrorMessage : "شماره اموال وجود ندارد");
            var _service = (WorkOrderService) validationContext.GetService(typeof(IWorkOrderService));
            var workorder = _service.IsDublicateActiveAR(value.ToString());
            if (workorder == null)
                return ValidationResult.Success;
            return new ValidationResult(ErrorMessage != null
                ? ErrorMessage
                : $"برای اموال یک درخواست با کد رهگیری {workorder.WONo} ثبت شده که همچنان در دست اقدام است");
        }

        private bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key)) return false;

            attributes.Add(key, value);
            return true;
        }
    }
}