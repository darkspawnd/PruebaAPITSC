using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaAPI.Models
{
    public class CountrySubdivision
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string LocalizedName { get; set; }

        [ForeignKey("CountryId")]
        [JsonIgnore]
        public Country Country { get; set; }
        public int CountryId { get; set; }
    }
}
