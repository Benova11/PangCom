using Cysharp.Threading.Tasks;
using Game.Configs;
using Game.Configs.Levels;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Screens.Scripts.PauseMenu
{
    public class PauseMenuPopup : BasePopupPrefab<CurrentLevelState>
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        
        public override UniTask Show(CurrentLevelState currentLevelState)
        {
            Time.timeScale = 0;
            _scoreText.text = "Current Score:" + currentLevelState.Score;
            
            return UniTask.CompletedTask;
        }
        
        public void OnQuitClicked()
        {
            ClosePopup();
            SceneManager.LoadSceneAsync(SystemSceneIndexes.MAIN_MENU_BUILD_ID);
        }

        public void OnResumeLevelClicked()
        {
            ClosePopup();
        }

        public override void ClosePopup()
        {
            Time.timeScale = 1;
            Destroy(gameObject);
        }
    }
}