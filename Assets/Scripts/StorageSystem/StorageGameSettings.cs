using UnityEngine;

namespace StorageSystem
{
    public static class StorageGameSettings
    {
        public static void SaveGameSettings(GameSettings gameSettings)
        {
            var saveData = JsonUtility.ToJson(gameSettings);
            PlayerPrefs.SetString(StorageApi.LevelStateId, saveData);
        }

        public static bool LoadGameSettings(out GameSettings result)
        {
            var saveData = PlayerPrefs.GetString(StorageApi.LevelStateId);
            if (string.IsNullOrEmpty(saveData))
            {
                result = default;
                return false;
            }

            result = JsonUtility.FromJson<GameSettings>(saveData);
            return true;
        }

        public static void Clean() => PlayerPrefs.DeleteKey(StorageApi.LevelStateId);
    }
}