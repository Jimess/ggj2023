using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornDisabler : MonoBehaviour
{
    private bool isEnd = false;
    private bool lastTouch = false;

    private void OnEnable()
    {
        LevelController.OnEnd += DisableAcorn;
        LevelController.OnStart += EnableAcorn;
    }

    private void OnDisable()
    {
        LevelController.OnEnd -= DisableAcorn;
        LevelController.OnStart -= EnableAcorn;
        CollisionManager.OnAcornTrackCollision -= RegisterLastTouch;
        CollisionManager.OnAcornDamageTaken -= RegisterLastTouch;
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AcornRotator>().enabled = false;
        GetComponent<AcornJumper>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DisableAcorn(bool win)
    {
        GetComponent<AcornRotator>().enabled = false;
        GetComponent<AcornJumper>().enabled = false;
        if (win)
        {
            StartCoroutine(WaitForLastTouch());
            CollisionManager.OnAcornTrackCollision += RegisterLastTouch;
            CollisionManager.OnAcornDamageTaken += RegisterLastTouch;
        }
    }

    private void EnableAcorn()
    {
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<AcornRotator>().enabled = true;
        GetComponent<AcornJumper>().enabled = true;
    }

    IEnumerator WaitForLastTouch()
    {
        while (!lastTouch)
        {
            yield return null;
        }
        GetComponent<Rigidbody2D>().isKinematic = true;
        Collider2D[] cols = GetComponentsInChildren<Collider2D>();
        LevelController.Instance.SummonTree(transform.position);

        foreach (Collider2D col in cols)
        {
            col.enabled = false;
        }
    }

    private void RegisterLastTouch(float bounceForce, Collision2D collision2D)
    {
        lastTouch = true;
        CollisionManager.OnAcornTrackCollision -= RegisterLastTouch;
        CollisionManager.OnAcornDamageTaken -= RegisterLastTouch;
    }

    private void RegisterLastTouch()
    {
        lastTouch = true;
        CollisionManager.OnAcornTrackCollision -= RegisterLastTouch;
        CollisionManager.OnAcornDamageTaken -= RegisterLastTouch;
    }
}