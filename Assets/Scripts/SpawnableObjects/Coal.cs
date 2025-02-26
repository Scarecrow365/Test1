using Tags;
using UnityEngine;

namespace SpawnableObjects
{
    public class Coal : SpawnableObject
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Color fireColor;
        [SerializeField] private Color blackColor;
        [SerializeField] private ParticleSystem particleFire;

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.transform.TryGetComponent(out Coal item)) 
                UpdateState(item.ItemTag);
        }

        private void UpdateState(ItemTags collisionItemTag)
        {
            if (ItemTag == ItemTags.BlackCoal && collisionItemTag == ItemTags.FireCoal)
            {
                ItemTag = ItemTags.FireCoal;
                UpdateView();
            }
        }

        protected override void UpdateView()
        {
            switch (ItemTag)
            {
                case ItemTags.FireCoal:
                    spriteRenderer.color = fireColor;
                    particleFire.gameObject.SetActive(true);
                    particleFire.Play();
                    break;
                case ItemTags.BlackCoal:
                    spriteRenderer.color = blackColor;
                    particleFire.gameObject.SetActive(false);
                    break;
            }
        }
    }
}