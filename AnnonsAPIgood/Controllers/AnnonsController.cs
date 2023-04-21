using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AnnonsAPIgood.Models;
using Microsoft.EntityFrameworkCore;

namespace AnnonsAPIgood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnonsController : ControllerBase
    {
        private readonly Db1Context _context;
        public AnnonsController(Db1Context context)
        {
            _context = context;
        }

        //TODO
        // put annonsor

        [HttpGet ("/annons/ads", Name = "Get all adds")]
        public async Task<IActionResult> GetAllAds()
        {
            return Ok(await _context.TblAds
                
                .ToListAsync());
        }
        [HttpGet ("/annons/{Id}", Name = "Get ad with Id")]
        public async Task<IActionResult> GetAdWithId(int Id)
        {
            return Ok(await _context.TblAds
                .Where(p => p.AdId.Equals(Id))
                .ToListAsync());
        }
        [HttpGet ("/annonsor/{Id}", Name = "Get advertiser with Id")]
        public async Task<IActionResult> GetAnnonsorById(int Id)
        {
            return Ok(await _context.TblAnnonsorers
                .Where(p => p.AnId.Equals(Id))
                .Include(p => p.AnFakturaadressNavigation)
                .Include(p => p.AnUtdadressNavigation)
                .ToListAsync());
        }
        [HttpGet ("/adresses/{Id}", Name = "Get adresses tied to an advertiser")]
        public async Task<IActionResult> GetAdressesWithId(int Id)
        {
            return Ok(await _context.TblAdresses
                .Where(p => p.AdrAnnonsor.Equals(Id))
                .ToListAsync());
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateNewAd([FromBody] Ad ad)
        //{
        //    try
        //    {
        //        //Validate data
        //        if (string.IsNullOrEmpty(ad.AdRubrik)) throw new Exception("Heading of ad is empty!");
        //        int checkAdIdExists = (from dbAd in _context.TblAds where dbAd.AdId.Equals(ad.AdId) select dbAd).Count();
        //        if (checkAdIdExists > 0) throw new Exception("Id already exists!");
        //        //int checkAnnonsorExists = (from dbAn in _context.TblAnnonsorers where dbAn.AnId.Equals(ad.AdAnnonsor) select dbAn).Count();
        //        //if (checkAnnonsorExists == 0) throw new Exception("That advertiser does not exist");
        //        _context.Add(ad);
        //        await _context.SaveChangesAsync(); //måste callas
        //    } catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }

            
        //    return Ok(ad.AdId);
        //}
        [HttpPut ("/annonsor/update")]
        public async Task<IActionResult> UpdateAnnonsor([FromBody] Annonsorer An)
        {
            Console.WriteLine("Här");

            try
            {
                if (string.IsNullOrEmpty(An.AnNamn)) throw new Exception("Advertiser must have a name");
                if (!An.AnOrgnr.HasValue && !An.AnSubnr.HasValue) throw new Exception("Either an org number or subscriber number has to be present");
                if (An.AnUtdadressNavigation == null && An.AnFakturaadressNavigation == null) throw new Exception("No adress");
                var checkAdIdExists = (from dbAn in _context.TblAnnonsorers where dbAn.AnId.Equals(An.AnId) select dbAn).Count();
                if (checkAdIdExists < 1) throw new Exception("Id does not exists!");
                //kontroller att adresserna finns annars skapa?

                var toBeUpdated = _context.TblAnnonsorers.Where(p => p.AnId == An.AnId).FirstOrDefault();
                if (toBeUpdated != null)
                {
                    toBeUpdated.AnNamn = An.AnNamn;
                    toBeUpdated.AnTele = An.AnTele;
                    toBeUpdated.AnOrgnr = An.AnOrgnr;
                    toBeUpdated.AnSubnr = An.AnSubnr;
                    toBeUpdated.AnUtdadressNavigation = An.AnUtdadressNavigation;
                    toBeUpdated.AnFakturaadressNavigation = An.AnFakturaadressNavigation;
                }

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(An.AnId);
        }

        [HttpPost]
        [Route ("/annons_new")]
        public async Task<IActionResult> AddAnnons(AddAnnonsRequest addAnnonsRequest)
        {
            var ad = new Ad()
            {
                AdId = addAnnonsRequest.AdId,
                AdRubrik = addAnnonsRequest.AdRubrik,
                AdInnehåll = addAnnonsRequest.AdInnehåll,
                AdPris = addAnnonsRequest.AdPris,
                AdAnnonspris = addAnnonsRequest.AdAnnonspris,
                AdAnnonsor = addAnnonsRequest.AdAnnonsor,
            };

            await _context.TblAds.AddAsync(ad);
            await _context.SaveChangesAsync();

            return Ok(ad);
        }

        [HttpPost ("/annonsor/create")]
        public async Task<IActionResult> CreateAnnonsor([FromBody] Annonsorer An)
        {
            Random rand = new Random();
            var postId = rand.Next();
            var annonsor = new Annonsorer
            {
                AnId = postId,
                AnNamn = An.AnNamn,
                AnTele = An.AnTele,
                AnOrgnr = An.AnOrgnr,
                AnSubnr = An.AnSubnr
            };
            try
            {
                if (string.IsNullOrEmpty(An.AnNamn)) throw new Exception("Advertiser must have a name");
                if (!An.AnOrgnr.HasValue && !An.AnSubnr.HasValue) throw new Exception("Either an org number or subscriber number has to be present");
                if (An.AnUtdadressNavigation == null && An.AnFakturaadressNavigation == null) throw new Exception("No adress");
                var checkAdIdExists = (from dbAn in _context.TblAnnonsorers where dbAn.AnId.Equals(An.AnId) select dbAn).Count();
                if (checkAdIdExists > 1) throw new Exception("Id does already exist!");
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            if(An.AnFakturaadressNavigation != null)
            {
                An.AnFakturaadressNavigation.AdrAnnonsor = postId;
                await _context.TblAdresses.AddAsync(An.AnFakturaadressNavigation);
            }
            if (An.AnUtdadressNavigation != null)
            {
                An.AnUtdadressNavigation.AdrAnnonsor = postId;
                await _context.TblAdresses.AddAsync(An.AnUtdadressNavigation);
            }
            await _context.SaveChangesAsync();
            _context.TblAnnonsorers.Add(annonsor);
            await _context.SaveChangesAsync();
            return Ok(An.AnId);
        }
    }
}
