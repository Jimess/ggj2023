using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : Singleton<ParticleManager>
{
    List<Particle> particleCollection;

    // Update is called once per frame
    void Update()
    {
        
    }

    [System.Serializable]
    public struct Particle
    {
        public string name;
        public GameObject prefab;
    }
}
