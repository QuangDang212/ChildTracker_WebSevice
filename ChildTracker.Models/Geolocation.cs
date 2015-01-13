namespace ChildTracker.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Geolocation
    {
        public int ID { get; set; }

        [Required]
        public decimal Latitude { get; set; }

        [Required]
        public decimal Longitude { get; set; }

        [Required]
        public string UserID { get; set; }

        public virtual User User { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }
    }
}
