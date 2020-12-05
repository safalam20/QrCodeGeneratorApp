using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QrCodeGeneratorApp.dto
{
    public class ArtikelSearch
    {
        [Required]
        public string Artikelnummer { get; set; }

        public string LieferantId { get; set; }

        public int? Year { get; set; }

        public string Season { get; set; }
    }
}
