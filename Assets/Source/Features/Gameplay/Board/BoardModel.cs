using System;
using System.Collections.Generic;
using Source.Application;
using Source.Features.Gameplay.Items;

namespace Source.Features.Gameplay.Board
{
    public class BoardModel : BaseModel<BoardModel>
    {
        public Dictionary<ItemType, int> RemainingItemsOnField { get; } = new();
        public BoardConfiguration BoardConfiguration { get; }

        public BoardModel(BoardConfiguration boardConfiguration)
        {
            BoardConfiguration = boardConfiguration;
            UpdateModel();
        }
    }
}