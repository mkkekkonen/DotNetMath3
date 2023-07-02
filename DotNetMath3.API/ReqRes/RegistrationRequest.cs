using System.ComponentModel.DataAnnotations;

namespace DotNetMath3.API.ReqRes
{
    public class RegistrationRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}