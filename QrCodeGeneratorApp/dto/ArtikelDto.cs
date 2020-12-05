using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrCodeGeneratorApp.dto
{
    public class ArtikelDto
    {
        public string Artikelnummer { get; set; }
        
        public int? UserInternId { get; set; }
        public string Lieferant { get; set; }
        public string Ean { get; set; }

        public string LieferantCode { get; set; }
    }
}
