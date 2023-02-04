using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : Singleton<LevelController>
{
    public delegate void OnStartDelegate();
    public delegate void OnEndDelegate(bool win);

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

    private void Start()
    {
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        Debug.Log("Starts game");
        Time.timeScale = 1f;
        OnStart?.Invoke();
    }

    public void EndGame(bool win)
    {
        OnEnd?.Invoke(win);
    }

    public void ReduceLife()
    {
        if (IsInvurnable()) return;
        invurnable = true;
        StartCoroutine(StartInvurnableTimer(1f));

        acornLife -= 1;
        Debug.Log($"Damage taken, life remaning: {acornLife}");
        if (acornLife <= 0)
        {
            EndGame(false);
        }
    }

    public int GetAcornLife()
    {
        return acornLife;
    }

    public bool IsInvurnable()
    {
        return invurnable;
    }

    IEnumerator StartInvurnableTimer(float invurnableTime)
    {
        yield return new WaitForSeconds(invurnableTime);
        invurnable = false;
    }
}
