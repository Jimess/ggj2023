using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : Singleton<LevelController>
{
    private const int MAX_LIFE = 2;
    public delegate void OnStartDelegate();
    public delegate void OnEndDelegate(bool win);

    public static OnStartDelegate OnStart;
    public static OnEndDelegate OnEnd;

    [SerializeField]
    private int acornLife = MAX_LIFE;
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
        //Time.timeScale = 0f;
    }

    public void StartGame()
    {
        Debug.Log("Starts game");
        //Time.timeScale = 1f;
        OnStart?.Invoke();
    }

    public void EndGame(bool win)
    {
        OnEnd?.Invoke(win);
    }

    public void IncreaseLife()
    {
        acornLife += 1;
        if (acornLife > MAX_LIFE)
        {
            acornLife = MAX_LIFE;
        }
    }
    public void ReduceLife()
    {
        if (IsInvurnable()) return;
        MakeInvurnable(1.0f);

        acornLife -= 1;
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

    public void MakeInvurnable(float invurnableTime)
    {
        invurnable = true;
        StartCoroutine(StartInvurnableTimer(invurnableTime));
    }

    IEnumerator StartInvurnableTimer(float invurnableTime)
    {
        yield return new WaitForSeconds(invurnableTime);
        invurnable = false;
    }
}
