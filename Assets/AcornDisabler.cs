using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornDisabler : MonoBehaviour
{
    private void OnEnable()
    {
        LevelController.OnEnd += DisableAcorn;
    }

    private void OnDisable()
    {
        LevelController.OnEnd -= DisableAcorn;
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
}
