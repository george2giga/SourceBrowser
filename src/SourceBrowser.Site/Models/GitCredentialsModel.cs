using System.ComponentModel.DataAnnotations;

namespace SourceBrowser.Site.Models
{
    public class GitCredentialsModel
    {
        [Required]
        [Display(Name = "Repository")]
        public string RepositoryUrl { get; set; }

        //[Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        //[Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        public bool NeedCredentials { get; set; }
    }
}