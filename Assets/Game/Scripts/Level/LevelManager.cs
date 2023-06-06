using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Configs.Collectable;
using Game.Configs.Levels;
using Game.Events;
using Game.Infrastructures.BallSpawners;
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
    [SerializeField] private List<Ball> _currentBalls;
    [SerializeField] private List<Obstacle> _obstacles;

    #endregion

    #region Fields

    const int Z_POSITIONS = 0;

    private CountDownTimer _countDownTimer;
    private List<IDestroyable> _collectables;
    private BallPoppingHandler _ballPoppingHandler;

    #endregion

    #region Events

    public event Action<EndLevelResult> LevelEnded;

    #endregion

    #region Propertie

    public List<Projectile> SupportedAmmos => _levelModel.SupportedAmmos;

    #endregion

    private void Awake()
    {
        _collectables = new List<IDestroyable>();
        _ballPoppingHandler = new BallPoppingHandler();
    }

    private void Start()
    {
        AdjustZPosition();
        InitializeTimer();

        GameplayEventBus<GameplayEventType, DestroyBallEventArgs>.Subscribe(GameplayEventType.BallDestroyed, OnBallPopped);
        GameplayEventBus<CollectableEventType, CollectableEventContent<RewardContent>>.Subscribe(CollectableEventType.CollectableCreated, OnCollectableCreated);
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

    private async void OnBallPopped(DestroyBallEventArgs destroyBallEventArgs)
    {
        _currentBalls = await HandleBallPopped(destroyBallEventArgs);

        if (_currentBalls.Count == 0)
        {
            LevelEnded?.Invoke(new EndLevelResult(_levelModel.CurrentScore, true, _levelModel.LevelIndex));
        }
    }

    private async UniTask<List<Ball>> HandleBallPopped(DestroyBallEventArgs destroyBallEventArgs)
    {
        return _currentBalls = await _ballPoppingHandler.HandleBallPopped(_currentBalls, destroyBallEventArgs);
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
        var collectable = content.Args.Destroyable;
        collectable.Destroyed += OnCollectableCollected;
        _collectables.Add(content.Args.Destroyable);
    }
    
    private void OnCollectableCollected(IDestroyable reward)
    {
        _collectables.Remove(reward);
    }

    private void DestroyObstaclesLeft()
    {
        if (_obstacles == null) return;

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
        if (_collectables == null) return;

        foreach (var collectable in _collectables)
        {
            collectable?.DestroySelf();
        }
    }
    
    private void DestroyBallsLeft()
    {
        if (_currentBalls == null) return;

        foreach (var ball in _currentBalls)
        {
            ball.DestroySelf();
        }
    }

    private void OnDestroy()
    {
        _countDownTimer.TimesUp -= OnTimesUp;
        _countDownTimer.TimerTick -= OnTimerTick;

        GameplayEventBus<GameplayEventType, DestroyBallEventArgs>.Unsubscribe(GameplayEventType.BallDestroyed, OnBallPopped);
        GameplayEventBus<CollectableEventType, CollectableEventContent<RewardContent>>.Unsubscribe(CollectableEventType.CollectableCreated, OnCollectableCreated);

        DestroyBallsLeft();
        DestroyObstaclesLeft();
        DestroyCollectableLeft();
    }
}