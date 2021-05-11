using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationModule : IModule
{
    [SerializeField] private Vector3 rotationVelocity;
    [SerializeField] private Vector3 rotationAcceleration;

    public override void InitParticle(Particle particle)
    {
        particle.Set<Vector3>("Rotation Velocity", rotationVelocity);
        particle.Set<Vector3>("Rotation Acceleration", rotationAcceleration);
    }

    public override void UpdateParticles(HashSet<Particle> aliveParticles)
    {
        foreach (Particle p in aliveParticles)
        {
            p.Set<Vector3>("Rotation", p.Get<Vector3>("Rotation") + p.Get<Vector3>("Rotation Velocity") * Time.deltaTime);
            p.Set<Vector3>("Rotation Velocity", p.Get<Vector3>("Rotation Velocity") + p.Get<Vector3>("Rotation Acceleration") * Time.deltaTime);
        }
    }
}
