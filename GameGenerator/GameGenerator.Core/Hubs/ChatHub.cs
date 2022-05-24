using GameGenerator.Core.Abstractions.Services;
using GameGenerator.Core.Abstractions.Services.MapUsers;
using GameGenerator.Core.Abstractions.Services.OnGoingGame;
using GameGenerator.Core.Models.MapUsers;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using System.Linq;
using GameGenerator.Core.Models.OnGoingGame;
using System.Collections.Generic;
using GameGenerator.Core.Models;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IUserService _userService;
        private readonly IConnectionService _connectionService;
        private readonly IOnGoingGameService _onGoingGameService;
        private readonly ICardService _cardService;
        private readonly IOnGoingCardService _onGoingCardService;

        public ChatHub(IUserService userService, IConnectionService connectionService, IOnGoingGameService onGoingGameService, ICardService cardService, IOnGoingCardService onGoingCardService)
        {
            _userService = userService;
            _connectionService = connectionService;
            _onGoingGameService = onGoingGameService;
            _cardService = cardService;
            _onGoingCardService = onGoingCardService;
        }

        
        public async Task AddNewConnection(string name,string groupName)
        {

            UserEntry user = await _userService.GetByIdAsync(name);
            

            if (user is null)
            {
                var newUser = new UserEntry
                {
                    UserName=name,
                    UserType= name=="admin" ? "admin" : "player", //ce face daca adauga playeru adaua nule de admin?
                    UserGroup=groupName
                };

                var game = await _onGoingGameService.GetByGroupAsync(groupName);
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

            if (name != "admin") await AddConnectedUser(groupName, name);
        }

        public async override Task OnDisconnectedAsync(Exception ex)
        {
            var connection = await _connectionService.GetConnection(Context.ConnectionId);
            
            if (connection is null)
            {
                return;
            }

            connection.Connected = false;

            await _connectionService.UpdateAsync(Context.ConnectionId, connection);

            await base.OnDisconnectedAsync(ex);
        }


        public async Task AddConnectedUser(string group, string name)
        {
            var users = await _userService.GetAllByGroupAsync(group);

            foreach (var user in users)
            {
                if (user.UserType == "admin")
                {
                    foreach (var connection in user.Connections)
                    {
                        await Clients.Client(connection.ConnectionID).SendAsync("AddConnectedUser", name);
                    }
                }
            }

        }


        public async Task StartRound(string gameName, int gameRound)
        {
            await GetCardsForAll(gameName, "black", 1);

            var playedCards = await _onGoingCardService.GetByGroupAsync(gameName);
            var users = await _userService.GetAllByGroupAsync(gameName);
            int nrOfCardsInPlay;

            foreach (var user in users)
            {
                nrOfCardsInPlay = playedCards.Where(c => c.Round == gameRound && c.UserName==user.UserName).Count();

                if (nrOfCardsInPlay < 5)
                {
                    await GetCardsForEach(user.UserName,gameName, "white", 5 - nrOfCardsInPlay);
                }
            }


            var onGoingGame = await _onGoingGameService.GetByGroupAsync(gameName);
            onGoingGame.CurrentRound += 1;
            await _onGoingGameService.UpdateAsync(onGoingGame.Id, onGoingGame);
        }


        public async Task GetCardsForAll(string groupName, string cardType, int nrOfCards)
        {

            var currentGame = await _onGoingGameService.GetByGroupAsync(groupName);

            var availableCards = await _cardService.GetAllAvailableForGameAsync(currentGame.GameId);

            var list = availableCards.Where(c => c.CardType == cardType).ToList();
            var extractedCards = list.OrderBy(arg => Guid.NewGuid()).Take(nrOfCards).ToList(); ;

            var users = await _userService.GetAllByGroupAsync(groupName);

            foreach (var user in users)
            {
                foreach (var connection in user.Connections.Where(c=>c.Connected==true))
                {
                    await Clients.Client(connection.ConnectionID).SendAsync("AddCards", extractedCards, cardType);
                }
            }

            foreach (var card in extractedCards)
            {
                await _onGoingCardService.CreateAsync(new OnGoingCardEntry() 
                {
                      CardId=card.Id,
                      Round=0,
                      UserName= Context.User.Identity.Name,
                      OnGoingGameGroup= groupName
                });

            }
        }

        public async Task GetCardsForEach(string userName,string groupName, string cardType, int nrOfCards)
        {

            var currentGame = await _onGoingGameService.GetByGroupAsync(groupName);

            List<CardEntry> availableCards=null;
            List<CardEntry> list = null;
            List<CardEntry> extractedCards = null;
            
            var users = await _userService.GetAllByGroupAsync(groupName);

            var user = users.FirstOrDefault(u => u.UserName == userName);

            availableCards = await _cardService.GetAllAvailableForGameAsync(currentGame.GameId);

            list = availableCards.Where(c => c.CardType == cardType).ToList();
            extractedCards = list.OrderBy(arg => Guid.NewGuid()).Take(nrOfCards).ToList();

            foreach (var connection in user.Connections.Where(c=>c.Connected==true))
            {
                await Clients.Client(connection.ConnectionID).SendAsync("AddCards", extractedCards , cardType);

            };

            foreach (var card in extractedCards)
            {
                await _onGoingCardService.CreateAsync(new OnGoingCardEntry()
                {
                    CardId = card.Id,
                    Round = currentGame.CurrentRound+1,
                    UserName = Context.User.Identity.Name,
                    OnGoingGameGroup = groupName
                });

            };

        }


    }
}