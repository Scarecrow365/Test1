using System.Linq;
using UnityEngine;
using View;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "LevelContainer", menuName = "KFC Scriptable Objects/Giant Spin/Level Container")]
    public class LevelContainer : ScriptableObject
    {
        [SerializeField] private MainView[] levelsPrefabs;

        public int LevelsCount => levelsPrefabs.Length;

        public MainView GetLevelPrefab(string id)
        {
            return levelsPrefabs.FirstOrDefault(prefab => prefab.LevelId == id);
        }

        public MainView GetNextLevelPrefab(string levelId)
        {
            MainView result = null;
            for (int index = 0; index < levelsPrefabs.Length; index++)
                if (levelsPrefabs[index].LevelId == levelId)
                    result = levelsPrefabs.Length <= index + 1 ? null : levelsPrefabs[index + 1];

            return result;
        }
    }
}