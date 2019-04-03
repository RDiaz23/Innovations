using System.ComponentModel.DataAnnotations;

namespace Innovations.Model.Schemas
{
    public class Car
    {
        public string enrollment { get; set; }

        public string brand { get; set; }

        public string model { get; set; }

        public bool active { get; set; }
    }
}
