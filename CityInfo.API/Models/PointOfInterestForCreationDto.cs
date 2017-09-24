using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Models
{
    public class PointOfInterestForCreationDto
    {
        [Required(ErrorMessage = "You should provide a name value")]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(200)]
        [MinLength(1, ErrorMessage = "Min length is 1 character")]
        public string Description { get; set; }
    }
}
