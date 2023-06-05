using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class PlayerInputHudController : MonoBehaviour
    {
        [SerializeField] private Button _input1Button;

        private void Start()
        {
            // SimpleInput.GetAxisRaw()
            SimpleInputHelper.TriggerKeyClick(KeyCode.Space);
        }

        private void OnFireClicked()
        {
            
        }
    }
}