using DG.Tweening;
using Scriptables;
using StorageSystem;
using UnityEngine;
using View;

public class Main : MonoBehaviour
{
    [SerializeField] private LevelContainer levelContainer;
    [SerializeField] private SpawnResources spawnResources;
    [SerializeField] private GameSettings defaultSaveableGameSettings;
        
    private LevelFactory levelFactory;
    private MainController mainController;
    private GameSettings currentGameSettings;

    private void Awake()
    {
        levelFactory = new LevelFactory();
    }

    public void Start()
    {
        Init();
    }

    private void Init()
    {
        SetUpSettings();
        InitController();
        
        var level = GetStartLevel();
        if (level == null)
        {
            Debug.Log("Level not found. Return to Main Menu.");
            QuitGame();
            return;
        }
        
        PrepareController(level);
    }
    
    private void InitController()
    {
        mainController = new MainController();
        mainController.OnQuit += QuitGame;
        mainController.OnLevelRestart += OnRestart;
        mainController.OnLevelComplete += OnComplete;
    }

    private void PrepareController(MainView level)
    {
        mainController.Init(level, spawnResources);
        mainController.StartGame(currentGameSettings.useTutorial);
            
        UpdateSaveData(level);
    }

    private void OnComplete()
    {
        var level = GetNextLevel();
        if (level == null)
        {
            Debug.Log("Level not found. Return to Main Menu.");
            QuitGame();
            return;
        }

        PrepareController(level);
    }

    private void SetUpSettings()
    {
        var result = StorageGameSettings.LoadGameSettings(out var gameSettings);
        currentGameSettings = result ? gameSettings : defaultSaveableGameSettings;
        currentGameSettings = defaultSaveableGameSettings;
    }

    private void UpdateSaveData(MainView level)
    {
        currentGameSettings.useTutorial = false;
        currentGameSettings.levelId = level.LevelId;
        StorageGameSettings.SaveGameSettings(currentGameSettings);
    }

    private MainView GetStartLevel()
    {
        var levelPrefab = levelContainer.GetLevelPrefab(currentGameSettings.levelId);
        return levelPrefab == null ? null : levelFactory.CreateLevel(levelPrefab);
    }

    private MainView GetNextLevel()
    {
        var levelPrefab = levelContainer.GetNextLevelPrefab(mainController.LevelId);
        return levelPrefab == null ? null : levelFactory.CreateLevel(levelPrefab);
    }
    
    private void OnDestroy()
    {
        mainController?.Clean();
        DOTween.KillAll();
        ClearSaveData();
    }

    private void OnRestart() => RestartGame();
    private void RestartGame() => PrepareController(levelContainer.GetLevelPrefab(mainController.LevelId));
    
    private static void QuitGame() => Debug.Break();
    private static void ClearSaveData() => StorageGameSettings.Clean();
}