using GameGenerator.Core.Abstractions.Services.MapUsers;
using GameGenerator.Core.Models.MapUsers;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IUserService _userService;
        private readonly IConnectionService _connectionService;

        public ChatHub(IUserService userService, IConnectionService connectionService)
        {
            _userService = userService;
            _connectionService = connectionService;
        }

        
        public async Task AddConnection(string name,string groupName)
        {
  
            var user= await _userService.GetByIdAsync(name);

            if (user is null)
            {
                var newUser = new UserEntry
                {
                    UserName=name,
                    UserType="player",
                    UserGroup=groupName
                };

                await _userService.CreateAsync(newUser);
            }

            var newConnection = new ConnectionEntry
            {
                ConnectionID = Context.ConnectionId,
                UserAgent = Context.GetHttpContext().Request.Headers["User-Agent"],
                Connected = true,
                UserName=name
            };

            await _connectionService.CreateAsync(newConnection);

            
        }

        public async override Task OnDisconnectedAsync(Exception ex)
        {
            var connection = await _connectionService.GetConnection(Context.ConnectionId);

            connection.Connected = false;

            await _connectionService.UpdateAsync(Context.ConnectionId, connection);

            await base.OnDisconnectedAsync(ex);
        }


        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}