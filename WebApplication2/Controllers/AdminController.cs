using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Math;
using System.Xml.Linq;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly Models.context context;
        public AdminController(context context)
        {
            this.context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetInfs()
        {
            return Ok(context.Organizations.ToList().Select(p => new
            {
                Name = p.Name,
                Camps = p.Camps.Select(s => new
                {
                    Certificates = s.Certificate.Select(l => new
                    {
                        l.url
                    }),
                    CampPhotos = s.CampPhotos.Select(l => new
                    {
                        l.url
                    }),
                    foodInformation = s.foodInformation,
                    Description = s.Description,
                    TermsAndPayment = s.TermsAndPayment,
                    territoryArea = s.territoryArea,
                    Address = s.Address,
                    campName = s.campName,
                    supportTelephone = s.supportTelephone,
                    TypeCamp = s.TypeCamp.ToString(),
                    haveSportObjects = s.haveSportObjects,
                    Shifts = s.Shifts.Select(l => new
                    {
                        ShiftName = l.ShiftName,
                        Duration = l.Duration,
                        DateBegin = l.DateBegin.Date,
                        DateEnd = l.DateEnd.Date,
                        Price = l.Price,
                        FreeSeats = l.FreeSeats,
                        Capacity = l.Capacity,
                        Requests = l.Requests.Select(p => new
                        {
                            ShiftName = p.Shift.ShiftName,
                            CampName = p.Shift.Camp.campName,
                            Organization = p.Shift.Camp.Organization.Name,
                            AmountToBePaid = p.AmountToBePaid,
                            IsConfirmed = p.isConfirmed,
                            IsPaid = p.IsPaid,
                            Child = p.Child.LastName + " " + p.Child.FirstName + " " + p.Child.MiddleName
                        })

                    })
                })
            }));
        }
    }
}
