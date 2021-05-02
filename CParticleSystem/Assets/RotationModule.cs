using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RotationModule : IModule
{
    [SerializeField] private Vector3 rotationVelocity;
    [SerializeField] private Vector3 rotationAcceleration;

    public void InitParticle(Particle particle, CParticleSystem system)
    {
        particle.Set<Vector3>("Rotation Velocity", rotationVelocity);
        particle.Set<Vector3>("Rotation Acceleration", rotationAcceleration);
    }

    public void Update(HashSet<Particle> aliveParticles)
    {
        foreach (Particle p in aliveParticles)
        {
            p.Set<Vector3>("Rotation", p.Get<Vector3>("Rotation") + p.Get<Vector3>("Rotation Velocity") * Time.deltaTime);
            p.Set<Vector3>("Rotation Velocity", p.Get<Vector3>("Rotation Velocity") + p.Get<Vector3>("Rotation Acceleration") * Time.deltaTime);
        }
    }
}
