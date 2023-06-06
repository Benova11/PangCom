using System;
using Game.Configs;
using Game.Configs.Levels;
using Game.Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Screens.Scripts.MainMenu
{
    public class MainMenuView : MonoBehaviour
    {
        #region EditorComponents

        [SerializeField] private Toggle _twoPlayersToggle;
        [SerializeField] private GameConfigModel _gameConfigModel;

        #endregion

        #region Methods

        private void Start()
        {
            _twoPlayersToggle.isOn = _gameConfigModel.GameMode == GameMode.TwoPlayers;
        }

        public void OnGameModeToggled(bool isTwoPlayersOn)
        {
            var toggleValue = _twoPlayersToggle.isOn;
            _gameConfigModel.GameMode = toggleValue ? GameMode.TwoPlayers : GameMode.SinglePlayer;
        }

        public void OnPlayClicked()
        {
            SceneManager.LoadSceneAsync(SystemSceneIndexes.GAME_MANAGER_BUILD_ID);
        }

        public void OnQuitClicked()
        {
            Application.Quit();
        }

        #endregion
    }
}