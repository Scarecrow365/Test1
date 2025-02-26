using System;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class SettingsPanel : MonoBehaviour
    {
        [SerializeField] private Button quitButton;
        [SerializeField] private Button restartButton;

        public event Action OnQuit;
        public event Action OnRestart;

        public void Start()
        {
            quitButton.onClick.AddListener(QuitGame);
            restartButton.onClick.AddListener(RestartGame);
        }

        private void QuitGame() => OnQuit?.Invoke();

        private void RestartGame() => OnRestart?.Invoke();
    }
}