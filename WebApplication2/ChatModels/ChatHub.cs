using Microsoft.AspNetCore.SignalR;
using WebApplication2.Models;

namespace WebApplication2.ChatModels
{
    public class ChatHub : Hub
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private context context;

        public ChatHub(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            var scope = _serviceScopeFactory.CreateScope();
            var _term = scope.ServiceProvider.GetRequiredService<context>();
            context = _term;
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public async Task Send(string message, string username, string groupname)
        {
            var parent = context.Parents.FirstOrDefault(p => p.Email == username);
            var org = context.Organizations.FirstOrDefault(p => p.Email == username);
            if (parent != null)
            {
                var messageSe = new Message { Dialog = context.Dialogs.FirstOrDefault(p=>p.name == groupname), messageTime = DateTime.Now, textMessage = message, Parent = parent };
                context.Add(messageSe);
                context.SaveChanges();
                await this.Clients.Group(groupname).SendAsync("Send", new { messageTime = messageSe.messageTime, message = messageSe.textMessage, sender = "parent"}, username);
            }
            else if(org!=null)
            {
                var messageSe = new Message { Dialog = context.Dialogs.FirstOrDefault(p => p.name == groupname), messageTime = DateTime.Now, textMessage = message, Organization = org };
                context.Add(messageSe);
                context.SaveChanges();
                await this.Clients.Group(groupname).SendAsync("Send", new { messageTime = messageSe.messageTime, message = messageSe.textMessage, sender = "org" }, username);
            }
        }

        public async Task Enter(string groupname)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupname);
        }

    }
}
