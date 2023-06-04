using System;
using System.Collections.Generic;
using Game.Configs.Balls;
using Game.Configs.Levels;
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

    public event Action<EndLevelResult> LevelEnded;

    #endregion


    private void Start()
    {
        InitializeTimer();
        _amountOfBallsInstances = _initialBalls.Count;
        GameplayEventBus<GameplayEventType, DestroyEventArgs>.Subscribe(GameplayEventType.BallDestroyed, OnBallPopped);
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
            LevelEnded?.Invoke(new EndLevelResult(_levelModel.CurrentScore, true, _levelModel.LevelIndex));
        }
    }

    private void OnTimerTick(int remainingTime)
    {
        _levelModel.RemainingTime = remainingTime;
    }

    private void OnTimesUp()
    {
        LevelEnded?.Invoke(new EndLevelResult(_levelModel.CurrentScore, false, _levelModel.LevelIndex));
    }

    private void OnDestroy()
    {
        _countDownTimer.TimesUp -= OnTimesUp;
        _countDownTimer.TimerTick -= OnTimerTick;
    }
}