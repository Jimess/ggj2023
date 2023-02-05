using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornBodyCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(CollisionManager.TRACK_TAG))
        {
            CollisionManager.Instance.OnDamageTaken(collision.contacts[0].point);
        }
    }
}
