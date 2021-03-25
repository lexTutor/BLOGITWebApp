using BLOGIT.Commons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace BLOGIT.Models.ViewModels
{
    public class AddPostVM: IWithPhotoUpload
    {
        public BlogUser PostCreator { get; set; }
        
        public  List<PostCategories> PostCategories { get; set; }

        [Required]
        public string PostTitle { get; set; }

        [Required]
        [ValidatePostContentLength(30,ErrorMessage = "A post must have at least 30 words.")]
        [VaLidateDataContent(ErrorMessage = "Your post contains unaccecptable words")]
        public string PostDetails { get; set; }

        public IFormFile Image { get; set; }

        public string ImagePath { get; set; }

        [Required]
        public string[] CategoriesId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;

    }


 
}
