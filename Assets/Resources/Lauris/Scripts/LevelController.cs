using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : Singleton<LevelController>
{
    public delegate void OnStartDelegate();
    public delegate void OnEndDelegate();

    public static OnStartDelegate OnStart;
    public static OnEndDelegate OnEnd;

    private int acornLife = 3;

    private void OnEnable()
    {
        CollisionManager.OnAcornEndCollision += EndGame;
        CollisionManager.OnAcornDamageTaken += ReduceLife;
    }

    private void OnDestroy()
    {
        CollisionManager.OnAcornEndCollision -= EndGame;
        CollisionManager.OnAcornDamageTaken -= ReduceLife;
    }

    public void StartGame()
    {
        Debug.Log("Starts game");
        OnStart?.Invoke();
    }

    public void EndGame()
    {
        OnEnd?.Invoke();
    }

    public void ReduceLife()
    {
        acornLife -= 1;
        if (acornLife <= 0) {
            EndGame();
        }
    }

    public int getAcornLife()
    {
        return acornLife;
    }
}
