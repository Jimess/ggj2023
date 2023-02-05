using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : Singleton<CollisionManager>
{
    public const string TRACK_TAG = "Track";
    public const string POWERUP_TAG = "Powerup";
    public const string END_TAG = "End";
    public const string ACORN_TAG = "Acorn";
    private bool isCD = false;

    public delegate void OnAcornTrackCollisionDelegate(float bounceForce, Collision2D collision2D);
    public delegate void OnAcornEndCollisionDelegate(bool win);
    public delegate void OnAcornPowerupCollisionDelegate(PowerUp powerup, Vector2 position);
    public delegate void OnAcornDamageTakenDelegate(Vector2 position = new Vector2());

    public static OnAcornTrackCollisionDelegate OnAcornTrackCollision;
    public static OnAcornEndCollisionDelegate OnAcornEndCollision;
    public static OnAcornPowerupCollisionDelegate OnAcornPowerupCollision;
    public static OnAcornDamageTakenDelegate OnAcornDamageTaken;

    public void OnPowerUp(PowerUp powerUp, Vector2 position)
    {
        OnAcornPowerupCollision?.Invoke(powerUp, position);
    }
    public void OnDamageTaken(Vector2 position)
    {
        OnAcornDamageTaken?.Invoke(position);
    }

    public void OnCollision(GameObject acorn, GameObject other, Collision2D collision2D)
    {
        if (isCD) return;
        isCD = true;
        StartCoroutine(CD(1f));

        AudioManager.Instance.PlaySFXDetached("helmet_hit");

        if (other.CompareTag(TRACK_TAG))
        {
            Debug.Log($"HIT {other.name}");
            OnAcornTrackCollision?.Invoke(acorn.GetComponent<AcornCollision>().getBounceForce(), collision2D);
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
