using System.Collections.Generic;
using DefaultNamespace;
using Game.Scripts;
using UnityEngine;
using Utils.Timer;

public class LevelManager : MonoBehaviour
{
    #region Editor Components

    [SerializeField] private LevelModel _levelModel;
    
    [SerializeField] private List<Ball> _currentBalls;
    [SerializeField] private List<Obstacle> _obstacles;

    #endregion

    #region Fields

    private CountDownTimer _countDownTimer;

    #endregion
    
    
    private void Start()
    {
        InitializeTimer();
        //start balls?
        //start rewards spanwer
        //set hud properties
        //check game state - win <-> lose
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

    private void OnTimerTick(int obj)
    {
        throw new System.NotImplementedException();
    }

    private void OnTimesUp()
    {
        throw new System.NotImplementedException();
    }
}
