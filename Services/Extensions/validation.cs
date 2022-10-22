using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Extensions
{
    public static class test
    {
        public static bool IsValid<T>(this T t, out List<Exception> errors)
        {
            errors = new List<Exception>();
            var validationResults = new List<ValidationResult>();
            var vc = new ValidationContext(t, null, null);
            Validator.TryValidateObject(t, vc, validationResults, true);
            if (validationResults == null || !validationResults.Any())
            {
                return true;
            }
            foreach (var item in validationResults)
            {
                errors.Add(new Exception(item?.ErrorMessage));  
            }
            return false;

        }

    }


}
