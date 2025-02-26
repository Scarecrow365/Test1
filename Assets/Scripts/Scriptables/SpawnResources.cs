using SpawnableObjects;
using Tags;
using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "SpawnResources", menuName = "KFC Scriptable Objects/Giant Spin/SpawnResources")]
    public class SpawnResources : ScriptableObject
    {
        [SerializeField] private Coin coinPrefab;
        [SerializeField] private Coal coalPrefab;

        public SpawnableObject GetResource(ItemTags spawnerItemTag)
        {
            switch (spawnerItemTag)
            {
                case ItemTags.GoldCoin or ItemTags.SilverCoin: return coinPrefab;
                case ItemTags.FireCoal or ItemTags.BlackCoal: return coalPrefab;
                default:
                    Debug.LogError("Asset not found");
                    return null;
            }
        }
    }
}