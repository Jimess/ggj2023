using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornRotator : MonoBehaviour
{
    [SerializeField]
    private float inputForce = 1.0f;

    [SerializeField]
    private float initialRotation = 10.0f;

    [SerializeField]
    private float minimalRotation = 1.0f;
    private Rigidbody2D acorn;
    private Vector3 oldAngle;
    void Start()
    {
        acorn = GetComponent<Rigidbody2D>();
        acorn.AddTorque(initialRotation);
    }

    void Update()
    {
        Debug.Log(transform.rotation.eulerAngles);
    }

    void FixedUpdate()
    {
        float inputDirection = Input.GetAxis("Horizontal");
        acorn.AddTorque(inputForce * inputDirection);
    }
}
