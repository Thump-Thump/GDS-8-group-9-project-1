using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GenericSingletonClass<GameManager>
{
    public enum PlayerSpeed
    {
        Slow,
        Normal,
        Fast
    }

    private PlayerSpeed _playerSpeed = PlayerSpeed.Normal;
    private Boolean _gameRunning = true;

    public void StopGame()
    {
        this._gameRunning = false;
    }
    public Boolean IsGameRunning()
    {
        return this._gameRunning;
    }

    public PlayerSpeed GetPlayerSpeed()
    {
        return this._playerSpeed;
    }

    public void SetSlowPlayerSpeed()
    {
        this._playerSpeed = PlayerSpeed.Slow;
    }
    
    public void SetNormalPlayerSpeed()
    {
        this._playerSpeed = PlayerSpeed.Normal;
    }
    
    public void SetFastPlayerSpeed()
    {
        this._playerSpeed = PlayerSpeed.Fast;
    }
}