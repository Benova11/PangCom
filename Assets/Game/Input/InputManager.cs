using UnityEngine;

namespace Game
{
    //todo config second player input
    public static class InputManager
    {
        #region Consts

        private const string HORIZONTAL_AXIS_NAME = "Horizontal";

        private const KeyCode SHOOT_REQUEST_BUTTON = KeyCode.Space;

        private const KeyCode SWITCH_WEAPON_REQUEST_BUTTON = KeyCode.LeftControl;

        #endregion

        #region Methods

        public static float GetMovementInput()
        {
            return Input.GetAxisRaw(HORIZONTAL_AXIS_NAME);
        }

        public static bool IsShootRequested()
        {
            return Input.GetKeyDown(SHOOT_REQUEST_BUTTON);
        }

        public static bool IsSwitchWeaponRequested()
        {
            return Input.GetKeyDown(SWITCH_WEAPON_REQUEST_BUTTON);
        }

        #endregion
    }
}