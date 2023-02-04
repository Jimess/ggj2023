using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : Singleton<CollisionManager>
{
    private const string TRACK_TAG = "Track";
    private const string POWERUP_TAG = "Powerup";
    private const string END_TAG = "End";
    private bool isCD = false;

    public delegate void OnAcornTrackCollisionDelegate(float bounceForce, Vector2 hitPoint);
    public delegate void OnAcornEndCollisionDelegate();
    public delegate void OnAcornPowerupCollisionDelegate();

    public static OnAcornTrackCollisionDelegate OnAcornTrackCollision;
    public static OnAcornEndCollisionDelegate OnAcornEndCollision;
    public static OnAcornPowerupCollisionDelegate OnAcornPowerupCollision;

    private void Start()
    {

    }

    public void OnCollision(GameObject acorn, GameObject other, Vector2 hitPoint)
    {
        if (isCD) return;
        if (other.CompareTag(TRACK_TAG))
        {
            Debug.Log($"HIT {other.name}");
            OnAcornTrackCollision?.Invoke(acorn.GetComponent<AcornCollision>().getBounceForce(), hitPoint);
        }

        if (other.CompareTag(END_TAG))
        {
            OnAcornEndCollision?.Invoke(); ;
        }

        isCD = true;
        StartCoroutine(CD(1f));
    }


    IEnumerator CD(float cdTime)
    {
        yield return new WaitForSeconds(0.1f);
        isCD = false;
    }
}
