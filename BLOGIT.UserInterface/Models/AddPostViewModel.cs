using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace BLOGIT.Models.ViewModels
{
    public class AddPostViewModel : IWithPhotoUpload
    {
        [Required]
        public BlogUser PostCreator { get; set; }

        public List<PostCategories> PostCategories { get; set; }

        [Required]
        public string PostTitle { get; set; }

        [Required]
        public string PostDetails { get; set; }

        public IFormFile Image { get; set; }

        public string ImagePath { get; set; }
        public string[] CategoriesId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }

        public DateTime Time { get; set; } = DateTime.Now;

    }

    public class ValidateTitle : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (string.IsNullOrWhiteSpace(value.ToString()))
            {
                return false;
            }
            return true;
        }
    }

    public class ValidatePostContentLength : ValidationAttribute
    {
        public ValidatePostContentLength(int minval)
        {
            this.MinValue = minval;
        }
        public int MinValue { get; set; }

        public override bool IsValid(object value)
        {
            if (value.ToString().Split(' ').Length < MinValue)
            {
                return false;
            }
            return true;
        }
    }

    public class VaLidateDataContent : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var contetWords = value.ToString().Split(" ");
            List<string> BadWords = new List<string> { "Fuck", "Nigga", "Dick head", "Shit", "Piss off", "Dick head", "Asshole", "Son of a bitch" };
            if (BadWords.Intersect(contetWords).Any())
            {
                return false;
            }
            return true;
        }
    }
}
