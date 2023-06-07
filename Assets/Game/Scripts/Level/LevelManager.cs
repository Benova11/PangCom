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

    private int _currentScore;
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

        GameplayEventBus<GameplayEventType, LevelStatsUpdatedArgs>.Publish(GameplayEventType.LevelStatsUpdated, new LevelStatsUpdatedArgs(0, 0));

        GameplayEventBus<GameplayEventType, DestroyBallEventArgs>.Subscribe(GameplayEventType.BallDestroyed, OnBallPopped);
        GameplayEventBus<CollectableEventType, CollectableEventContent<RewardContent>>.Subscribe(CollectableEventType.RewardCollected, OnRewardCollected);
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
            StopTimer();
            LevelEnded?.Invoke(new EndLevelResult(_currentScore, true, _levelModel.LevelIndex));
        }
    }

    private async UniTask<List<Ball>> HandleBallPopped(DestroyBallEventArgs destroyBallEventArgs)
    {
        var newCurrentBalls = await _ballPoppingHandler.HandleBallPopped(_currentBalls, destroyBallEventArgs);
        return newCurrentBalls;
    }

    private void OnTimerTick(int remainingTime)
    {
        GameplayEventBus<GameplayEventType, LevelStatsUpdatedArgs>.Publish(GameplayEventType.LevelStatsUpdated, new LevelStatsUpdatedArgs(_currentScore, remainingTime));
    }

    private void OnTimesUp()
    {
        LevelEnded?.Invoke(new EndLevelResult(_currentScore, false, _levelModel.LevelIndex));
    }

    private void OnRewardCollected(CollectableEventContent<RewardContent> content)
    {
        _currentScore += content.Args.Amount;
        GameplayEventBus<GameplayEventType, LevelStatsUpdatedArgs>.Publish(GameplayEventType.LevelStatsUpdated, new LevelStatsUpdatedArgs(_currentScore, _countDownTimer.CurrentTime));
    }

    public void OnPlayersDead(Action<EndLevelResult> onLevelEndedCallback)
    {
        StopTimer();
        onLevelEndedCallback?.Invoke(new EndLevelResult(_currentScore, false, _levelModel.LevelIndex));
    }

    private void StopTimer()
    {
        _countDownTimer.TimesUp -= OnTimesUp;
        _countDownTimer.TimerTick -= OnTimerTick;
        _countDownTimer.StopTimer();
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

        for (int i = _obstacles.Count - 1; i >= 0; i--)
        {
            var obstacle = _obstacles[i];

            if (obstacle != null)
            {
                Destroy(obstacle.gameObject);
            }
        }

        _obstacles.Clear();
    }

    private void DestroyCollectableLeft()
    {
        if (_collectables == null) return;

        for (int i = _collectables.Count - 1; i >= 0; i--)
        {
            var collectable = _collectables[i];
            if (collectable != null)
            {
                collectable.Destroyed -= OnCollectableCollected;
                collectable.DestroySelf();
            }
        }

        _collectables.Clear();
    }

    private void DestroyBallsLeft()
    {
        if (_currentBalls == null) return;

        for (int i = _currentBalls.Count - 1; i >= 0; i--)
        {
            var ball = _currentBalls[i];
            if (ball != null)
            {
                Destroy(ball.gameObject);
            }
        }

        _currentBalls.Clear();
    }

    private void OnDestroy()
    {
        _countDownTimer.TimesUp -= OnTimesUp;
        _countDownTimer.TimerTick -= OnTimerTick;

        _countDownTimer = null;

        GameplayEventBus<GameplayEventType, DestroyBallEventArgs>.Unsubscribe(GameplayEventType.BallDestroyed, OnBallPopped);
        GameplayEventBus<CollectableEventType, CollectableEventContent<RewardContent>>.Unsubscribe(CollectableEventType.RewardCollected, OnRewardCollected);
        GameplayEventBus<CollectableEventType, CollectableEventContent<RewardContent>>.Unsubscribe(CollectableEventType.CollectableCreated, OnCollectableCreated);

        DestroyBallsLeft();
        DestroyObstaclesLeft();
        DestroyCollectableLeft();
    }
}