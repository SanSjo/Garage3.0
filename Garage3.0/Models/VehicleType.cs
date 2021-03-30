using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Http;

namespace Garage3.Models
{
    public class VehicleType
    {
        [Key]
        public string Type { get; set; }
        public float Size { get; set; }
        public string imgSrc { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

    }
}