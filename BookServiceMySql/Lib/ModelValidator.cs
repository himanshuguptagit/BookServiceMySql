using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookServiceMySql.Lib
{
    public class ModelValidator
    {
        public static bool isValid<T>(T t, List<ValidationResult>  results = null)
        {
            bool ret = true;
            var ctx = new ValidationContext(t);
            if(results == null)
            {
                results = new List<ValidationResult>();
            }

            if (!Validator.TryValidateObject(t, ctx, results, true))
            {
                foreach (var errors in results)
                {
                    Console.WriteLine("Error {0}", errors);
                    ret = false;
                }
            }

            return ret;
        }
    }
}