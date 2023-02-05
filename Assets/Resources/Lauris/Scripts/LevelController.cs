using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LevelController : Singleton<LevelController>
{
    private const int MAX_LIFE = 3;
    public delegate void OnStartDelegate();
    public delegate void OnEndDelegate(bool win);
    public delegate void OnInvurnableStateChangeDelegate(bool invurnable);

    public static OnStartDelegate OnStart;
    public static OnEndDelegate OnEnd;
    public static OnInvurnableStateChangeDelegate OnInvurnableStateChange;

    [SerializeField]
    private int acornLife = MAX_LIFE;
    private int maxLife;
    private bool invurnable = false;

    [SerializeField] GameObject tree;
    [SerializeField] CinemachineVirtualCamera treeCam;

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
        maxLife = acornLife;
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
        if (acornLife > maxLife)
        {
            acornLife = maxLife;
        }
    }
    public void ReduceLife(Vector2 pos)
    {
        if (IsInvurnable())
        {
            AudioManager.Instance.PlaySFXDetached("haha");
            return;
        }
        MakeInvurnable(0.2f, false);

        AudioManager.Instance.PlaySFXDetached("hurt");

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

    public void MakeInvurnable(float invurnableTime, bool emitStateChanges)
    {
        invurnable = true;
        if (emitStateChanges) OnInvurnableStateChange?.Invoke(invurnable);
        StartCoroutine(StartInvurnableTimer(invurnableTime, emitStateChanges));
    }

    public void SummonTree(Vector2 position)
    {
        GameObject otherTree = Instantiate(tree, position, Quaternion.identity);
        otherTree.GetComponent<TreeGrow>().Grow(position);
        treeCam.m_Follow = otherTree.transform;
        treeCam.enabled = true;
        //cineConfiner.m_BoundingShape2D = tree.GetComponent<PolygonCollider2D>();
    }

    IEnumerator StartInvurnableTimer(float invurnableTime, bool emitStateChanges)
    {
        yield return new WaitForSeconds(invurnableTime);
        invurnable = false;
        if (emitStateChanges) OnInvurnableStateChange?.Invoke(invurnable);
    }
}
