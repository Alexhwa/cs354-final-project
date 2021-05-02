using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MovementModule : IModule
{
    [SerializeField] private float speed;
    [SerializeField] private float coneSpread;
    [SerializeField] private Vector3 coneRotation;

    [SerializeField] private Vector3 acceleration;

    public void InitParticle(Particle particle, CParticleSystem system)
    {
        // random point inside unit circle
        var circlePoint = Quaternion.Euler(0, 0, Random.Range(0f, 360f)) * Vector2.right * coneSpread * Random.Range(0f, 1f);
        
        // vector pointing through the center of the cone
        var coneDirection = Quaternion.Euler(coneRotation) * system.transform.forward;
        
        // axes for the circular face of the cone
        var coneUp = Quaternion.Euler(coneRotation) * system.transform.up;
        var coneRight = Quaternion.Euler(coneRotation) * system.transform.right;

        var randomDirection = coneDirection + coneRight * circlePoint.x + coneUp * circlePoint.y;
        
        particle.Set<Vector3>("Velocity", randomDirection.normalized * speed);
        particle.Set<Vector3>("Acceleration", acceleration);
    }

    public void Update(HashSet<Particle> aliveParticles)
    {
        foreach (Particle p in aliveParticles) {
            p.Set<Vector3>("Position", p.Get<Vector3>("Position") + p.Get<Vector3>("Velocity") * Time.deltaTime);
            p.Set<Vector3>("Velocity", p.Get<Vector3>("Velocity") + p.Get<Vector3>("Acceleration") * Time.deltaTime);
        }
    }
}
