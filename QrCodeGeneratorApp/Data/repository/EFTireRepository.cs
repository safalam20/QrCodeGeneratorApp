using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QrCodeGeneratorApp.dto;
using QrCodeGeneratorApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrCodeGeneratorApp.Data.repository
{
    public class EFTireRepository : ITireRepository
    {
        private TireInventoryContext context;
        public EFTireRepository(TireInventoryContext ctx)
        {
            context = ctx;
        }
       

        public async Task<List<ArtikelDto>> GetByLieferant(string lieferantId)
        {
            var lifId = new SqlParameter("@lieferantId", lieferantId);
            var artikelsByLiferant =await context.products
                .FromSqlRaw("SELECT * from artikelLieferantFunction() where Lieferant=@lieferantId", lifId)
                .Select(ad => new ArtikelDto()
                {
                    Artikelnummer = ad.Artikelnummer,
                    UserInternId = ad.User_InternId,
                    Lieferant = ad.Lieferant,
                    Ean = ad.EAN,
                    LieferantCode = ad.Code
                }
                ).ToListAsync();
             
                    
            return artikelsByLiferant;
        }
        public async Task<ArtikelDto> GetByArtikel(string artikelId)
        {
            var artikId = new SqlParameter("@artikelId", artikelId);
            var itemByArtikel =await context.products
                .FromSqlRaw("SELECT * from artikelLieferantFunction() where Artikelnummer=@artikelId", artikId)
                .Select(ad => new ArtikelDto()
                {
                    Artikelnummer = ad.Artikelnummer,
                    UserInternId = ad.User_InternId,
                    Lieferant = ad.Lieferant,
                    Ean = ad.EAN,
                    LieferantCode = ad.Code
                }
                ).FirstOrDefaultAsync();

            return itemByArtikel;
        }

       

        public async Task<ArtikelDto> GetByArtikel(string artikelNummer,string lieferantId)
        {
            var p1 = new SqlParameter("@arN", artikelNummer);
            var p2 = new SqlParameter("@lifI", lieferantId);
            var itemByArtikel =await context.products
                .FromSqlRaw("SELECT * from artikelLieferantFunction() where Artikelnummer=@arN and Lieferant=@lifI", p1,p2)
                .Select(ad => new ArtikelDto()
                {
                    Artikelnummer = ad.Artikelnummer,
                    UserInternId = ad.User_InternId,
                    Lieferant = ad.Lieferant,
                    Ean = ad.EAN,
                    LieferantCode = ad.Code
                }
                ).FirstOrDefaultAsync();


            return itemByArtikel;
        }
        
    }
  
}
