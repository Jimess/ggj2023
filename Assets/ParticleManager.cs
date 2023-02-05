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
        CollisionManager.OnAcornPowerupCollision += OnPowerUpParticle;
        CollisionManager.OnAcornDamageTaken += OnAcornHitParticle;
    }

    private void OnDisable()
    {
        CollisionManager.OnAcornTrackCollision -= OnCollisionParticle;
        CollisionManager.OnAcornPowerupCollision -= OnPowerUpParticle;
        CollisionManager.OnAcornDamageTaken -= OnAcornHitParticle;
    }

    public void OnCollisionParticle(float bounceForce, Collision2D collision2d)
    {
        SpawnParticles(PARTICLES.ACORN_GOOD_HIT, collision2d.contacts[0].point, collision2d.contacts[0].normal, 5f);
    }

    public void OnAcornHitParticle(Vector2 position)
    {
        SpawnParticles(PARTICLES.ACORN_BODY_HIT, position, Vector2.zero, 5f);
    }

    public void OnPowerUpParticle(PowerUp powerUpType, Vector2 position)
    {
        System.Random rng = new System.Random();
        GameObject particle = particleCollection.Where(w => w.powerUpLink == powerUpType).Select(s => s.prefab).OrderBy(o => rng.Next()).FirstOrDefault();

        if (!particle) throw new KeyNotFoundException($"CANNOT FIND PARTICLE SET BY {powerUpType}");
        particle = Instantiate(particle, position, Quaternion.identity, transform);
        Destroy(particle, 2f);
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
        public PowerUp powerUpLink;
        public GameObject prefab;
    }

    public enum PARTICLES
    {
        DEFAULT, ACORN_GOOD_HIT, ACORN_BODY_HIT
    }
}
