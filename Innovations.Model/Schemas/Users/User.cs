using System.ComponentModel.DataAnnotations;

namespace Innovations.Model.Schemas.Users
{
    public class User
    {
        public int iduser { get; set; }
        [Required]
        public int idrole { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener más de 100 caracteres, ni menos de 3 caracteres.")]
        public string name { get; set; }
        public string lastName { get; set; }
        public string phone { get; set; }
        [Required]
        public string email { get; set; }
        public string password { get; set; }
        [Required]
        public byte[] password_hash { get; set; }
        [Required]
        public byte[] password_salt { get; set; }
        public bool active { get; set; }

        public Role role { get; set; }
    }
}
