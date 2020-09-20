using System.ComponentModel.DataAnnotations;

namespace TurbineJobMVC.Models.ViewModels
{
    public class AuthenticateViewModel
    {
        [Required] public string Username { get; set; }

        [Required] public string Password { get; set; }
    }
}