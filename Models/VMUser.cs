using EcosystemApp.DTOs;
using System.ComponentModel.DataAnnotations;

namespace EcosystemApp.Models
{
    public class VMUser
    {
        [Required]
        public UserDTO User { get; set; }

        [Required(ErrorMessage = "Contraseña de verificación requerida.")]
        public string VerificationPass { get; set; }
    }
}
