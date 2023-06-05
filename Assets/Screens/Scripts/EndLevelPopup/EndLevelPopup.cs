using Cysharp.Threading.Tasks;
using Game.Configs;
using Game.Configs.Levels;
using Game.Events;
using Screens.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Screens.Popups
{
    public class EndLevelPopup : BasePopupPrefab<EndLevelResult> 
    {
        #region EditorComponents

        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _resultText;

        #endregion

        #region Fields

        private EndLevelResult _endLevelResult;

        #endregion

        #region Methods

        public override UniTask Show(EndLevelResult endLevelResult)
        {
            _endLevelResult = endLevelResult;

            _scoreText.text = endLevelResult.Score.ToString();
            _resultText.text = endLevelResult.IsSuccess ? "Success!" : "Fail";

            return UniTask.CompletedTask;
        }

        public override void ClosePopup()
        {
            Destroy(gameObject);
        }

        public void OnQuitClicked()
        {
            ClosePopup();
            SceneManager.LoadSceneAsync(SystemSceneIndexes.MAIN_MENU_BUILD_ID);
        }

        public void OnNextLevelClicked()
        {
            var nextLevelEventArgs = new NextLevelEventArgs(_endLevelResult.LevelIndex);
            GameplayEventBus<GameplayEventType, NextLevelEventArgs>.Publish(GameplayEventType.NextLevelRequested, nextLevelEventArgs);
            ClosePopup();
        }

        #endregion
    }
}