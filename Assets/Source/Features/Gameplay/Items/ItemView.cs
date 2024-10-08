using UnityEngine;

namespace Source.Features.Gameplay.Items
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private ItemType _itemType;
        [SerializeField] private Rigidbody _rigidbody;

        public Rigidbody Rigidbody => _rigidbody;

        public ItemType ItemType => _itemType;
    }
}