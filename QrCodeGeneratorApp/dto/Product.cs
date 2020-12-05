using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QrCodeGeneratorApp.dto
{
    [Keyless]
    public class Product
    {
        public string Artikelnummer { get; set; }

        public int? User_InternId { get; set; }
        public string Lieferant { get; set; }
        public string EAN { get; set; }

        public string Code { get; set; }
    }
}
