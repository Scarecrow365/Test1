using System.Collections;
using System.Collections.Generic;
using Scriptables;
using SpawnableObjects;
using Tags;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private ItemTags spawnerItemTag;
    [SerializeField] private Vector2 objectsCountRange;
    [SerializeField] private float spawnPosXRange = 1;
    [SerializeField, Range(0, 1)] private float spawnInterval = 0.1f;
        
    private SpawnResources container;
    public List<SpawnableObject> SpawnedObjects { get; private set; } = new();

    public void Construct(SpawnResources spawnResources)
    {
        container = spawnResources;
    }
        
    public void Spawn()
    {
        if (gameObject.activeInHierarchy)
            StartCoroutine(SpawnObjectsWithDelay());
    }

    private IEnumerator SpawnObjectsWithDelay()
    {
        var count = (int)Random.Range(objectsCountRange.x, objectsCountRange.y);
        SpawnedObjects = new List<SpawnableObject>(count);

        for (int i = 0; i < count; i++)
        {
            var prefab = container.GetResource(spawnerItemTag);
            var item = Instantiate(prefab, transform);
            var posX = Random.Range(-spawnPosXRange, spawnPosXRange);
            var spawnPos = new Vector3(posX, item.transform.localPosition.y);
                
            item.Construct(spawnerItemTag, spawnPos);
                
            SpawnedObjects.Add(item);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}