using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CrazyTank.Interface;

namespace CrazyTank.UI
{
    public class UIProcess : MonoBehaviour, IInitializer, IDisplaying
    {
        [SerializeField] private RawImage _gunImage;
        [SerializeField] private TextMeshProUGUI _gunName;

        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _quitButton;

        [SerializeField] private GameObject _buttonsPanel;
        [SerializeField] private GameObject _pausePanel;
        [SerializeField] private GameObject _gameOverPanel;

        private IControllable _iControllable;

        public void SetControllable(IControllable controllable)
        {
            _iControllable = controllable;
            Initialized();
        }

        public void Initialized()
        {
            _pauseButton.onClick.AddListener(() => SetPausePanelStatus(true));
            _continueButton.onClick.AddListener(() => SetPausePanelStatus(false));
            _restartButton.onClick.AddListener(() => _iControllable.RestartGame());
            _quitButton.onClick.AddListener(() => _iControllable.QuitGame());
        }

        private void OnDestroy()
        {
            DeInitialized();
        }

        public void DeInitialized()
        {
            _pauseButton.onClick.RemoveAllListeners();
            _restartButton.onClick.RemoveAllListeners();
            _quitButton.onClick.RemoveAllListeners();
        }

        private void SetPausePanelStatus(bool isOpen)
        {
            ShowButtonPanel(isOpen);
            _pausePanel.SetActive(isOpen);
            _iControllable.SetPause(isOpen);
        }

        private void ShowButtonPanel(bool isOpen) => _buttonsPanel.SetActive(isOpen);

        public void ChangeUIGun(Texture2D image, string name)
        {
            _gunImage.texture = image;
            _gunName.text = name;
        }

        public void GameOver()
        {
            ShowButtonPanel(true);
            _continueButton.enabled = false;
            _gameOverPanel.SetActive(true);
        }
    }
}