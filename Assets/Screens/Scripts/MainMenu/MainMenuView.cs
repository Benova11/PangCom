using UnityEngine;

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
            //loader
            //move to game scene
            //locate game manager?
        }

        public void OnQuitClicked()
        {
            Application.Quit();
        }

        #endregion
    }
}