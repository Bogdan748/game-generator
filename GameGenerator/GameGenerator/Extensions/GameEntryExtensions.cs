using GameGenerator.Core.Models;
using GameGenerator.Core.Models.OnGoingGame;
using GameGenerator.Models;
using GameGenerator.Models.OnGoingGame;
using System;


namespace GameGenerator.Extensions
{
    public static  class GameEntryExtensions
    {
        public static GameEntryViewModel ToViewModel(this GameEntry gameEntry)
        {
            if (gameEntry is null)
            {
                throw new ArgumentNullException(nameof(gameEntry));
            }

            return new GameEntryViewModel
            {
                Id = gameEntry.Id,
                Name=gameEntry.Name
            };
        }

        public static GameEntry ToGameEntry(this GameEntryViewModel viewModel)
        {
            if (viewModel is null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            return new GameEntry
            {
                Id = viewModel.Id,
                Name = viewModel.Name
            };
        }

        public static OnGoingGameEntry ToOnGoingGameEntry(this OnGoingGameViewModel viewModel)
        {
            if (viewModel is null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            return new OnGoingGameEntry
            {
                Id = viewModel.Id,
                GameGroup = viewModel.GameGroup,
                GameId=viewModel.GameId,
                CurrentRound=viewModel.CurrentRound
            };
        }

        public static OnGoingGameViewModel ToOnGoingGameViewModel(this OnGoingGameEntry entry)
        {
            if (entry is null)
            {
                throw new ArgumentNullException(nameof(entry));
            }

            return new OnGoingGameViewModel
            {
                Id = entry.Id,
                GameGroup = entry.GameGroup,
                GameId=entry.GameId,
                CurrentRound=entry.CurrentRound
                
            };
        }
    }
}
