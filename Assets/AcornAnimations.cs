using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornAnimations : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private const string GOOD_HIT = "goodHit";

    private void OnEnable()
    {
        CollisionManager.OnAcornTrackCollision += GoodHitAnim;
    }

    private void OnDisable()
    {
        CollisionManager.OnAcornTrackCollision -= GoodHitAnim;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GoodHitAnim(float bounceForce, Vector2 point)
    {
        anim.SetTrigger(GOOD_HIT);
    }
}
