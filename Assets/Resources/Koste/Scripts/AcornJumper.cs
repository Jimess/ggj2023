using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornJumper : MonoBehaviour
{
    private const float MAX_BOUNCE_ANGLE = 80.0f;
    private const float MIN_BOUNCE_ANGLE = 30.0f;
    private const float BOUNCE_ANGLE_DELTA = MAX_BOUNCE_ANGLE - MIN_BOUNCE_ANGLE;

    private Rigidbody2D acorn;

    [Range(MIN_BOUNCE_ANGLE, MAX_BOUNCE_ANGLE)]
    public float angle = BOUNCE_ANGLE_DELTA / 2;

    [Range(0.0f, BOUNCE_ANGLE_DELTA)]
    public float angleRandomnes = MIN_BOUNCE_ANGLE;

    void Start()
    {
        acorn = GetComponent<Rigidbody2D>();
    }
    void OnEnable()
    {
        CollisionManager.OnAcornTrackCollision += Bounce;
    }


    void OnDisable()
    {
        CollisionManager.OnAcornTrackCollision -= Bounce;
    }

    public void Bounce(float bounceForce, Collision2D collision2D)
    {
        if (!enabled) return;

        Vector2 bounceVector = CalculateBounceVector();
        // Debug.Log(bounceVector);
        acorn.velocity = Vector2.zero;
        acorn.AddForce(bounceVector * bounceForce, ForceMode2D.Impulse);
        //acorn.AddForceAtPosition(bounceVector * bounceForce, point, ForceMode2D.Impulse);
    }

    private Vector2 CalculateBounceVector()
    {
        float angle = RandomizeAngle(this.angle);
        //return new Vector2(Mathf.Abs(Mathf.Cos(angle)), Mathf.Abs(Mathf.Sin(angle)));

        return Quaternion.AngleAxis(angle, Vector3.forward) * new Vector2(1, 0).normalized;
    }

    private float RandomizeAngle(float angle)
    {
        return Mathf.Clamp(angle + Random.Range(-angleRandomnes, angleRandomnes), MIN_BOUNCE_ANGLE, MAX_BOUNCE_ANGLE);
        //return 
    }
}
