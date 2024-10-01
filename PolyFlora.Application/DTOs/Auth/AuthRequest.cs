
using System.ComponentModel.DataAnnotations;

namespace PolyFlora.Application.DTOs.Auth
{
    public record AuthRequest
    (
        [Required] string email,
        [Required] string password
    );
}
