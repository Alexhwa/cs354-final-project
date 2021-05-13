using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityOverLifeTimeModule : IModule
{
    [SerializeField] private AnimationCurve xVelocity; // corresponds to binormal
    [SerializeField] private AnimationCurve yVelocity; // corresponds to normal
    [SerializeField] private AnimationCurve zVelocity; // corresponds to base

    public override void InitParticle(Particle particle)
    {
        var baseVelocity = particle.Get<Vector3>("Velocity");
        particle.Set<Vector3>("Base Velocity", baseVelocity);

        var otherAxis = Vector3.up; 
        if (baseVelocity.x == 0 && baseVelocity.z == 0) {
            // edge case
            otherAxis = -Vector3.forward;
        }

        var binormalVelocity = Vector3.Cross(otherAxis, baseVelocity);
        binormalVelocity = binormalVelocity.normalized * baseVelocity.magnitude;
        particle.Set<Vector3>("Binormal Velocity", binormalVelocity);

        var normalVelocity = Vector3.Cross(baseVelocity, binormalVelocity);
        normalVelocity = normalVelocity.normalized * baseVelocity.magnitude;
        particle.Set<Vector3>("Normal Velocity", normalVelocity);

        particle.Set<Vector3>("Velocity",
            particle.Get<Vector3>("Binormal Velocity") * xVelocity.Evaluate(0) +
            particle.Get<Vector3>("Normal Velocity") * yVelocity.Evaluate(0) +
            particle.Get<Vector3>("Base Velocity") * zVelocity.Evaluate(0)
        );
        Debug.Log(particle.Get<Vector3>("Binormal Velocity") * xVelocity.Evaluate(0) +
            particle.Get<Vector3>("Normal Velocity") * yVelocity.Evaluate(0) +
            particle.Get<Vector3>("Base Velocity") * zVelocity.Evaluate(0));
    }
    public override void UpdateParticles(HashSet<Particle> aliveParticles)
    {
        foreach (Particle p in aliveParticles)
        {
            var curveT = 1 - p.Get<float>("Lifetime") / p.Get<float>("Total_Lifetime");

            p.Set<Vector3>("Velocity",
                p.Get<Vector3>("Binormal Velocity") * xVelocity.Evaluate(curveT) +
                p.Get<Vector3>("Normal Velocity") * yVelocity.Evaluate(curveT) +
                p.Get<Vector3>("Base Velocity") * zVelocity.Evaluate(curveT)
            );
        }
    }
}
