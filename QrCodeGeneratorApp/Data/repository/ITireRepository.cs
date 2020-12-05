using QrCodeGeneratorApp.dto;
using QrCodeGeneratorApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrCodeGeneratorApp.Data.repository
{
    public interface ITireRepository
    {
  
        Task<List<ArtikelDto>> GetByLieferant(string lieferantId);
        Task<ArtikelDto> GetByArtikel(string artikelNummer);
        Task<ArtikelDto> GetByArtikel(string artikelNummer, string lieferantId);

    }
}
