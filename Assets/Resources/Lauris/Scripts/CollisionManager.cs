using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : Singleton<CollisionManager>
{
    public const string TRACK_TAG = "Track";
    public const string POWERUP_TAG = "Powerup";
    public const string END_TAG = "End";
    private bool isCD = false;

    public delegate void OnAcornTrackCollisionDelegate(float bounceForce, Vector2 hitPoint);
    public delegate void OnAcornEndCollisionDelegate(bool win);
    public delegate void OnAcornPowerupCollisionDelegate();
    public delegate void OnAcornDamageTakenDelegate();

    public static OnAcornTrackCollisionDelegate OnAcornTrackCollision;
    public static OnAcornEndCollisionDelegate OnAcornEndCollision;
    public static OnAcornPowerupCollisionDelegate OnAcornPowerupCollision;
    public static OnAcornDamageTakenDelegate OnAcornDamageTaken;

    public void OnDamageTaken()
    {
        OnAcornDamageTaken?.Invoke();
    }

    public void OnCollision(GameObject acorn, GameObject other, Vector2 hitPoint)
    {
        if (isCD) return;
        isCD = true;
        StartCoroutine(CD(1f));

        if (other.CompareTag(TRACK_TAG))
        {
            Debug.Log($"HIT {other.name}");
            OnAcornTrackCollision?.Invoke(acorn.GetComponent<AcornCollision>().getBounceForce(), hitPoint);
        }

        if (other.CompareTag(END_TAG))
        {
            OnAcornEndCollision?.Invoke(true); ;
        }
    }

    IEnumerator CD(float cdTime)
    {
        yield return new WaitForSeconds(0.1f);
        isCD = false;
    }
}
