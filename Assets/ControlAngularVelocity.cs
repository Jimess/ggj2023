using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAngularVelocity : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2;

    [Range(0, 500f)]
    [SerializeField] private float angVelLimit;

    [Range(0, 100f)]
    [SerializeField] private float speedUp;

    [Range(0, 100f)]
    [SerializeField] private float slowDown;

    [SerializeField] float angvel;
    [SerializeField] float velClampX;
    [SerializeField] float velClampY;

    bool isSlow;
    bool isSpeed;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        angvel = rb2.angularVelocity;
        GetInput();
        if (isSlow)
        {
            SlowDown();
        } else if (isSpeed)
        {
            SpeedUp();
        }
        LimitAngularVelocity();
        ClampVelocity();
    }

    private void ClampVelocity()
    {
        rb2.velocity = new Vector2(Mathf.Clamp(rb2.velocity.x, -velClampX, velClampX), Mathf.Clamp(rb2.velocity.y, -velClampY, velClampY));
    }

    private void LimitAngularVelocity()
    {
        // Debug.Log("Current vel " + rb2.velocity);
        //angVelLimit = Mathf.Abs(rb2.angularVelocity);


        rb2.angularVelocity = Mathf.Clamp(rb2.angularVelocity, -angVelLimit, angVelLimit);
    }

    private void SlowDown()
    {
        //Debug.Log($"Slowing down {slowDown / 100 * Time.fixedDeltaTime}");
        rb2.angularVelocity *=  (100 - slowDown)/100;
    }

    private void SpeedUp()
    {
        //Debug.Log($"Slowing down {slowDown / 100 * Time.fixedDeltaTime}");
        rb2.angularVelocity /= (100 - speedUp) / 100;
    }

    private void GetInput()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        if (dirX < 0)
        {
            isSlow = true;
        } else
        {
            isSlow = false;
        }

        if (dirX > 0)
        {
            isSpeed = true;
        } else
        {
            isSpeed = false;
        }
    }
}
