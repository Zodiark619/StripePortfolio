using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace StripePortfolio.Areas.GrandArchive.Attributes
{ 
    public class MinElementsAttribute: ValidationAttribute  
    {
        private readonly int _minCount;

        public MinElementsAttribute(int minCount)
        {
            _minCount = minCount;
            ErrorMessage = $"Please select at least {_minCount} item(s).";
        }
        
        public override bool IsValid(object value)
        {
         
            if (value is IEnumerable list)
            {
                int count = 0;
                foreach (var item in list) count++;
                return count >= _minCount;
            }
            return false;
        }
    }
}
