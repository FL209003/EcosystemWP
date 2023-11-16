using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace EcosystemApp.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Escriba su nombre de usuario.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Escriba su contraseña.")]
        public string Password { get; set; }
    }
}
