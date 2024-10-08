using System.Collections.Generic;
using Source.Features.Gameplay.Items;
using UnityEngine;

namespace Source.Features.Gameplay.Cards
{
    [CreateAssetMenu]
    public class CardsConfiguration : ScriptableObject
    {
        [SerializeField] private List<CardsView> _items = new ();

        public bool TryGet(ItemType type, out CardsView prefab)
        {
            prefab = _items.Find(item => item.ItemType == type);
            return prefab != null;
        }
    }
}