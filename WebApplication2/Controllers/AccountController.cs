using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebApplication2.Models;
using Microsoft.AspNetCore.Identity;
using WebApplication2.Email;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using WebApplication2.ViewModel;
using Org.BouncyCastle.Asn1.Mozilla;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly Models.context context;
        public AccountController(context context)
        {
            this.context = context;
        }
        //private List<Parent> people = new List<Parent>
        //{
        //    new Parent{Login="admin@gmail.com", Password="12345", Role = "admin"},
        //    new Parent{Login="qwerty@gmail.com", Password="55555", Role = "user"}
        //};

        [HttpPost]
        [Route("reg")]
        public async Task<ActionResult> Registration(ViewModel.RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var us = context.Parents.FirstOrDefault(p => p.Email == model.Email);
                    if (us != null)
                        return BadRequest("Пользователь с такиим email уже существует.");
                    var code = new Random().Next(111111, 999999);
                    context.Parents.Add(new Parent { Email = model.Email, Password = HashPassword(model.Password), FirstName = model.FirstName, LastName = model.LastName, MiddleName = model.MiddleName, confirmCode = code });
                    context.SaveChanges();
                    EmailService emailService = new EmailService();
                    await emailService.SendEmailAsync(model.Email, "Подтвердите свой аккаунт",
                        $"Код подтверждения: <a>{code}</a>");
                    return Ok("Регистрация завершена. На вашу почту отправлено письмо с подтверждением.");
                    //else if (model.role == "Организация")
                    //{
                    //    var us = context.Organizations.FirstOrDefault(p => p.Email == model.Email);
                    //    if (us != null)
                    //        return BadRequest("Пользователь с такиим email уже существует.");
                    //    var code = new Random().Next(111111, 999999);
                    //    context.Organizations.Add(new Organization { Email = model.Email, Password = HashPassword(model.Password), confirmCode = code });
                    //    context.SaveChanges();
                    //    EmailService emailService = new EmailService();
                    //    await emailService.SendEmailAsync(model.Email, "Подтвердите свой аккаунт",
                    //        $"Код подтверждения: <a>{code}</a>");
                    //    return Ok("Регистрация завершена.");
                    //}
                }
                else
                {
                    return BadRequest("Некорректные данные.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }


        [HttpPost]
        [Route("emailConfirm")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmCodeReset(EmailConfirmViewModel model)
        {
            if (model.Email == null || model.code == null)
            {
                return BadRequest();
            }
            var user = context.Parents.FirstOrDefault(p => p.Email == model.Email);
            if (user != null)
            {
                if (user.confirmCode == int.Parse(model.code))
                {
                    user.emailIsConfirmed = true;
                    user.confirmCode = 0;
                    context.SaveChanges();
                    return Ok("Успешно подтверждено");
                }
                else
                    return BadRequest("Неверный код");
            }
            return BadRequest("Такого пользователя не существует.");
        }

        private static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return ByteArraysEqual(buffer3, buffer4);
        }

        private static bool ByteArraysEqual(byte[] firstHash, byte[] secondHash)
        {
            int _minHashLength = firstHash.Length <= secondHash.Length ? firstHash.Length : secondHash.Length;
            var xor = firstHash.Length ^ secondHash.Length;
            for (int i = 0; i < _minHashLength; i++)
                xor |= firstHash[i] ^ secondHash[i];
            return 0 == xor;
        }

        [HttpPost]
        [Route("sign")]
        public IActionResult Token(SignViewModel model)
        {
            var claims = User.Claims.ToList();
            var identity = GetIdentity(model.Email, model.Password);
            if (identity == null)
                return BadRequest(new { errorText = "Invalid username or password." });
            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name,
                role = identity.Claims.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value
            };

            return new JsonResult(response);
        }

        [HttpGet]
        [Route("getProfileData")]
        [Authorize(Roles = "Parent")]
        public async Task<ActionResult> GetProfileData()
        {
            try
            {
                var user = context.Parents.FirstOrDefault(p => p.Email == User.Identity.Name);
                return Ok(new
                {
                    TypeParent = user.ParentStatus.ToString(),
                    Citizenship = user.Citizenship?.fullName,
                    LastName = user.LastName,
                    FirstName = user.FirstName,
                    MiddleName = user.MiddleName,
                    Birthday = user.Birthday,
                    PersonalDocument = new
                    {
                        TypeDocument = user.PersonalDocument?.PlacementDocument.ToString(),
                        Series = user.PersonalDocument?.series,
                        Number = user.PersonalDocument?.number,
                        DateOfIssue = user.PersonalDocument?.dateOfIssue,
                        Issue = user.PersonalDocument?.issuedBy,
                        ValidityTime = user.PersonalDocument?.PlacementDocument == PlacementDocument.Other ? user.PersonalDocument.validityTime : null
                    },
                    AddressRegistration = new
                    {
                        Index = user.RegistrationAddress?.index,
                        Country = user.RegistrationAddress?.Citizenship?.name,
                        Region = user.RegistrationAddress?.region,
                        District = user.RegistrationAddress?.district,
                        Locality = user.RegistrationAddress?.locality,
                        Street = user.RegistrationAddress?.street,
                        House = user.RegistrationAddress?.houseHumber,
                        Housing = user.RegistrationAddress?.housing,
                        Flat = user.RegistrationAddress?.flat
                    },
                    FactAddress = new
                    {
                        Index = user.FactAddress?.index,
                        Country = user.FactAddress?.Citizenship?.name,
                        Region = user.FactAddress?.region,
                        District = user.FactAddress?.district,
                        Locality = user.FactAddress?.locality,
                        Street = user.FactAddress?.street,
                        House = user.FactAddress?.houseHumber,
                        Housing = user.FactAddress?.housing,
                        Flat = user.FactAddress?.flat
                    },
                    Snils = user.Snils,
                    Telephone = user.telephoneNumber,
                    Email = user.Email,
                    Children = user.Children.Select(p => new
                    {
                        idChild = p.idChild,
                        LastName = p.LastName,
                        FirstName = p.FirstName,
                        MiddleName = p.MiddleName,
                        Birthday = p.Birthday,
                        PersonalDocument = new
                        {
                            TypeDocument = p.PersonalDocument?.PlacementDocument.ToString(),
                            ViewDocument = p.PersonalDocument?.TypePersonalDocument.ToString(),
                            Series = p.PersonalDocument?.series,
                            Number = p.PersonalDocument?.number,
                            DateOfIssue = p.PersonalDocument?.dateOfIssue,
                            Issue = p.PersonalDocument?.issuedBy,
                            ValidityTime = p.PersonalDocument?.PlacementDocument == PlacementDocument.Other ? user.PersonalDocument.validityTime : null
                        },
                        AddressRegistration = new
                        {
                            Index = p.RegistrationAddress?.index,
                            Country = p.RegistrationAddress?.Citizenship?.name,
                            Region = p.RegistrationAddress?.region,
                            District = p.RegistrationAddress?.district,
                            Locality = p.RegistrationAddress?.locality,
                            Street = p.RegistrationAddress?.street,
                            House = p.RegistrationAddress?.houseHumber,
                            Housing = p.RegistrationAddress?.housing,
                            Flat = p.RegistrationAddress?.flat
                        },
                        Snils = p.Snils,
                        Telephone = p.telephoneNumber,
                    })
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("editMainDataParent")]
        [Authorize(Roles = "Parent")]
        public async Task<ActionResult> EditMainDataParent(EditMainDataViewModel model)
        {
            try
            {
                var user = await context.Parents.FirstOrDefaultAsync(p => p.Email == User.Identity.Name);
                user.Birthday = model.Birthday ?? user.Birthday;
                user.ParentStatus = model.ParentStatus;
                user.telephoneNumber = model.Telephone ?? user.telephoneNumber;
                user.FirstName = model.FirstName ?? user.FirstName;
                user.LastName = model.LastName ?? user.LastName;
                user.MiddleName = model.MiddleName ?? user.MiddleName;
                user.Citizenship = context.Citizenships.FirstOrDefault(p => p.fullName == model.Citizenship);
                user.Snils = model.Snils ?? user.Snils;
                context.SaveChanges();
                return Ok("Данные были успешно изменены.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("editMainDataChild")]
        [Authorize(Roles = "Parent")]
        public async Task<ActionResult> EditMainDataChild(EditMainDataChildViewModel model)
        {
            try
            {
                var user = await context.Parents.FirstOrDefaultAsync(p => p.Email == User.Identity.Name);
                var child = user.Children.FirstOrDefault(p => p.idChild == model.idChild);
                child = child ?? new Child();

                child.Birthday = model.Birthday ?? child.Birthday;
                child.telephoneNumber = model.Telephone ?? child.telephoneNumber;
                child.FirstName = model.FirstName ?? child.FirstName;
                child.LastName = model.LastName ?? child.LastName;
                child.MiddleName = model.MiddleName ?? child.MiddleName;
                child.Citizenship = context.Citizenships.FirstOrDefault(p => p.fullName == model.Citizenship);
                child.Snils = model.Snils ?? child.Snils;
                if(child.idChild == 0)
                {
                    user.Children.Add(child);
                }
                context.SaveChanges();
                return Ok("Данные были успешно изменены.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("editPersonalDataParent")]
        [Authorize(Roles = "Parent")]
        public async Task<ActionResult> EditPersonalDocumentDataParent(EditPersonalDocumentViewModel model)
        {
            try
            {
                var user = await context.Parents.FirstOrDefaultAsync(p => p.Email == User.Identity.Name);
                var doc = user.PersonalDocument ?? new PersonalDocument();
                doc.PlacementDocument = model.PlacementDocument;
                doc.dateOfIssue = model.DateOfIssue ?? doc.dateOfIssue;
                doc.validityTime = model.ValidityTime ?? doc.validityTime;
                doc.series = model.Series ?? doc.series;
                doc.number = model.Number ?? doc.number;
                doc.issuedBy = model.Issue ?? doc.issuedBy;
                if (doc.idPersonalDocument == 0)
                    user.PersonalDocument = doc;
                context.SaveChanges();
                return Ok("Данные были успешно изменены.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("editPersonalDataChild")]
        [Authorize(Roles = "Parent")]
        public async Task<ActionResult> EditPersonalDocumentDataChild(EditPersonalDocumentChildViewModel model)
        {
            try
            {
                var user = await context.Parents.FirstOrDefaultAsync(p => p.Email == User.Identity.Name);
                var child = user.Children.FirstOrDefault(p => p.idChild == model.idChild);
                if(child == null)
                {
                    return NotFound("Такого ребенка не существует.");
                }
                var doc = child.PersonalDocument ?? new PersonalDocument();
                doc.TypePersonalDocument = model.TypePersonalDocument;
                doc.PlacementDocument = model.PlacementDocument;
                doc.dateOfIssue = model.DateOfIssue ?? doc.dateOfIssue;
                doc.validityTime = model.ValidityTime ?? doc.validityTime;
                doc.series = model.Series ?? doc.series;
                doc.number = model.Number ?? doc.number;
                doc.issuedBy = model.Issue ?? doc.issuedBy;
                if (doc.idPersonalDocument == 0)
                    child.PersonalDocument = doc;
                context.SaveChanges();
                return Ok("Данные были успешно изменены.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("editAddressRegDataChild")]
        [Authorize(Roles = "Parent")]
        public async Task<ActionResult> EditAddressRegDataChild(EditAddressRegDataViewModel model)
        {
            try
            {
                var user = await context.Parents.FirstOrDefaultAsync(p => p.Email == User.Identity.Name);
                var child = user.Children.FirstOrDefault(p => p.idChild == model.idChild);
                if (child == null)
                {
                    return NotFound("Такого ребенка не существует.");
                }
                var reg = child.RegistrationAddress ?? new RegistrationAddress();
                reg.Citizenship = await context.Citizenships.FirstOrDefaultAsync(p=>p.name == model.Country);
                reg.district = model.District ?? reg.district;
                reg.flat = model.Flat ?? reg.flat;
                reg.houseHumber = model.House ?? reg.houseHumber;
                reg.housing = model.Housing ?? reg.housing;
                reg.index = model.Index ?? reg.index;
                reg.locality = model.Locality ?? reg.locality;
                reg.region = model.Region ?? reg.region;
                reg.street = model.Street ?? reg.street;
                if (reg.idRegistrationAddress == 0)
                    child.RegistrationAddress = reg;
                context.SaveChanges();
                return Ok("Данные были успешно изменены.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("editFactAddressDataChild")]
        [Authorize(Roles = "Parent")]
        public async Task<ActionResult> EditAddressFactDataChild(EditAddressRegDataViewModel model)
        {
            try
            {
                var user = await context.Parents.FirstOrDefaultAsync(p => p.Email == User.Identity.Name);
                var child = user.Children.FirstOrDefault(p => p.idChild == model.idChild);
                if (child == null)
                {
                    return NotFound("Такого ребенка не существует.");
                }
                var reg = child.FactAddress ?? new FactAddress();
                reg.Citizenship = await context.Citizenships.FirstOrDefaultAsync(p => p.name == model.Country);
                reg.district = model.District ?? reg.district;
                reg.flat = model.Flat ?? reg.flat;
                reg.houseHumber = model.House ?? reg.houseHumber;
                reg.housing = model.Housing ?? reg.housing;
                reg.index = model.Index ?? reg.index;
                reg.locality = model.Locality ?? reg.locality;
                reg.region = model.Region ?? reg.region;
                reg.street = model.Street ?? reg.street;
                if (reg.idFactAddress == 0)
                    child.FactAddress = reg;
                context.SaveChanges();
                return Ok("Данные были успешно изменены.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("editAddressRegDataParent")]
        [Authorize(Roles = "Parent")]
        public async Task<ActionResult> EditAddressRegDataParent(EditAddressRegDataViewModel model)
        {
            try
            {
                var user = await context.Parents.FirstOrDefaultAsync(p => p.Email == User.Identity.Name);
                var reg = user.RegistrationAddress ?? new RegistrationAddress();
                reg.Citizenship = await context.Citizenships.FirstOrDefaultAsync(p => p.name == model.Country);
                reg.district = model.District ?? reg.district;
                reg.flat = model.Flat ?? reg.flat;
                reg.houseHumber = model.House ?? reg.houseHumber;
                reg.housing = model.Housing ?? reg.housing;
                reg.index = model.Index ?? reg.index;
                reg.locality = model.Locality ?? reg.locality;
                reg.region = model.Region ?? reg.region;
                reg.street = model.Street ?? reg.street;
                if (reg.idRegistrationAddress == 0)
                    user.RegistrationAddress = reg;
                context.SaveChanges();
                return Ok("Данные были успешно изменены.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("editFactAddressDataParent")]
        [Authorize(Roles = "Parent")]
        public async Task<ActionResult> EditAddressFactDataParent(EditAddressRegDataViewModel model)
        {
            try
            {
                var user = await context.Parents.FirstOrDefaultAsync(p => p.Email == User.Identity.Name);
                var reg = user.FactAddress ?? new FactAddress();
                reg.Citizenship = await context.Citizenships.FirstOrDefaultAsync(p => p.name == model.Country);
                reg.district = model.District ?? reg.district;
                reg.flat = model.Flat ?? reg.flat;
                reg.houseHumber = model.House ?? reg.houseHumber;
                reg.housing = model.Housing ?? reg.housing;
                reg.index = model.Index ?? reg.index;
                reg.locality = model.Locality ?? reg.locality;
                reg.region = model.Region ?? reg.region;
                reg.street = model.Street ?? reg.street;
                if (reg.idFactAddress == 0)
                    user.FactAddress = reg;
                context.SaveChanges();
                return Ok("Данные были успешно изменены.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("deleteChild")]
        [Authorize(Roles = "Parent")]
        public async Task<ActionResult> DeleteChild([FromForm]int idChild)
        {
            try
            {
                var user = await context.Parents.FirstOrDefaultAsync(p => p.Email == User.Identity.Name);
                var child = user.Children.FirstOrDefault(p => p.idChild == idChild);
                if (child == null)
                {
                    return NotFound("Такого ребенка не существует.");
                }
                context.Children.Remove(child);
                context.SaveChanges();
                return Ok("Ребенок УСПЕШНО удален.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        private ClaimsIdentity GetIdentity(string username, string password)
        {
            var parents = context.Parents.ToList();
            Parent parent = null;
            Organization org = null;
            Admin admin = context.Admins.Where(p=>p.Password == password && p.Email == username).FirstOrDefault();
            foreach (var item in parents)
            {
                if (VerifyHashedPassword(item.Password, password))
                {
                    parent = item;
                }
            }
            var orgs = context.Organizations.ToList();
            foreach (var item in orgs)
            {
                if (VerifyHashedPassword(item.Password, password))
                {
                    org = item;
                }
            }
            if (parent != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, parent.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, "Parent"),

                };
                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            else if (org != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, org.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, "Organization"),

                };
                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            else if(admin != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, admin.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, "Admin"),

                };
                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null;
        }

        [HttpPost]
        [Route("sendfeedback")]
        [Authorize(Roles ="Parent")]
        public async Task<ActionResult> SendComment(ViewModel.SendCommentViewModel model)
        {
            var parent = context.Parents.FirstOrDefault(p => p.Email == User.Identity.Name);
            parent.Feedbacks.Add(new Feedback
            {
                comment = model.Comment,
                datePublished = model.DatePublished,
                estimation = model.Estimation,
                idShift = model.idShift
            });
            context.SaveChanges();
            return Ok("Комментарий успешно оставлен.");
        }

        [HttpGet]
        [Route("requests")]
        [Authorize(Roles = "Parent")]
        public async Task<ActionResult> GetRequestsList()
        {
            var parent = context.Parents.FirstOrDefault(p => p.Email == User.Identity.Name);
            
            context.SaveChanges();
            return Ok(parent.Requests.Select(p=> new
            {
                ShiftName = p.Shift.ShiftName,
                CampName = p.Shift.Camp.campName,
                Organization = p.Shift.Camp.Organization.Name,
                AmountToBePaid =  p.AmountToBePaid,
                IsConfirmed =  p.isConfirmed,
                IsPaid =  p.IsPaid,
                Child = p.Child.LastName + " " + p.Child.FirstName + " " + p.Child.MiddleName
            }));
        }
    }
}
