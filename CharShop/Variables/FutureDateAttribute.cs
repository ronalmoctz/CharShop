﻿using System.ComponentModel.DataAnnotations;

namespace CharShop.Variables
{
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            return value is DateTime date && date > DateTime.UtcNow;
        }
    }
}
