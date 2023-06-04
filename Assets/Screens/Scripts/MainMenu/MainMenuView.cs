using Game.Configs;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Screens.Scripts.MainMenu
{
    public class MainMenuView : MonoBehaviour
    {
        #region EditorComponents

        [SerializeField] private MainMenuModel _mainMenuModel;

        #endregion

        #region Methods

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