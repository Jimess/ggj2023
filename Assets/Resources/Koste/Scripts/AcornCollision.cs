using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornCollision : MonoBehaviour
{

    [SerializeField]
    private float bounceForce = 5.0f;

    public float getBounceForce() {
        return bounceForce;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    CollisionManager.Instance.OnCollision(this, collision.gameObject);
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollisionManager.Instance.OnCollision(gameObject, collision.gameObject, collision);
    }
}
