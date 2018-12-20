
using GoGo.Models;
using GoGo.Models.Enums;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ViewModels.Attributes;

namespace ViewModels
{
    public class CreateDestinationViewModel //: IMapFrom<Destination>//, IHaveCustomMappings
    {
        [Required]
        [Display(Name = "Image")]
        [DataType(DataType.Upload)]
        [BindProperty]
        public IFormFile Image { get; set; }

        [Required]
        public LevelOfDifficulty Level { get; set; }

        [Required]
        public string Naame { get; set; }

        [Required]
        [StringLength(7000, MinimumLength = 10)]
        public string Description { get; set; }

        [Required]
        [StartDateBaforeEndDate("EndDateToJoin", ErrorMessage = "End date to join must by before Start date")]
        [CompareWithToday(ErrorMessage = "This date is passed enter new date:)")]
        public DateTime StartDate { get; set; }

        [Required]
        [StartDateBaforeEndDate("StartDate", ErrorMessage = "Start date must by before End date")]
        public DateTime EndDate { get; set; }

        [Required]
        public DateTime EndDateToJoin { get; set; }
    }

    //[AttributeUsage(AttributeTargets.Property)]
    //public class StartDateBaforeEndDateAttribute : ValidationAttribute
    //{
    //    private readonly string startDateProperty;

    //    public StartDateBaforeEndDateAttribute(string startDateProperty)
    //    {
    //        this.startDateProperty = startDateProperty;
    //    }

    //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //    {
    //        ErrorMessage = ErrorMessageString;

    //        var currentValue = (DateTime)value;

    //        var property = validationContext.ObjectType.GetProperty(this.startDateProperty);

    //        if (property == null)
    //            throw new ArgumentException("Property with this name not found");

    //        var comparisonValue = (DateTime)property.GetValue(validationContext.ObjectInstance);

    //        if (currentValue < comparisonValue)
    //            return new ValidationResult(ErrorMessage);

    //        return ValidationResult.Success;
    //    }
    //}
}
