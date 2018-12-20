using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ViewModels.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class StartDateBaforeEndDateAttribute : ValidationAttribute
    {
        private readonly string startDateProperty;

        public StartDateBaforeEndDateAttribute(string startDateProperty)
        {
            this.startDateProperty = startDateProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;

            var currentValue = (DateTime)value;

            var property = validationContext.ObjectType.GetProperty(this.startDateProperty);

            if (property == null)
                throw new ArgumentException("Property with this name not found");

            var comparisonValue = (DateTime)property.GetValue(validationContext.ObjectInstance);

            if (currentValue < comparisonValue)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
