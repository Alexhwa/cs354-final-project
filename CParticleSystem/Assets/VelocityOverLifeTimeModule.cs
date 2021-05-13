using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityOverLifeTimeModule : IModule
{
    [SerializeField] private AnimationCurve xVelocity;
    [SerializeField] private AnimationCurve yVelocity;
    [SerializeField] private AnimationCurve zVelocity;

    public override void InitParticle(Particle particle)
    {
        particle.Set<Vector3>("Initial Velocity", particle.Get<Vector3>("Velocity"));
    }
    public override void UpdateParticles(HashSet<Particle> aliveParticles)
    {
        foreach (Particle p in aliveParticles)
        {
            var curveX = 1 - p.Get<float>("Lifetime") / p.Get<float>("Total_Lifetime");
            var originalVel = p.Get<Vector3>("Velocity");
            p.Set<Vector3>("Velocity", new Vector3(originalVel.x + xVelocity.Evaluate(curveX), originalVel.y + yVelocity.Evaluate(curveX), originalVel.z + zVelocity.Evaluate(curveX)));
        }
    }
}
