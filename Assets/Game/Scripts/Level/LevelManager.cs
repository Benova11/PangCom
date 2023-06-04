using System;
using System.Collections.Generic;
using Game.Configs.Balls;
using Game.Events;
using Game.Models;
using Game.Scripts;
using UnityEngine;
using Utils.Timer;

public class LevelManager : MonoBehaviour
{
    #region Editor Components

    [SerializeField] private LevelModel _levelModel;
    [SerializeField] private List<Ball> _initialBalls;
    [SerializeField] private List<Obstacle> _obstacles;

    #endregion

    #region Fields

    public int _amountOfBallsInstances;
    private CountDownTimer _countDownTimer;

    #endregion

    #region Events

    public event Action<bool> LevelEnded; 

    #endregion
    
    
    private void Start()
    {
        InitializeTimer();
        _amountOfBallsInstances = _initialBalls.Count;
        GameplayEventBus<GameplayEventType,DestroyEventArgs>.Subscribe(GameplayEventType.BallDestroyed, OnBallPopped);
        //set hud properties
        //go to next level?

        //check for how many players?
    }

    private void InitializeTimer()
    {
        _countDownTimer = new CountDownTimer();
        _countDownTimer.StartTimer(_levelModel.TimePerLevel);
        _countDownTimer.TimesUp += OnTimesUp;
        _countDownTimer.TimerTick += OnTimerTick;
    }

    private void OnBallPopped(DestroyEventArgs destroyEventArgs)
    {
        var ballSize = destroyEventArgs.Ball.BallModel.BallSize;
        _amountOfBallsInstances = ballSize != BallSize.X1 ? _amountOfBallsInstances + 1 : _amountOfBallsInstances - 1;
        
        if (_amountOfBallsInstances == 0)
        {
            LevelEnded?.Invoke(true);
        }
    }

    private void OnTimerTick(int remainingTime)
    {
        _levelModel.RemainingTime = remainingTime;
    }

    private void OnTimesUp()
    {
        LevelEnded?.Invoke(false);
    }

    private void OnDestroy()
    {
        _countDownTimer.TimesUp -= OnTimesUp;
        _countDownTimer.TimerTick -= OnTimerTick;
    }
}
