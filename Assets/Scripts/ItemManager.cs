using System;
using Tags;
using UnityEngine;

public class ItemManager
{
    private int allCollectiblesCount;
    private int collectedTargetObjectsCount;
        
    private Spawner[] spawners;
    private ItemTags targetItemTag;

    public int CompletePercentage { get; private set; }
        
    public event Action OnLevelFailed;
    public event Action OnLevelComplete;

    public void Init(Spawner[] spawners, ItemTags targetItemTag)
    {
        this.spawners = spawners;
        this.targetItemTag = targetItemTag;
    }

    public void HandleCollect()
    {
        collectedTargetObjectsCount++;
        CalculateCollectable();
    }

    public void HandleDestroy()
    {
        UpdateState();
    }

    public void UpdateState()
    {
        if (collectedTargetObjectsCount >= allCollectiblesCount)
        {
            OnLevelComplete?.Invoke();
            return;
        }

        if (IsAllKeysItemsCollected() && collectedTargetObjectsCount < allCollectiblesCount)
        {
            OnLevelFailed?.Invoke();
        }
    }

    private void CalculateCollectable()
    {
        CheckActiveCollectibles();
        CompletePercentage = (int)((collectedTargetObjectsCount / (float)allCollectiblesCount) * 100);
    }

    private void CheckActiveCollectibles()
    {
        var objectsCount = 0;
        foreach (var spawner in spawners) 
            objectsCount += spawner.SpawnedObjects.Count;

        if (objectsCount > allCollectiblesCount)
            allCollectiblesCount = objectsCount;
    }

    private bool IsAllKeysItemsCollected()
    {
        var keysItemsCount = 0;
            
        foreach (var spawner in spawners)
        {
            foreach (var item in spawner.SpawnedObjects)
            {
                if (item != null 
                    && item.gameObject.activeInHierarchy 
                    && item.ItemTag == targetItemTag)
                {
                    keysItemsCount++;
                }
            }
        }

        return keysItemsCount == 0;
    }
}