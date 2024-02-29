using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace WebApplication1.Controllers
{


    public class ChatHub : Hub
    {
    static    SortedList<string, string> Users = new();
        public async Task GetLive(string name,string id)
        {
            if (!string.IsNullOrWhiteSpace(id) &&Users.ContainsKey(id))
                return;
          
            string uid = Guid.NewGuid().ToString();
            Users.Add(uid, name);
            await Clients.Caller.SendAsync("LoggedInSuccessfully", uid);
            await Clients.All.SendAsync("NewUser",$"{name} is live now");
        }
        
        public async Task CustomMessage(string Message,string senderId, string id = null)
        {
            if(id==null)
            {
                
                await Clients.All.SendAsync("RecivedMessageAll", Users[senderId],Message);
            }
            else
            {
                await Clients.Users(id).SendAsync(Message);
            }
        }


    }
}
