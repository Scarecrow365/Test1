using Tags;
using UnityEngine;

namespace SpawnableObjects
{
    public class Coin : SpawnableObject
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Color gold;
        [SerializeField] private Color silver;

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.transform.TryGetComponent(out Coin item)) 
                UpdateState(item.ItemTag);
        }

        private void UpdateState(ItemTags collisionItemTag)
        {
            if (ItemTag == ItemTags.SilverCoin && collisionItemTag == ItemTags.GoldCoin)
            {
                ItemTag = ItemTags.GoldCoin;
                UpdateView();
            }
        }

        protected override void UpdateView()
        {
            spriteRenderer.color = ItemTag switch
            {
                ItemTags.GoldCoin => gold,
                ItemTags.SilverCoin => silver,
                _ => spriteRenderer.color
            };
        }
    }
}