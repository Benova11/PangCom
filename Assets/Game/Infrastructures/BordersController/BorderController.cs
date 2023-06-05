using UnityEngine;

namespace Game.Infrastructures
{
    public class BorderController : MonoBehaviour
    {
        [SerializeField] private Transform _topBorder;
        [SerializeField] private Transform _leftBorder;
        [SerializeField] private Transform _rightBorder;
        [SerializeField] private Transform _bottomBorder;

        public void Start()
        {
            AdjustBordersPosition();
        }

        private void AdjustBordersPosition()
        {
            float screenHeight = Screen.height;
            float screenWidth = Screen.width;

            Camera mainCamera = Camera.main;

            if (mainCamera != null)
            {
                Vector3 topBorderPosition = mainCamera.ScreenToWorldPoint(new Vector3(screenWidth / 2f, screenHeight, 0f));
                Vector3 bottomBorderPosition = mainCamera.ScreenToWorldPoint(new Vector3(screenWidth / 2f, 0f, 0f));
                Vector3 leftBorderPosition = mainCamera.ScreenToWorldPoint(new Vector3(0f, screenHeight / 2f, 0f));
                Vector3 rightBorderPosition = mainCamera.ScreenToWorldPoint(new Vector3(screenWidth, screenHeight / 2f, 0f));

                _topBorder.position = topBorderPosition;
                _bottomBorder.position = bottomBorderPosition;
                _leftBorder.position = leftBorderPosition;
                _rightBorder.position = rightBorderPosition;

                var mainCameraTransform = mainCamera.transform;
                mainCameraTransform.position = new Vector3(mainCameraTransform.position.x, mainCameraTransform.position.y, -8f);
            }
        }
    }
}