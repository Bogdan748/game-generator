using GameGenerator.Core.Models;
using GameGenerator.Models;
using System;


namespace CardGenerator.Extensions
{
    public static  class CardEntryExtensions
    {
        public static CardEntryViewModel ToViewModel(this CardEntry cardEntry)
        {
            if (cardEntry is null)
            {
                throw new ArgumentNullException(nameof(cardEntry));
            }

            return new CardEntryViewModel
            {
                Id = cardEntry.Id,
                Text=cardEntry.Text,
                CardType=cardEntry.CardType,
                GameId=cardEntry.GameId
            };
        }

        public static CardEntry ToCardEntry(this CardEntryViewModel viewModel)
        {
            if (viewModel is null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            return new CardEntry
            {
                Id = viewModel.Id,
                Text = viewModel.Text,
                CardType = viewModel.CardType,
                GameId = viewModel.GameId
            };
        }
    }
}
