using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : Singleton<CollisionManager>
{
    private const string TRACK_TAG = "Track";
    private const string POWERUP_TAG = "Powerup";
    private const string END_TAG = "End";

    public delegate void OnAcornTrackCollisionDelegate();
    public delegate void OnAcornEndCollisionDelegate();
    public delegate void OnAcornPowerupCollisionDelegate();

    public static OnAcornTrackCollisionDelegate OnAcornTrackCollision;
    public static OnAcornEndCollisionDelegate OnAcornEndCollision;
    public static OnAcornPowerupCollisionDelegate OnAcornPowerupCollision;

    private void Start()
    {
        
    }

    public static void OnCollision(GameObject acorn, GameObject other)
    {
        if (other.CompareTag(TRACK_TAG))
        {
            OnAcornTrackCollision();
        }

        if (other.CompareTag(END_TAG))
        {
            OnAcornEndCollision();
        }
    }
}
