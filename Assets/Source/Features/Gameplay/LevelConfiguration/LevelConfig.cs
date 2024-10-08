using System;
using System.Collections.Generic;
using Source.Features.Gameplay.Items;
using UnityEngine;

namespace Source.Features.Gameplay.LevelConfiguration
{
    [CreateAssetMenu]
    public class LevelConfig : ScriptableObject
    {
        public List<SpawnedItemData> itemsToSpawn;
        public int duration;
    }

    [Serializable]
    public class SpawnedItemData
    {
        public ItemType type;
        public int amount;
    }
}