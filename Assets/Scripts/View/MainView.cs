using System;
using Tags;
using TriggerZones;
using UnityEngine;

namespace View
{
    public abstract class MainView : MonoBehaviour
    {
        [field: SerializeField] public string LevelId { get; private set; }
        [field: SerializeField] public Spawner[] Spawners { get; private set; }
        [field: SerializeField] public Tutorial Tutorial { get; private set; }

        [SerializeField] protected TriggerZone triggerZone;
        [SerializeField] private LoseScreen loseScreen;
        [SerializeField] private WinScreen winScreen;
        [SerializeField] private PlayerInput playerInput;

        protected bool isFailedCalled;
        
        private Action completeCallback;

        public event Action OnQuit;
        public event Action OnLevelRestart;
        public event Action OnTargetItemCollect;
        public event Action OnTargetItemDestroy;

        public ItemTags TargetItemTag => triggerZone.TargetItemTag;

        private void Start() => Init();

        public void Release() => Destroy(gameObject);

        public void UpdateView(float result)
        {
            //scorePanel.UpdateResultText(result);
        }

        public void UpdateScore(int score, int totalScore)
        {
            //winScreen.SetScores(score, totalScore);
        }

        public virtual void Complete(Action callback)
        {
            completeCallback = callback;
            //winScreen.Show();
            playerInput.Disable();
        }

        public virtual void FireTriggerLevelFailed()
        {
            if (isFailedCalled)
                return;
            
            isFailedCalled = true;
            //loseScreen.Show();
            playerInput.Disable();
        }

        protected virtual void Init()
        {
            Subscribe();
            isFailedCalled = false;
            playerInput.Enable();
            // scorePanel.Show();
            // scorePanel.UpdateResultText(0);
        }

        protected void FireTriggerItemDestroyed() => OnTargetItemDestroy?.Invoke();

        protected virtual void OnTriggerPositiveFired() => FireTriggerItemCollect();

        protected virtual void Subscribe()
        {
            //winScreen.OnQuit += () => OnQuit?.Invoke();
            //winScreen.OnNext += CompleteLevel;
            //winScreen.OnRetry += OnRetryPressed;
            //loseScreen.OnRetry += OnRetryPressed;
            triggerZone.OnTriggerPositive += OnTriggerPositiveFired;
        }

        protected void FireTriggerItemCollect()
        {
            OnTargetItemCollect?.Invoke();
        }

        private void OnRetryPressed()
        {
            OnLevelRestart?.Invoke();
            playerInput.Enable();
            triggerZone.OnTriggerNegative -= FireTriggerLevelFailed;
        }

        private void CompleteLevel()
        {
            completeCallback?.Invoke();
            // scorePanel.Complete();
        }
    }
}