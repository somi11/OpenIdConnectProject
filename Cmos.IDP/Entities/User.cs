using System.ComponentModel.DataAnnotations;

namespace Cmos.IDP.Entities
{
    public class User : IConcurrencyAware
    {
        [Key]
       public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Subject { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]

        public string Password { get; set; }

        [Required]
        public bool Active { get; set; }

        [ConcurrencyCheck]

        public string ConcurrencyStamp { get; set; }  = Guid.NewGuid().ToString();

        public ICollection<UserClaim> Claims { get; set; } = new List<UserClaim>();

    }
}
