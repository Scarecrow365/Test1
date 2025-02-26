using System;
using Scriptables;
using UnityEngine;
using View;

public class MainController
{
    private MainView view;
    private Spawner[] spawners;

    private ItemManager itemManager;

    public event Action OnQuit;
    public event Action OnLevelRestart;
    public event Action OnLevelComplete;

    public string LevelId => view.LevelId;

    public void Init(MainView view, SpawnResources spawnResources)
    {
        this.view?.Release();
        this.view = view;
            
        InitSpawners(view.Spawners, spawnResources);
        InitItemManager();

        Subscribe();
    }

    public void StartGame(bool useTutorial)
    {
        foreach (var spawner in spawners) 
            spawner.Spawn();

        SetUpTutorial(useTutorial);
    }
        
    public void Clean()
    {
        Unsubscribe();

        spawners = null;
        itemManager = null;
    }

    private void InitSpawners(Spawner[] spawners, SpawnResources spawnResources)
    {
        this.spawners = spawners;

        foreach (var spawner in this.spawners) 
            spawner.Construct(spawnResources);
    }
        
    private void InitItemManager()
    {
        itemManager = new();
        itemManager.Init(view.Spawners, view.TargetItemTag);
    }

    private void Subscribe()
    {
        view.OnQuit += OnQuitPressed;
        view.OnLevelRestart += RestartLevel;
        view.OnTargetItemCollect += OnTargetItemCollect;
        view.OnTargetItemDestroy += OnTargetItemDestroy;

        itemManager.OnLevelFailed += OnLevelFailed;
        itemManager.OnLevelComplete += LevelComplete;
    }

    private void Unsubscribe()
    {
        if (view != null)
        {
            view.OnQuit -= OnQuitPressed;
            view.OnLevelRestart -= RestartLevel;
            view.OnTargetItemCollect -= OnTargetItemCollect;
            view.OnTargetItemDestroy -= OnTargetItemDestroy;
        }

        if (itemManager != null)
        {
            itemManager.OnLevelFailed -= OnLevelFailed;
            itemManager.OnLevelComplete -= LevelComplete;
        }
    }

    private void SetUpTutorial(bool useTutorial)
    {
        if (useTutorial)
            view.Tutorial.Show();
        else
            view.Tutorial.gameObject.SetActive(false);
    }

    private void OnTargetItemCollect()
    {
        itemManager.HandleCollect();
        view.UpdateView(itemManager.CompletePercentage);
        itemManager.UpdateState();
    }

    private void OnTargetItemDestroy()
    {
        itemManager.HandleDestroy();
        view.UpdateView(itemManager.CompletePercentage);
    }

    private void LevelComplete()
    {
        // view.Complete(() =>
        // {
        //     Clean();
        //     OnLevelComplete?.Invoke();
        // });

        Clean();
        OnLevelComplete?.Invoke();
    }
        
    private void OnLevelFailed()
    {
        view.FireTriggerLevelFailed();
    }

    private void RestartLevel()
    {
        Unsubscribe();
        OnLevelRestart?.Invoke();
    }
        
    private void OnQuitPressed() => OnQuit?.Invoke();
}