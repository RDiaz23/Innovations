using System.ComponentModel.DataAnnotations;

namespace Innovations.Model.Schemas
{
    public class Driver
    {
        public string dni { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener más de 100 caracteres, ni menos de 3 caracteres.")]
        public string name { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El apellido no debe de tener más de 100 caracteres, ni menos de 2 caracteres.")]
        public string lastName { get; set; }
        public int points { get; set; }
        public string email { get; set; }
        public bool active { get; set; }
    }
}
