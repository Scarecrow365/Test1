using Tags;
using UnityEngine;

namespace SpawnableObjects
{
    public abstract class SpawnableObject : MonoBehaviour
    {
        [field: SerializeField] public ItemTags ItemTag { get; protected set; }

        public virtual void Construct(ItemTags itemTag, Vector3 spawnPos)
        {
            ItemTag = itemTag;
            transform.localPosition = spawnPos;

            UpdateView();
        }

        public void Release()
        {
            gameObject.SetActive(false);
        }
        
        protected abstract void UpdateView();
    }
}