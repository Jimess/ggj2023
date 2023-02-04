using UnityEngine;

public class CollisionManager : Singleton<CollisionManager>
{
    private const string TRACK_TAG = "Track";
    private const string POWERUP_TAG = "Powerup";
    private const string END_TAG = "End";

    public delegate void OnAcornTrackCollisionDelegate(float bounceForce);
    public delegate void OnAcornEndCollisionDelegate();
    public delegate void OnAcornPowerupCollisionDelegate();

    public static OnAcornTrackCollisionDelegate OnAcornTrackCollision;
    public static OnAcornEndCollisionDelegate OnAcornEndCollision;
    public static OnAcornPowerupCollisionDelegate OnAcornPowerupCollision;

    private void Start()
    {

    }

    public void OnCollision(AcornCollision collision, GameObject other)
    {
        if (other.CompareTag(TRACK_TAG))
        {
            OnAcornTrackCollision?.Invoke(collision.getBounceForce());
        }

        if (other.CompareTag(END_TAG))
        {
            OnAcornEndCollision?.Invoke(); ;
        }
    }
}
