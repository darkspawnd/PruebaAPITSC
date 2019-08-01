using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaAPI.RequestModels
{
    public class CountryRequestModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "The Alpha-2 Code length must be 2")]
        public string Alpha2Code { get; set; }
        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "The Alpha-3 Code length must be 3")]
        public string Alpha3Code { get; set; }

        [Required]
        //[StringLength(3, MinimumLength = 3, ErrorMessage = "The Numeric Code length must be 3")]
        public int NumericCode { get; set; }
        //[RegularExpression("^(ISO)*", ErrorMessage = "The ISO Reference must have the ISO prefix")]
        public string ISOReference { get; set; }

        public bool Independent { get; set; }
    }
}
