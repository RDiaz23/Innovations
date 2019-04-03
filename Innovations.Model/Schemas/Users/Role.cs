using System.ComponentModel.DataAnnotations;

namespace Innovations.Model.Schemas.Users
{
    public class Role
    {
        public int idrole { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "el nombre no debe tener mas de 30 caracteres")]
        public string name { get; set; }
        public string description { get; set; }
        public bool active { get; set; }
    }
}
