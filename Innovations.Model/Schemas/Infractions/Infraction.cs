using System.ComponentModel.DataAnnotations;
using System;

namespace Innovations.Model.Schemas
{
    public class Infraction
    {
       public int id { get; set; }
       public string description { get; set; }
       public float points { get; set; }       
       public string dni { get; set; }
       public string enrollment { get; set; }
       public DateTime date { get; set; }
    
        public Driver driver { get; set; }
        public Car car { get; set; }

    }
}
