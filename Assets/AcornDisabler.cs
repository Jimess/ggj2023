using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornDisabler : MonoBehaviour
{
    private void OnEnable()
    {
        LevelController.OnEnd += DisableAcorn;
        LevelController.OnStart += EnableAcorn;
    }

    private void OnDisable()
    {
        LevelController.OnEnd -= DisableAcorn;
        LevelController.OnStart -= EnableAcorn;
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AcornRotator>().enabled = false;
        GetComponent<AcornJumper>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DisableAcorn(bool win)
    {
        GetComponent<AcornRotator>().enabled = false;
        GetComponent<AcornJumper>().enabled = false;
    }

    private void EnableAcorn()
    {
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<AcornRotator>().enabled = true;
        GetComponent<AcornJumper>().enabled = true;
    }
}
