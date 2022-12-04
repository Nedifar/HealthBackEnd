using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.ChatModels;
using WebApplication2.Models;
using WebApplication2.ViewModel;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        Models.context context;
        
        public ChatController(Models.context context)
        {
            this.context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> getChats()
        {
            return Ok(context.Dialogs.Where(p => p.Parent.Email == User.Identity.Name || p.Organization.Email == User.Identity.Name).ToList().Select(p => new
            {
                Organization = p.Organization.Name,
                Parent = p.Parent.LastName + " " + p.Parent.FirstName + " " + p.Parent.MiddleName,
                GroupName = p.name,
                Messages = p.Messages.Select(s => new
                {
                    s.textMessage,
                    sender = s.Parent == null ? "Org" : "Parent",
                    s.messageTime,
                }
                )
            }));
        }

        [HttpPost]
        [Route("connectWithOrg")]
        [Authorize]
        public async Task<ActionResult> ConnectWithOrg(ConnectWithOrgViewModel model)
        {
            var dialog = new Dialog { Parent = context.Parents.FirstOrDefault(p => p.Email == User.Identity.Name), Organization = context.Organizations.FirstOrDefault(p => p.Email == model.UserNameOrg) };
            dialog.name = dialog.Parent.Email +"|" + dialog.Organization.Email;
            context.Dialogs.Add(dialog);
            context.SaveChanges();
            return Ok(dialog.name);
        }
    }
}
