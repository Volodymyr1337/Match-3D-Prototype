using UnityEngine;

namespace Source.Features.Gameplay.Board
{
    [CreateAssetMenu]
    public class BoardConfiguration : ScriptableObject
    {
        [SerializeField] private Vector2 _offset;
        [SerializeField] private Vector2 _area;

        public Vector2 Offset => _offset;
        public Vector2 Area => _area;
    }
}