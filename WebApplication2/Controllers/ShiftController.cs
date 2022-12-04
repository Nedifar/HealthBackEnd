using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftController : ControllerBase
    {
        Models.context context;

        public ShiftController(context context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("getshifts")]
        public async Task<ActionResult> GetShitft()
        {
            return Ok(context.Shifts.ToList().Select(p => new
            {
                Camp = new
                {
                    Name = p.Camp.campName,
                    Type = p.Camp.TypeCamp.ToString(),
                    HaveSportObjects = p.Camp.haveSportObjects,
                    Telephone = p.Camp.supportTelephone,
                    Address = p.Camp.Address,
                    Description = p.Camp.Description,
                    FoodInformation = p.Camp.foodInformation,
                    TermsAndPayment = p.Camp.TermsAndPayment,
                    TerritoryArea = p.Camp.territoryArea,
                    WorkingMode = p.Camp.WorkingMode,
                    Photos = p.Camp.CampPhotos.Select(s => new { url = s.url }),
                    Certificates = p.Camp.Certificate.Select(s => new { url = s.url })
                },
                Name = p.ShiftName,
                Capacity = p.Capacity,
                FreeSeats = p.FreeSeats,
                Price = p.Price,
                DateBegin = p.DateBegin.Date,
                DateEnd = p.DateEnd.Date,
                Dauration = p.Duration,
                SeasonCamp = p.SeasonCamp.ToString(),
                Feedbacks = p.Feedbacks.Select(s => new
                {
                    ParentName = s.Parent.FirstName + " " + s.Parent.MiddleName,
                    Comment = s.comment,
                    DatePublished = s.datePublished.Date,
                    Estimation = s.estimation
                })
            }));
        }

        [HttpPost]
        [Authorize(Roles = "Organization")]
        [Route("createCamp")]
        public async Task<ActionResult> CreateCamp(ViewModel.CreateCampViewModel model)
        {
            try
            {
                var camp = new Camp
                {
                    Address = model.Address,
                    TermsAndPayment = model.TermsAndPayment,
                    Description = model.Description,
                    campName = model.CampName,
                    supportTelephone = model.SupportTelephone,
                    haveSportObjects = model.HaveSportObjects,
                    foodInformation = model.FoodInformation,
                    housingCount = model.HousingCount,
                    territoryArea = model.TerritoryArea,
                    WorkingMode = model.WorkingMode,
                    TypeCamp = model.TypeCamp,
                    Organization = context.Organizations.FirstOrDefault(p => p.Email == User.Identity.Name)
                };
                context.Camps.Add(camp);
                context.SaveChanges();
                return Ok("Успешно");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Organization")]
        [Route("AddPhotosToCamp")]
        public async Task<ActionResult> AddPhotos(List<IFormFile> formFiles, [FromForm] int idCamp)
        {
            try
            {
                var camp = context.Camps.FirstOrDefault(p => p.idCamp == idCamp);
                int counter = 0;
                counter = camp.CampPhotos.LastOrDefault()?.idCampPhoto ?? 0;
                if (camp == null) return BadRequest("Данного лагеря не существует");
                foreach(var file in formFiles)
                {
                    if(file.Length > 0)
                    {
                        var filePath = $"{AppDomain.CurrentDomain.BaseDirectory}images/CampPhotos/camp{camp.idCamp}{counter + 1}.jpeg";
                        using(var stream = System.IO.File.Create(filePath))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }
                    camp.CampPhotos.Add(new CampPhoto { url = $"https://gamification.oksei.ru/HealthServer/CampPhotos/camp{camp.idCamp}{counter + 1}.jpeg" });
                    counter++;
                }
                context.SaveChanges();
                return Ok("Успешно");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Organization")]
        [Route("AddCertificateToCamp")]
        public async Task<ActionResult> AddCert(List<IFormFile> formFiles, [FromForm] int idCamp)
        {
            try
            {
                var camp = context.Camps.FirstOrDefault(p => p.idCamp == idCamp);
                int counter = 0;
                counter = camp.Certificate.LastOrDefault()?.idCertificate ?? 0;
                if (camp == null) return BadRequest("Данного лагеря не существует");
                foreach (var file in formFiles)
                {
                    if (file.Length > 0)
                    {
                        var filePath = $"{AppDomain.CurrentDomain.BaseDirectory}images/Certificates/cert{camp.idCamp}{counter + 1}.jpeg";
                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }
                    camp.Certificate.Add(new Certificate { url = $"https://gamification.oksei.ru/HealthServer/Certificates/cert{camp.idCamp}{counter + 1}.jpeg" });
                    counter++;
                }
                context.SaveChanges();
                return Ok("Успешно");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Organization")]
        [Route("createShift")]
        public async Task<ActionResult> CreateShift(ViewModel.CreateShiftViewModel model)
        {
            try
            {
                var shift = new Shift
                {
                    ShiftName = model.ShiftName,
                    DateBegin = model.DateBegin,
                    DateEnd = model.DateEnd,
                    Capacity = model.Capacity,
                    Price = model.Price,
                    SeasonCamp = model.SeasonCamp,
                    idCamp = model.idCamp

                };
                context.Shifts.Add(shift);
                context.SaveChanges();
                return Ok("Успешно");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
