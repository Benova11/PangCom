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
            SceneManager.LoadSceneAsync("GameScene");
        }

        public void OnQuitClicked()
        {
            Application.Quit();
        }

        #endregion
    }
}