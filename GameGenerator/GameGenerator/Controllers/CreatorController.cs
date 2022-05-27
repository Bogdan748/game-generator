﻿
using CardGenerator.Extensions;
using GameGenerator.Core.Abstractions.Services;
using GameGenerator.Core.Abstractions.Services.MapUsers;
using GameGenerator.Core.Abstractions.Services.OnGoingGame;
using GameGenerator.Core.Models.MapUsers;
using GameGenerator.Core.Models.OnGoingGame;
using GameGenerator.Extensions;
using GameGenerator.Infrastructure;
using GameGenerator.Models;
using GameGenerator.Models.OnGoingGame;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameGenerator.Controllers
{
    public class CreatorController : Controller
    {
        private readonly IOnGoingGameService _onGoingGameService;
        private readonly IUserService _usersService;
        private readonly IGameService _gameService;

        public CreatorController(IOnGoingGameService onGoingGameService, IUserService usersService, IGameService gameService)
        {
            _onGoingGameService = onGoingGameService;
            _usersService = usersService;
            _gameService = gameService;
        }
        public async Task<IActionResult> Index()
        {
            var games = await _gameService.GetAllAsync();
            ViewBag.listOfGames = games;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StartGame([Bind("GameGroup","GameId")] OnGoingGameViewModel viewModel)
        {
            viewModel.CurrentRound = 1;
            

            if (ModelState.IsValid)
            {

                await _onGoingGameService.CreateAsync(viewModel.ToOnGoingGameEntry());

                var user = new UserEntry
                {
                    UserGroup = viewModel.GameGroup,
                    UserName = HttpContext.User.Identity.Name,
                    UserType = "admin",
                    OnGoingGameEntry = await _onGoingGameService.GetByGroupAsync(viewModel.GameGroup)
                };

                await _usersService.CreateAsync(user);

               

                ViewBag.usrName = HttpContext.User.Identity.Name;
                

                return View(viewModel);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> OnGoingGame()  //TESTE
        {
            OnGoingGameEntry viewModel = await _onGoingGameService.GetByIdAsync(2);

            return View(viewModel.ToOnGoingGameViewModel());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGoingGame([Bind("GameGroup")] OnGoingGameViewModel viewModel)
        {
            OnGoingGameEntry onGoingGame = await _onGoingGameService.GetByGroupAsync(viewModel.GameGroup);

            return View(onGoingGame.ToOnGoingGameViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EndGame([Bind("GameGroup")] OnGoingGameViewModel viewModel)
        {
            OnGoingGameEntry onGoingGame = await _onGoingGameService.GetByGroupAsync(viewModel.GameGroup);

            return View(onGoingGame.ToOnGoingGameViewModel());
        }
    }
}
