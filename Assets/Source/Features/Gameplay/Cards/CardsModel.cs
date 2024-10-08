using System;
using Source.Application;
using Source.Features.Gameplay.Items;

namespace Source.Features.Gameplay.Cards
{
    public class CardsModel : BaseModel<CardsModel>
    {
        public ItemType TargetItemType { get; private set; }
        
        public void TargetItem(ItemType itemType)
        {
            TargetItemType = itemType;
            UpdateModel();
        }
    }
}