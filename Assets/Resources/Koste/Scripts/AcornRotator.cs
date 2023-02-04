using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornRotator : MonoBehaviour
{
    [SerializeField]
    private float inputForce = 5.0f;

    [SerializeField]
    private float initialRotation = 60.0f;

    [SerializeField]
    private float maxRotation = 240.0f;

    private Rigidbody2D acorn;
    void Start()
    {
        acorn = GetComponent<Rigidbody2D>();
        //acorn.angularVelocity = initialRotation;
    }

    void FixedUpdate()
    {
        float inputDirection = Input.GetAxis("Horizontal");
        acorn.angularVelocity += inputForce * -inputDirection;
        Mathf.Clamp(acorn.angularVelocity, -maxRotation, maxRotation);
    }
}
