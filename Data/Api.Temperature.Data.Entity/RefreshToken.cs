using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Api.Temperature.Data.Entity
{
    public class RefreshToken
    {
        [Key]
        public string Token { get; set; }
        public string UserId { get; set; }
        public DateTime Expires { get; set; }
        public bool Used { get; set; }
        public bool Invalidated { get; set; }

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }

    }
}
