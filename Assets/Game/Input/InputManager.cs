using Game.Configs;
using UnityEngine;

namespace Game
{
    //todo config second player input
    //todo change key code to mobile
    public static class InputManager
    {
        #region Consts

        private const string SHOOT_REQUEST_AXIS_NAME = "Shoot";
        private const string HORIZONTAL_AXIS_NAME = "Horizontal";
        private const KeyCode TOGGLE_PAUSE_GAME = KeyCode.Escape;
        private const KeyCode SWITCH_WEAPON_REQUEST_BUTTON = KeyCode.RightControl;

        #endregion

        #region Methods

        public static float GetMovementInput(PlayerInputOrder playerInputOrder)
        {
            return SimpleInput.GetAxisRaw(HORIZONTAL_AXIS_NAME + (int)playerInputOrder);
        }

        public static bool IsShootRequested(PlayerInputOrder playerInputOrder)
        {
            return SimpleInput.GetButtonDown(SHOOT_REQUEST_AXIS_NAME + (int)playerInputOrder);
        }

        public static bool IsSwitchWeaponRequested()
        {
            return false;
        }

        public static bool IsToggleRequested()
        {
            return Input.GetKeyDown(TOGGLE_PAUSE_GAME);
        }

        #endregion
    }
}