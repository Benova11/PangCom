using Game.Configs;
using UnityEngine;

namespace Game
{
    public static class InputManager
    {
        #region Consts

        //keyboard input
        //p1 right left : arrows, shoot right ctrl
        //p2 right left : wasd, shoot right aly
        
        private const string SHOOT_REQUEST_AXIS_NAME = "Fire";
        private const string HORIZONTAL_AXIS_NAME = "Horizontal";
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

        #endregion
    }
}