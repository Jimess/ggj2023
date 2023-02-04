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

    private void Awake()
    {
        CollisionManager.OnAcornEndCollision += EndGame;
    }

    private void OnDestroy()
    {
        CollisionManager.OnAcornEndCollision -= EndGame;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        OnStart();
    }

    public void EndGame()
    {
        OnEnd();
    }
}
