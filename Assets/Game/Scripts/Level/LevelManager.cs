using System;
using System.Collections.Generic;
using Game.Configs.Balls;
using Game.Configs.Levels;
using Game.Events;
using Game.Models;
using Game.Scripts;
using Game.Scripts.Collectables;
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
    private List<IDestroyable> _collectables;

    #endregion

    #region Events

    public event Action<EndLevelResult> LevelEnded;

    #endregion


    private void Start()
    {
        InitializeTimer();

        _amountOfBallsInstances = _initialBalls.Count;

        GameplayEventBus<GameplayEventType, DestroyEventArgs>.Subscribe(GameplayEventType.BallDestroyed, OnBallPopped);

        //todo subscribe tor rewards event bus 
        //keep them in list
        //destroy whtas lefft onlevel destoy;

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

    private void DestroyObstaclesLeft()
    {
        foreach (var obstacle in _obstacles)
        {
            Destroy(obstacle.gameObject);
        }
    }

    private void DestroyCollectableLeft()
    {
        foreach (var collectable in _collectables)
        {
            collectable.Destroy();
        }
    }

    private void OnDestroy()
    {
        _countDownTimer.TimesUp -= OnTimesUp;
        _countDownTimer.TimerTick -= OnTimerTick;

        DestroyObstaclesLeft();
        DestroyCollectableLeft();
    }
}