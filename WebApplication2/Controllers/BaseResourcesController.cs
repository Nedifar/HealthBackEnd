using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using WebApplication2.Models;
namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseResourcesController : ControllerBase
    {
        Models.context context;

        public BaseResourcesController(context context)
        {
            this.context = context;
        }

        [HttpGet("address")]
        public async Task<ActionResult> GetAddress(string addressPart)
        {
            using(HttpClient http = new HttpClient())
            {
                try
                {
                    var request = await http.GetAsync($"https://data.pbprog.ru/api/address/full-address/parse?token=4f67e5c3f0ef4034b05a311838639232597f9425&addressText={addressPart}&resultLimit=10&hierarchyMode=1");
                    var response = request.Content.ReadAsAsync<List<ViewModel.AddressViewModel.Class1>>().Result;
                    
                    return Ok(response.Select(p => new
                    {
                        Value = p.value
                    }));
                }
                catch(Exception ex)
                {
                    return BadRequest();
                }
            }
        }

        [HttpGet]
        [Route("countries")]
        public async Task<ActionResult> GetCountries()
        {
            var countries = await context.Citizenships.ToListAsync();
            return Ok(countries.Select(p => new
            {
                id = p.idCitizenship,
                name = p.name
            }));
        }

        [HttpGet]
        [Route("Citizenships")]
        public async Task<ActionResult> GetCitizenships()
        {
            var citizenships = await context.Citizenships.ToListAsync();
            return Ok(citizenships.Select(p => new
            {
                id = p.idCitizenship,
                fullName = p.fullName
            }));
        }

        [HttpGet]
        [Route("Regions")]
        public async Task<ActionResult> GetRegions()
        {
            var citizenships = await context.Regions.ToListAsync();
            return Ok(citizenships.Select(p => new
            {
                id = p.idRegion,
                Name = p.regionName
            }));
        }

        [HttpGet]
        [Route("Districts/{idRegion}")]
        public async Task<ActionResult> GetDistricts(int idRegion)
        {
            var districts = await context.Districts.Where(p=>p.idRegion == idRegion).ToListAsync();
            return Ok(districts.Select(p => new
            {
                id = p.idDistrict,
                Name = p.name
            }));
        }

        [HttpGet]
        [Route("Localities/{idDistrict}")]
        public async Task<ActionResult> GetDistrict(int idDistrict)
        {
            var localities = await context.Localities.Where(p => p.idDistrict == idDistrict).ToListAsync();
            return Ok(localities.Select(p => new
            {
                id = p.idLocality,
                Name = p.name
            }));
        }

        [HttpGet]
        [Route("Streets/{idLocality}")]
        public async Task<ActionResult> GetStreet(int idLocality)
        {
            var streets = await context.Streets.Where(p => p.idLocality == idLocality).ToListAsync();
            return Ok(streets.Select(p => new
            {
                id = p.idStreet,
                Name = p.name
            }));
        }
    }
}
