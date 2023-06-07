using System;
using UnityEngine;
using Utils.Timer;

namespace Game.Scripts
{
    public class TimedExpandingProjectile : Projectile
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private int _timeToDestroy = 3;

        private float _currentYScaleValue;
        private float _currentYPositionValue;
        private CountDownTimer _countDownTimer;
        
        private void Start()
        {
            _currentYScaleValue = _transform.localScale.y;
            _currentYPositionValue = _transform.position.y;

            InitializeTimer();
        }

        private void InitializeTimer()
        {
            _countDownTimer = new CountDownTimer();
            _countDownTimer.StartTimer(_timeToDestroy);
            _countDownTimer.TimesUp += OnTimesUp;
        }

        private void OnTimesUp()
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            _countDownTimer.TimesUp -= OnTimesUp;
            
            if (!_countDownTimer.IsTimesUp)
            {
                _countDownTimer.StopTimer();
            }
        }


        protected override void Act()
        {
            _currentYScaleValue += Time.deltaTime * _speed;
            _currentYPositionValue += Time.deltaTime * _speed;
            
            var position = _transform.position;
            var localScale = _transform.localScale;

            _transform.position = new Vector3(position.x, _currentYPositionValue, position.z);;
            _transform.localScale = new Vector3(localScale.x, _currentYScaleValue, localScale.z);
        }
        
        
    }
}