using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace BLOGIT.Commons
{
    public class VaLidateDataContent : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }
            var contetWords = value.ToString().ToLower().Split(" ");
            List<string> BadWords = new List<string> { "fuck", "nigga", "dick head", "shit", "piss off", "asshole", "son of a bitch" };
            if (BadWords.Intersect(contetWords).Any())
            {
                return false;
            }
            return true;
        }
    }
}
