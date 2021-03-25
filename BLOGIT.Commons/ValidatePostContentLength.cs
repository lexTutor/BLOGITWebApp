using System;
using System.ComponentModel.DataAnnotations;

namespace BLOGIT.Commons
{
    public class ValidatePostContentLength : ValidationAttribute
    {
        public ValidatePostContentLength(int minval)
        {
            this.MinValue = minval;
        }
        public int MinValue { get; set; }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }
            if (value.ToString().Split(' ').Length < MinValue)
            {
                return false;
            }
            return true;
        }
    }
}
