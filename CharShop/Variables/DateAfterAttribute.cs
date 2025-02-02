using Serilog;
using System.ComponentModel.DataAnnotations;

namespace CharShop.Variables
{
    public class DateAfterAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public DateAfterAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
            if (property == null)
            {
                Log.Information("DateAfterAttribute: Comparison property not found.");
                return new ValidationResult($"Unknown property: {_comparisonProperty}");
            }

            var comparisonValue = property.GetValue(validationContext.ObjectInstance);
            if (comparisonValue == null)
            {
                Log.Information("DateAfterAttribute: Comparison value is null.");
                return new ValidationResult($"Comparison value for property {_comparisonProperty} is null.");
            }

            if (value is DateTime date && comparisonValue is DateTime comparisonDate && date < comparisonDate)
            {
                Log.Information("DateAfterAttribute: Date is not after the comparison date.");
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
