using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;
using System.Text.RegularExpressions;
using WebApplication2.Email;
using WebApplication2.Models;
using WebApplication2.ViewModel;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequsetController : ControllerBase
    {
        Models.context context;

        public RequsetController(context context)
        {
            this.context = context;
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles ="Parent")]
        public async Task<ActionResult> CreateRequest(CreateRequestViewModel model)
        {
            context.Requests.Add(new Models.Request { idChild = model.idChild, idShift = model.idShift, AmountToBePaid = model.AmountToBePaid });
            var child = context.Children.FirstOrDefault(p=>p.idChild == model.idChild);
            var shift = context.Shifts.FirstOrDefault(p=>p.idShift == model.idShift);
            WordprocessingDocument wordDoc = WordprocessingDocument.Open($"{AppDomain.CurrentDomain.BaseDirectory}DockPatterns/ContractPattern.docx", true);
            string docText = null;
            using(StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
            {
                docText = sr.ReadToEnd();
            }
            ReplaceMethod("<docNumber>", new Random().Next(1111111, 9999999).ToString(), docText);
            ReplaceMethod("<docDay>", DateTime.Now.Day.ToString(), docText);
            ReplaceMethod("<docMonth>", DateTime.Now.Month.ToString(), docText );
            ReplaceMethod("<campType>", shift.Camp.TypeCamp.ToString(), docText);
            ReplaceMethod("<campDirLastName>", "", docText);
            ReplaceMethod("<campDirFirstName>", "", docText);
            ReplaceMethod("<campDirMiddleName>", "", docText);
            ReplaceMethod("<clientLastName>", child.Parent.LastName, docText);
            ReplaceMethod("<clientFirstName>", child.Parent.FirstName, docText);
            ReplaceMethod("<clientMiddleName>", child.Parent.MiddleName, docText);
            ReplaceMethod("<campName>", shift.Camp.campName, docText);
            ReplaceMethod("<campOnePrice>", shift.Price.ToString(), docText);
            ReplaceMethod("<campGeneralPrice>", shift.Price.ToString(), docText);
            ReplaceMethod("<shiftName>", shift.ShiftName, docText);
            ReplaceMethod("<shiftNumber>", "1", docText);
            ReplaceMethod("<shiftGrantProcent>", "", docText);
            ReplaceMethod("<shiftTotalPrice>", shift.Price.ToString(), docText);
            ReplaceMethod("<CampAdress>", shift.Camp.Address, docText);
            ReplaceMethod("<campPhoneNumber>", shift.Camp.supportTelephone, docText);
            ReplaceMethod("<campINN>", shift.Camp.Organization.INN, docText);
            ReplaceMethod("<campKPP>", shift.Camp.Organization.KPP, docText);
            ReplaceMethod("<campOKPO>", shift.Camp.Organization.OKPO, docText);
            ReplaceMethod("<campOGRN>", shift.Camp.Organization.OGRN, docText);
            ReplaceMethod("<checkNumber>", shift.Camp.Organization.CheckNumber, docText);
            ReplaceMethod("<checkCorres>", shift.Camp.Organization.CheckCorres, docText);
            ReplaceMethod("<checkBIK>", shift.Camp.Organization.BIK, docText);
            ReplaceMethod("<campEMail>", shift.Camp.Organization.Email, docText);
            ReplaceMethod("<campDirectorABB>", shift.Camp.Organization.Director, docText);
            ReplaceMethod("<passportSerial>", child.Parent.PersonalDocument.series, docText);
            ReplaceMethod("<passportNumber>", child.Parent.PersonalDocument.number, docText);
            ReplaceMethod("<passportGetDate>", child.Parent.PersonalDocument.dateOfIssue.Date.ToString(), docText);
            ReplaceMethod("<passportGetOrganization>", child.Parent.PersonalDocument.issuedBy, docText);

            using (var sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
            {
                sw.Write(docText);
            }

            EmailService emailService = new EmailService();
            await emailService.SendEmailAsync(child.Parent.Email, "Документ",
                $"Для завершения оформления путевки вам необходимо произвести оплату: ");

            context.SaveChanges();
            return Ok("Заявка успешно создана.");
        }

        private void ReplaceMethod(string old, string neww, string docText)
        {
            string regex = "";
            docText.Replace(old, neww);
            Regex regexText = new Regex(regex);
            docText = regexText.Replace(docText, neww);
        }

        [HttpPost]
        [Route("pay")]
        [Authorize(Roles = "Parent")]
        public async Task<ActionResult> PayRequest(PayRequestViewModel model)
        {
            var request = context.Requests.FirstOrDefault(p=>p.idRequest == model.idRequest);
            if (request == null)
                return NotFound();
            request.IsPaid = true;
            request.PaymentType = model.PaymentType;
            context.SaveChanges();
            return Ok("Заявка успешно создана.");
        }

        [HttpPost]
        [Route("confirm")]
        [Authorize(Roles ="Organization")]
        public async Task<ActionResult> ConfirmRequest(ConfirmRequestViewModel model)
        {
            var request = context.Requests.FirstOrDefault(p => p.idRequest == model.idRequest);
            if (request == null)
                return NotFound();
            request.isConfirmed = true;
            context.SaveChanges();
            return Ok("Успешно");
        }

        public record class ConfirmRequestViewModel(int idRequest);

    }
}
