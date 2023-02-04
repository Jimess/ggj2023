using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class ParticleManager : Singleton<ParticleManager>
{ 
    [SerializeField] List<Particle> particleCollection;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        CollisionManager.OnAcornTrackCollision += OnCollisionParticle;
    }

    private void OnDisable()
    {
        CollisionManager.OnAcornTrackCollision -= OnCollisionParticle;
    }

    public void OnCollisionParticle(float bounceForce, Collision2D collision2d)
    {
        SpawnParticles(PARTICLES.ACORN_GOOD_HIT, collision2d.contacts[0].point, collision2d.contacts[0].normal, 5f);
    }

    public void SpawnParticles(PARTICLES particleType, Vector2 position, Vector2 lookAtDir, float lifetime)
    {
        GameObject particle = particleCollection.Where(w => w.particlyType == particleType).Select(s => s.prefab).FirstOrDefault();

        if (!particle) throw new KeyNotFoundException($"CANNOT FIND PARTICLE SET BY {particleType}");

        //ParticleSystem ps = particle.GetComponent<ParticleSystem>();
        particle = Instantiate(particle, position, Quaternion.LookRotation(particle.transform.rotation.eulerAngles, lookAtDir), transform);
        Destroy(particle, lifetime);
    }

    [System.Serializable]
    public struct Particle
    {
        public PARTICLES particlyType;
        //public OTHERENUM_FOR_POWERUPS powerUpLink;
        public GameObject prefab;
    }

    public enum PARTICLES
    {
        DEFAULT, ACORN_GOOD_HIT, TEST
    }
}
