using System;
using SpawnableObjects;
using Tags;
using UnityEngine;

namespace TriggerZones
{
    public class TriggerZone : MonoBehaviour
    {
        [field: SerializeField] public ItemTags TargetItemTag { get; private set; }
        
        public event Action OnTriggerPositive;
        public event Action OnTriggerNegative;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.transform.TryGetComponent(out SpawnableObject item)) 
                return;

            if (item.ItemTag == TargetItemTag)
            {
                item.Release();
                OnTriggerPositive?.Invoke();
            }
            else
            {
                OnTriggerNegative?.Invoke();
            }
        }
    }
}
