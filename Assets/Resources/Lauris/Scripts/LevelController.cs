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

    [SerializeField]
    private int acornLife = 3;
    private bool invurnable = false;

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
        if (isInvurnable()) return;
        invurnable = true;
        StartCoroutine(StartInvurnableTimer(1f));

        acornLife -= 1;
        Debug.Log($"Damage taken, life remaning: {acornLife}");
        if (acornLife <= 0)
        {
            EndGame();
        }
    }

    public int getAcornLife()
    {
        return acornLife;
    }

    public bool isInvurnable()
    {
        return invurnable;
    }

    IEnumerator StartInvurnableTimer(float invurnableTime)
    {
        yield return new WaitForSeconds(invurnableTime);
        invurnable = false;
    }
}
