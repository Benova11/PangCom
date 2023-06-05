using System;
using System.Collections.Generic;
using Game.Configs.Balls;
using Game.Configs.Collectable;
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

    [SerializeField] private Transform _transform;
    [SerializeField] private LevelModel _levelModel;
    [SerializeField] private List<Ball> _initialBalls;
    [SerializeField] private List<Obstacle> _obstacles;

    #endregion

    #region Fields
    
    const int Z_POSITIONS = 0;
    
    public int _amountOfBallsInstances;
    private CountDownTimer _countDownTimer;
    private List<IDestroyable> _collectables;

    #endregion

    #region Events

    public event Action<EndLevelResult> LevelEnded;

    #endregion

    #region Pro

    public List<Projectile> SupportedAmmos => _levelModel.SupportedAmmos;

    #endregion

    private void Awake()
    {
        _collectables = new List<IDestroyable>();
    }

    private void Start()
    {
        AdjustZPosition();
        InitializeTimer();

        _amountOfBallsInstances = _initialBalls.Count;

        GameplayEventBus<GameplayEventType, DestroyBallEventArgs>.Subscribe(GameplayEventType.BallDestroyed, OnBallPopped);
        GameplayEventBus<CollectableEventType, CollectableEventContent<RewardContent>>.Subscribe(CollectableEventType.CollectableCreated, OnCollectableCreated);

        //set hud properties
        //go to next level?

        //check for how many players?
    }

    private void AdjustZPosition()
    {
        var position = _transform.position;
        position = new Vector3(position.x, position.y, Z_POSITIONS);
        _transform.position = position;
    }

    private void InitializeTimer()
    {
        _countDownTimer = new CountDownTimer();
        _countDownTimer.StartTimer(_levelModel.TimePerLevel);
        _countDownTimer.TimesUp += OnTimesUp;
        _countDownTimer.TimerTick += OnTimerTick;
    }

    private void OnBallPopped(DestroyBallEventArgs destroyBallEventArgs)
    {
        var ballSize = destroyBallEventArgs.Ball.BallModel.BallSize;
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
    
    public void OnPlayersDead(Action<EndLevelResult> onLevelEndedCallback)
    {
        onLevelEndedCallback?.Invoke(new EndLevelResult(_levelModel.CurrentScore, false, _levelModel.LevelIndex));
    }
    
    private void OnCollectableCreated(CollectableEventContent<RewardContent> content)
    {
        _collectables.Add(content.Args.Destroyable);
    }

    private void DestroyObstaclesLeft()
    {
        if(_obstacles == null) return;

        foreach (var obstacle in _obstacles)
        {
            if (obstacle != null)
            {
                Destroy(obstacle.gameObject);
            }
        }
    }

    private void DestroyCollectableLeft()
    {
        if(_collectables == null) return;
        
        foreach (var collectable in _collectables)
        {
            collectable?.Destroy();
        }
    }

    private void OnDestroy()
    {
        _countDownTimer.TimesUp -= OnTimesUp;
        _countDownTimer.TimerTick -= OnTimerTick;
        
        GameplayEventBus<GameplayEventType, DestroyBallEventArgs>.Unsubscribe(GameplayEventType.BallDestroyed, OnBallPopped);
        GameplayEventBus<CollectableEventType, CollectableEventContent<RewardContent>>.Unsubscribe(CollectableEventType.CollectableCreated, OnCollectableCreated);

        DestroyObstaclesLeft();
        DestroyCollectableLeft();
    }
}