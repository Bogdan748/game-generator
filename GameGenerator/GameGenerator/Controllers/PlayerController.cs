using GameGenerator.Core.Abstractions.Services.OnGoingGame;
using GameGenerator.Extensions;
using GameGenerator.Models.MapUsers;
using GameGenerator.Models.OnGoingGame;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameGenerator.Controllers
{
    [AllowAnonymous]
    public class PlayerController : Controller
    {
        private readonly IOnGoingGameService _service;
        public PlayerController(IOnGoingGameService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Playground([Bind("UserName,UserGroup")] UserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var onGoingGame = await _service.GetByGroupAsync(viewModel.UserGroup);
                if (onGoingGame is not null)
                {
                    return View(viewModel);
                }
                else
                {
                    ViewBag.Error = $"There is no ongoing game with the Id {viewModel.UserGroup}";
                    return RedirectToAction(nameof(Index));
                }
                
            }

            ViewBag.Error = $"The Id {viewModel.UserGroup} is invalid.";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Playground(OnGoingGameViewModel viewModel)
        {

            return View(viewModel);
        }
    }
}
