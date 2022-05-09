using GameGenerator.Core.Models;
using GameGenerator.Models;
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
    }
}
