﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.CustomAttributes
{
    public class IsBeforeAttribute : ValidationAttribute
    {
        private readonly string propertyToCompare;
        public IsBeforeAttribute(string _propertyToCompare, string ErrorMessage= "")
        {
            this.propertyToCompare = _propertyToCompare;
            this.ErrorMessage = ErrorMessage;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                DateTime dateToCompare = (DateTime)validationContext
                .ObjectType
                .GetProperty(propertyToCompare)
                .GetValue(validationContext.ObjectInstance);

                if ((DateTime)value < dateToCompare)
                {
                    return ValidationResult.Success;
                }
            }
            catch (Exception)
            { }

            return new ValidationResult(ErrorMessage);
        }
    }
}
