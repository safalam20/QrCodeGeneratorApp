using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QrCodeGeneratorApp.dto
{
    public class ManufacturerSearch
    {
        [Required]
        public string LieferantId { get; set; }

        [Required]
        public int? Year { get; set; }

        [Required]
        public string Season { get; set; }
    }
}
